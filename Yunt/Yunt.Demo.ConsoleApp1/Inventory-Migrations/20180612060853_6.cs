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
                name: "WareHousesId",
                table: "WareHouses");

            migrationBuilder.DropColumn(
                name: "SparePartsId",
                table: "InventoryAlarmInfo");

            migrationBuilder.AddColumn<int>(
                name: "SparePartsTypeId",
                table: "InventoryAlarmInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WareHousesId",
                table: "InHouse",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparePartsTypeId",
                table: "InventoryAlarmInfo");

            migrationBuilder.AddColumn<string>(
                name: "WareHousesId",
                table: "WareHouses",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SparePartsId",
                table: "InventoryAlarmInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WareHousesId",
                table: "InHouse",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
