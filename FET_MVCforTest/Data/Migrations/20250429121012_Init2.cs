using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FET_MVCforTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Constraints");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Constraints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DetailsJson",
                table: "Constraints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Constraints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Constraints_TeacherId",
                table: "Constraints",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constraints_Teachers_TeacherId",
                table: "Constraints",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constraints_Teachers_TeacherId",
                table: "Constraints");

            migrationBuilder.DropIndex(
                name: "IX_Constraints_TeacherId",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "DetailsJson",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Constraints");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Constraints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Constraints",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
