using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_V1_06012024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProficiencyId",
                table: "Qualifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Qualifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proficiencies",
                columns: table => new
                {
                    ProficiencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProficiencyCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proficiencies", x => x.ProficiencyId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.InsertData(
                table: "Proficiencies",
                columns: new[] { "ProficiencyId", "Description", "ProficiencyCode" },
                values: new object[,]
                {
                    { 1, "Basic level of proficiency", "A1" },
                    { 2, "Elementary level of proficiency", "A2" },
                    { 3, "Intermediate level of proficiency", "B1" },
                    { 4, "Upper Intermediate level of proficiency", "B2" },
                    { 5, "Advanced level of proficiency", "C1" },
                    { 6, "Proficient/native-like level of proficiency", "C2" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "SkillName" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Japanese" },
                    { 3, "Korean" },
                    { 4, "Chinese" },
                    { 5, "Spanish" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_ProficiencyId",
                table: "Qualifications",
                column: "ProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_SkillId",
                table: "Qualifications",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Proficiencies_ProficiencyId",
                table: "Qualifications",
                column: "ProficiencyId",
                principalTable: "Proficiencies",
                principalColumn: "ProficiencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Skills_SkillId",
                table: "Qualifications",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Proficiencies_ProficiencyId",
                table: "Qualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Skills_SkillId",
                table: "Qualifications");

            migrationBuilder.DropTable(
                name: "Proficiencies");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_ProficiencyId",
                table: "Qualifications");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_SkillId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "ProficiencyId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Qualifications");
        }
    }
}
