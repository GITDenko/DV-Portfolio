using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DVPortfolio.Migrations
{
    public partial class UpdatedWebsites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailURL",
                table: "Website");

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Website",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Website",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Photo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 9, 17, 10, 46, 825, DateTimeKind.Local).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 9, 17, 10, 46, 826, DateTimeKind.Local).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Website",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageURL" },
                values: new object[] { new DateTime(2022, 3, 9, 17, 10, 46, 826, DateTimeKind.Local).AddTicks(9382), "google.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Website");

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailURL",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductUrl",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                columns: new[] { "CreatedOn", "ThumbnailURL" },
                values: new object[] { new DateTime(2022, 2, 18, 18, 4, 13, 327, DateTimeKind.Local).AddTicks(9870), "google.jpg" });
        }
    }
}
