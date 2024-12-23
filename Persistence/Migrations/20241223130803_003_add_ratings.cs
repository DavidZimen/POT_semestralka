using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _003_add_ratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rating",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    film_id = table.Column<Guid>(type: "uuid", nullable: true),
                    show_id = table.Column<Guid>(type: "uuid", nullable: true),
                    episode_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => x.id);
                    table.CheckConstraint("CHK_One_Type_Not_Null", "\"film_id\" IS NOT NULL OR \"show_id\" IS NOT NULL OR \"episode_id\" IS NOT NULL");
                    table.CheckConstraint("CHK_Rating_value", "\"value\" >= 1 AND \"value\" <= 10");
                    table.ForeignKey(
                        name: "FK_rating_episode_episode_id",
                        column: x => x.episode_id,
                        principalSchema: "application_schema",
                        principalTable: "episode",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_rating_film_film_id",
                        column: x => x.film_id,
                        principalSchema: "application_schema",
                        principalTable: "film",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_rating_show_show_id",
                        column: x => x.show_id,
                        principalSchema: "application_schema",
                        principalTable: "show",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_rating_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "application_schema",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rating_episode_id",
                schema: "application_schema",
                table: "rating",
                column: "episode_id");

            migrationBuilder.CreateIndex(
                name: "IX_rating_film_id",
                schema: "application_schema",
                table: "rating",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "IX_rating_show_id",
                schema: "application_schema",
                table: "rating",
                column: "show_id");

            migrationBuilder.CreateIndex(
                name: "IX_rating_user_id",
                schema: "application_schema",
                table: "rating",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rating",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "user",
                schema: "application_schema");
        }
    }
}
