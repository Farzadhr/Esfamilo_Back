using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Web.Data.Migrations
{
    public partial class addimgprofileinappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "ImgProfileUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgProfileUrl",
                table: "AspNetUsers",
                newName: "Gender");
        }
    }
}
