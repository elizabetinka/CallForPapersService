using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeActivityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "activity",
                table: "applications",
                newName: "activity_activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 13, 56, 47, 872, DateTimeKind.Local).AddTicks(6160),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 3, 0, 15, 740, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.AlterColumn<string>(
                name: "activity_activity",
                table: "applications",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "Id", "login", "password" },
                values: new object[] { new Guid("e0fcb604-2da6-415f-b5fa-6ac149e0ef69"), "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "Id",
                keyValue: new Guid("e0fcb604-2da6-415f-b5fa-6ac149e0ef69"));

            migrationBuilder.RenameColumn(
                name: "activity_activity",
                table: "applications",
                newName: "activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 3, 0, 15, 740, DateTimeKind.Local).AddTicks(1760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 13, 56, 47, 872, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.AlterColumn<string>(
                name: "activity",
                table: "applications",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
