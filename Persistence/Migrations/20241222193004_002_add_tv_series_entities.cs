using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _002_add_tv_series_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "show",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "season",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    show_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_season", x => x.id);
                    table.ForeignKey(
                        name: "FK_season_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "application_schema",
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "episode",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    release_data = table.Column<DateOnly>(type: "date", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    director_id = table.Column<Guid>(type: "uuid", nullable: false),
                    season_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_episode", x => x.id);
                    table.ForeignKey(
                        name: "FK_episode_director_director_id",
                        column: x => x.director_id,
                        principalSchema: "application_schema",
                        principalTable: "director",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_episode_season_season_id",
                        column: x => x.season_id,
                        principalSchema: "application_schema",
                        principalTable: "season",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_episode_director_id",
                schema: "application_schema",
                table: "episode",
                column: "director_id");

            migrationBuilder.CreateIndex(
                name: "IX_episode_season_id",
                schema: "application_schema",
                table: "episode",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "IX_episode_actor_actor_id",
                schema: "application_schema",
                table: "episode_actor",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_season_show_id",
                schema: "application_schema",
                table: "season",
                column: "show_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "episode_actor",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "episode",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "season",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "show",
                schema: "application_schema");
        }
    }
}
