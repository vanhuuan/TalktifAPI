using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class Emailservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat_Room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatRoomName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    numOfMember = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_Room", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    hobbies = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    confirmedEmail = table.Column<bool>(type: "bit", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender = table.Column<int>(type: "int", nullable: false),
                    chatRoomId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    sentAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.id);
                    table.ForeignKey(
                        name: "FK__Message__chatRoo__7F2BE32F",
                        column: x => x.chatRoomId,
                        principalTable: "Chat_Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reporter = table.Column<int>(type: "int", nullable: false),
                    suspect = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.id);
                    table.ForeignKey(
                        name: "FK__Report__reporter__73BA3083",
                        column: x => x.reporter,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_ChatRoom",
                columns: table => new
                {
                    user = table.Column<int>(type: "int", nullable: false),
                    chatRoomId = table.Column<int>(type: "int", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Cha__4372E63A933B1139", x => new { x.user, x.chatRoomId });
                    table.ForeignKey(
                        name: "FK__User_Chat__chatR__7B5B524B",
                        column: x => x.chatRoomId,
                        principalTable: "Chat_Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__User_ChatR__user__7A672E12",
                        column: x => x.user,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_RefreshToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user = table.Column<int>(type: "int", nullable: false),
                    refreshToken = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    device = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_RefreshToken", x => x.id);
                    table.ForeignKey(
                        name: "FK__User_Refre__user__628FA481",
                        column: x => x.user,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_chatRoomId",
                table: "Message",
                column: "chatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reporter",
                table: "Report",
                column: "reporter");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616459FFB8D3",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatRoom_chatRoomId",
                table: "User_ChatRoom",
                column: "chatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RefreshToken_user",
                table: "User_RefreshToken",
                column: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "User_ChatRoom");

            migrationBuilder.DropTable(
                name: "User_RefreshToken");

            migrationBuilder.DropTable(
                name: "Chat_Room");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
