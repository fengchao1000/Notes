using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init20200530 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskType",
                table: "TSTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "TSTasks");
        }
    }
}
