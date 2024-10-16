using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _001_initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    hotel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
