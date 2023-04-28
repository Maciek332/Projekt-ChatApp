using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ChatApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "user",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "message",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "groupmessage",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GroupID);
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropIndex(
                name: "GroupMemeberID",
                table: "user");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "user");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "message",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "groupmessage",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
