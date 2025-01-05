using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _004_add_genres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "modified_at",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "modified_by",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "application_schema",
                table: "rating",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(50", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "film_genre",
                schema: "application_schema",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    film_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_genre", x => new { x.genre_id, x.film_id });
                    table.ForeignKey(
                        name: "FK_film_genre_film_film_id",
                        column: x => x.film_id,
                        principalSchema: "application_schema",
                        principalTable: "film",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_film_genre_genre_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "application_schema",
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "show_genre",
                schema: "application_schema",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    show_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show_genre", x => new { x.genre_id, x.show_id });
                    table.ForeignKey(
                        name: "FK_show_genre_genre_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "application_schema",
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_show_genre_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "application_schema",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_film_genre_film_id",
                schema: "application_schema",
                table: "film_genre",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Genre_Name",
                schema: "application_schema",
                table: "genre",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_show_genre_show_id",
                schema: "application_schema",
                table: "show_genre",
                column: "show_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_genre",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "show_genre",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "application_schema");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "application_schema",
                table: "rating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                schema: "application_schema",
                table: "rating",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                schema: "application_schema",
                table: "rating",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                schema: "application_schema",
                table: "rating",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
