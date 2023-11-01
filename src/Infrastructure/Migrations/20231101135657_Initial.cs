using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("15813940-b20f-48c4-af36-c3375d69339d"), "Category 2 Description", "Category Name 2" },
                    { new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), "Category 1 Description", "Category Name 1" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("25e7bfec-7c34-4081-bbd7-e7c90e13bd27"), new Guid("15813940-b20f-48c4-af36-c3375d69339d"), "Product 3 Description", "Product 3 Name", 30.0 },
                    { new Guid("5dc9ba5e-53c0-4166-87de-5f6f57021256"), new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), "Product 2 Description", "Product 2 Name", 20.0 },
                    { new Guid("8ff2a6b2-7f3d-4b5f-8069-821f8765a3dd"), new Guid("15813940-b20f-48c4-af36-c3375d69339d"), "Product 1 Description", "Product 1 Name", 10.0 },
                    { new Guid("d57b60ef-449f-4b86-bfa7-9102bf93dff6"), new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"), "Product 4 Description", "Product 4 Name", 40.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
