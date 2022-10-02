using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsManager.DataModels.Migrations
{
    public partial class AddedDeletedOnDateToBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TAccounts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TAccounts");
        }
    }
}
