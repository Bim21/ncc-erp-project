using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using OfficeOpenXml;
using NccCore.IoC;
using Abp.Authorization.Users;
using Microsoft.AspNetCore.Hosting;
using ProjectManagement.Entities;
using NccCore.Paging;
using NccCore.Extension;
using ProjectManagement.Services.HRM;
using ProjectManagement.Services.HRM.Dto;
using Abp.Configuration;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using System.Text;
using ProjectManagement.Constants;
using NccCore.Uitls;
using ProjectManagement.NccCore.Helper;
using Microsoft.AspNetCore.Http;
using ProjectManagement.Configuration;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using ProjectManagement.Constants.Enum;

namespace ProjectManagement.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IWorkScope _workScope;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly HrmService _hrmService;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IWorkScope workScope,
            IWebHostEnvironment webHostEnvironment,
            HrmService hrmService,
            KomuService komuService,
            ISettingManager settingManager,
            IHttpContextAccessor httpContextAccessor)
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
            _hrmService = hrmService;
            _komuService = komuService;
            _settingManager = settingManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.Pages_Users_ViewAll, PermissionNames.Pages_Users_ViewOnlyMe)]
        public async Task<GridResult<UserDto>> GetAllPaging(GridParam input, long? skillId)
        {
            bool isViewAll = await PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Users_ViewAll);

            var userSkills = _workScope.GetAll<UserSkill>();
            var users = _workScope.GetAll<User>()
                .Where(x => isViewAll || x.Id == AbpSession.UserId.Value)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Name = x.Name,
                    Surname = x.Surname,
                    EmailAddress = x.EmailAddress,
                    UserCode = x.UserCode,
                    AvatarPath = "/avatars/" + x.AvatarPath,
                    UserType = x.UserType,
                    UserLevel = x.UserLevel,
                    Branch = x.Branch,
                    IsActive = x.IsActive,
                    FullName = x.Name + " " + x.Surname,
                    FullNameNormal = x.Surname + " " + x.Name,
                    UserSkills = userSkills.Where(s => s.UserId == x.Id).Select(s => new UserSkillDto
                    {
                        UserId = s.UserId,
                        SkillId = s.SkillId,
                        SkillName = s.Skill.Name
                    }).ToList(),
                    RoleNames = _roleManager.Roles.Where(r => x.Roles.Select(x => x.RoleId).Contains(r.Id)).Select(r => r.NormalizedName).ToArray()
                }).Where(x => !skillId.HasValue || userSkills.Where(y => y.UserId == x.Id).Select(y => y.SkillId).Contains(skillId.Value));

            return await users.GetGridResult(users, input);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Create)]
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

            if (input.UserSkills != null)
            {
                foreach (var s in input.UserSkills)
                {
                    var skill = new UserSkill
                    {
                        UserId = user.Id,
                        SkillId = s.SkillId
                    };
                    await _workScope.InsertAndGetIdAsync(skill);
                }
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Update, PermissionNames.Pages_Users_UpdateMySkills)]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            bool isUpdateAll = await PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Users_Update);

            CheckUpdatePermission();

            if (isUpdateAll)
            {

                var user = await _userManager.GetUserByIdAsync(input.Id);

                MapToEntity(input, user);

                CheckErrors(await _userManager.UpdateAsync(user));

                if (input.RoleNames != null)
                {
                    CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
                }
            }

            var userSkills = await _workScope.GetAll<UserSkill>().Where(x => x.UserId == input.Id).ToListAsync();
            var currenUserSkillId = userSkills.Select(x => x.SkillId);

            var deleteSkillId = currenUserSkillId.Except(input.UserSkills.Select(x => x.SkillId));
            var deleteSkill = userSkills.Where(x => deleteSkillId.Contains(x.SkillId));
            var addSkill = input.UserSkills.Where(x => !currenUserSkillId.Contains(x.SkillId));

            foreach (var item in deleteSkill)
            {
                await _workScope.DeleteAsync<UserSkill>(item);
            }

            foreach (var item in addSkill)
            {
                var userSkill = new UserSkill
                {
                    UserId = item.UserId,
                    SkillId = item.SkillId
                };
                await _workScope.InsertAndGetIdAsync(userSkill);
            }

            return await GetAsync(input);
        }

        [AbpAllowAnonymous]
        [HttpGet]
        public async Task<EmployeeInformationDto> GetEmployeeInformation(string email)
        {
            if (!CheckSecurityCode())
                throw new UserFriendlyException("SecretCode does not match!");
            if (string.IsNullOrEmpty(email)) return null;
            var user = await _workScope.GetAll<User>().FirstOrDefaultAsync(u => u.EmailAddress == email);
            if (user == null) return null;
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                var userFromHRM = await _hrmService.GetUserFromHRMByEmail(user.EmailAddress);
                user.PhoneNumber = userFromHRM?.Phone;
                await _workScope.UpdateAsync(user);
            }
            var projectUsers = await (from pu in _workScope.GetAll<ProjectUser>().Include(x => x.Project).Include(x => x.Project.PM).Where(x => x.UserId == user.Id)
                                      select new
                                      {
                                          ProjectId = pu.ProjectId,
                                          ProjectName = pu.Project.Name,
                                          PmName = pu.Project.PM != null ? pu.Project.PM.Surname.Trim() + " " + pu.Project.PM.Name.Trim() : string.Empty,
                                          StartTime = pu.StartTime,
                                          ProjectRole = pu.ProjectRole
                                      }).OrderByDescending(x => x.StartTime).ToListAsync();
            var employeeInfo = new EmployeeInformationDto()
            {
                EmployeeId = user.Id,
                EmailAddress = user.EmailAddress,
                EmployeeName = user.Surname.Trim() + " " + user.Name.Trim(),
                PhoneNumber = user.PhoneNumber,
                Branch = Enum.GetName(typeof(Branch), user.Branch),
                RoleType = Enum.GetName(typeof(UserType), user.UserType),
                ProjectDtos = new List<ProjectDTO>()
            };
            if (projectUsers.Any())
            {
                employeeInfo.ProjectDtos.AddRange(projectUsers.Select(x => new ProjectDTO()
                {
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectName,
                    PmName = x.PmName,
                    StartTime = x.StartTime,
                    ProjectRole = Enum.GetName(typeof(ProjectUserRole), x.ProjectRole)
                }));
            }
            return employeeInfo;
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Delete)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var hasProject = await _workScope.GetAll<Project>().AnyAsync(x => x.PMId == input.Id);
            if (hasProject)
                throw new UserFriendlyException("User is a project manager !");

            var useSkills = await _workScope.GetAll<UserSkill>().Where(x => x.UserId == input.Id).ToListAsync();
            foreach (var item in useSkills)
            {
                await _workScope.DeleteAsync(item);
            }

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
            var userSkill = _workScope.GetAll<UserSkill>().Where(x => x.UserId == user.Id).Select(x => new UserSkillDto
            {
                UserId = x.UserId,
                SkillId = x.SkillId,
                SkillName = x.Skill.Name
            });
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            userDto.UserSkills = userSkill.ToList();

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
        public async Task<List<UserDto>> GetAllUserActive(bool onlyStaff,bool isFake=false)
        {
            var query = _workScope.GetAll<User>().Where(u => u.IsActive&& isFake?true:(u.UserType != UserType.FakeUser))
                .Where(x => onlyStaff ? x.UserType != UserType.Internship : true)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Surname = u.Surname,
                    EmailAddress = u.EmailAddress,
                    FullName = u.FullName,
                    AvatarPath = "/avatars/" + u.AvatarPath,
                    UserType = u.UserType,
                    UserLevel = u.UserLevel,
                    Branch = u.Branch,
                });
            return await query.ToListAsync();
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllWithDeactiveUser()
        {
            var query = _workScope.GetAll<User>()
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Surname = u.Surname,
                    EmailAddress = u.EmailAddress,
                    FullName = u.FullName,
                    AvatarPath = "/avatars/" + u.AvatarPath,
                    UserType = u.UserType,
                    UserLevel = u.UserLevel,
                    Branch = u.Branch,
                    IsActive = u.IsActive,
                    UserCode = u.UserCode
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
                            user.AvatarPath = avatarPath;
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

                    for (int row = 2; row <= rowCount; row++)
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
        [AbpAuthorize(PermissionNames.Pages_Users_AutoUpdateUserFromHRM)]
        public async Task<object> AutoUpdateUserFromHRM()
        {
            var userFromHRMs = await _hrmService.GetUserFromHRM();
            var userFromHRMEmails = userFromHRMs.Select(x => x.EmailAddress).ToList();

            var currentUsers = await _workScope.GetAll<User>().ToListAsync();
            var currentUserEmails = currentUsers.Select(x => x.EmailAddress).ToList();

            var successListInsert = new List<string>();
            var failedListInsert = new List<string>();
            var successListUpdate = new List<string>();
            var failedListUpdate = new List<string>();

            var updatefakeUsers = currentUsers.Where(x => !userFromHRMEmails.Contains(x.EmailAddress)).ToList();
            foreach (var user in updatefakeUsers)
            {
                try
                {
                    user.UserType = UserType.FakeUser;
                    await _workScope.UpdateAsync(user);
                    successListUpdate.Add(user.EmailAddress);
                }
                catch (Exception e)
                {
                    failedListUpdate.Add(user.EmailAddress + " error =>" + e.Message);
                }
            }

            foreach (var user in userFromHRMs.OrderByDescending(x => x.UserLevel))
            {
                if (currentUserEmails.Contains(user.EmailAddress))
                {
                    try
                    {
                        var updateUser = await UpdateUserFromHRM(user);
                        successListUpdate.Add(user.EmailAddress);
                    }
                    catch (Exception e)
                    {
                        failedListUpdate.Add(user.EmailAddress + " error =>" + e.Message);
                    }
                }
                else
                {
                    try
                    {
                        if (user.IsActive)
                        {
                            var createUser = await InsertUserFromHRM(user);
                            successListInsert.Add(user.EmailAddress);
                        }
                    }
                    catch (Exception e)
                    {
                        failedListInsert.Add(user.EmailAddress + " error =>" + e.Message);
                    }
                }
            }
            if (successListInsert.Count > 0)
            {
                var listUser = string.Empty;
                var users = userFromHRMs.Where(x => successListInsert.Contains(x.EmailAddress)).ToList();
                foreach (var user in users)
                {
                    listUser += (UserHelper.GetUserName(user.EmailAddress) ?? user.UserName) + ", ";
                }
                listUser = listUser.Remove(listUser.Length - 2, 2);
                var message = new StringBuilder();
                message.AppendLine($"Welcome các nhân viên mới vào làm việc tại công ty, đó là **{listUser}**. Các PM hãy nhanh tay pick nhân viên vào dự án ngay nào.");
                await _komuService.NotifyToChannel(new KomuMessage
                {
                    Message = message.ToString(),
                    CreateDate = DateTimeUtils.GetNow(),
                }, ChannelTypeConstant.GENERAL_CHANNEL);
            }
            return new
            {
                successListInsert,
                failedListInsert,
                successListUpdate,
                failedListUpdate,
            };
        }
        [HttpPost]
        //[AbpAuthorize(PermissionNames.Pages_Users_UpdateStarRateFromTimesheet)]
        [AbpAllowAnonymous]
        public async Task<List<UpdateStarRateFromTimesheetDto>> UpdateStarRateFromTimesheet(List<UpdateStarRateFromTimesheetDto> input)
        {
            if (!CheckSecurityCode())
            {
                throw new UserFriendlyException("SecretCode does not match!");
            }
            foreach (var item in input)
            {
                var user = await _workScope.GetAll<User>().Where(x => x.EmailAddress == item.EmailAddress).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.StarRate = item.StarRate;
                    await _workScope.UpdateAsync(user);
                }
            }
            CurrentUnitOfWork.SaveChanges();
            return input;
        }
        private async Task<CreateUserDto> InsertUserFromHRM(AutoUpdateUserDto user)
        {
            var createUser = new CreateUserDto
            {
                UserName = user.EmailAddress,
                Name = user.Name,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                UserCode = user.UserCode,
                UserType = user.UserType,
                UserLevel = user.UserLevel,
                Branch = user.Branch.Value,
                IsActive = user.IsActive,
                Password = "123Abc123@",
                RoleNames = new string[] { "EMPLOYEE" }
            };
            await CreateAsync(createUser);
            return createUser;
        }
        private async Task<UserDto> UpdateUserFromHRM(AutoUpdateUserDto user)
        {
            var currentUser = await _workScope.GetAll<User>().Where(x => x.EmailAddress == user.EmailAddress).FirstOrDefaultAsync();
            var userSkills = await _workScope.GetAll<UserSkill>().Where(x => x.UserId == currentUser.Id).Select(x => new UserSkillDto
            {
                SkillId = x.SkillId,
                SkillName = x.Skill.Name,
                UserId = x.UserId
            }).ToListAsync();
            var userRoleIds = _workScope.GetAll<UserRole>().Where(x => x.UserId == currentUser.Id).Select(x => x.RoleId).ToArray();
            var roles = _roleManager.Roles.Where(r => userRoleIds.Contains(r.Id)).Select(r => r.NormalizedName);
            var updateUser = new UserDto
            {
                Id = currentUser.Id,
                UserName = user.EmailAddress,
                Name = user.Name,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                UserCode = user.UserCode,
                UserType = user.UserType,
                UserLevel = user.UserLevel,
                Branch = user.Branch.HasValue ? user.Branch.Value : currentUser.Branch,
                FullName = user.FullName,
                UserSkills = userSkills,
                RoleNames = roles.ToArray(),
                IsActive = user.IsActive,
                AvatarPath = currentUser.AvatarPath,
                CreationTime = currentUser.CreationTime,
                LastLoginTime = currentUser.LastModificationTime
            };
            await UpdateAsync(updateUser);
            return updateUser;
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
        private bool CheckSecurityCode()
        {
            var secretCode = SettingManager.GetSettingValue(AppSettingNames.SecurityCode);
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var securityCodeHeader = header["X-Secret-Key"];
            if (secretCode == securityCodeHeader)
                return true;
            return false;
        }

    }
}

