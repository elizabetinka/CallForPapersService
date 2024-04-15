using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAdmisUsersAddFieldSendTimeDeleteDaftField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applications_users_user_id",
                table: "applications");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_applications_user_id",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "added_date",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "daft",
                table: "applications");

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 14, 21, 31, 36, 631, DateTimeKind.Local).AddTicks(6350));

            migrationBuilder.AddColumn<DateTime>(
                name: "send_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_date",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "send_date",
                table: "applications");

            migrationBuilder.AddColumn<DateTime>(
                name: "added_date",
                table: "applications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 16, 10, 49, 700, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.AddColumn<bool>(
                name: "daft",
                table: "applications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "Id", "login", "password" },
                values: new object[] { new Guid("498aeec6-ec93-47ad-a32c-7638b194a0e3"), "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_applications_user_id",
                table: "applications",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_applications_users_user_id",
                table: "applications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
