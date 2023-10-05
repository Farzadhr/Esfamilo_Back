using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esfamilo_Data.Migrations
{
    public partial class AddChatMessageinlobbytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatMessageInLobbies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LobbyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatMessageInLobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chatMessageInLobbies_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chatMessageInLobbies_LobbyId",
                table: "chatMessageInLobbies",
                column: "LobbyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatMessageInLobbies");
        }
    }
}
