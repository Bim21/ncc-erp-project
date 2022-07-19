using Abp;
using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.APIs.Public.Dto;
using ProjectManagement.Configuration;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Public
{
    public class PublicAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        public async Task<GetURIDto> GetConfigUri()
        {
            return new GetURIDto
            {
                TimesheetURI = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri),
                GoogleClientAppId = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId)
            };
        }


        [AbpAllowAnonymous]
        [HttpGet]
        public List<UserInOutProject> GetTempUsersInOutProjectHistory(string projectCode)
        {
            var projectId = GetProjectIdByCode(projectCode);

            if (projectId == default)
                throw new UserFriendlyException("Project not exist !");

            var tempProjectUsers = WorkScope.GetAll<ProjectUser>()
                    .Where(s => s.ProjectId == projectId)
                    .Where(s => s.User.UserType != UserType.FakeUser)
                    .Where(s => s.IsPool)
                    .Where(s => s.Status != ProjectUserStatus.Future)
                    .Select(s => new
                    {
                        s.User.EmailAddress,
                        s.UserId,
                        s.StartTime,
                        s.AllocatePercentage
                    })
                    .ToList();

            var resultList = tempProjectUsers
                .GroupBy(s => new { s.EmailAddress, s.UserId })
                .Select(s =>
             new UserInOutProject
             {
                 EmailAddress = s.Key.EmailAddress,
                 ListTimeInOut = s.Select(x => new TimeJoinOut
                 {
                     DateAt = x.StartTime.Date,
                     IsJoin = x.AllocatePercentage > 0
                 }).OrderBy(x => x.DateAt).ToList()
             }).ToList();

            return resultList;
        }


        private long GetProjectIdByCode(string projectCode)
        {
            return WorkScope.GetAll<Project>()
                .Where(x => x.Code.ToUpper() == projectCode.ToUpper())
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        [AbpAllowAnonymous]
        [HttpGet]
        public List<string> GetCurrentTempEmailsInProject(string projectCode)
        {
            var projectId = GetProjectIdByCode(projectCode);

            if (projectId == default)
                throw new UserFriendlyException($"Project with code {projectCode} is not exist!");

            var emails = WorkScope.GetAll<ProjectUser>()
                    .Where(s => s.ProjectId == projectId)
                    .Where(s => s.User.UserType != UserType.FakeUser)
                    .Where(s => s.Status == ProjectUserStatus.Present)
                    .Where(s => s.AllocatePercentage > 0)
                    .Where(s => s.IsPool)
                    .OrderByDescending(s => s.StartTime)
                    .Select(s => s.User.EmailAddress)
                    .Distinct()
                    .ToList();

            return emails;
        }

        [AbpAllowAnonymous]
        [HttpGet]
        public List<CurrentTempProjectUserDto> GetAllCurrentTempProjectUser()
        {
            var results = WorkScope.GetAll<ProjectUser>()
                    .Where(s => s.User.UserType != UserType.FakeUser)
                    .Where(s => s.Status == ProjectUserStatus.Present)
                    .Where(s => s.AllocatePercentage > 0)
                    .Where(s => s.IsPool)
                    .OrderByDescending(s => s.StartTime)
                    .Select(s => new CurrentTempProjectUserDto
                    {
                        EmailAddress = s.User.EmailAddress,
                        ProjectCode = s.Project.Code
                    }).ToList();

            return results;
        }

    }
}
