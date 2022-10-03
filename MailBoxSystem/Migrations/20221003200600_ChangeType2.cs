using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailBoxSystem.Migrations
{
    public partial class ChangeType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempToken",
                table: "Users",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
