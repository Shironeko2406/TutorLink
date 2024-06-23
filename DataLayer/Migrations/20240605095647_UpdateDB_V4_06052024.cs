using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V4_06052024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ParentFeedbacks");

            //migrationBuilder.DropTable(
            //    name: "Appointments");

            migrationBuilder.CreateTable(
                name: "AppointmentFeedbacks",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentFeedbacks", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_AppointmentFeedbacks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_AppointmentFeedbacks_PostRequests_PostId",
                        column: x => x.PostId,
                        principalTable: "PostRequests",
                        principalColumn: "PostId");
                    table.ForeignKey(
                        name: "FK_AppointmentFeedbacks_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "TutorId");
                });


            migrationBuilder.CreateIndex(
                name: "IX_AppointmentFeedbacks_AccountId",
                table: "AppointmentFeedbacks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentFeedbacks_PostId",
                table: "AppointmentFeedbacks",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentFeedbacks_TutorId",
                table: "AppointmentFeedbacks",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentFeedbacks");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AppTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_PostRequests_PostId",
                        column: x => x.PostId,
                        principalTable: "PostRequests",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentFeedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsSucessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentFeedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_ParentFeedbacks_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PostId",
                table: "Appointments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentFeedbacks_AppointmentId",
                table: "ParentFeedbacks",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PostId",
                table: "Appointments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentFeedbacks_AppointmentId",
                table: "ParentFeedbacks",
                column: "AppointmentId");
        }
    }
}
