using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalTrials.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToTrialId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrials_TrialId",
                table: "ClinicalTrials",
                column: "TrialId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClinicalTrials_TrialId",
                table: "ClinicalTrials");
        }
    }
}
