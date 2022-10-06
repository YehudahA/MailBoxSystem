using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailBoxSystem.Migrations
{
    public partial class ChangeTypeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempToken",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "LetterBoxStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliverTime",
                value: new DateTime(2022, 10, 5, 20, 9, 9, 516, DateTimeKind.Local).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DeliverTime" },
                values: new object[] { new DateTime(2022, 10, 6, 20, 9, 9, 516, DateTimeKind.Local).AddTicks(8008), new DateTime(2022, 10, 6, 19, 9, 9, 516, DateTimeKind.Local).AddTicks(8014) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "TempToken",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempToken",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LetterBoxStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliverTime",
                value: new DateTime(2022, 10, 2, 23, 5, 59, 890, DateTimeKind.Local).AddTicks(5419));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DeliverTime" },
                values: new object[] { new DateTime(2022, 10, 3, 23, 5, 59, 890, DateTimeKind.Local).AddTicks(5471), new DateTime(2022, 10, 3, 22, 5, 59, 890, DateTimeKind.Local).AddTicks(5476) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "TempToken",
                value: 0);
        }
    }
}
