using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockCommandChallenge.Core.Migrations
{
    public partial class MessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MessageText = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SentTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
