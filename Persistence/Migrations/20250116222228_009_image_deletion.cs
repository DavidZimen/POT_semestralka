using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _009_image_deletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_episode_image_image_id",
                schema: "application_schema",
                table: "episode",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_film_image_image_id",
                schema: "application_schema",
                table: "film",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_person_image_image_id",
                schema: "application_schema",
                table: "person",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_show_image_image_id",
                schema: "application_schema",
                table: "show",
                column: "image_id",
                principalSchema: "application_schema",
                principalTable: "image",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
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
    }
}
