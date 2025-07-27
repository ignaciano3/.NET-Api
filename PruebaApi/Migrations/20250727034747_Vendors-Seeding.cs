using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaApi.Migrations
{
    /// <inheritdoc />
    public partial class VendorsSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VendorProducts",
                columns: new[] { "ProductsId", "VendorsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VendorProducts",
                keyColumns: new[] { "ProductsId", "VendorsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "VendorProducts",
                keyColumns: new[] { "ProductsId", "VendorsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "VendorProducts",
                keyColumns: new[] { "ProductsId", "VendorsId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
