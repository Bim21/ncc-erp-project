using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class add_multiTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Timesheets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ResourceRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProjectUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProjectUserBills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProjectMilestones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProjectCheckLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PMReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PMReportProjects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PMReportProjectIssues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "CheckListItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "CheckListItemMandatorys",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "CheckListCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AuditSessions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AuditResults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AuditResultPeoples",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ResourceRequests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProjectUserBills");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProjectMilestones");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProjectCheckLists");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PMReports");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PMReportProjects");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PMReportProjectIssues");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "CheckListItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "CheckListItemMandatorys");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "CheckListCategories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AuditSessions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AuditResults");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AuditResultPeoples");
        }
    }
}
