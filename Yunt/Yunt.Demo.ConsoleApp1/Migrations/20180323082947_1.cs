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
                    AbsSpindleTravel = table.Column<float>(nullable: false),
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    MovaStress = table.Column<float>(nullable: false),
                    OilFeedTempreature = table.Column<float>(nullable: false),
                    OilReturnTempreatur = table.Column<float>(nullable: false),
                    PowerFactor = table.Column<float>(nullable: false),
                    SpindleTravel = table.Column<float>(nullable: false),
                    TankTemperature = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Vibrate1 = table.Column<float>(nullable: false),
                    Vibrate2 = table.Column<float>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgAbsSpindleTravel = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgMovaStress = table.Column<float>(nullable: false),
                    AvgOilFeedTempreature = table.Column<float>(nullable: false),
                    AvgOilReturnTempreatur = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTravel = table.Column<float>(nullable: false),
                    AvgTankTemperature = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgAbsSpindleTravel = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgMovaStress = table.Column<float>(nullable: false),
                    AvgOilFeedTempreature = table.Column<float>(nullable: false),
                    AvgOilReturnTempreatur = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTravel = table.Column<float>(nullable: false),
                    AvgTankTemperature = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    AccumulativeWeight = table.Column<float>(nullable: false),
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    GravitySensorMill = table.Column<float>(nullable: false),
                    InstantWeight = table.Column<float>(nullable: false),
                    MS420mA = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    PulsesSecond = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false),
                    WeightUnit = table.Column<float>(nullable: false)
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
                    AccumulativeWeight = table.Column<float>(nullable: false),
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgGravitySensorMill = table.Column<float>(nullable: false),
                    AvgInstantWeight = table.Column<float>(nullable: false),
                    AvgMS420mA = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgPulsesSecond = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    AvgWeightUnit = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    AccumulativeWeight = table.Column<float>(nullable: false),
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgGravitySensorMill = table.Column<float>(nullable: false),
                    AvgInstantWeight = table.Column<float>(nullable: false),
                    AvgMS420mA = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgPulsesSecond = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    AvgWeightUnit = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    Current = table.Column<float>(nullable: false),
                    Current2 = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    Current = table.Column<float>(nullable: false),
                    Current2 = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    Current = table.Column<float>(nullable: false),
                    Current2 = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleToothRollCrusherByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVib",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    OilFeedStress = table.Column<float>(nullable: false),
                    OilReturnStress = table.Column<float>(nullable: false),
                    PowerFactor = table.Column<float>(nullable: false),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    SpindleTemperature3 = table.Column<float>(nullable: false),
                    SpindleTemperature4 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVib", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVibByDay",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgOilFeedStress = table.Column<float>(nullable: false),
                    AvgOilReturnStress = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature3 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature4 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVibByDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVibByHour",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgOilFeedStress = table.Column<float>(nullable: false),
                    AvgOilReturnStress = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature3 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature4 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVibByHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactCrusher",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Motor1ActivePower = table.Column<float>(nullable: false),
                    Motor1Current_A = table.Column<float>(nullable: false),
                    Motor1Current_B = table.Column<float>(nullable: false),
                    Motor1Current_C = table.Column<float>(nullable: false),
                    Motor1PowerFactor = table.Column<float>(nullable: false),
                    Motor1Voltage_A = table.Column<float>(nullable: false),
                    Motor1Voltage_B = table.Column<float>(nullable: false),
                    Motor1Voltage_C = table.Column<float>(nullable: false),
                    Motor2ActivePower = table.Column<float>(nullable: false),
                    Motor2Current_A = table.Column<float>(nullable: false),
                    Motor2Current_B = table.Column<float>(nullable: false),
                    Motor2Current_C = table.Column<float>(nullable: false),
                    Motor2PowerFactor = table.Column<float>(nullable: false),
                    Motor2Voltage_A = table.Column<float>(nullable: false),
                    Motor2Voltage_B = table.Column<float>(nullable: false),
                    Motor2Voltage_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Vibrate1 = table.Column<float>(nullable: false),
                    Vibrate2 = table.Column<float>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    AvgMotor1Current_A = table.Column<float>(nullable: false),
                    AvgMotor1Current_B = table.Column<float>(nullable: false),
                    AvgMotor1Current_C = table.Column<float>(nullable: false),
                    AvgMotor1PowerFactor = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_A = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_B = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_C = table.Column<float>(nullable: false),
                    AvgMotor2Current_A = table.Column<float>(nullable: false),
                    AvgMotor2Current_B = table.Column<float>(nullable: false),
                    AvgMotor2Current_C = table.Column<float>(nullable: false),
                    AvgMotor2PowerFactor = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_A = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_B = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_C = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    Motor1ActivePower = table.Column<float>(nullable: false),
                    Motor2ActivePower = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    AvgMotor1Current_A = table.Column<float>(nullable: false),
                    AvgMotor1Current_B = table.Column<float>(nullable: false),
                    AvgMotor1Current_C = table.Column<float>(nullable: false),
                    AvgMotor1PowerFactor = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_A = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_B = table.Column<float>(nullable: false),
                    AvgMotor1Voltage_C = table.Column<float>(nullable: false),
                    AvgMotor2Current_A = table.Column<float>(nullable: false),
                    AvgMotor2Current_B = table.Column<float>(nullable: false),
                    AvgMotor2Current_C = table.Column<float>(nullable: false),
                    AvgMotor2PowerFactor = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_A = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_B = table.Column<float>(nullable: false),
                    AvgMotor2Voltage_C = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    Motor1ActivePower = table.Column<float>(nullable: false),
                    Motor2ActivePower = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotiveSpindleTemperature1 = table.Column<float>(nullable: false),
                    MotiveSpindleTemperature2 = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    RackSpindleTemperature1 = table.Column<float>(nullable: false),
                    RackSpindleTemperature2 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Vibrate1 = table.Column<float>(nullable: false),
                    Vibrate2 = table.Column<float>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgMotiveSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgMotiveSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgRackSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgRackSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgMotiveSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgMotiveSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgRackSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgRackSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    Frequency = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgFrequency = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgFrequency = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    Capicity = table.Column<float>(nullable: false),
                    EmbeddedDeviceId = table.Column<int>(nullable: false),
                    FeedSize = table.Column<float>(nullable: false),
                    FinalSize = table.Column<string>(nullable: true),
                    MotorId = table.Column<string>(nullable: true),
                    MotorPower = table.Column<float>(nullable: false),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductionLineId = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    StandValue = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorIdFactories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MotorIndex = table.Column<int>(nullable: false),
                    MotorTypeId = table.Column<string>(nullable: true),
                    ProductionLineId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorIdFactories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorParams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Param = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MotorTypeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pulverizer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Vibrate1 = table.Column<float>(nullable: false),
                    Vibrate2 = table.Column<float>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    BearingSpeed = table.Column<float>(nullable: false),
                    Current = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    BearingSpeed = table.Column<float>(nullable: false),
                    Current = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    BearingSpeed = table.Column<float>(nullable: false),
                    Current = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    Current = table.Column<double>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    OilFeedTempreature = table.Column<double>(nullable: false),
                    OilReturnTempreature = table.Column<double>(nullable: false),
                    TankTemperature = table.Column<double>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgOilFeedTempreature = table.Column<float>(nullable: false),
                    AvgOilReturnTempreatur = table.Column<float>(nullable: false),
                    AvgTankTemperature = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgOilFeedTempreature = table.Column<float>(nullable: false),
                    AvgOilReturnTempreatur = table.Column<float>(nullable: false),
                    AvgTankTemperature = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Vibrate1 = table.Column<float>(nullable: false),
                    Vibrate2 = table.Column<float>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgVibrate1 = table.Column<float>(nullable: false),
                    AvgVibrate2 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    WearValue1 = table.Column<float>(nullable: false),
                    WearValue2 = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    Current_A = table.Column<float>(nullable: false),
                    Current_B = table.Column<float>(nullable: false),
                    Current_C = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    PowerFactor = table.Column<float>(nullable: false),
                    SpindleTemperature1 = table.Column<float>(nullable: false),
                    SpindleTemperature2 = table.Column<float>(nullable: false),
                    SpindleTemperature3 = table.Column<float>(nullable: false),
                    SpindleTemperature4 = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    Voltage_A = table.Column<float>(nullable: false),
                    Voltage_B = table.Column<float>(nullable: false),
                    Voltage_C = table.Column<float>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature3 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature4 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                    ActivePower = table.Column<float>(nullable: false),
                    AvgCurrent_A = table.Column<float>(nullable: false),
                    AvgCurrent_B = table.Column<float>(nullable: false),
                    AvgCurrent_C = table.Column<float>(nullable: false),
                    AvgPowerFactor = table.Column<float>(nullable: false),
                    AvgSpindleTemperature1 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature2 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature3 = table.Column<float>(nullable: false),
                    AvgSpindleTemperature4 = table.Column<float>(nullable: false),
                    AvgVoltage_A = table.Column<float>(nullable: false),
                    AvgVoltage_B = table.Column<float>(nullable: false),
                    AvgVoltage_C = table.Column<float>(nullable: false),
                    LoadStall = table.Column<float>(nullable: false),
                    MotorId = table.Column<string>(nullable: true),
                    RunningTime = table.Column<float>(nullable: false),
                    Time = table.Column<DateTimeOffset>(nullable: false)
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
                name: "HVib");

            migrationBuilder.DropTable(
                name: "HVibByDay");

            migrationBuilder.DropTable(
                name: "HVibByHour");

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
                name: "MotorIdFactories");

            migrationBuilder.DropTable(
                name: "MotorParams");

            migrationBuilder.DropTable(
                name: "MotorType");

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
