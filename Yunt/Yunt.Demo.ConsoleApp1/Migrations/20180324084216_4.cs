using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_VibrosieveByHour_MotorId",
            //    table: "VibrosieveByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_VibrosieveByDay_MotorId",
            //    table: "VibrosieveByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_Vibrosieve_MotorId",
            //    table: "Vibrosieve");

            //migrationBuilder.DropIndex(
            //    name: "IX_VerticalCrusherByHour_MotorId",
            //    table: "VerticalCrusherByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_VerticalCrusherByDay_MotorId",
            //    table: "VerticalCrusherByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_VerticalCrusher_MotorId",
            //    table: "VerticalCrusher");

            //migrationBuilder.DropIndex(
            //    name: "IX_PulverizerByHour_MotorId",
            //    table: "PulverizerByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_PulverizerByDay_MotorId",
            //    table: "PulverizerByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_Pulverizer_MotorId",
            //    table: "Pulverizer");

            //migrationBuilder.DropIndex(
            //    name: "IX_MotorType_MotorTypeId",
            //    table: "MotorType");

            //migrationBuilder.DropIndex(
            //    name: "IX_Motor_MotorId",
            //    table: "Motor");

            //migrationBuilder.DropIndex(
            //    name: "IX_MaterialFeederByHour_MotorId",
            //    table: "MaterialFeederByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_MaterialFeederByDay_MotorId",
            //    table: "MaterialFeederByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_MaterialFeeder_MotorId",
            //    table: "MaterialFeeder");

            //migrationBuilder.DropIndex(
            //    name: "IX_JawCrusherByHour_MotorId",
            //    table: "JawCrusherByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_JawCrusherByDay_MotorId",
            //    table: "JawCrusherByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_JawCrusher_MotorId",
            //    table: "JawCrusher");

            //migrationBuilder.DropIndex(
            //    name: "IX_ImpactCrusherByHour_MotorId",
            //    table: "ImpactCrusherByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_ImpactCrusherByDay_MotorId",
            //    table: "ImpactCrusherByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_ImpactCrusher_MotorId",
            //    table: "ImpactCrusher");

            //migrationBuilder.DropIndex(
            //    name: "IX_HVibByHour_MotorId",
            //    table: "HVibByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_HVibByDay_MotorId",
            //    table: "HVibByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_HVib_MotorId",
            //    table: "HVib");

            //migrationBuilder.DropIndex(
            //    name: "IX_ConveyorByHour_MotorId",
            //    table: "ConveyorByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_ConveyorByDay_MotorId",
            //    table: "ConveyorByDay");

            //migrationBuilder.DropIndex(
            //    name: "IX_Conveyor_MotorId",
            //    table: "Conveyor");

            //migrationBuilder.DropIndex(
            //    name: "IX_ConeCrusherByHour_MotorId",
            //    table: "ConeCrusherByHour");

            //migrationBuilder.DropIndex(
            //    name: "IX_ConeCrusher_MotorId",
            //    table: "ConeCrusher");

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Vibrosieve",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusher",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Pulverizer",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorType",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Param",
                table: "MotorParams",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorParams",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MotorParams",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductionLineId",
                table: "MotorIdFactories",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorIdFactories",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "Motor",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Motor",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeeder",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusher",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusher",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVib",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Conveyor",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByHour",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByDay",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusher",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByHour_MotorId_Time",
                table: "VibrosieveByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByDay_MotorId_Time",
                table: "VibrosieveByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Vibrosieve_MotorId_Time",
                table: "Vibrosieve",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByHour_MotorId_Time",
                table: "VerticalCrusherByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByDay_MotorId_Time",
                table: "VerticalCrusherByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusher_MotorId_Time",
                table: "VerticalCrusher",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByHour_MotorId_Time",
                table: "PulverizerByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByDay_MotorId_Time",
                table: "PulverizerByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Pulverizer_MotorId_Time",
                table: "Pulverizer",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_MotorType_MotorTypeId",
                table: "MotorType",
                column: "MotorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorParams_MotorTypeId_Param_Description",
                table: "MotorParams",
                columns: new[] { "MotorTypeId", "Param", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_MotorIdFactories_MotorIndex_MotorTypeId_ProductionLineId",
                table: "MotorIdFactories",
                columns: new[] { "MotorIndex", "MotorTypeId", "ProductionLineId" });

            migrationBuilder.CreateIndex(
                name: "IX_Motor_MotorId_MotorTypeId_EmbeddedDeviceId",
                table: "Motor",
                columns: new[] { "MotorId", "MotorTypeId", "EmbeddedDeviceId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByHour_MotorId_Time",
                table: "MaterialFeederByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByDay_MotorId_Time",
                table: "MaterialFeederByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeeder_MotorId_Time",
                table: "MaterialFeeder",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByHour_MotorId_Time",
                table: "JawCrusherByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByDay_MotorId_Time",
                table: "JawCrusherByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusher_MotorId_Time",
                table: "JawCrusher",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByHour_MotorId_Time",
                table: "ImpactCrusherByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByDay_MotorId_Time",
                table: "ImpactCrusherByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusher_MotorId_Time",
                table: "ImpactCrusher",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_HVibByHour_MotorId_Time",
                table: "HVibByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_HVibByDay_MotorId_Time",
                table: "HVibByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_HVib_MotorId_Time",
                table: "HVib",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByHour_MotorId_Time",
                table: "ConveyorByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByDay_MotorId_Time",
                table: "ConveyorByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Conveyor_MotorId_Time",
                table: "Conveyor",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusherByHour_MotorId_Time",
                table: "ConeCrusherByHour",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusherByDay_MotorId_Time",
                table: "ConeCrusherByDay",
                columns: new[] { "MotorId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusher_MotorId_Time",
                table: "ConeCrusher",
                columns: new[] { "MotorId", "Time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByHour_MotorId_Time",
                table: "VibrosieveByHour");

            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByDay_MotorId_Time",
                table: "VibrosieveByDay");

            migrationBuilder.DropIndex(
                name: "IX_Vibrosieve_MotorId_Time",
                table: "Vibrosieve");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByHour_MotorId_Time",
                table: "VerticalCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByDay_MotorId_Time",
                table: "VerticalCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusher_MotorId_Time",
                table: "VerticalCrusher");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByHour_MotorId_Time",
                table: "PulverizerByHour");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByDay_MotorId_Time",
                table: "PulverizerByDay");

            migrationBuilder.DropIndex(
                name: "IX_Pulverizer_MotorId_Time",
                table: "Pulverizer");

            migrationBuilder.DropIndex(
                name: "IX_MotorType_MotorTypeId",
                table: "MotorType");

            migrationBuilder.DropIndex(
                name: "IX_MotorParams_MotorTypeId_Param_Description",
                table: "MotorParams");

            migrationBuilder.DropIndex(
                name: "IX_MotorIdFactories_MotorIndex_MotorTypeId_ProductionLineId",
                table: "MotorIdFactories");

            migrationBuilder.DropIndex(
                name: "IX_Motor_MotorId_MotorTypeId_EmbeddedDeviceId",
                table: "Motor");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByHour_MotorId_Time",
                table: "MaterialFeederByHour");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByDay_MotorId_Time",
                table: "MaterialFeederByDay");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeeder_MotorId_Time",
                table: "MaterialFeeder");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByHour_MotorId_Time",
                table: "JawCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByDay_MotorId_Time",
                table: "JawCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusher_MotorId_Time",
                table: "JawCrusher");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByHour_MotorId_Time",
                table: "ImpactCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByDay_MotorId_Time",
                table: "ImpactCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusher_MotorId_Time",
                table: "ImpactCrusher");

            migrationBuilder.DropIndex(
                name: "IX_HVibByHour_MotorId_Time",
                table: "HVibByHour");

            migrationBuilder.DropIndex(
                name: "IX_HVibByDay_MotorId_Time",
                table: "HVibByDay");

            migrationBuilder.DropIndex(
                name: "IX_HVib_MotorId_Time",
                table: "HVib");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByHour_MotorId_Time",
                table: "ConveyorByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByDay_MotorId_Time",
                table: "ConveyorByDay");

            migrationBuilder.DropIndex(
                name: "IX_Conveyor_MotorId_Time",
                table: "Conveyor");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusherByHour_MotorId_Time",
                table: "ConeCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusherByDay_MotorId_Time",
                table: "ConeCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusher_MotorId_Time",
                table: "ConeCrusher");

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VibrosieveByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Vibrosieve",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "VerticalCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "PulverizerByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Pulverizer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorType",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Param",
                table: "MotorParams",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorParams",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MotorParams",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ProductionLineId",
                table: "MotorIdFactories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "MotorIdFactories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "MotorTypeId",
                table: "Motor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Motor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeederByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "MaterialFeeder",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "JawCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ImpactCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVibByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "HVib",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConveyorByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "Conveyor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByHour",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusherByDay",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MotorId",
                table: "ConeCrusher",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

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
    }
}
