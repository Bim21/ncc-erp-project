using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectManagement.Services.Talent.Dtos
{
    public class RecruitmentTalentDto
    {
        [Required]
        public long ResourceRequestId { get; set; }
        [Required]
        public long PositionId { get; set; }
        [Required]
        public long BranchId { get; set; }
    }
}
