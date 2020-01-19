using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FC.Notes.Migrations
{
    public partial class mysql_init202001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITItinerarys",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Note = table.Column<string>(maxLength: 512, nullable: false),
                    TripTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AccountBookID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITItinerarys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ITAccountBooks",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ItineraryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITAccountBooks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ITAccountBooks_ITItinerarys_ItineraryID",
                        column: x => x.ItineraryID,
                        principalTable: "ITItinerarys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITOverheadItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(maxLength: 512, nullable: false),
                    Money = table.Column<decimal>(nullable: false),
                    AccountBookID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITOverheadItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ITOverheadItems_ITAccountBooks_AccountBookID",
                        column: x => x.AccountBookID,
                        principalTable: "ITAccountBooks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITAccountBooks_ItineraryID",
                table: "ITAccountBooks",
                column: "ItineraryID");

            migrationBuilder.CreateIndex(
                name: "IX_ITItinerarys_AccountBookID",
                table: "ITItinerarys",
                column: "AccountBookID");

            migrationBuilder.CreateIndex(
                name: "IX_ITOverheadItems_AccountBookID",
                table: "ITOverheadItems",
                column: "AccountBookID");

            migrationBuilder.AddForeignKey(
                name: "FK_ITItinerarys_ITAccountBooks_AccountBookID",
                table: "ITItinerarys",
                column: "AccountBookID",
                principalTable: "ITAccountBooks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITAccountBooks_ITItinerarys_ItineraryID",
                table: "ITAccountBooks");

            migrationBuilder.DropTable(
                name: "ITOverheadItems");

            migrationBuilder.DropTable(
                name: "ITItinerarys");

            migrationBuilder.DropTable(
                name: "ITAccountBooks");
        }
    }
}
