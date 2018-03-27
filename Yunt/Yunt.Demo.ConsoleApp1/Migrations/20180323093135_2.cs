using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Vibrosieve",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Pulverizer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorType",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Motor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeeder",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVib",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Conveyor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByHour_MotorId",
                table: "VibrosieveByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByDay_MotorId",
                table: "VibrosieveByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_Vibrosieve_MotorId",
                table: "Vibrosieve",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByHour_MotorId",
                table: "VerticalCrusherByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByDay_MotorId",
                table: "VerticalCrusherByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusher_MotorId",
                table: "VerticalCrusher",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByHour_MotorId",
                table: "PulverizerByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByDay_MotorId",
                table: "PulverizerByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_Pulverizer_MotorId",
                table: "Pulverizer",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_MotorType_MotorTypeId",
                table: "MotorType",
                column: "MotorTypeId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_Motor_MotorId",
                table: "Motor",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByHour_MotorId",
                table: "MaterialFeederByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByDay_MotorId",
                table: "MaterialFeederByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeeder_MotorId",
                table: "MaterialFeeder",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByHour_MotorId",
                table: "JawCrusherByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByDay_MotorId",
                table: "JawCrusherByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusher_MotorId",
                table: "JawCrusher",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByHour_MotorId",
                table: "ImpactCrusherByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByDay_MotorId",
                table: "ImpactCrusherByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusher_MotorId",
                table: "ImpactCrusher",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_HVibByHour_MotorId",
                table: "HVibByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_HVibByDay_MotorId",
                table: "HVibByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_HVib_MotorId",
                table: "HVib",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByHour_MotorId",
                table: "ConveyorByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByDay_MotorId",
                table: "ConveyorByDay",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_Conveyor_MotorId",
                table: "Conveyor",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusherByHour_MotorId",
                table: "ConeCrusherByHour",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusher_MotorId",
                table: "ConeCrusher",
                column: "MotorId",
                unique: true)
                .Annotation("MySql:SpatialIndex", "SPATIAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByHour_MotorId",
                table: "VibrosieveByHour");

            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByDay_MotorId",
                table: "VibrosieveByDay");

            migrationBuilder.DropIndex(
                name: "IX_Vibrosieve_MotorId",
                table: "Vibrosieve");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByHour_MotorId",
                table: "VerticalCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByDay_MotorId",
                table: "VerticalCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusher_MotorId",
                table: "VerticalCrusher");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByHour_MotorId",
                table: "PulverizerByHour");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByDay_MotorId",
                table: "PulverizerByDay");

            migrationBuilder.DropIndex(
                name: "IX_Pulverizer_MotorId",
                table: "Pulverizer");

            migrationBuilder.DropIndex(
                name: "IX_MotorType_MotorTypeId",
                table: "MotorType");

            migrationBuilder.DropIndex(
                name: "IX_Motor_MotorId",
                table: "Motor");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByHour_MotorId",
                table: "MaterialFeederByHour");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByDay_MotorId",
                table: "MaterialFeederByDay");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeeder_MotorId",
                table: "MaterialFeeder");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByHour_MotorId",
                table: "JawCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByDay_MotorId",
                table: "JawCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusher_MotorId",
                table: "JawCrusher");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByHour_MotorId",
                table: "ImpactCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByDay_MotorId",
                table: "ImpactCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusher_MotorId",
                table: "ImpactCrusher");

            migrationBuilder.DropIndex(
                name: "IX_HVibByHour_MotorId",
                table: "HVibByHour");

            migrationBuilder.DropIndex(
                name: "IX_HVibByDay_MotorId",
                table: "HVibByDay");

            migrationBuilder.DropIndex(
                name: "IX_HVib_MotorId",
                table: "HVib");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByHour_MotorId",
                table: "ConveyorByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByDay_MotorId",
                table: "ConveyorByDay");

            migrationBuilder.DropIndex(
                name: "IX_Conveyor_MotorId",
                table: "Conveyor");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusherByHour_MotorId",
                table: "ConeCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusher_MotorId",
                table: "ConeCrusher");

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Vibrosieve",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Pulverizer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorType",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Motor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeeder",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVib",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Conveyor",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
