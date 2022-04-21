using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class Delete_Currency_And_Add_ChargeType_CurrencyId_column_to_TimesheetProjectBill_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "TimesheetProjectBills");

            migrationBuilder.AddColumn<int>(
                name: "ChargeType",
                table: "TimesheetProjectBills",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "TimesheetProjectBills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetProjectBills_CurrencyId",
                table: "TimesheetProjectBills",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetProjectBills_Currencies_CurrencyId",
                table: "TimesheetProjectBills",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetProjectBills_Currencies_CurrencyId",
                table: "TimesheetProjectBills");

            migrationBuilder.DropIndex(
                name: "IX_TimesheetProjectBills_CurrencyId",
                table: "TimesheetProjectBills");

            migrationBuilder.DropColumn(
                name: "ChargeType",
                table: "TimesheetProjectBills");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "TimesheetProjectBills");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "TimesheetProjectBills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
