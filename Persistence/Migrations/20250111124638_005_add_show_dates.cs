using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _005_add_show_dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "release_data",
                schema: "application_schema",
                table: "film",
                newName: "release_date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "end_date",
                schema: "application_schema",
                table: "show",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "release_date",
                schema: "application_schema",
                table: "show",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                schema: "application_schema",
                table: "show");

            migrationBuilder.DropColumn(
                name: "release_date",
                schema: "application_schema",
                table: "show");

            migrationBuilder.RenameColumn(
                name: "release_date",
                schema: "application_schema",
                table: "film",
                newName: "release_data");
        }
    }
}
