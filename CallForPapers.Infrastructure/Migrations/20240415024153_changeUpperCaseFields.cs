using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUpperCaseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_applications",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "applications");

            migrationBuilder.RenameTable(
                name: "applications",
                newName: "Applications");
            

            migrationBuilder.RenameColumn(
                name: "activity_activity",
                table: "Applications",
                newName: "activity");

            migrationBuilder.RenameColumn(
                name: "send_date",
                table: "Applications",
                newName: "send_data");

            migrationBuilder.AddColumn<DateTime>(
                name: "create_data",
                table: "Applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 15, 5, 41, 53, 543, DateTimeKind.Local).AddTicks(5910));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "create_data",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "applications");

            migrationBuilder.RenameColumn(
                name: "activity",
                table: "applications",
                newName: "activity_activity");

            migrationBuilder.RenameColumn(
                name: "send_data",
                table: "applications",
                newName: "send_date");

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 15, 1, 13, 21, 978, DateTimeKind.Local).AddTicks(6940));

            migrationBuilder.AddPrimaryKey(
                name: "PK_applications",
                table: "applications",
                column: "Id");
        }
    }
}
