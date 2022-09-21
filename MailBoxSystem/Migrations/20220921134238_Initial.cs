using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailBoxSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LetterBoxStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    DeliverTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PullTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterBoxStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterBoxStatuses_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecieverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecieverPhone = table.Column<int>(type: "int", nullable: false),
                    BoxId = table.Column<int>(type: "int", nullable: true),
                    DeliverTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PullTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Boxes",
                columns: new[] { "Id", "Discriminator", "LocalId", "Size" },
                values: new object[] { 2, "P", "01", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EMail", "Name", "Password", "PhoneNumber", "TempToken" },
                values: new object[] { 1, "ye.altman@gmail.com", "Yehudah", "1234", 535481815, null });

            migrationBuilder.InsertData(
                table: "Boxes",
                columns: new[] { "Id", "Discriminator", "Line1", "Line2", "LocalId", "OwnerId" },
                values: new object[] { 1, "L", "אלטמן", "קומה 1", "01", 1 });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "BoxId", "Code", "CreatedDate", "DeliverTime", "PullTime", "RecieverName", "RecieverPhone", "SenderName" },
                values: new object[] { 1, 2, "R1234", new DateTime(2022, 9, 21, 16, 42, 38, 129, DateTimeKind.Local).AddTicks(2406), new DateTime(2022, 9, 21, 15, 42, 38, 129, DateTimeKind.Local).AddTicks(2412), null, "יהודה", 535481815, "AliExpress" });

            migrationBuilder.InsertData(
                table: "LetterBoxStatuses",
                columns: new[] { "Id", "BoxId", "DeliverTime", "PullTime" },
                values: new object[] { 1, 1, new DateTime(2022, 9, 20, 16, 42, 38, 129, DateTimeKind.Local).AddTicks(2313), null });

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_OwnerId",
                table: "Boxes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterBoxStatuses_BoxId",
                table: "LetterBoxStatuses",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_BoxId",
                table: "Packages",
                column: "BoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterBoxStatuses");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
