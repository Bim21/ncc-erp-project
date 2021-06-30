using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class edit_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_PMId",
                table: "AuditResultPeoples");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditResultPeoples_AbpUserRoles_RoleId",
                table: "AuditResultPeoples");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AbpUsers_PmId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_AuditResultPeoples_PMId",
                table: "AuditResultPeoples");

            migrationBuilder.DropIndex(
                name: "IX_AuditResultPeoples_RoleId",
                table: "AuditResultPeoples");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "TimesheetFile",
                table: "TimesheetProjects");

            migrationBuilder.DropColumn(
                name: "ExpenseCount",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PMReports");

            migrationBuilder.DropColumn(
                name: "PMId",
                table: "AuditResultPeoples");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AuditResultPeoples");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AuditResultPeoples");

            migrationBuilder.RenameColumn(
                name: "PmId",
                table: "Projects",
                newName: "PMId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_PmId",
                table: "Projects",
                newName: "IX_Projects_PMId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Timesheets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "TimesheetProjects",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeDone",
                table: "ResourceRequests",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<byte>(
                name: "AllocatePercentage",
                table: "ProjectUsers",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpense",
                table: "ProjectUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFutureActive",
                table: "ProjectUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "BillRole",
                table: "ProjectUserBills",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ProjectUserBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PMReports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "PMReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PMId",
                table: "AuditResults",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsPass",
                table: "AuditResultPeoples",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AuditResults_PMId",
                table: "AuditResults",
                column: "PMId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResults_AbpUsers_PMId",
                table: "AuditResults",
                column: "PMId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AbpUsers_PMId",
                table: "Projects",
                column: "PMId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditResults_AbpUsers_PMId",
                table: "AuditResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AbpUsers_PMId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_AuditResults_PMId",
                table: "AuditResults");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "TimesheetProjects");

            migrationBuilder.DropColumn(
                name: "IsExpense",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "IsFutureActive",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ProjectUserBills");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PMReports");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PMReports");

            migrationBuilder.DropColumn(
                name: "PMId",
                table: "AuditResults");

            migrationBuilder.DropColumn(
                name: "IsPass",
                table: "AuditResultPeoples");

            migrationBuilder.RenameColumn(
                name: "PMId",
                table: "Projects",
                newName: "PmId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_PMId",
                table: "Projects",
                newName: "IX_Projects_PmId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TimesheetFile",
                table: "TimesheetProjects",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeDone",
                table: "ResourceRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AllocatePercentage",
                table: "ProjectUsers",
                type: "real",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<bool>(
                name: "ExpenseCount",
                table: "ProjectUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "BillRole",
                table: "ProjectUserBills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PMReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PMId",
                table: "AuditResultPeoples",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "Quantity",
                table: "AuditResultPeoples",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "AuditResultPeoples",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditResultPeoples_PMId",
                table: "AuditResultPeoples",
                column: "PMId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditResultPeoples_RoleId",
                table: "AuditResultPeoples",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_PMId",
                table: "AuditResultPeoples",
                column: "PMId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResultPeoples_AbpUserRoles_RoleId",
                table: "AuditResultPeoples",
                column: "RoleId",
                principalTable: "AbpUserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AbpUsers_PmId",
                table: "Projects",
                column: "PmId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
