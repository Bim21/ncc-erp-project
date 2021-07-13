using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class themnullcuratorIdandendTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "AuditSessions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "CuratorId",
                table: "AuditResultPeoples",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples",
                column: "CuratorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "AuditSessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CuratorId",
                table: "AuditResultPeoples",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples",
                column: "CuratorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
