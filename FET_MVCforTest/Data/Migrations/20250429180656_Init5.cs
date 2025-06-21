using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FET_MVCforTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Constraints");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Day",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Constraints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
