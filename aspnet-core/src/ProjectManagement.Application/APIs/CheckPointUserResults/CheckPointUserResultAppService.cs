using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.APIs.CheckPointUserResults.Dto;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.RequestLevel
{
    public class CheckPointUserResultAppService : ProjectManagementAppServiceBase
    {
        [HttpGet]
        public async Task<object> GetAll(long phaseId)
        {
            var phase = await WorkScope.GetAsync<Phase>(phaseId);
            if (phase.Type == PhaseType.Sub)
            {
                var resultSub = WorkScope.GetAll<CheckPointUser>()
                    .Where(x => x.PhaseId == phaseId)
                    .Select(x => new
                    {
                        Id = x.Id,
                        PhaseId = x.Phase.Name,
                        Note = x.Note,
                        ReviewerName = x.Reviewer.FullName,
                        Score = x.Score.Value,
                        Status = x.Status,
                        Type = x.Type,
                        UserName = x.User.Name,
                    });
                return await resultSub.ToListAsync();
            }

            var cput = from a in WorkScope.GetAll<CheckPointUserResultTag>()
                       join b in WorkScope.GetAll<Tag>() on a.TagId equals b.Id
                       select new
                       {
                           Id = a.Id,
                           CPURId = a.CheckPointUserResultId,
                           TagName = b.Name,
                       };

            var resultMain = from cpur in WorkScope.GetAll<CheckPointUserResult>()
                             where cpur.PhaseId == phaseId
                             select new
                             {
                                 Id = cpur.Id,
                                 PhaseId = cpur.Phase.Name,
                                 UserName = cpur.User.FullName,
                                 PMName = cpur.PM.FullName,
                                 PMNote = cpur.PMNote,
                                 CurrentLevel = cpur.OldLevel,
                                 NewLevel = cpur.NewLevel,
                                 PMScore = cpur.PMScore.Value,
                                 TeamScore = cpur.TeamScore.Value,
                                 ClientScore = cpur.ClientScore.Value,
                                 ExamScore = cpur.ExamScore.Value,
                                 Status = cpur.Status,
                                 Tag = cput.Where(x => x.CPURId == cpur.Id).ToList(),
                             };
            return await resultMain.ToListAsync();
        }
        [HttpPut]
        public async Task EditMain(long checkPointUserResultId, List<ResultTagDto> input)
        {
            var oldTags = WorkScope.GetAll<CheckPointUserResultTag>().Where(x => x.CheckPointUserResultId == checkPointUserResultId).ToList();

            var oldTagIds = oldTags.Select(x => x.TagId);
            var insertTags = input.Select(x => x.TagId).Except(oldTagIds);
            var deleteTags = oldTagIds.Except(input.Select(x => x.TagId));
            var deleteTagIds = oldTags.Where(x => deleteTags.Contains(x.TagId));

            foreach (var item in insertTags)
            {
                ResultTagDto temp = new ResultTagDto() { Id = 0, TagId = item, CheckPointUserResultId = checkPointUserResultId };
                await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<CheckPointUserResultTag>(temp));
            }

            foreach (var item in deleteTagIds)
            {
                await WorkScope.DeleteAsync<CheckPointUserResultTag>(item);
            }

            var cpuResult = await WorkScope.GetAsync<CheckPointUserResult>(checkPointUserResultId);

            cpuResult.Status = CheckPointUserResultStatus.FinalDone;
            await WorkScope.UpdateAsync<CheckPointUserResult>(cpuResult);
        }
        [HttpGet]
        public async Task<object> ShowShortDetails(long checkPointUserResultId)
        {
            var cpur = await WorkScope.GetAsync<CheckPointUserResult>(checkPointUserResultId);
            var cpus = WorkScope.GetAll<CheckPointUser>().Where(x => x.PhaseId == cpur.PhaseId && x.UserId == cpur.UserId)
                .Select(x => new
                {
                    Reviewer = x.Reviewer.FullName,
                    PhaseType = x.Phase.Type,
                    Note = x.Note,
                }).ToList();
            return cpus;
        }
    }
}
