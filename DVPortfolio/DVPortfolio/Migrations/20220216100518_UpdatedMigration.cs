using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DVPortfolio.Migrations
{
    public partial class UpdatedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Photo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 11, 5, 18, 348, DateTimeKind.Local).AddTicks(9066));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 11, 5, 18, 349, DateTimeKind.Local).AddTicks(1227));

            migrationBuilder.UpdateData(
                table: "Website",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 11, 5, 18, 349, DateTimeKind.Local).AddTicks(3457));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Photo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 10, 33, 31, 129, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 10, 33, 31, 130, DateTimeKind.Local).AddTicks(374));

            migrationBuilder.UpdateData(
                table: "Website",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 16, 10, 33, 31, 130, DateTimeKind.Local).AddTicks(2557));
        }
    }
}
