using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralErros.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: false),
                    Password = table.Column<string>(type: "varchar(15)", nullable: false),
                    CreatedAt = table.Column<byte[]>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Error",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Origin = table.Column<string>(type: "varchar(200)", nullable: false),
                    Status = table.Column<string>(type: "char", nullable: false),
                    NumberEvents = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "timestamp", nullable: false),
                    ApplicationLayerId = table.Column<int>(nullable: false),
                    EnvironmentId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error", x => x.Id);
                    table.CheckConstraint("constraint_status", "Status = 'y' or Status = 'n'");
                    table.ForeignKey(
                        name: "FK_Error_ApplicationLayer_ApplicationLayerId",
                        column: x => x.ApplicationLayerId,
                        principalTable: "ApplicationLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Error_Environment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Error_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Error_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Error_ApplicationLayerId",
                table: "Error",
                column: "ApplicationLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Error_EnvironmentId",
                table: "Error",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Error_LanguageId",
                table: "Error",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Error_LevelId",
                table: "Error",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Error");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ApplicationLayer");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Level");
        }
    }
}
