using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailBoxSystem.Migrations
{
    public partial class sender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Packages");

            migrationBuilder.CreateTable(
                name: "PackageSenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageSenders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PackageSenders",
                columns: new[] { "Id", "IconName", "Name" },
                values: new object[] { 1, "aliexpress.jpg", "Ali Express" });

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenderId",
                value: 1);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Packages",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_SenderId",
                table: "Packages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageSenders_SenderId",
                table: "Packages",
                column: "SenderId",
                principalTable: "PackageSenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageSenders_SenderId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "PackageSenders");

            migrationBuilder.DropIndex(
                name: "IX_Packages_SenderId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LetterBoxStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliverTime",
                value: new DateTime(2022, 9, 20, 16, 42, 38, 129, DateTimeKind.Local).AddTicks(2313));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DeliverTime", "SenderName" },
                values: new object[] { new DateTime(2022, 9, 21, 16, 42, 38, 129, DateTimeKind.Local).AddTicks(2406), new DateTime(2022, 9, 21, 15, 42, 38, 129, DateTimeKind.Local).AddTicks(2412), "AliExpress" });
        }
    }
}
