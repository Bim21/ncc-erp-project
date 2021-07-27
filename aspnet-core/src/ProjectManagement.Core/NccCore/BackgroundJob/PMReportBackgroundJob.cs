using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.NccCore.BackgroundJob
{
   public  class PMReportBackgroundJob : BackgroundJob<PMReportBackgroundJobArgs>, ITransientDependency
    {
        readonly IRepository<PMReport, long> _pmReport;
        public PMReportBackgroundJob(IRepository<PMReport, long> pmReport)
        {
            _pmReport = pmReport;
        }
        [UnitOfWork]
        public override void Execute(PMReportBackgroundJobArgs args)
        {
            Logger.Info("PMReport background trigger!");
            try
            {
                var pmReport = _pmReport.Get(args.PMReportId);
                pmReport.PMReportStatus = args.PMReportStatus;
                _pmReport.Update(pmReport);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
