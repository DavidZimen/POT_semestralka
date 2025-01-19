using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _012_table_rename_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_user_user_id",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                schema: "application_schema",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "application_schema",
                newName: "app_user",
                newSchema: "application_schema");

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user",
                schema: "application_schema",
                table: "app_user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_app_user_user_id",
                schema: "application_schema",
                table: "rating",
                column: "user_id",
                principalSchema: "application_schema",
                principalTable: "app_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_app_user_user_id",
                schema: "application_schema",
                table: "rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user",
                schema: "application_schema",
                table: "app_user");

            migrationBuilder.RenameTable(
                name: "app_user",
                schema: "application_schema",
                newName: "user",
                newSchema: "application_schema");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                schema: "application_schema",
                table: "user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_user_user_id",
                schema: "application_schema",
                table: "rating",
                column: "user_id",
                principalSchema: "application_schema",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
