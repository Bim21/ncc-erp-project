using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using NccCore.Anotations;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.PMReportProjects.Dto
{
    [AutoMapTo(typeof(PMReportProject))]
    public class PMReportProjectDto : EntityDto<long>
    {
        public long PMReportId { get; set; }
        public long ProjectId { get; set; }
        public PMReportProjectStatus Status { get; set; }
        public ProjectHealth ProjectHealth { get; set; }
        public long PMId { get; set; }
        public string Note { get; set; }
        public bool Seen { get; set; }
    }

    public class PMUnsentWeeklyReportDto
    {
        public int Index { get; set; }
        public string ProjectName { get; set; }
        public string WeeklyName { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
