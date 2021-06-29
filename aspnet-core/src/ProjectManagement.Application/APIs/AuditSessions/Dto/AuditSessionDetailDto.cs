using Abp.Domain.Entities;
using NccCore.Anotations;
using System;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.AuditSessions.Dto
{
    public class AuditSessionDetailDto : Entity<long>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ApplySearchAttribute]
        public string ProjectName { get; set; }
        [ApplySearchAttribute]
        public string PmName { get; set; }
        public ProjectStatus projectStatus { get; set; }
        public long CountFail { get; set; }
    }
}
