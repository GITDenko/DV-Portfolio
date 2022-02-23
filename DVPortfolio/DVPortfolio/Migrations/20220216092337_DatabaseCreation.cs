using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DVPortfolio.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photo_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Website_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Website_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "Hidden", "ImageURL", "Name" },
                values: new object[] { 1, false, "photography.png", "Photography" });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "Hidden", "ImageURL", "Name" },
                values: new object[] { 2, false, "videos.png", "Videos" });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "Hidden", "ImageURL", "Name" },
                values: new object[] { 3, false, "websites.png", "Websites" });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "Hidden", "ImageURL", "MainCategoryId", "Name" },
                values: new object[] { 1, false, "berlin.png", 1, "Berlin" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "CreatedOn", "Hidden", "MainCategoryId", "ProductUrl", "SubcategoryId" },
                values: new object[] { 1, new DateTime(2022, 2, 16, 10, 23, 36, 723, DateTimeKind.Local).AddTicks(6688), false, 2, "https://www.youtube.com/watch?v=izGwDsrQ1eQ", null });

            migrationBuilder.InsertData(
                table: "Website",
                columns: new[] { "Id", "CreatedOn", "Hidden", "MainCategoryId", "Name", "ProductUrl", "SubcategoryId", "ThumbnailURL" },
                values: new object[] { 1, new DateTime(2022, 2, 16, 10, 23, 36, 723, DateTimeKind.Local).AddTicks(8985), false, 3, "Google", "https://www.google.com", null, "google.jpg" });

            migrationBuilder.InsertData(
                table: "Photo",
                columns: new[] { "Id", "CreatedOn", "Hidden", "MainCategoryId", "ProductUrl", "SubcategoryId" },
                values: new object[] { 1, new DateTime(2022, 2, 16, 10, 23, 36, 723, DateTimeKind.Local).AddTicks(4460), false, null, "berlin.png", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_MainCategoryId",
                table: "Photo",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_SubcategoryId",
                table: "Photo",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_MainCategoryId",
                table: "Subcategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MainCategoryId",
                table: "Videos",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SubcategoryId",
                table: "Videos",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Website_MainCategoryId",
                table: "Website",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Website_SubcategoryId",
                table: "Website",
                column: "SubcategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Website");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "MainCategories");
        }
    }
}
