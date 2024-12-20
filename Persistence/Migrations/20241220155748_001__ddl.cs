using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _001__ddl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "application_schema");

            migrationBuilder.CreateTable(
                name: "person",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    middle_name = table.Column<string>(type: "varchar(100)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    bio = table.Column<string>(type: "varchar(1000)", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    country = table.Column<string>(type: "char(2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actor",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actor", x => x.id);
                    table.ForeignKey(
                        name: "FK_actor_person_person_id",
                        column: x => x.person_id,
                        principalSchema: "application_schema",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "director",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_director", x => x.id);
                    table.ForeignKey(
                        name: "FK_director_person_person_id",
                        column: x => x.person_id,
                        principalSchema: "application_schema",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "film",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    release_data = table.Column<DateOnly>(type: "date", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    director_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film", x => x.id);
                    table.ForeignKey(
                        name: "FK_film_director_director_id",
                        column: x => x.director_id,
                        principalSchema: "application_schema",
                        principalTable: "director",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_actor_person_id",
                schema: "application_schema",
                table: "actor",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_director_person_id",
                schema: "application_schema",
                table: "director",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_film_director_id",
                schema: "application_schema",
                table: "film",
                column: "director_id");

            migrationBuilder.CreateIndex(
                name: "IX_film_actor_actor_id",
                schema: "application_schema",
                table: "film_actor",
                column: "actor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_actor",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "actor",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "film",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "director",
                schema: "application_schema");

            migrationBuilder.DropTable(
                name: "person",
                schema: "application_schema");
        }
    }
}
