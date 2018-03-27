using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "VibrosieveByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "VibrosieveByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "Vibrosieve",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "VerticalCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "VerticalCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "VerticalCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "SimonsConeCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "SimonsConeCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "SimonsConeCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ReverHammerCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ReverHammerCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ReverHammerCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "PulverizerByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "PulverizerByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "Pulverizer",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MotorType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MotorParams",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MotorIdFactories",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "Motor",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MaterialFeederByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MaterialFeederByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "MaterialFeeder",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "JawCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "JawCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "JawCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ImpactCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ImpactCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ImpactCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "HVibByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "HVibByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "HVib",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "DoubleToothRollCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "DoubleToothRollCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "DoubleToothRollCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ConveyorByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ConveyorByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "Conveyor",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ConeCrusherByHour",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ConeCrusherByDay",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "ConeCrusher",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "VibrosieveByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "VibrosieveByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Vibrosieve",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "VerticalCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "VerticalCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "VerticalCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "SimonsConeCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "SimonsConeCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "SimonsConeCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ReverHammerCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ReverHammerCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ReverHammerCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "PulverizerByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "PulverizerByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Pulverizer",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MotorType",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MotorParams",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MotorIdFactories",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Motor",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MaterialFeederByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MaterialFeederByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "MaterialFeeder",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "JawCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "JawCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "JawCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ImpactCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ImpactCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ImpactCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "HVibByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "HVibByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "HVib",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "DoubleToothRollCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "DoubleToothRollCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "DoubleToothRollCrusher",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ConveyorByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ConveyorByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Conveyor",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ConeCrusherByHour",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ConeCrusherByDay",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "ConeCrusher",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
