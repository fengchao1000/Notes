using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKeyCrawler.Migrations
{
    public partial class Init20201114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAdditionalResourcess",
                columns: table => new
                {
                    ProductAdditionalResourceKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductAdditionalResourceJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionalResourcess", x => x.ProductAdditionalResourceKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    ProductAttributeKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductAttributeJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.ProductAttributeKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategorys",
                columns: table => new
                {
                    ProductCategoryKey = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(100)", nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: false),
                    DetailUrl = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategorys", x => x.ProductCategoryKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocuments",
                columns: table => new
                {
                    ProductDocumentKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductDocumentJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocuments", x => x.ProductDocumentKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductEnvExportClassifications",
                columns: table => new
                {
                    ProductEnvExportClassificationKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductEnvExportClassificationJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEnvExportClassifications", x => x.ProductEnvExportClassificationKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                columns: table => new
                {
                    ProductPictureKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    PictureUrl = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => x.ProductPictureKey);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ProductPriceKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductPriceJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.ProductPriceKey);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductKey = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(200)", nullable: false),
                    ImageTitle = table.Column<string>(type: "varchar(1200)", nullable: true),
                    DefPic = table.Column<string>(type: "varchar(2000)", nullable: false),
                    DefPdf = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    DigiKeyPartNumber = table.Column<string>(type: "varchar(200)", nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    ManufacturerProductNumber = table.Column<string>(type: "varchar(200)", nullable: true),
                    Supplier = table.Column<string>(type: "varchar(200)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DetailedDescription = table.Column<string>(type: "text", nullable: false),
                    ManufacturerStandardLeadTime = table.Column<string>(nullable: true),
                    MayAlso = table.Column<string>(nullable: true),
                    Html = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdditionalResourcess");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductCategorys");

            migrationBuilder.DropTable(
                name: "ProductDocuments");

            migrationBuilder.DropTable(
                name: "ProductEnvExportClassifications");

            migrationBuilder.DropTable(
                name: "ProductPictures");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
