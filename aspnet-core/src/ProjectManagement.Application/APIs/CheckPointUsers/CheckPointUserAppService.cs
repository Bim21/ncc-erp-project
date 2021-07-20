using Abp.Application.Services.Dto;
using NccCore.Paging;
using ProjectManagement.APIs.CheckPointUsers.Dto;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NccCore.Extension;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using static ProjectManagement.Constants.Enum.ProjectEnum;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.APIs.CheckPointUsers
{
    public class CheckPointUserAppService : ProjectManagementAppServiceBase
    {
        [HttpPost]
        public async Task<GridResult<CheckPointUserDto>> GetAllPagging(GridParam input)
        {
            var result = WorkScope.GetAll<CheckPointUser>()
                         .Include(x=>x.Reviewer)
                         .Include(x=>x.User)
                         .Select(x => new CheckPointUserDto
                         {
                             ReviewerName = x.Reviewer.FullName,
                             ReviewerEmail = x.Reviewer.EmailAddress,
                             ReviewerAvatar = x.Reviewer.AvatarPath,
                             UserName = x.User.FullName,
                             UserEmail = x.User.EmailAddress,
                             UserAvatar = x.User.AvatarPath,
                             Type = x.Type,
                             Status = x.Status
                         });

            return await result.GetGridResult(result, input);
        }

        public async Task<CheckPointUserDto> Get(long Id)
        {
            return await WorkScope.GetAll<CheckPointUser>()
                         .Include(x => x.Reviewer)
                         .Include(x => x.User)
                         .Where(x => x.Id == Id)
                         .Select(x => new CheckPointUserDto
                         {
                             ReviewerName = x.Reviewer.FullName,
                             ReviewerEmail = x.Reviewer.EmailAddress,
                             ReviewerAvatar = x.Reviewer.AvatarPath,
                             UserName = x.User.FullName,
                             UserEmail = x.User.EmailAddress,
                             UserAvatar = x.User.AvatarPath,
                             Type = x.Type,
                             Status = x.Status
                         }).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<CheckPointUserInputDto> Create(CheckPointUserInputDto input)
        {
            var isExist = await WorkScope.GetAll<CheckPointUser>().AnyAsync(x => x.ReviewerId == input.ReviewerId && x.UserId == input.UserId);
            if(isExist)
            {
                throw new UserFriendlyException("This user has been evaluated by this reviewer!");
            }
            var map = ObjectMapper.Map<CheckPointUser>(input);
            input.Id = await WorkScope.InsertAndGetIdAsync(map);
            //Tạo tiêu chí đánh giá 

            await GenerateDetail(input.Id, input.PhaseId);
            
            return input;
        }
        [HttpPut]
        public async Task<CheckPointUserInputDto> Update(CheckPointUserInputDto input)
        {
            var checkPointUser = await WorkScope.GetAsync<CheckPointUser>(input.Id);

            if (checkPointUser.Status == CheckPointUserStatus.Reviewed)
            {
                throw new UserFriendlyException("Reviewed. Can not edit!");
            }
            await WorkScope.UpdateAsync(ObjectMapper.Map<CheckPointUserInputDto, CheckPointUser>(input, checkPointUser));

            return input;
        }
        public async Task Delete(long Id)
        {
            var checkPointUser = await WorkScope.GetAsync<CheckPointUser>(Id);
            if(checkPointUser.Status != CheckPointUserStatus.Draft)
            {
                throw new UserFriendlyException("Reviewed. Can not delete!");
            }
            var details = WorkScope.GetAll<CheckPointUserDetail>().Where(x => x.CheckPointUserId == Id);
            foreach (var detail in details)
            {
                await WorkScope.DeleteAsync(detail);
            }

            await WorkScope.DeleteAsync(checkPointUser);

        }
        [HttpGet]
        public async Task GenerateReviewer(long phaseId)
        {
            var userProjects = await (from p in WorkScope.GetAll<ProjectUser>().Include(x => x.Project).Where(x => x.Status == ProjectUserStatus.Present)
                                      group p by new { p.UserId, p.ProjectId } into g
                                      select new
                                      {
                                          UserId = g.Key.UserId,
                                          ProjectId = g.Key.ProjectId
                                      }).ToListAsync();
            var mapPMProjects = await WorkScope.GetAll<Project>()
                                        .Select(x => new
                                        {
                                            ProjectId = x.Id,
                                            PMId = x.PMId
                                        }).ToDictionaryAsync(x => x.ProjectId, x => x.PMId);

            foreach (var item in userProjects)
            {
                bool isExist = await WorkScope.GetAll<CheckPointUser>().AnyAsync(x => x.UserId == item.UserId && x.ReviewerId == mapPMProjects[item.ProjectId]);
                //bỏ Pm đánh giá chính mình
                if (item.UserId != mapPMProjects[item.ProjectId] && !isExist)
                {
                    var checkPointUser = new CheckPointUser
                    {
                        UserId = item.UserId,
                        ReviewerId = mapPMProjects[item.ProjectId],
                        Type = CheckPointUserType.PM,
                        PhaseId = phaseId,
                        Status = CheckPointUserStatus.Draft
                    };
                    await WorkScope.InsertAndGetIdAsync(checkPointUser);
                    await GenerateDetail(checkPointUser.Id, phaseId);
                }

            }
        }

        //public async Task<object> GetUnReview(long reviewerId, long userId)
        //{
        //    var userProjects = await (from p in WorkScope.GetAll<ProjectUser>()
        //                              .Include(x => x.Project)
        //                              .Where(x => x.Status == ProjectUserStatus.Present)
        //                              group p by new { p.UserId, p.ProjectId } into g
        //                              select new
        //                              {
        //                                  UserId = g.Key.UserId,
        //                                  ProjectId = g.Key.ProjectId
        //                              }).ToListAsync();
        //    var userPMs =  (from u in userProjects
        //                  join p in WorkScope.GetAll<Project>()
        //                  on u.ProjectId equals p.Id
        //                  select new
        //                  {
        //                      UserId = u.UserId,
        //                      ReviewerId = p.PMId
        //                  }).ToList();

        //    var reviewed = WorkScope.GetAll<CheckPointUser>()
        //                            .Where(x => x.ReviewerId == reviewerId && x.UserId == userId)
        //                            .Select(x=> new
        //                            {
        //                                UserId = x.UserId,
        //                                reviewerId = x.ReviewerId
        //                            }).ToList();
        //    return await userPMs.Except(reviewed);
        //}

        private async Task GenerateDetail(long checkpointUserId, long phaseId)
        {
            var phase = await WorkScope.GetAsync<Phase>(phaseId);
            var criterias = WorkScope.GetAll<Criteria>();
            if (phase.IsCriteria)
            {
                foreach (var criteria in criterias)
                {
                    var detail = new CheckPointUserDetail
                    {
                        CriteriaId = criteria.Id,
                        CheckPointUserId = checkpointUserId
                    };

                    await WorkScope.InsertAsync(detail);
                }
            }
        }
    }
}
