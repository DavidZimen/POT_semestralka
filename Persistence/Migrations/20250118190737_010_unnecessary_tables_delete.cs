using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _010_unnecessary_tables_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_actor",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "show_actor",
                schema: "application_schema");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorEntityFilmEntity",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "ActorEntityShowEntity",
                schema: "application_schema");

            migrationBuilder.CreateTable(
                name: "film_actor",
                schema: "application_schema",
                columns: table => new
                {
                    film_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_actor", x => new { x.film_id, x.actor_id });
                    table.ForeignKey(
                        name: "FK_film_actor_actor_actor_id",
                        column: x => x.actor_id,
                        principalSchema: "application_schema",
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_film_actor_film_film_id",
                        column: x => x.film_id,
                        principalSchema: "application_schema",
                        principalTable: "film",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_film_actor_actor_id",
                schema: "application_schema",
                table: "film_actor",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_show_actor_actor_id",
                schema: "application_schema",
                table: "show_actor",
                column: "actor_id");
        }
    }
}
