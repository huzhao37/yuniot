using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventKind",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MotorTypeId = table.Column<string>(maxLength: 4, nullable: false),
                    Regulation = table.Column<string>(nullable: true),
                    Time = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorEventLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EventCode = table.Column<string>(nullable: true),
                    MotorId = table.Column<string>(maxLength: 20, nullable: false),
                    MotorName = table.Column<string>(nullable: true),
                    ProductionLineId = table.Column<string>(maxLength: 15, nullable: false),
                    Time = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorEventLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventKind_Code_MotorTypeId",
                table: "EventKind",
                columns: new[] { "Code", "MotorTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_MotorEventLog_ProductionLineId_MotorId",
                table: "MotorEventLog",
                columns: new[] { "ProductionLineId", "MotorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventKind");

            migrationBuilder.DropTable(
                name: "MotorEventLog");
        }
    }
}
