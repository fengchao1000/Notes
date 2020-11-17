﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKeyCrawler.Migrations
{
    public partial class Init20201118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }
    }
}
