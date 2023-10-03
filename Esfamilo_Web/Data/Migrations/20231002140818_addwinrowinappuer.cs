using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Web.Data.Migrations
{
    public partial class addwinrowinappuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Win",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Win",
                table: "AspNetUsers");
        }
    }
}
