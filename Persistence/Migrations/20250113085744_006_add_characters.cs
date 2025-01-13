using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _006_add_characters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    film_id = table.Column<Guid>(type: "uuid", nullable: true),
                    episode_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.id);
                    table.ForeignKey(
                        name: "FK_character_actor_person_id",
                        column: x => x.person_id,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_episode_episode_id",
                        column: x => x.episode_id,
                        principalSchema: "application_schema",
                        principalTable: "episode",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_character_film_film_id",
                        column: x => x.film_id,
                        principalSchema: "application_schema",
                        principalTable: "film",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_episode_id",
                schema: "application_schema",
                table: "character",
                column: "episode_id");

            migrationBuilder.CreateIndex(
                name: "IX_character_film_id",
                schema: "application_schema",
                table: "character",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "IX_character_person_id",
                schema: "application_schema",
                table: "character",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character",
                schema: "application_schema");
        }
    }
}
