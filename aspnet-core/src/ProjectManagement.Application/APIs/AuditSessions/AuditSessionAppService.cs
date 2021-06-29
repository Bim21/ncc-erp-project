using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using ProjectManagement.APIs.AuditSessions.Dto;
using ProjectManagement.Authorization;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.AuditSessions
{
    public class AuditSessionAppService : ProjectManagementAppServiceBase
    {
        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_Create)]
        public async Task<AuditSessionDto> Create(AuditSessionDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<AuditSession>(input));
            var activeProject = await WorkScope.GetAll<Project>()
                                .Where(x => x.Status != ProjectStatus.Closed && x.Status != ProjectStatus.Potential).ToListAsync();
            // auto thêm các project active
            foreach (var p in activeProject)
            {
                await WorkScope.InsertAsync(new AuditResult
                {
                    AuditSessionId = input.Id,
                    ProjectId = p.Id,
                });
            }
            return input;
        }
        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_AddAuditResult)]
        public async Task<List<AuditResultDto>> AddManyAuditResult(List<long> projectIds, long AuditSessionId)
        {
            List<AuditResultDto> result = new List<AuditResultDto>();
            foreach (var i in projectIds)
            {
                await WorkScope.InsertAsync(new AuditResult
                {
                    AuditSessionId = AuditSessionId,
                    ProjectId = i,
                });
                result.Add(new AuditResultDto { 
                    AuditSessionId = AuditSessionId,
                    ProjectId = i,
                });
            }
            return result;
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
        public async Task<GridResult<AuditSessionDto>> GetAllPaging(GridParam input)
        {
            // trong 1 audit result => list
            var sumQuantity = from arp in WorkScope.GetAll<AuditResultPeople>()
                              select new
                              {
                                  arp.AuditResult.AuditSessionId,
                                  quantity = arp.Quantity
                              };
            var countStatus = from ar in WorkScope.GetAll<AuditResult>()
                              select new
                              {
                                  ar.AuditSessionId,
                                  status = ar.Project.Status
                              };
            // trong 1 auditsession => list
            var query = from a in WorkScope.GetAll<AuditSession>()
                        select new AuditSessionDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            EndTime = a.EndTime,
                            StartTime = a.StartTime,
                            CountFail = sumQuantity.Where(x => x.AuditSessionId == a.Id).Sum(x => x.quantity),
                            CountProjectCheck = countStatus.Where(x => x.AuditSessionId == a.Id).Count(x => x.status == ProjectStatus.Maintain),
                            CountProjectCreate = countStatus.Where(x => x.AuditSessionId == a.Id).Count(x => x.status == ProjectStatus.InProgress)
                        };
            return await query.GetGridResult(query, input);
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_View)]
        public async Task<AuditSessionDetailDto> Get(long Id)
        {
            var checkExist = await WorkScope.GetAsync<AuditSession>(Id);
            var sumQuantity = from arp in WorkScope.GetAll<AuditResultPeople>()
                              select new
                              {
                                  arp.AuditResult.AuditSessionId,
                                  PmName = arp.User.Name,
                                  quantity = arp.Quantity
                              };

            return await (from ar in WorkScope.GetAll<AuditResult>().Where(x => x.AuditSessionId == Id)
                          select new AuditSessionDetailDto
                          {
                              Id = checkExist.Id,
                              StartTime = checkExist.StartTime,
                              EndTime = checkExist.EndTime,
                              PmName = sumQuantity.Where(x => x.AuditSessionId == Id).FirstOrDefault().PmName,
                              ProjectName = ar.Project.Name,
                              projectStatus = ar.Project.Status,
                              CountFail = sumQuantity.Where(x => x.AuditSessionId == Id).Sum(x => x.quantity),
                          }).FirstOrDefaultAsync();
        }

        [AbpAuthorize(PermissionNames.SaoDo_AuditSession_Delete)]
        public async Task Delete(long id)
        {
            var item = await WorkScope.GetAsync<AuditSession>(id);
            var delAuditResult = await WorkScope.GetAll<AuditResult>().Where(x => x.AuditSessionId == id).ToListAsync();
            var delAuditResultPeople = await WorkScope.GetAll<AuditResultPeople>().Where(x => delAuditResult.Select(y => y.Id).Contains(x.Id)).ToListAsync();

            foreach (var i in delAuditResultPeople)
            {
                await WorkScope.DeleteAsync(i);
            }
            foreach (var i in delAuditResult)
            {
                await WorkScope.DeleteAsync(i);
            }
            await WorkScope.DeleteAsync(item);
        }
    }
}
