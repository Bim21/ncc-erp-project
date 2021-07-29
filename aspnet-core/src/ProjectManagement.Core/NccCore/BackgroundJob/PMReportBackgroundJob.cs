using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using NccCore.IoC;
using ProjectManagement.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.NccCore.BackgroundJob
{
   public  class PMReportBackgroundJob : AsyncBackgroundJob<PMReportBackgroundJobArgs>, ITransientDependency
    {
        readonly IWorkScope _workScope;
        public PMReportBackgroundJob(IWorkScope workScope)
        {
            _workScope = workScope;
        }
        [UnitOfWork]
        protected override async Task ExecuteAsync(PMReportBackgroundJobArgs args)
        {
            Logger.Info("PMReport background trigger!");
            try
            {
                var pmReport = await _workScope.GetAsync<PMReport>(args.PMReportId);
                pmReport.PMReportStatus = args.PMReportStatus;
                await _workScope.UpdateAsync(pmReport);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
