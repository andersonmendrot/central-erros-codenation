using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralErros.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_layer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_layer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "environment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_environment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(maxLength: 254, nullable: false),
                    password = table.Column<string>(maxLength: 15, nullable: false),
                    timestamp = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "error",
                columns: table => new
                {
                    environment_id = table.Column<int>(nullable: false),
                    application_layer_id = table.Column<int>(nullable: false),
                    level_id = table.Column<int>(nullable: false),
                    id = table.Column<int>(nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: false),
                    origin = table.Column<string>(maxLength: 200, nullable: false),
                    status = table.Column<string>(nullable: false),
                    number_events = table.Column<int>(nullable: false),
                    timestamp = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_error", x => new { x.level_id, x.environment_id, x.application_layer_id });
                    table.CheckConstraint("constraint_status", "status = 'y' or status = 'n'");
                    table.ForeignKey(
                        name: "FK_error_application_layer_application_layer_id",
                        column: x => x.application_layer_id,
                        principalTable: "application_layer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_error_environment_environment_id",
                        column: x => x.environment_id,
                        principalTable: "environment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_error_level_level_id",
                        column: x => x.level_id,
                        principalTable: "level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_error_application_layer_id",
                table: "error",
                column: "application_layer_id");

            migrationBuilder.CreateIndex(
                name: "IX_error_environment_id",
                table: "error",
                column: "environment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "error");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "application_layer");

            migrationBuilder.DropTable(
                name: "environment");

            migrationBuilder.DropTable(
                name: "level");
        }
    }
}
