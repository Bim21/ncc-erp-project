using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class add_Curator_To_Table_AuditReusultPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CuratorId",
                table: "AuditResultPeoples",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AuditResultPeoples_CuratorId",
                table: "AuditResultPeoples",
                column: "CuratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples",
                column: "CuratorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditResultPeoples_AbpUsers_CuratorId",
                table: "AuditResultPeoples");

            migrationBuilder.DropIndex(
                name: "IX_AuditResultPeoples_CuratorId",
                table: "AuditResultPeoples");

            migrationBuilder.DropColumn(
                name: "CuratorId",
                table: "AuditResultPeoples");
        }
    }
}
