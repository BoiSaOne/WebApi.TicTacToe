using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.TicTacToe.Migrations
{
    public partial class addnewmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayersMakeMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersMakeMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayersMakeMoves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayersMakeMoves_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultGamePlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    ResultGame = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultGamePlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultGamePlayers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultGamePlayers_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersMakeMoves_GameId",
                table: "PlayersMakeMoves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersMakeMoves_PlayerId",
                table: "PlayersMakeMoves",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultGamePlayers_GameId",
                table: "ResultGamePlayers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultGamePlayers_PlayerId",
                table: "ResultGamePlayers",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "PlayersMakeMoves");

            migrationBuilder.DropTable(
                name: "ResultGamePlayers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
