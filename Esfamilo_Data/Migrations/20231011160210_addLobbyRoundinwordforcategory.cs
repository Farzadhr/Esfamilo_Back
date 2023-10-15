using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    public partial class addLobbyRoundinwordforcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LobbyRound",
                table: "WordForCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LobbyRound",
                table: "WordForCategories");
        }
    }
}
