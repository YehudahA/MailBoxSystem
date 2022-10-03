using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailBoxSystem.Migrations
{
    public partial class ChangeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalId",
                newName: "LocalNumber",
                table: "Boxes");

            migrationBuilder.AlterColumn<int>(
                name: "LocalNumber",
                table: "Boxes",
                type: "int",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
