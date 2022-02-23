using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DVPortfolio.Migrations
{
    public partial class UpdateDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_MainCategories_MainCategoryId",
                table: "Subcategories");

            migrationBuilder.UpdateData(
                table: "Photo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 18, 18, 4, 13, 326, DateTimeKind.Local).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 18, 18, 4, 13, 327, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Website",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 2, 18, 18, 4, 13, 327, DateTimeKind.Local).AddTicks(9870));

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_MainCategories_MainCategoryId",
                table: "Subcategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_MainCategories_MainCategoryId",
                table: "Subcategories");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_MainCategories_MainCategoryId",
                table: "Subcategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
