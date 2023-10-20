using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    public partial class addislimitlobbyinlobbytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLimitLobby",
                table: "Lobbies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLimitLobby",
                table: "Lobbies");
        }
    }
}
