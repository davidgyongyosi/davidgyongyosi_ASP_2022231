using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace davidgyongyosi_ASP_2022231.Data.Migrations
{
    public partial class profile_picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureContentType",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PictureData",
                table: "AspNetUsers",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureContentType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureData",
                table: "AspNetUsers");
        }
    }
}
