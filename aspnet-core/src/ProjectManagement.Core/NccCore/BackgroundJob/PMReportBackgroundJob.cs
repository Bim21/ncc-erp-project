using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using NccCore.IoC;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.NccCore.BackgroundJob
{
   public  class PMReportBackgroundJob : BackgroundJob<PMReportBackgroundJobArgs>, ITransientDependency
    {
        readonly IWorkScope _workScope;
        public PMReportBackgroundJob(IWorkScope workScope)
        {
            _workScope = workScope;
        }
        [UnitOfWork]
        public override void Execute(PMReportBackgroundJobArgs args)
        {
            Logger.Info("PMReport background trigger!");
            try
            {
                var pmReport = _workScope.Get<PMReport>(args.PMReportId);
                pmReport.PMReportStatus = args.PMReportStatus;
                _workScope.Update(pmReport);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
