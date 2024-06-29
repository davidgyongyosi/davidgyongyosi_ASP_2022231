using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace davidgyongyosi_ASP_2022231.Data.Migrations
{
    public partial class game_images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Games",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Games");
        }
    }
}
