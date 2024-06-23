using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V8_06192024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRequests_Accounts_CreatedBy",
                table: "PostRequests");

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("36c7691f-b087-4aba-a111-ffeaba8be973"));

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("723db346-097e-446a-8293-6fdc7c97bbdb"));

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("805a13e4-8058-4d90-9d14-7a50b75cada2"));

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accounts",
            //    columns: new[] { "AccountId", "Address", "AvatarUrl", "Email", "Fullname", "Gender", "Password", "Phone", "RoleId", "Username" },
            //    values: new object[,]
            //    {
            //        { new Guid("200ed418-8249-4f80-97a7-930bba1b046f"), "Ho Chi Minh, Viet Name", null, "staff@gmail.com", "STAFF", 0, "staff123", "0912377890", 2, "staff" },
            //        { new Guid("975e166f-1d2b-45f1-98e1-95f511269481"), "Ho Chi Minh, Viet Name", null, "admin@gmail.com", "ADMIN", 1, "admin123", "0945677876", 1, "admin" },
            //        { new Guid("a15eda6d-b90b-45b8-8778-10cdc3719013"), "Ho Chi Minh, Viet Name", null, "vana@gmail.com", "Tran Van A", 0, "@123", "0978988768", 4, "parent1" }
            //    });

            migrationBuilder.AddForeignKey(
                name: "FK_PostRequests_Accounts_CreatedBy",
                table: "PostRequests",
                column: "CreatedBy",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRequests_Accounts_CreatedBy",
                table: "PostRequests");

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("200ed418-8249-4f80-97a7-930bba1b046f"));

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("975e166f-1d2b-45f1-98e1-95f511269481"));

            //migrationBuilder.DeleteData(
            //    table: "Accounts",
            //    keyColumn: "AccountId",
            //    keyValue: new Guid("a15eda6d-b90b-45b8-8778-10cdc3719013"));

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Accounts");

            //migrationBuilder.InsertData(
            //    table: "Accounts",
            //    columns: new[] { "AccountId", "Address", "Email", "Fullname", "Gender", "Password", "Phone", "RoleId", "Username" },
            //    values: new object[,]
            //    {
            //        { new Guid("36c7691f-b087-4aba-a111-ffeaba8be973"), "Ho Chi Minh, Viet Name", "vana@gmail.com", "Tran Van A", 0, "@123", "0978988768", 4, "parent1" },
            //        { new Guid("723db346-097e-446a-8293-6fdc7c97bbdb"), "Ho Chi Minh, Viet Name", "admin@gmail.com", "ADMIN", 1, "admin123", "0945677876", 1, "admin" },
            //        { new Guid("805a13e4-8058-4d90-9d14-7a50b75cada2"), "Ho Chi Minh, Viet Name", "staff@gmail.com", "STAFF", 0, "staff123", "0912377890", 2, "staff" }
            //    });

            migrationBuilder.AddForeignKey(
                name: "FK_PostRequests_Accounts_CreatedBy",
                table: "PostRequests",
                column: "CreatedBy",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
