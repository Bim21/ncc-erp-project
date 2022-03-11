using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.ResourceService.Dto
{
    public class KomuProjectInfoDto
    {
        public long ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public KomuUserInfoDto PM { get; set; }

        public string KomuProjectInfo {
            get { 
                return $"project: **{ProjectName}** [pm: {PM.KomuAccountInfo}] "; 
            }
        }
    }
}
