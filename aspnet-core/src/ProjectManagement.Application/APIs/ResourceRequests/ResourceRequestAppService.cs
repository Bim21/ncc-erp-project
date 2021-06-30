using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.ResourceRequests.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Constants.Enum;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.APIs.ResourceRequests
{
    public class ResourceRequestAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<GetResourceRequestDto>> GetAllByProject(GridParam input, long projectId)
        {
            var query = WorkScope.GetAll<ResourceRequest>().Where(x => x.ProjectId == projectId)
                        .Select(x => new GetResourceRequestDto
                        {
                            Id = x.Id,
                            ProjectId = x.ProjectId,
                            Name = x.Name,
                            Status = x.Status.ToString(),
                            Note = x.Note,
                            TimeNeed = x.TimeNeed,
                            TimeDone = x.TimeDone.Value
                        });
            return await query.GetGridResult(query, input);
        }
    }
}
