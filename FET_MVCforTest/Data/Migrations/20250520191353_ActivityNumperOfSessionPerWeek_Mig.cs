using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FET_MVCforTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActivityNumperOfSessionPerWeek_Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinDaysBetweenActivities",
                table: "Activities",
                newName: "NumOfSessionsPerWeek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfSessionsPerWeek",
                table: "Activities",
                newName: "MinDaysBetweenActivities");
        }
    }
}
