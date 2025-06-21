using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FET_MVCforTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetailsJson",
                table: "Constraints",
                newName: "Details");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Constraints");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Constraints",
                newName: "DetailsJson");
        }
    }
}
