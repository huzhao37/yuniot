using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maintain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(maxLength: 20, nullable: true),
                    Operator = table.Column<string>(maxLength: 50, nullable: true),
                    Record = table.Column<string>(maxLength: 200, nullable: true),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    Time = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintain", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintain");
        }
    }
}
