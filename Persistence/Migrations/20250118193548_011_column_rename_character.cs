using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _011_column_rename_character : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_actor_person_id",
                schema: "application_schema",
                table: "character");

            migrationBuilder.DropTable(
                name: "ActorEntityFilmEntity",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "ActorEntityShowEntity",
                schema: "application_schema");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "application_schema",
                table: "character",
                newName: "actor_id");

            migrationBuilder.RenameIndex(
                name: "IX_character_person_id",
                schema: "application_schema",
                table: "character",
                newName: "IX_character_actor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_character_actor_actor_id",
                schema: "application_schema",
                table: "character",
                column: "actor_id",
                principalSchema: "application_schema",
                principalTable: "actor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_actor_actor_id",
                schema: "application_schema",
                table: "character");

            migrationBuilder.RenameColumn(
                name: "actor_id",
                schema: "application_schema",
                table: "character",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_character_actor_id",
                schema: "application_schema",
                table: "character",
                newName: "IX_character_person_id");

            migrationBuilder.CreateTable(
                name: "ActorEntityFilmEntity",
                schema: "application_schema",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEntityFilmEntity", x => new { x.ActorsId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_ActorEntityFilmEntity_actor_ActorsId",
                        column: x => x.ActorsId,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorEntityFilmEntity_film_FilmsId",
                        column: x => x.FilmsId,
                        principalSchema: "application_schema",
                        principalTable: "film",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorEntityShowEntity",
                schema: "application_schema",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShowsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEntityShowEntity", x => new { x.ActorsId, x.ShowsId });
                    table.ForeignKey(
                        name: "FK_ActorEntityShowEntity_actor_ActorsId",
                        column: x => x.ActorsId,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorEntityShowEntity_show_ShowsId",
                        column: x => x.ShowsId,
                        principalSchema: "application_schema",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorEntityFilmEntity_FilmsId",
                schema: "application_schema",
                table: "ActorEntityFilmEntity",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorEntityShowEntity_ShowsId",
                schema: "application_schema",
                table: "ActorEntityShowEntity",
                column: "ShowsId");

            migrationBuilder.AddForeignKey(
                name: "FK_character_actor_person_id",
                schema: "application_schema",
                table: "character",
                column: "person_id",
                principalSchema: "application_schema",
                principalTable: "actor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
