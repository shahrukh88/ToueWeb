using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToueWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Contenent", "CreatedDate", "Description", "ImageUrl", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Asia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), " Pakistan in Asia ", "https://media.istockphoto.com/id/182820726/photo/pakistan-flag.jpg?s=170667a&w=0&k=20&c=agCnt4sci9t5JN04AzZptdjGhMHE3huGn4RV35no5UM=", "Pakistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Europe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), " Eng in Europe ", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fstock.adobe.com%2Fsearch%2Fimages%3Fk%3Dengland%2Bflag&psig=AOvVaw3BaBgON3lhqW85O4yRVmZl&ust=1720108181970000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCOjv6L2ci4cDFQAAAAAdAAAAABAE", "Eng", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
