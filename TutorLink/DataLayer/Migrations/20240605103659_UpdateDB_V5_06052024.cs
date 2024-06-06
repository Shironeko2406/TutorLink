using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V5_06052024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepositId",
                table: "WalletTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositId);
                    table.ForeignKey(
                        name: "FK_Deposits_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_DepositId",
                table: "WalletTransactions",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_WalletId",
                table: "Deposits",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Deposits_DepositId",
                table: "WalletTransactions",
                column: "DepositId",
                principalTable: "Deposits",
                principalColumn: "DepositId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Deposits_DepositId",
                table: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_DepositId",
                table: "WalletTransactions");

            migrationBuilder.DropColumn(
                name: "DepositId",
                table: "WalletTransactions");
        }
    }
}
