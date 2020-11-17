using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKeyCrawler.Migrations
{
    public partial class Init20201114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Products",
                type: "varchar(1200)",
                nullable: true);
        }
    }
}
