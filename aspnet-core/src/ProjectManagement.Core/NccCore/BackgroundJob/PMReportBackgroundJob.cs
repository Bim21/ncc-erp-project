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
        readonly IRepository<PMReport, long> _pmReport;
        public PMReportBackgroundJob(IRepository<PMReport, long> pmReport)
        {
            _pmReport = pmReport;
        }
        [UnitOfWork]
        protected override async Task ExecuteAsync(PMReportBackgroundJobArgs args)
        {
            Logger.Info("PMReport background trigger!");
            try
            {
                var test = await _pmReport.GetAsync(args.PMReportId);
                test.PMReportStatus = args.PMReportStatus;
                await _pmReport.UpdateAsync(test);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
