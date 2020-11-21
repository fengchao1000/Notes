using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKeyCrawler.Migrations
{
    public partial class Init20201121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Html",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductHTML",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    Html = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHTML", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductHTML_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHTML");

            migrationBuilder.AddColumn<string>(
                name: "Html",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
