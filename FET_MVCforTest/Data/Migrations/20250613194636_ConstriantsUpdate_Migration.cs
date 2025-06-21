using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FET_MVCforTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConstriantsUpdate_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constraints_Teachers_TeacherId",
                table: "Constraints");

            migrationBuilder.DropIndex(
                name: "IX_Constraints_TeacherId",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "MaxDailyHours",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "MaxWeeklyHours",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Constraints");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Constraints",
                newName: "ConstraintsDetails");

            migrationBuilder.AddColumn<string>(
                name: "ConstraintName",
                table: "Constraints",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConstraintName",
                table: "Constraints");

            migrationBuilder.RenameColumn(
                name: "ConstraintsDetails",
                table: "Constraints",
                newName: "Type");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Constraints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Constraints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Constraints",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDailyHours",
                table: "Constraints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxWeeklyHours",
                table: "Constraints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Constraints",
                type: "time",
                nullable: true);

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
    }
}
