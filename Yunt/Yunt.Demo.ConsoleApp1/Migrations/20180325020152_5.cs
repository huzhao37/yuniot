using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByHour_Time_MotorId",
                table: "VibrosieveByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_VibrosieveByDay_Time_MotorId",
                table: "VibrosieveByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Vibrosieve_Time_MotorId",
                table: "Vibrosieve",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByHour_Time_MotorId",
                table: "VerticalCrusherByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusherByDay_Time_MotorId",
                table: "VerticalCrusherByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_VerticalCrusher_Time_MotorId",
                table: "VerticalCrusher",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByHour_Time_MotorId",
                table: "PulverizerByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_PulverizerByDay_Time_MotorId",
                table: "PulverizerByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pulverizer_Time_MotorId",
                table: "Pulverizer",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByHour_Time_MotorId",
                table: "MaterialFeederByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeederByDay_Time_MotorId",
                table: "MaterialFeederByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialFeeder_Time_MotorId",
                table: "MaterialFeeder",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByHour_Time_MotorId",
                table: "JawCrusherByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusherByDay_Time_MotorId",
                table: "JawCrusherByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_JawCrusher_Time_MotorId",
                table: "JawCrusher",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByHour_Time_MotorId",
                table: "ImpactCrusherByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusherByDay_Time_MotorId",
                table: "ImpactCrusherByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactCrusher_Time_MotorId",
                table: "ImpactCrusher",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_HVibByHour_Time_MotorId",
                table: "HVibByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_HVibByDay_Time_MotorId",
                table: "HVibByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_HVib_Time_MotorId",
                table: "HVib",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByHour_Time_MotorId",
                table: "ConveyorByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConveyorByDay_Time_MotorId",
                table: "ConveyorByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Conveyor_Time_MotorId",
                table: "Conveyor",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusherByHour_Time_MotorId",
                table: "ConeCrusherByHour",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusherByDay_Time_MotorId",
                table: "ConeCrusherByDay",
                columns: new[] { "Time", "MotorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConeCrusher_Time_MotorId",
                table: "ConeCrusher",
                columns: new[] { "Time", "MotorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByHour_Time_MotorId",
                table: "VibrosieveByHour");

            migrationBuilder.DropIndex(
                name: "IX_VibrosieveByDay_Time_MotorId",
                table: "VibrosieveByDay");

            migrationBuilder.DropIndex(
                name: "IX_Vibrosieve_Time_MotorId",
                table: "Vibrosieve");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByHour_Time_MotorId",
                table: "VerticalCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusherByDay_Time_MotorId",
                table: "VerticalCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_VerticalCrusher_Time_MotorId",
                table: "VerticalCrusher");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByHour_Time_MotorId",
                table: "PulverizerByHour");

            migrationBuilder.DropIndex(
                name: "IX_PulverizerByDay_Time_MotorId",
                table: "PulverizerByDay");

            migrationBuilder.DropIndex(
                name: "IX_Pulverizer_Time_MotorId",
                table: "Pulverizer");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByHour_Time_MotorId",
                table: "MaterialFeederByHour");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeederByDay_Time_MotorId",
                table: "MaterialFeederByDay");

            migrationBuilder.DropIndex(
                name: "IX_MaterialFeeder_Time_MotorId",
                table: "MaterialFeeder");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByHour_Time_MotorId",
                table: "JawCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusherByDay_Time_MotorId",
                table: "JawCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_JawCrusher_Time_MotorId",
                table: "JawCrusher");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByHour_Time_MotorId",
                table: "ImpactCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusherByDay_Time_MotorId",
                table: "ImpactCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_ImpactCrusher_Time_MotorId",
                table: "ImpactCrusher");

            migrationBuilder.DropIndex(
                name: "IX_HVibByHour_Time_MotorId",
                table: "HVibByHour");

            migrationBuilder.DropIndex(
                name: "IX_HVibByDay_Time_MotorId",
                table: "HVibByDay");

            migrationBuilder.DropIndex(
                name: "IX_HVib_Time_MotorId",
                table: "HVib");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByHour_Time_MotorId",
                table: "ConveyorByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConveyorByDay_Time_MotorId",
                table: "ConveyorByDay");

            migrationBuilder.DropIndex(
                name: "IX_Conveyor_Time_MotorId",
                table: "Conveyor");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusherByHour_Time_MotorId",
                table: "ConeCrusherByHour");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusherByDay_Time_MotorId",
                table: "ConeCrusherByDay");

            migrationBuilder.DropIndex(
                name: "IX_ConeCrusher_Time_MotorId",
                table: "ConeCrusher");

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
    }
}
