using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.CriteriaCategories.Dto
{
    [AutoMapTo(typeof(CriteriaCategory))]
    public class CriteriaCategoryDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
