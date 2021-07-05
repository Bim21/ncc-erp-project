using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class nullable_ResourceRequestId_Table_projectUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_ResourceRequests_ResourceRequestId",
                table: "ProjectUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ResourceRequestId",
                table: "ProjectUsers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_ResourceRequests_ResourceRequestId",
                table: "ProjectUsers",
                column: "ResourceRequestId",
                principalTable: "ResourceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_ResourceRequests_ResourceRequestId",
                table: "ProjectUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ResourceRequestId",
                table: "ProjectUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_ResourceRequests_ResourceRequestId",
                table: "ProjectUsers",
                column: "ResourceRequestId",
                principalTable: "ResourceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
