using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropIndex(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "user");

            migrationBuilder.RenameTable(
                name: "message",
                newName: "message");

            migrationBuilder.CreateTable(
                name: "usergroup",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int(11)", nullable: false),
                    GroupID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "UserGroupGroup",
                        column: x => x.GroupID,
                        principalTable: "group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserGroupUser",
                        column: x => x.UserID,
                        principalTable: "user",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "UserGroupGroup",
                table: "usergroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "UserGroupUser",
                table: "usergroup",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usergroup");

            migrationBuilder.RenameTable(
                name: "message",
                newName: "message");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "user",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

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
