using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProjectManagement.Authorization.Roles;
using ProjectManagement.Authorization.Users;
using ProjectManagement.MultiTenancy;
using ProjectManagement.Entities;

namespace ProjectManagement.EntityFrameworkCore
{
    public class ProjectManagementDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<AuditResult> AuditResults { get; set; }
        public DbSet<AuditResultPeople> AuditResultPeoples { get; set; }
        public DbSet<AuditSession> AuditSessions { get; set; }
        public DbSet<CheckListCategory> CheckListCategories { get; set; }
        public DbSet<CheckListItem> CheckListItems { get; set; }
        public DbSet<CheckListItemMandatory> CheckListItemMandatorys { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<PMReport> PMReports { get; set; }
        public DbSet<PMReportProject> PMReportProjects { get; set; }
        public DbSet<PMReportProjectIssue> PMReportProjectIssues { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCheckList> ProjectCheckLists { get; set; }
        public DbSet<ProjectMilestone> ProjectMilestones { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectUserBill> ProjectUserBills { get; set; }
        public DbSet<ResourceRequest> ResourceRequests { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TimesheetProject> TimesheetProjects { get; set; }
        public DbSet<CriteriaCategory> CriteriaCategories { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<CheckPointUser> CheckPointUsers { get; set; }
        public DbSet<CheckPointUserDetail> CheckPointUserDetails { get; set; }
        public DbSet<CheckPointUserResult> CheckPointUserResults { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CheckPointUserResultTag> CheckPointUserResultTags { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
            : base(options)
        {
        }
    }
}
