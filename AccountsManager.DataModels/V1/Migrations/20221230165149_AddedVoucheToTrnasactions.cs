using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsManager.DataModels.Migrations
{
    public partial class AddedVoucheToTrnasactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VoucherId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VoucherId",
                table: "Transactions",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vouchers_VoucherId",
                table: "Transactions",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vouchers_VoucherId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_VoucherId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Transactions");
        }
    }
}
