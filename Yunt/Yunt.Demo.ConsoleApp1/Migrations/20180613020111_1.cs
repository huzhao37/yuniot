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
                name: "Collectdevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<string>(nullable: true),
                    Productionline_Id = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectdevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dataconfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Collectdevice_Index = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Datatype_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataconfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dataformmodel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bit = table.Column<int>(nullable: true),
                    BitDesc = table.Column<string>(nullable: true),
                    Collectdevice_Index = table.Column<string>(nullable: true),
                    DataPhysicalAccuracy = table.Column<string>(nullable: true),
                    DataPhysicalFeature = table.Column<string>(nullable: true),
                    DataPhysicalId = table.Column<int>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DebugValue = table.Column<int>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    Divalue = table.Column<int>(nullable: true),
                    Dovalue = table.Column<int>(nullable: true),
                    FieldParam = table.Column<string>(nullable: true),
                    FieldParamEn = table.Column<string>(nullable: true),
                    FormatId = table.Column<int>(nullable: true),
                    Index = table.Column<int>(nullable: true),
                    LineId = table.Column<int>(nullable: true),
                    MachineId = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    MotorId = table.Column<string>(nullable: true),
                    MotorTypeName = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    WarnValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataformmodel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Datatype",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bit = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InByte = table.Column<int>(nullable: false),
                    OutIntArray = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datatype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messagequeue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Collectdevice_Index = table.Column<string>(nullable: true),
                    Com_Type = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Pwd = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Route_Key = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Timer = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Write_Read = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messagequeue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorparams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    PhysicId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorparams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motortype",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MotorTypeId = table.Column<string>(nullable: true),
                    MotorTypeName = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motortype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Physicfeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Accur = table.Column<float>(nullable: false),
                    Format = table.Column<int>(nullable: false),
                    PhysicType = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physicfeature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    End = table.Column<int>(nullable: false),
                    FinishCy1 = table.Column<int>(nullable: false),
                    FinishCy2 = table.Column<int>(nullable: false),
                    FinishCy3 = table.Column<int>(nullable: false),
                    FinishCy4 = table.Column<int>(nullable: false),
                    MainCy = table.Column<int>(nullable: false),
                    ProductionlineId = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Start = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collectdevice");

            migrationBuilder.DropTable(
                name: "Dataconfig");

            migrationBuilder.DropTable(
                name: "Dataformmodel");

            migrationBuilder.DropTable(
                name: "Datatype");

            migrationBuilder.DropTable(
                name: "Messagequeue");

            migrationBuilder.DropTable(
                name: "Motorparams");

            migrationBuilder.DropTable(
                name: "Motortype");

            migrationBuilder.DropTable(
                name: "Physicfeature");

            migrationBuilder.DropTable(
                name: "ProductionPlans");
        }
    }
}
