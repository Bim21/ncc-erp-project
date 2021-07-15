﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Accounts;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Roles.Dto;
using ProjectManagement.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Constants.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using OfficeOpenXml;
using Newtonsoft.Json;
using NccCore.IoC;
using Abp.Authorization.Users;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using Microsoft.AspNetCore.Hosting;

namespace ProjectManagement.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IWorkScope _workScope;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IWorkScope workScope,
            IWebHostEnvironment webHostEnvironment)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _workScope = workScope;
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllUserActive(bool onlyStaff)
        {
            var query = _workScope.GetAll<User>().Where(u => u.IsActive)
                .Where(x => onlyStaff ? x.UserType != UserType.Internship : true)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Surname = u.Surname,
                    EmailAddress = u.EmailAddress,
                    FullName = u.FullName,
                    AvatarPath = u.AvatarPath,
                    UserType = u.UserType,
                    UserLevel = u.UserLevel,
                    Branch = u.Branch,
                    Gender = u.Gender
                });
            return await query.ToListAsync();
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Pages_Users_UpdateAvatar)]
        public async Task<string> UpdateAvatar([FromForm] AvatarDto input)
        {
            String path = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (input != null && input.File != null && input.File.Length > 0)
            {
                string FileExtension = Path.GetExtension(input.File.FileName).ToLower();

                if (FileExtension == ".jpeg" || FileExtension == ".png" || FileExtension == ".jpg" || FileExtension == ".gif")
                {
                    if (input.File.Length > 1048576)
                    {
                        throw new UserFriendlyException(String.Format("File needs to be less than 1MB!"));
                    }
                    else
                    {
                        //get user to take name + code
                        User user = await _userManager.GetUserByIdAsync(input.UserId);
                        //set avatar name = milisecond + id + name + extension
                        String avatarPath = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                            + "_" + input.UserId
                            + "_" + user.UserName
                            + Path.GetExtension(input.File.FileName);
                        using (var stream = System.IO.File.Create(Path.Combine(_webHostEnvironment.WebRootPath, "avatars", avatarPath)))
                        {
                            await input.File.CopyToAsync(stream);
                            user.AvatarPath = "/avatars/" + avatarPath;
                            await _userManager.UpdateAsync(user);
                        }
                        return "/avatars/" + avatarPath;
                    }
                }
                else
                {
                    throw new UserFriendlyException(String.Format("File can not upload!"));
                }
            }
            else
            {
                throw new UserFriendlyException(String.Format("No file upload!"));
            }
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Pages_Users_ImportUserFromFile)]
        public async Task<Object> ImportUserFromFile([FromForm] FileInputDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException(String.Format("No file upload!"));
            }

            var path = new String[] { ".xlsx", ".xltx" };
            if (!path.Contains(Path.GetExtension(input.File.FileName)))
            {
                throw new UserFriendlyException(String.Format("Invalid file upload, only acept extension .xlsx, .xltx"));
            }

            List<User> listUser = new List<User>();
            var failedList = new List<string>();
            var successList = new List<User>();

            var roleEmployee = _roleManager.GetRoleByName(RoleConstants.ROLE_EMPLOYEE);

            using (var stream = new MemoryStream())
            {
                await input.File.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for(int row = 2; row <= rowCount; row++)
                    {
                        var fullName = worksheet.Cells[row, 3].Value.ToString().Trim();
                        var name = SplitUsername(fullName);

                        listUser.Add(new User
                        {
                            UserName = $"{name.Name.Trim().ToLower()}.{name.SurName.Replace(" ", "").ToLower()}",
                            Name = name.Name,
                            Surname = name.SurName,
                            EmailAddress = worksheet.Cells[row, 8].Value.ToString().Trim().ToLower(),
                            NormalizedEmailAddress = worksheet.Cells[row, 8].Value.ToString().Trim().ToUpper(),
                            NormalizedUserName = "DEFAULT",
                            IsActive = true,
                            Password = "123qwe",
                            UserCode = worksheet.Cells[row, 2].Value.ToString().Trim()
                        });
                    }
                }
            }

            int index = 1;
            foreach (var user in listUser)
            {
                try
                {
                    user.Id = await _workScope.InsertAndGetIdAsync(user);

                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = roleEmployee.Id
                    };
                    await _workScope.InsertAndGetIdAsync(userRole);

                    successList.Add(user);
                }
                catch (Exception e)
                {
                    failedList.Add("Error: Row: " + index + ". " + e.Message);
                }
                index++;
            }

            if (successList.Count() < 1)
            {
                throw new UserFriendlyException(String.Format("Invalid excel data."));
            }
            return new { successList, failedList };
        }

        private NameSplitDto SplitUsername(string fullName)
        {
            var name = "";
            var surName = "";
            for (int i = 0; i < fullName.Length; i++)
            {
                if (fullName[i] == ' ')
                {
                    name = fullName.Substring(0, i);
                    surName = fullName.Substring(i + 1, fullName.Length - i - 1);
                    break;
                }
            }
            return new NameSplitDto
            {
                Name = name,
                SurName = surName
            };
        }
    }
}

