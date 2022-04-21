using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class Add_InvoiceDateSetting_PaymentDueBy_column_to_Client_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "InvoiceDateSetting",
                table: "Clients",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "PaymentDueBy",
                table: "Clients",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceDateSetting",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PaymentDueBy",
                table: "Clients");
        }
    }
}
