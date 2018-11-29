using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace gbat.Demo.ConsoleApp1.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryAlarmInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<long>(nullable: false),
                    InventoryBalance = table.Column<int>(nullable: false),
                    SparePartsId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAlarmInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    FactoryInfo = table.Column<string>(nullable: true),
                    InOperator = table.Column<string>(nullable: true),
                    InTime = table.Column<long>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    OutOperator = table.Column<string>(nullable: true),
                    OutTime = table.Column<long>(nullable: false),
                    SparePartsId = table.Column<string>(nullable: true),
                    SparePartsName = table.Column<string>(nullable: true),
                    SparePartsStatus = table.Column<int>(nullable: false),
                    SparePartsTypeId = table.Column<string>(nullable: true),
                    UselessTime = table.Column<long>(nullable: false),
                    WareHousesId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SparePartsIdFactories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SparePartsIndex = table.Column<int>(nullable: false),
                    SparePartsTypeId = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartsIdFactories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SparePartsType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<long>(nullable: false),
                    InventoryAlarmLimits = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SparePartsTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<long>(nullable: false),
                    Keeper = table.Column<string>(nullable: true),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    WareHousesId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryAlarmInfo");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "SparePartsIdFactories");

            migrationBuilder.DropTable(
                name: "SparePartsType");

            migrationBuilder.DropTable(
                name: "WareHouses");
        }
    }
}
