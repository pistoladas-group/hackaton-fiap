using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton.Api.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FileId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ProcessStatusId = table.Column<byte>(type: "TINYINT", nullable: false),
                    ProcessTypeId = table.Column<byte>(type: "TINYINT", nullable: false),
                    ErrorMessage = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SizeInBytes = table.Column<int>(type: "INT", nullable: false),
                    Url = table.Column<string>(type: "VARCHAR(2500)", nullable: true),
                    ContentType = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ProcessStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileProcesses");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
