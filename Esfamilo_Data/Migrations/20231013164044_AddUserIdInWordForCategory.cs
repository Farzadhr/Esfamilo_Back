using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    public partial class AddUserIdInWordForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WordForCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WordForCategories");
        }
    }
}
