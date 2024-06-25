using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V6_06142024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Address", "Email", "Fullname", "Gender", "Password", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("5735ae19-bc13-4a72-abf7-d8e32a4bd7c8"), "Ho Chi Minh, Viet Name", "vana@gmail.com", "Tran Van A", 0, "@123", "0978988768", 4, "parent1" },
                    { new Guid("83fd2c23-d9c5-4a73-bc13-82ac9a812e3a"), "Ho Chi Minh, Viet Name", "admin@gmail.com", "ADMIN", 1, "admin123", "0945677876", 1, "admin" },
                    { new Guid("ad016e07-6414-4b2f-bbfd-cc83233915ea"), "Ho Chi Minh, Viet Name", "staff@gmail.com", "STAFF", 0, "staff123", "0912377890", 2, "staff" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("5735ae19-bc13-4a72-abf7-d8e32a4bd7c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("83fd2c23-d9c5-4a73-bc13-82ac9a812e3a"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("ad016e07-6414-4b2f-bbfd-cc83233915ea"));
        }
    }
}
