using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureOrm.Migrations
{
    /// <inheritdoc />
    public partial class changeNotNullToRequed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("e0fcb604-2da6-415f-b5fa-6ac149e0ef69"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 16, 10, 49, 700, DateTimeKind.Local).AddTicks(7880),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 13, 56, 47, 872, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "login", "password" },
                values: new object[] { new Guid("498aeec6-ec93-47ad-a32c-7638b194a0e3"), "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("498aeec6-ec93-47ad-a32c-7638b194a0e3"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 13, 56, 47, 872, DateTimeKind.Local).AddTicks(6160),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 16, 10, 49, 700, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "login", "password" },
                values: new object[] { new Guid("e0fcb604-2da6-415f-b5fa-6ac149e0ef69"), "admin", "admin" });
        }
    }
}
