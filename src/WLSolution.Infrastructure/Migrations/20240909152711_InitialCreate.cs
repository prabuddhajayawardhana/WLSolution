using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WLSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("0d3df80f-bd4e-4111-8be5-ee7884392b00"), "Electronics", "Product B", 199.99m, 30 },
                    { new Guid("69c911b6-a393-44c6-83a0-da3f99608d87"), "Clothing", "Product C", 49.99m, 100 },
                    { new Guid("ba3f1b53-3107-47bd-9ce6-10d71653fba0"), "Furniture", "Product E", 399.99m, 20 },
                    { new Guid("ee43749a-c0f4-4df2-8658-e7c6b47f01ed"), "Clothing", "Product D", 79.99m, 80 },
                    { new Guid("f03dae1e-2f1c-4eb9-bc23-86724df04e0c"), "Electronics", "Product A", 299.99m, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
