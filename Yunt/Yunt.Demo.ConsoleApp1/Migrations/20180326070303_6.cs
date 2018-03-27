using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capicity",
                table: "Motor");

            migrationBuilder.AddColumn<string>(
                name: "ProductSpecification",
                table: "Motor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSpecification",
                table: "Motor");

            migrationBuilder.AddColumn<float>(
                name: "Capicity",
                table: "Motor",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
