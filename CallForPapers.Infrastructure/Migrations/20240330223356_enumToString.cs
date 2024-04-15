using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class enumToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 1, 33, 56, 227, DateTimeKind.Local).AddTicks(3130),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 1, 28, 15, 394, DateTimeKind.Local).AddTicks(120));

            migrationBuilder.AlterColumn<string>(
                name: "activity",
                table: "applications",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 1, 28, 15, 394, DateTimeKind.Local).AddTicks(120),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 1, 33, 56, 227, DateTimeKind.Local).AddTicks(3130));

            migrationBuilder.AlterColumn<int>(
                name: "activity",
                table: "applications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
