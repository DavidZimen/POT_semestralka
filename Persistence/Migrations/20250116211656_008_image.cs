using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _008_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                schema: "application_schema",
                table: "show",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                schema: "application_schema",
                table: "person",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                schema: "application_schema",
                table: "film",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                schema: "application_schema",
                table: "episode",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "image",
                schema: "application_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    type = table.Column<string>(type: "varchar(20)", nullable: false),
                    data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_show_image_id",
                schema: "application_schema",
                table: "show",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_image_id",
                schema: "application_schema",
                table: "person",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_film_image_id",
                schema: "application_schema",
                table: "film",
                column: "image_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_episode_image_id",
                schema: "application_schema",
                table: "episode",
                column: "image_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_episode_image_image_id",
                schema: "application_schema",
                table: "episode",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_film_image_image_id",
                schema: "application_schema",
                table: "film",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_person_image_image_id",
                schema: "application_schema",
                table: "person",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_show_image_image_id",
                schema: "application_schema",
                table: "show",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episode_image_image_id",
                schema: "application_schema",
                table: "episode");

            migrationBuilder.DropForeignKey(
                name: "FK_film_image_image_id",
                schema: "application_schema",
                table: "film");

            migrationBuilder.DropForeignKey(
                name: "FK_person_image_image_id",
                schema: "application_schema",
                table: "person");

            migrationBuilder.DropForeignKey(
                name: "FK_show_image_image_id",
                schema: "application_schema",
                table: "show");

            migrationBuilder.DropTable(
                name: "image",
                schema: "application_schema");

            migrationBuilder.DropIndex(
                name: "IX_show_image_id",
                schema: "application_schema",
                table: "show");

            migrationBuilder.DropIndex(
                name: "IX_person_image_id",
                schema: "application_schema",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_film_image_id",
                schema: "application_schema",
                table: "film");

            migrationBuilder.DropIndex(
                name: "IX_episode_image_id",
                schema: "application_schema",
                table: "episode");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "application_schema",
                table: "show");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "application_schema",
                table: "person");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "application_schema",
                table: "film");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "application_schema",
                table: "episode");
        }
    }
}
