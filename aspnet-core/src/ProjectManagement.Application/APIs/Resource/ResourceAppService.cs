using Abp.Authorization;
using Abp.Configuration;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using NccCore.Paging;
using NccCore.Uitls;
using ProjectManagement.APIs.PMReportProjectIssues;
using ProjectManagement.APIs.ProjectUsers;
using ProjectManagement.APIs.ProjectUsers.Dto;
using ProjectManagement.APIs.ResourceRequests.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Constants;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using ProjectManagement.NccCore.Helper;
using ProjectManagement.Services.Komu;
using ProjectManagement.Services.Komu.KomuDto;
using ProjectManagement.Services.ResourceManager;
using ProjectManagement.Services.ResourceManager.Dto;
using ProjectManagement.Services.ResourceService.Dto;
using ProjectManagement.Users;
using ProjectManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Resource
{
    [AbpAuthorize]
    public class ResourceAppService : ProjectManagementAppServiceBase
    {
        private readonly ProjectUserAppService _projectUserAppService;
        private readonly PMReportProjectIssueAppService _pMReportProjectIssueAppService;
        private readonly IUserAppService _userAppService;
        private readonly ResourceManager _resourceManager;

        private readonly UserManager _userManager;
        private ISettingManager _settingManager;
        private KomuService _komuService;

        public ResourceAppService(
            ProjectUserAppService projectUserAppService,
            PMReportProjectIssueAppService pMReportProjectIssueAppService,
            KomuService komuService,
            UserManager userManager,
            ResourceManager resourceManager,
            ISettingManager settingManager,
            IUserAppService userAppService)
        {
            _projectUserAppService = projectUserAppService;
            _pMReportProjectIssueAppService = pMReportProjectIssueAppService;
            _komuService = komuService;
            _resourceManager = resourceManager;
            _settingManager = settingManager;
            _userAppService = userAppService;
            _userManager = userManager;
        }


        [HttpPost]
        [AbpAuthorize]
        public async Task PlanEmployeeJoinOrOutProject(InputPlanResourceDto input)
        {
            if (input.AllocatePercentage <= 0)
            {
                var pu = _resourceManager.ValidateUserWorkingInThisProject(input.UserId, input.ProjectId);
                input.ProjectRole = pu.Result.ProjectRole;
            }
            await _resourceManager.AddFuturePUAndNofity(input);
        }

        [HttpPost]
        [AbpAuthorize()]
        public async Task ConfirmOutProject(ConfirmOutProjectDto input)
        {
            await _resourceManager.ConfirmOutProject(input);
        }

        [HttpPost]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject)]
        public async Task AddUserFromPoolToTempProject(AddResourceToProjectDto input)
        {
            await _resourceManager.CreatePresentProjectUserAndNofity(input);
        }

        [HttpGet]
        [AbpAuthorize(PermissionNames.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject
            , PermissionNames.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject)]
        public async Task ConfirmJoinProject(long projectUserId, DateTime startTime)
        {
            await _resourceManager.ConfirmJoinProject(projectUserId, startTime);
        }

        [HttpPost]
        [AbpAuthorize()]
        public async Task EditProjectUserPlan(EditProjectUserDto input)
        {
            await _resourceManager.EditProjectUserPlan(input);
        }




        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllResourceDto>> GetVendorResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetResources(input, true);
        }

        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllResourceDto>> GetAllResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetResources(input, false);
        }


        [HttpPost]
        [AbpAuthorize]
        public async Task<GridResult<GetAllPoolResourceDto>> GetAllPoolResource(InputGetResourceDto input)
        {
            return await _resourceManager.GetAllPoolResource(input);
        }


        [HttpDelete]
        [AbpAuthorize]
        public async Task CancelResourcePlan(long projectUserId)
        {
            //var pu = await WorkScope.GetAll<ProjectUser>()
            //    .Include(s => s.User)
            //    .Include(s => s.Project)
            //    .Include(s => s.Project.PM)
            //    .Where(s => s.Id == projectUserId)
            //    .Select(s => new
            //    {
            //        ProjectUser = s,
            //        ProjectCode = s.Project.Code,
            //        ProjectName = s.Project.Name,
            //        EmployeeName = s.User.Name + " " + s.User.Surname,
            //        PMName = s.Project.PM.Name + " " + s.Project.PM.Surname,
            //        InOutString = s.AllocatePercentage > 0 ? "vào dự án" : "ra dự án"
            //    }).FirstOrDefaultAsync();

            //var projectUser = pu.ProjectUser;

            //if (projectUser.Status != ProjectUserStatus.Future)
            //{
            //    throw new UserFriendlyException(String.Format("projectUser with id {0} is not future!", projectUser.Id));
            //}
            //if (projectUser.CreatorUserId != AbpSession.UserId.Value)
            //{
            //    bool allowCancelAnyPlan = await PermissionChecker.IsGrantedAsync(PermissionNames.DeliveryManagement_ResourceRequest_CancelAnyPlanResource);
            //    if (!allowCancelAnyPlan)
            //    {
            //        throw new UserFriendlyException(String.Format("You don't have permission to cancel resource plan of other people!"));
            //    }
            //}

            //await WorkScope.DeleteAsync(projectUser);

            //if (!pu.ProjectCode.Equals(AppConsts.CHO_NGHI_PROJECT_CODE, StringComparison.OrdinalIgnoreCase))
            //{
            //    var komuMessage = new StringBuilder();
            //    komuMessage.Append($"PM **{pu.PMName}** đã **HỦY** plan: ");
            //    komuMessage.Append($"**{pu.EmployeeName}** {pu.InOutString } **{pu.ProjectName}** ");
            //    komuMessage.Append($"từ ngày **{projectUser.StartTime:dd/MM/yyyy}**, ");

            //    await _komuService.NotifyToChannel(new KomuMessage
            //    {
            //        CreateDate = DateTimeUtils.GetNow(),
            //        Message = komuMessage.ToString(),
            //    },
            //    ChannelTypeConstant.PM_CHANNEL);
            //}

        }

        [HttpPut]
        public async Task updateUserPoolNote(UpdateUserPoolNoteDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            user.PoolNote = input.Note;
            await WorkScope.UpdateAsync<User>(user);
        }
    }
}
