using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace gbat.Demo.ConsoleApp1.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "InHouse");

            migrationBuilder.AlterColumn<long>(
                name: "BatchNo",
                table: "OutHouse",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BatchNo",
                table: "OutHouse",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "InHouse",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
