using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace gbat.Demo.ConsoleApp1.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "SparePartsIdFactories");

            migrationBuilder.DropColumn(
                name: "SparePartsTypeId",
                table: "SparePartsType");

            migrationBuilder.CreateTable(
                name: "InHouse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BatchNo = table.Column<string>(maxLength: 20, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    FactoryInfo = table.Column<string>(maxLength: 50, nullable: true),
                    InOperator = table.Column<string>(maxLength: 10, nullable: true),
                    InTime = table.Column<long>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    SparePartsTypeId = table.Column<string>(maxLength: 10, nullable: true),
                    UnitPrice = table.Column<float>(nullable: false),
                    WareHousesId = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InHouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutHouse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BatchNo = table.Column<string>(maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MotorId = table.Column<string>(maxLength: 20, nullable: true),
                    OutOperator = table.Column<string>(maxLength: 10, nullable: true),
                    OutTime = table.Column<long>(nullable: false),
                    SparePartsStatus = table.Column<int>(nullable: false),
                    SparePartsTypeId = table.Column<string>(maxLength: 10, nullable: true),
                    UnitPrice = table.Column<float>(nullable: false),
                    UselessTime = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutHouse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InHouse");

            migrationBuilder.DropTable(
                name: "OutHouse");

            migrationBuilder.AddColumn<string>(
                name: "SparePartsTypeId",
                table: "SparePartsType",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    FactoryInfo = table.Column<string>(maxLength: 50, nullable: true),
                    InOperator = table.Column<string>(maxLength: 10, nullable: true),
                    InTime = table.Column<long>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    MotorId = table.Column<string>(maxLength: 20, nullable: true),
                    OutOperator = table.Column<string>(maxLength: 10, nullable: true),
                    OutTime = table.Column<long>(nullable: false),
                    SparePartsId = table.Column<string>(maxLength: 20, nullable: true),
                    SparePartsName = table.Column<string>(maxLength: 20, nullable: true),
                    SparePartsStatus = table.Column<int>(nullable: false),
                    SparePartsTypeId = table.Column<string>(maxLength: 10, nullable: true),
                    UselessTime = table.Column<long>(nullable: false),
                    WareHousesId = table.Column<string>(maxLength: 20, nullable: true)
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
                    SparePartsTypeId = table.Column<string>(maxLength: 10, nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartsIdFactories", x => x.Id);
                });
        }
    }
}
