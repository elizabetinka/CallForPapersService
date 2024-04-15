using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDaftField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 3, 0, 15, 740, DateTimeKind.Local).AddTicks(1760),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 2, 8, 31, 39, DateTimeKind.Local).AddTicks(1930));

            migrationBuilder.AddColumn<bool>(
                name: "daft",
                table: "applications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "daft",
                table: "applications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 2, 8, 31, 39, DateTimeKind.Local).AddTicks(1930),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 3, 31, 3, 0, 15, 740, DateTimeKind.Local).AddTicks(1760));
        }
    }
}
