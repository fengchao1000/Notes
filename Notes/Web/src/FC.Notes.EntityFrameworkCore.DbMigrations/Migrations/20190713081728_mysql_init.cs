using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "BMBookmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    //CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    //LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    //DeletionTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 512, nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    LinkUrl = table.Column<string>(maxLength: 64, nullable: false),
                    LinkSourceType = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 1048576, nullable: true),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCrawl = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMBookmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BMTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BookmarkId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    UsageCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMTags", x => x.Id);
                });

            
            migrationBuilder.CreateTable(
                name: "BMBookmarkTags",
                columns: table => new
                {
                    BookmarkId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMBookmarkTags", x => new { x.BookmarkId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BMBookmarkTags_BMBookmarks_BookmarkId",
                        column: x => x.BookmarkId,
                        principalTable: "BMBookmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BMBookmarkTags_BMTags_TagId",
                        column: x => x.TagId,
                        principalTable: "BMTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

             

            migrationBuilder.CreateIndex(
                name: "IX_BMBookmarkTags_TagId",
                table: "BMBookmarkTags",
                column: "TagId");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 

            migrationBuilder.DropTable(
                name: "BMBookmarkTags");

             

            migrationBuilder.DropTable(
                name: "BMBookmarks");

            migrationBuilder.DropTable(
                name: "BMTags");

           
        }
    }
}
