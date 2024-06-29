using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace davidgyongyosi_ASP_2022231.Data.Migrations
{
    public partial class generated_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GenreUid",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropIndex(
                name: "IX_Games_GenreUid",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_OwnerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GenreUid",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesUid = table.Column<string>(type: "TEXT", nullable: false),
                    PlatformsUid = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesUid, x.PlatformsUid });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Games_GamesUid",
                        column: x => x.GamesUid,
                        principalTable: "Games",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platforms_PlatformsUid",
                        column: x => x.PlatformsUid,
                        principalTable: "Platforms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSiteUser",
                columns: table => new
                {
                    GamesUid = table.Column<string>(type: "TEXT", nullable: false),
                    OwnersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSiteUser", x => new { x.GamesUid, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_GameSiteUser_AspNetUsers_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSiteUser_Games_GamesUid",
                        column: x => x.GamesUid,
                        principalTable: "Games",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsUid",
                table: "GamePlatform",
                column: "PlatformsUid");

            migrationBuilder.CreateIndex(
                name: "IX_GameSiteUser_OwnersId",
                table: "GameSiteUser",
                column: "OwnersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "GameSiteUser");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Games",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "GenreUid",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GameId = table.Column<string>(type: "TEXT", nullable: false),
                    PlatformId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreUid",
                table: "Games",
                column: "GenreUid");

            migrationBuilder.CreateIndex(
                name: "IX_Games_OwnerId",
                table: "Games",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GenreUid",
                table: "Games",
                column: "GenreUid",
                principalTable: "Genres",
                principalColumn: "Uid");
        }
    }
}
