using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qna.DAL.Migrations
{
    public partial class UpdateKeyOfVoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_UserId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                columns: new[] { "UserId", "AnswerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_UserId",
                table: "Vote",
                column: "UserId");
        }
    }
}
