using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.AuditResults.Dto;
using ProjectManagement.APIs.AuditSessions.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.AuditSessions
{
    [AbpAuthorize]
    public class AuditSessionAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_Create)]
        public async Task<AuditSessionDto> Create(AuditSessionDto input)
        {
            if(input.StartTime > input.EndTime)
            {
                throw new UserFriendlyException("Start Time can't be greater than End Time.");
            }
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<AuditSession>(input));
            var activeProject = await WorkScope.GetAll<Project>()
                                .Where(x => x.Status != ProjectStatus.Closed )
                                .ToListAsync();
            //auto thêm các project active
            foreach (var p in activeProject)
            {
                await WorkScope.InsertAndGetIdAsync(new AuditResult
                {
                    AuditSessionId = input.Id,
                    ProjectId = p.Id,
                    PMId = p.PMId
                });
            }
            return input;
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_AddAuditResult)]
        public async Task<List<AuditResultDto>> AddManyAuditResult(List<long> projectIds, long auditSessionId)
        {
            var query = await WorkScope.GetAll<Project>()
                        .Where(x => projectIds.Contains(x.Id))
                        .Select(x => new AuditResultDto
                        {
                            AuditSessionId = auditSessionId,
                            ProjectId = x.Id,
                            PMId = x.PMId
                        })
                        .ToListAsync();
            foreach (var i in query)
            {
                i.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<AuditResult>(i));
            }
            return query;
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_Update)]
        public async Task<AuditSessionDto> Update(AuditSessionDto input)
        {
            var isExist = await WorkScope.GetAsync<AuditSession>(input.Id);
            ObjectMapper.Map(input, isExist);
            await WorkScope.UpdateAsync(isExist);
            return input;
        }
        
        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_ViewAll)]
        [HttpPost]
        public async Task<GridResult<AuditSessionDetailDto>> GetAllPaging(GridParam input)
        {
            //trong 1 audit result => list
            var listSessionPeople = WorkScope.GetAll<AuditResultPeople>()
                                     .Select(x => new { x.AuditResult.AuditSessionId, x.IsPass });
            var countStatus = from ar in WorkScope.GetAll<AuditResult>()
                              select new
                              {
                                  ar.AuditSessionId,
                                  ar.ProjectId,
                                  status = ar.Status
                              };
            //trong 1 auditsession => list
            var query = from a in WorkScope.GetAll<AuditSession>()
                        select new AuditSessionDetailDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            EndTime = a.EndTime,
                            StartTime = a.StartTime,
                            CountFail = listSessionPeople.Where(x => x.AuditSessionId == a.Id && !x.IsPass).Count(),
                            CountProjectCheck = countStatus.Count(x => x.AuditSessionId == a.Id && x.status == AuditResultStatus.Done),
                            CountProjectCreate = countStatus.Count(x=>x.AuditSessionId == a.Id)
                        };
            return await query.GetGridResult(query, input);
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_View)]
        public async Task<List<AuditSessionDetailDto>> Get(long Id)
        {
            var checkExist = await WorkScope.GetAsync<AuditSession>(Id);
            var listSessionPeople = WorkScope.GetAll<AuditResultPeople>()
                                     .Select(x => new { x.AuditResultId, x.IsPass });
            var namePM = await WorkScope.GetAll<User>().ToDictionaryAsync(x => x.Id);

            return await (from ar in WorkScope.GetAll<AuditResult>().Where(x => x.AuditSessionId == Id)
                          select new AuditSessionDetailDto
                          {
                              Id = ar.Id,
                              StartTime = checkExist.StartTime,
                              EndTime = checkExist.EndTime,
                              PmName = namePM.ContainsKey(ar.PMId) ? namePM[ar.PMId].Name : null,
                              ProjectId = ar.Project.Id,
                              ProjectName = ar.Project.Name,
                              AuditResultStatus = ar.Status.ToString(),
                              CountFail = listSessionPeople.Where(x => x.AuditResultId == ar.Id && !x.IsPass).Count(),
                              Status = ar.Status.ToString()
                          }).ToListAsync();
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_Delete)]
        public async Task Delete(long id)
        {
            var delAuditResult = await WorkScope.GetAll<AuditResult>().Where(x => x.AuditSessionId == id).ToListAsync();
            if (delAuditResult.Count > 0)
            {
                throw new UserFriendlyException("Audit Session with '" + id + "' has Audit result. Please delete them before deleting.");
            }

            var delAuditResultPeople = await WorkScope.GetAll<AuditResultPeople>()
                                        .AnyAsync(x => delAuditResult.Select(y => y.Id).Contains(x.Id));
            if (delAuditResultPeople)
            {
                throw new UserFriendlyException("Audit Session with '" + id + "' has Audit Result People . Please delete them before deleting.");

            }

            await WorkScope.DeleteAsync<AuditSession>(id);
        }
    }
}
