using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "BMTags",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "BMCategorys",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "BMBookmarkTags",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "BMBookmarks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "BMBookmarkCategorys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "BMTags");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "BMCategorys");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "BMBookmarkTags");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "BMBookmarks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "BMBookmarkCategorys");
        }
    }
}
