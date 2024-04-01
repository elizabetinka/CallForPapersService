using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureOrm.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    activity = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    plan = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    added_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 3, 31, 1, 28, 15, 394, DateTimeKind.Local).AddTicks(120)),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.id);
                    table.CheckConstraint("ValidApplication", "user_id IS NOT NULL AND (activity IS NOT NULL  OR name IS NOT NULL OR description IS NOT NULL OR plan IS NOT NULL )");
                    table.ForeignKey(
                        name: "FK_applications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applications_user_id",
                table: "applications",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropColumn(
                name: "login",
                table: "users");
        }
    }
}
