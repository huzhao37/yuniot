using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "OffSet",
                table: "Motor",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Slope",
                table: "Motor",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "UseCalc",
                table: "Motor",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OffSet",
                table: "Motor");

            migrationBuilder.DropColumn(
                name: "Slope",
                table: "Motor");

            migrationBuilder.DropColumn(
                name: "UseCalc",
                table: "Motor");
        }
    }
}
