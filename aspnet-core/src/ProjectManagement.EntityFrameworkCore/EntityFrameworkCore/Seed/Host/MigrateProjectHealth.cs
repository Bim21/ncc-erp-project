using System;
using System.Linq;
using Abp.Timing;

namespace ProjectManagement.EntityFrameworkCore.Seed.Host
{
    public class MigrateProjectHealth
    {
        /// <summary>
        /// Update lại Project Health khi sửa lại value Enum 0,1,2 -> 1,2,3
        /// </summary>
        private readonly ProjectManagementDbContext _context;

        public MigrateProjectHealth(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            MigrateProjectHealthNewEnum();
        }

        private void MigrateProjectHealthNewEnum()
        {
            var oldPMReportProject = _context.PMReportProjects
                .Where(x => x.CreationTime.Date < new DateTime(2023, 02, 22, 0, 0, 0, 0))
                .ToList();
            oldPMReportProject.ForEach(x =>
            {
                x.ProjectHealth += 1;
            });
            _context.SaveChanges();
        }
    }
}