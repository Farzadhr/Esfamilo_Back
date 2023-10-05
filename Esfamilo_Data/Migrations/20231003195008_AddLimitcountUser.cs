using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    public partial class AddLimitcountUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LimitUserCount",
                table: "Lobbies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitUserCount",
                table: "Lobbies");
        }
    }
}
