using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qna.DAL.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { "1a0bd150-7dbc-4e10-8143-120823519c4c", "Aya", "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ==" },
                    { "249a987b-6a40-4c77-9ecb-84de821ea9df", "Ahmed", "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ==" },
                    { "5e227bad-f701-4aa4-899a-f7ff5381ca39", "Mustafa", "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ==" },
                    { "60b0c149-8d64-489a-89dc-81c3d36266e4", "Maha", "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ==" },
                    { "81530d90-ff5c-485e-98b4-b6a215a8f86c", "Islam", "AFdDaV7xPSc76LKz37gq7akbcG5TK6hnX//AedyawbPCObpCTCIXEmlX9YXIMn1OMQ==" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: "1a0bd150-7dbc-4e10-8143-120823519c4c");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: "249a987b-6a40-4c77-9ecb-84de821ea9df");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: "5e227bad-f701-4aa4-899a-f7ff5381ca39");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: "60b0c149-8d64-489a-89dc-81c3d36266e4");

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: "81530d90-ff5c-485e-98b4-b6a215a8f86c");
        }
    }
}
