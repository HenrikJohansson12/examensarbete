using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ica" },
                    { 2, "Willys" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "BrandId", "City", "InternalStoreId", "Name", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 1, 2, "Borås", "2110", "Willys Knalleland", "Knallelandsvägen 3", "50637" },
                    { 2, 1, "Borås", "1004101", "Ica City Knalleland", "Knallelandsvägen 12", "50637" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
