using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _007_characters_from_episode_to_show : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_episode_episode_id",
                schema: "application_schema",
                table: "character");

            migrationBuilder.DropTable(
                name: "episode_actor",
                schema: "application_schema");

            migrationBuilder.RenameColumn(
                name: "episode_id",
                schema: "application_schema",
                table: "character",
                newName: "show_id");

            migrationBuilder.RenameIndex(
                name: "IX_character_episode_id",
                schema: "application_schema",
                table: "character",
                newName: "IX_character_show_id");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "application_schema",
                table: "character",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "show_actor",
                schema: "application_schema",
                columns: table => new
                {
                    show_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show_actor", x => new { x.show_id, x.actor_id });
                    table.ForeignKey(
                        name: "FK_show_actor_actor_actor_id",
                        column: x => x.actor_id,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_show_actor_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "application_schema",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Character_FilmOrShow",
                schema: "application_schema",
                table: "character",
                sql: "(film_id IS NOT NULL AND show_id IS NULL) OR (film_id IS NULL AND show_id IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_show_actor_actor_id",
                schema: "application_schema",
                table: "show_actor",
                column: "actor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_character_show_show_id",
                schema: "application_schema",
                table: "character",
                column: "show_id",
                principalSchema: "application_schema",
                principalTable: "show",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_show_show_id",
                schema: "application_schema",
                table: "character");

            migrationBuilder.DropTable(
                name: "show_actor",
                schema: "application_schema");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Character_FilmOrShow",
                schema: "application_schema",
                table: "character");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "application_schema",
                table: "character");

            migrationBuilder.RenameColumn(
                name: "show_id",
                schema: "application_schema",
                table: "character",
                newName: "episode_id");

            migrationBuilder.RenameIndex(
                name: "IX_character_show_id",
                schema: "application_schema",
                table: "character",
                newName: "IX_character_episode_id");

            migrationBuilder.CreateTable(
                name: "episode_actor",
                schema: "application_schema",
                columns: table => new
                {
                    episode_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_episode_actor", x => new { x.episode_id, x.actor_id });
                    table.ForeignKey(
                        name: "FK_episode_actor_actor_actor_id",
                        column: x => x.actor_id,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_episode_actor_episode_episode_id",
                        column: x => x.episode_id,
                        principalSchema: "application_schema",
                        principalTable: "episode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_episode_actor_actor_id",
                schema: "application_schema",
                table: "episode_actor",
                column: "actor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_character_episode_episode_id",
                schema: "application_schema",
                table: "character",
                column: "episode_id",
                principalSchema: "application_schema",
                principalTable: "episode",
                principalColumn: "id");
        }
    }
}
