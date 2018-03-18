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
                name: "ConeCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    MovaStress = table.Column<double>(nullable: false),
                    OilFeedTempreature = table.Column<double>(nullable: false),
                    OilReturnTempreature = table.Column<double>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    SpindleTravel = table.Column<double>(nullable: false),
                    TankTemperature = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConeCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConeCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageMovaStress = table.Column<double>(nullable: false),
                    AverageOilFeedTempreature = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageSpindleTravel = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConeCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConeCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageMovaStress = table.Column<double>(nullable: false),
                    AverageOilFeedTempreature = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageSpindleTravel = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConeCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conveyor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccumulativeWeight = table.Column<double>(nullable: false),
                    BootFlagBit = table.Column<int>(nullable: false),
                    Current = table.Column<double>(nullable: false),
                    Frequency = table.Column<double>(nullable: false),
                    InstantWeight = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    Velocity = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false),
                    ZeroCalibration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conveyor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConveyorByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccumulativeWeight = table.Column<double>(nullable: false),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFrequency = table.Column<double>(nullable: false),
                    AverageInstantWeight = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVelocity = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveyorByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConveyorByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccumulativeWeight = table.Column<double>(nullable: false),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFrequency = table.Column<double>(nullable: false),
                    AverageInstantWeight = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVelocity = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveyorByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoubleToothRollCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    Current2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleToothRollCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoubleToothRollCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    Current2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleToothRollCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoubleToothRollCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    Current2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleToothRollCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    Current2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    SpindleTemperature1 = table.Column<double>(nullable: false),
                    SpindleTemperature2 = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageCurrent2 = table.Column<double>(nullable: false),
                    AverageSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageSpindleTemperature2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    OnOffCounts = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageCurrent2 = table.Column<double>(nullable: false),
                    AverageSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageSpindleTemperature2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    OnOffCounts = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JawCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotiveSpindleTemperature1 = table.Column<double>(nullable: false),
                    MotiveSpindleTemperature2 = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    RackSpindleTemperature1 = table.Column<double>(nullable: false),
                    RackSpindleTemperature2 = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JawCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JawCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageMotiveSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageMotiveSpindleTemperature2 = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageRackSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageRackSpindleTemperature2 = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JawCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JawCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageMotiveSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageMotiveSpindleTemperature2 = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageRackSpindleTemperature1 = table.Column<double>(nullable: false),
                    AverageRackSpindleTemperature2 = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JawCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialFeeder",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    Frequency = table.Column<double>(nullable: false),
                    InFrequency = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Velocity = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFeeder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialFeederByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFrequency = table.Column<double>(nullable: false),
                    AverageVelocity = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFeederByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialFeederByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFrequency = table.Column<double>(nullable: false),
                    AverageVelocity = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialFeederByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildTime = table.Column<DateTimeOffset>(nullable: false),
                    Capicity = table.Column<double>(nullable: false),
                    EmbeddedDeviceId = table.Column<int>(nullable: false),
                    FeedSize = table.Column<double>(nullable: false),
                    FinalSize = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsBeltWeight = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsDisplay = table.Column<bool>(nullable: false),
                    IsMainBeltWeight = table.Column<bool>(nullable: false),
                    IsOutConveyor = table.Column<bool>(nullable: false),
                    LatestDataTime = table.Column<DateTimeOffset>(nullable: false),
                    LatestMaintainTime = table.Column<DateTimeOffset>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    MotorPower = table.Column<double>(nullable: false),
                    MotorTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motortype",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<double>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FeedSize = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    MaintenancePeriod = table.Column<string>(nullable: true),
                    MotorPower = table.Column<double>(nullable: false),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motortype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pulverizer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    FanCurrent = table.Column<double>(nullable: false),
                    GraderCurrent = table.Column<double>(nullable: false),
                    GraderRotateSpeed = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pulverizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PulverizerByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFanCurrent = table.Column<double>(nullable: false),
                    AverageGraderCurrent = table.Column<double>(nullable: false),
                    AverageGraderRotateSpeed = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PulverizerByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PulverizerByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageFanCurrent = table.Column<double>(nullable: false),
                    AverageGraderCurrent = table.Column<double>(nullable: false),
                    AverageGraderRotateSpeed = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PulverizerByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReverHammerCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BearingSpeed = table.Column<double>(nullable: false),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    SpindleTemperature1 = table.Column<double>(nullable: false),
                    SpindleTemperature2 = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReverHammerCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReverHammerCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BearingSpeed = table.Column<double>(nullable: false),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    SpindleTemperature1 = table.Column<double>(nullable: false),
                    SpindleTemperature2 = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReverHammerCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReverHammerCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BearingSpeed = table.Column<double>(nullable: false),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    SpindleTemperature1 = table.Column<double>(nullable: false),
                    SpindleTemperature2 = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReverHammerCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimonsConeCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    OilFeedTempreature = table.Column<double>(nullable: false),
                    OilReturnTempreature = table.Column<double>(nullable: false),
                    TankTemperature = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimonsConeCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimonsConeCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageOilFeedTempreature = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimonsConeCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimonsConeCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageOilFeedTempreature = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimonsConeCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerticalCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BearingTempreature = table.Column<double>(nullable: false),
                    Current = table.Column<double>(nullable: false),
                    Current2 = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LubricatingOilPressure = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    OilReturnTempreature = table.Column<double>(nullable: false),
                    Oscillation = table.Column<double>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    TankTemperature = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerticalCrusher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerticalCrusherByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageBearingTempreature = table.Column<double>(nullable: false),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageCurrent2 = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageOscillation = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerticalCrusherByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerticalCrusherByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageBearingTempreature = table.Column<double>(nullable: false),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AverageCurrent2 = table.Column<double>(nullable: false),
                    AverageOilReturnTempreature = table.Column<double>(nullable: false),
                    AverageOscillation = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTankTemperature = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerticalCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vibrosieve",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Current = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    PowerFactor = table.Column<double>(nullable: false),
                    ReactivePower = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true),
                    TotalPower = table.Column<double>(nullable: false),
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vibrosieve", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VibrosieveByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VibrosieveByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VibrosieveByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageCurrent = table.Column<double>(nullable: false),
                    AveragePowerFactor = table.Column<double>(nullable: false),
                    AverageReactivePower = table.Column<double>(nullable: false),
                    AverageTotalPower = table.Column<double>(nullable: false),
                    AverageVoltage = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoadStall = table.Column<double>(nullable: false),
                    MotorId = table.Column<int>(nullable: false),
                    RunningTime = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VibrosieveByHour", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConeCrusher");

            migrationBuilder.DropTable(
                name: "ConeCrusherByDay");

            migrationBuilder.DropTable(
                name: "ConeCrusherByHour");

            migrationBuilder.DropTable(
                name: "Conveyor");

            migrationBuilder.DropTable(
                name: "ConveyorByDay");

            migrationBuilder.DropTable(
                name: "ConveyorByHour");

            migrationBuilder.DropTable(
                name: "DoubleToothRollCrusher");

            migrationBuilder.DropTable(
                name: "DoubleToothRollCrusherByDay");

            migrationBuilder.DropTable(
                name: "DoubleToothRollCrusherByHour");

            migrationBuilder.DropTable(
                name: "ImpactCrusher");

            migrationBuilder.DropTable(
                name: "ImpactCrusherByDay");

            migrationBuilder.DropTable(
                name: "ImpactCrusherByHour");

            migrationBuilder.DropTable(
                name: "JawCrusher");

            migrationBuilder.DropTable(
                name: "JawCrusherByDay");

            migrationBuilder.DropTable(
                name: "JawCrusherByHour");

            migrationBuilder.DropTable(
                name: "MaterialFeeder");

            migrationBuilder.DropTable(
                name: "MaterialFeederByDay");

            migrationBuilder.DropTable(
                name: "MaterialFeederByHour");

            migrationBuilder.DropTable(
                name: "Motor");

            migrationBuilder.DropTable(
                name: "Motortype");

            migrationBuilder.DropTable(
                name: "Pulverizer");

            migrationBuilder.DropTable(
                name: "PulverizerByDay");

            migrationBuilder.DropTable(
                name: "PulverizerByHour");

            migrationBuilder.DropTable(
                name: "ReverHammerCrusher");

            migrationBuilder.DropTable(
                name: "ReverHammerCrusherByDay");

            migrationBuilder.DropTable(
                name: "ReverHammerCrusherByHour");

            migrationBuilder.DropTable(
                name: "SimonsConeCrusher");

            migrationBuilder.DropTable(
                name: "SimonsConeCrusherByDay");

            migrationBuilder.DropTable(
                name: "SimonsConeCrusherByHour");

            migrationBuilder.DropTable(
                name: "VerticalCrusher");

            migrationBuilder.DropTable(
                name: "VerticalCrusherByDay");

            migrationBuilder.DropTable(
                name: "VerticalCrusherByHour");

            migrationBuilder.DropTable(
                name: "Vibrosieve");

            migrationBuilder.DropTable(
                name: "VibrosieveByDay");

            migrationBuilder.DropTable(
                name: "VibrosieveByHour");
        }
    }
}
