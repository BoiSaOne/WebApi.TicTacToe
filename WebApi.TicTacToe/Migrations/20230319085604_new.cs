using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.TicTacToe.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomPlayers_Rooms_RoomId",
                table: "RoomPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "HostId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomPlayers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "RoomPlayers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms",
                column: "HostId",
                unique: true,
                filter: "[HostId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_PlayerId",
                table: "RoomPlayers",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPlayers_Rooms_RoomId",
                table: "RoomPlayers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPlayers_Users_PlayerId",
                table: "RoomPlayers",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomPlayers_Rooms_RoomId",
                table: "RoomPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPlayers_Users_PlayerId",
                table: "RoomPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomPlayers_PlayerId",
                table: "RoomPlayers");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "RoomPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "HostId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms",
                column: "HostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPlayers_Rooms_RoomId",
                table: "RoomPlayers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_HostId",
                table: "Rooms",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
