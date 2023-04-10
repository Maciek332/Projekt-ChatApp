using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ChatApp.Migrations
{
    /// <inheritdoc />
    public partial class AddKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropIndex(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "user");

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    MessageAuthor = table.Column<int>(type: "int(11)", nullable: false),
                    MessageDestination = table.Column<int>(type: "int(11)", nullable: false),
                    MessageContent = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MessageID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    GroupsGroupId = table.Column<int>(type: "int(11)", nullable: false),
                    UsersUserId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.GroupsGroupId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserGroup_group_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_user_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UsersUserId",
                table: "UserGroup",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "user",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MessageAuthor = table.Column<int>(type: "int(11)", nullable: false),
                    MessageContent = table.Column<string>(type: "longtext", nullable: false),
                    MessageDestination = table.Column<int>(type: "int(11)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MessageID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "GroupMemeberID",
                table: "user",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "GroupMemeberID",
                table: "user",
                column: "GroupID",
                principalTable: "group",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
