using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "BMCategorys",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "BMCategorys");
        }
    }
}
