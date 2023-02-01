﻿using NccCore.Anotations;
using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.AuditLogs.Dto
{
    public class GetAuditLogDto
    {
        public long? UserId { get; set; }
        [ApplySearch]
        public string EmailAddress { get; set; }
        [ApplySearch]
        public string MethodName { get; set; }
        public string Parameters { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int ExecutionDuration { get; set; }
        [ApplySearch]
        public string ServiceName { get; set; }
        [ApplySearch]
        public string UserIdString { get; set; }
        public string Note => AuditLogUtils.GetNote(ServiceName, MethodName);
    }
}
