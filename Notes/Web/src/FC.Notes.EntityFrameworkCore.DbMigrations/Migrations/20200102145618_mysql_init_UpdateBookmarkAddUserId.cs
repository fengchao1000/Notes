using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init_UpdateBookmarkAddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BMBookmarks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BMBookmarks");
        }
    }
}
