using Abp.Application.Services;
using NccCore.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Services.ProjectTimesheet.Dto;

namespace ProjectManagement.Services.ProjectTimesheet
{
    public class ProjectTimesheetManager : ApplicationService
    {
        private readonly IWorkScope _workScope;
        private readonly ILogger<ProjectTimesheetManager> Logger;

        public ProjectTimesheetManager(IWorkScope workScope, ILogger<ProjectTimesheetManager> logger)
        {
            _workScope = workScope;
            this.Logger = logger;
        }

        public async Task<long> GetActiveTimesheetId()
        {
            return await _workScope.GetAll<Entities.Timesheet>()
                .Where(s => s.IsActive)
               .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
               .Select(x => x.Id)
               .FirstOrDefaultAsync();
        }
        public async Task<TimesheetDto> GetActiveTimesheet()
        {
            return await _workScope.GetAll<Entities.Timesheet>()
                .Where(s => s.IsActive)
               .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
               .Select(x => new TimesheetDto {
                   Id = x.Id,
                   Month = x.Month,
                   Year = x.Year,
                   IsActive = x.IsActive
               })
               .FirstOrDefaultAsync();
        }

        public async Task<TimesheetDto> GetTimesheetById(long id)
        {
            return await _workScope.GetAll<Entities.Timesheet>()
                .Where(s => s.Id == id)
               .Select(x => new TimesheetDto
               {
                   Id = x.Id,
                   Month = x.Month,
                   Year = x.Year,
                   IsActive = x.IsActive
               })
               .FirstOrDefaultAsync();
        }


        public async Task CreateTimesheetProjectBill(ProjectUserBill pub,Project project)
        {
            var activeTimesheetId = await GetActiveTimesheetId();
            if (activeTimesheetId == default)
            {
                Logger.LogInformation("CreateTimesheetProjectBill() no activeTimesheetId");
                return;
            }
            var tpb = new TimesheetProjectBill
            {
                TimesheetId = activeTimesheetId,
                IsActive = true,
                BillRate = pub.BillRate,
                BillRole = pub.BillRole,
                //CurrencyId = pub.CurrencyId,
                ProjectId = pub.ProjectId,
                UserId = pub.UserId,
                WorkingTime = 0,
                ChargeType = project.ChargeType,
                CurrencyId = project.CurrencyId
            };

            await _workScope.InsertAsync(tpb);
        }

        public async Task UpdateTimesheetProjectBill(ProjectUserBill pub)
        {
            var activeTimesheetId = await GetActiveTimesheetId();
            if (activeTimesheetId == default)
            {
                Logger.LogInformation("UpdateTimesheetProjectBill() no activeTimesheetId");
                return;
            }

            var tpb = await _workScope.GetAll<TimesheetProjectBill>()
                .Where(s => s.ProjectId == pub.ProjectId)
                .Where(s => s.TimesheetId == activeTimesheetId)
                .Where(s => s.UserId == pub.UserId)
                .FirstOrDefaultAsync();

            if (tpb == default)
            {
                Logger.LogInformation($"UpdateTimesheetProjectBill() not found TimesheetProjectBill ProjectId={pub.ProjectId}, TimesheetId={activeTimesheetId}, UserId={pub.UserId}");
                return;
            }

            tpb.BillRate = pub.BillRate;
            tpb.BillRole = pub.BillRole;
            //tpb.Currency = pub.Currency;
            tpb.IsActive = pub.isActive;

            await _workScope.UpdateAsync(tpb);
        }

        public async Task DeleteTimesheetProjectBill(long projectId, long timesheetId)
        {
            var tspbList = await _workScope.GetAll<TimesheetProjectBill>()
                .Where(x => x.ProjectId == projectId && x.TimesheetId == timesheetId)                
                .ToListAsync();

            if (tspbList == null || tspbList.Count == 0)
            {
                return;
            }
            
            foreach (var item in tspbList)
            {
                item.IsDeleted = true;                
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProjectUserBillDto>> GetListProjectUserBillDto(int year, int month, long? projectId)
        {
            var firstDayOfMonth = new DateTime(year, month, 1).Date;
            var q = await _workScope.GetAll<ProjectUserBill>()
               .Where(s => s.Project.IsCharge == true)
               .Where(s => s.isActive)
               .Where(s => !projectId.HasValue || s.ProjectId == projectId.Value)
               .Where(s => !s.EndTime.HasValue || s.EndTime.Value.Date >= firstDayOfMonth)
               .Select(s => new ProjectUserBillDto
               {
                   ProjectId = s.ProjectId,
                   UserId = s.UserId,
                   BillRate = s.BillRate,
                   BillRole = s.BillRole,
                   StartTime = s.StartTime,
                   EndTime = s.EndTime,
                   ChargeType = s.Project.ChargeType,
                   CurrencyId = s.Project.CurrencyId
               }).ToListAsync();

            return q;
        }

    }
}
