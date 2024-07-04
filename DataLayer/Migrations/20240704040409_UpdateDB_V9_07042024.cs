using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V9_07042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("200ed418-8249-4f80-97a7-930bba1b046f"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("975e166f-1d2b-45f1-98e1-95f511269481"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("a15eda6d-b90b-45b8-8778-10cdc3719013"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUsername",
                table: "PostRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Address", "AvatarUrl", "Email", "Fullname", "Gender", "Password", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("3c03312e-1a59-47ef-99dd-8375d37a4130"), "Ho Chi Minh, Viet Name", null, "admin@gmail.com", "ADMIN", 1, "admin123", "0945677876", 1, "admin" },
                    { new Guid("7c464da0-bb4b-4916-91f8-a32de8f709e1"), "Ho Chi Minh, Viet Name", null, "staff@gmail.com", "STAFF", 0, "staff123", "0912377890", 2, "staff" },
                    { new Guid("930ee83b-c8eb-42a2-82e9-64b4476b9654"), "Ho Chi Minh, Viet Name", null, "vana@gmail.com", "Tran Van A", 0, "@123", "0978988768", 4, "parent1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("3c03312e-1a59-47ef-99dd-8375d37a4130"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("7c464da0-bb4b-4916-91f8-a32de8f709e1"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("930ee83b-c8eb-42a2-82e9-64b4476b9654"));

            migrationBuilder.DropColumn(
                name: "CreatedByUsername",
                table: "PostRequests");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Address", "AvatarUrl", "Email", "Fullname", "Gender", "Password", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("200ed418-8249-4f80-97a7-930bba1b046f"), "Ho Chi Minh, Viet Name", null, "staff@gmail.com", "STAFF", 0, "staff123", "0912377890", 2, "staff" },
                    { new Guid("975e166f-1d2b-45f1-98e1-95f511269481"), "Ho Chi Minh, Viet Name", null, "admin@gmail.com", "ADMIN", 1, "admin123", "0945677876", 1, "admin" },
                    { new Guid("a15eda6d-b90b-45b8-8778-10cdc3719013"), "Ho Chi Minh, Viet Name", null, "vana@gmail.com", "Tran Van A", 0, "@123", "0978988768", 4, "parent1" }
                });
        }
    }
}
