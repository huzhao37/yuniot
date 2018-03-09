using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categorycreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    categoryname = table.Column<string>(unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_command",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    command = table.Column<string>(unicode: false, maxLength: 400, nullable: false),
                    commandcreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    commandname = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    commandstate = table.Column<byte>(nullable: false),
                    nodeid = table.Column<int>(nullable: false),
                    taskid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_command", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_config",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    configkey = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    configvalue = table.Column<string>(unicode: false, nullable: false),
                    istest = table.Column<bool>(nullable: false),
                    lastupdatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    remark = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_error",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    errorcreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    errortype = table.Column<byte>(nullable: false),
                    msg = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    nodeid = table.Column<int>(nullable: false),
                    taskid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_error", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_log",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    logcreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    logtype = table.Column<byte>(nullable: false),
                    msg = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    nodeid = table.Column<int>(nullable: false),
                    taskid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_node",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ifcheckstate = table.Column<bool>(nullable: false),
                    nodecreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    nodeip = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    nodelastupdatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    nodename = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_node", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_performance",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cpu = table.Column<double>(nullable: false),
                    installdirsize = table.Column<double>(nullable: false),
                    lastupdatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    memory = table.Column<double>(nullable: false),
                    nodeid = table.Column<int>(nullable: false),
                    taskid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_performance", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_task",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoryid = table.Column<int>(nullable: false),
                    nodeid = table.Column<int>(nullable: false),
                    taskappconfigjson = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    taskcreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    taskcreateuserid = table.Column<int>(nullable: false),
                    taskcron = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    taskerrorcount = table.Column<int>(nullable: false),
                    tasklastendtime = table.Column<DateTime>(type: "datetime", nullable: false),
                    tasklasterrortime = table.Column<DateTime>(type: "datetime", nullable: false),
                    tasklaststarttime = table.Column<DateTime>(type: "datetime", nullable: false),
                    taskmainclassdllfilename = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    taskmainclassnamespace = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    taskname = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    taskremark = table.Column<string>(unicode: false, maxLength: 5000, nullable: false),
                    taskruncount = table.Column<long>(nullable: false),
                    taskstate = table.Column<byte>(nullable: false),
                    taskupdatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    taskversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_task", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tempdata",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    taskid = table.Column<int>(nullable: false),
                    tempdatajson = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    tempdatalastupdatetime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tempdata", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usercreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    useremail = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    username = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    userrole = table.Column<byte>(nullable: false),
                    userstaffno = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    usertel = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_version",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    taskid = table.Column<int>(nullable: false),
                    version = table.Column<int>(nullable: false),
                    versioncreatetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    zipfile = table.Column<byte[]>(type: "mediumblob ", nullable: false),
                    zipfilename = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_version", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_command_nodeid",
                table: "tb_command",
                column: "nodeid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_error_errorcreatetime",
                table: "tb_error",
                column: "errorcreatetime");

            migrationBuilder.CreateIndex(
                name: "IX_tb_error_errortype",
                table: "tb_error",
                column: "errortype");

            migrationBuilder.CreateIndex(
                name: "IX_tb_log_logcreatetime",
                table: "tb_log",
                column: "logcreatetime");

            migrationBuilder.CreateIndex(
                name: "IX_tb_performance_taskid",
                table: "tb_performance",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_task_nodeid",
                table: "tb_task",
                column: "nodeid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_task_taskstate",
                table: "tb_task",
                column: "taskstate");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tempdata_taskid",
                table: "tb_tempdata",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_version_taskid",
                table: "tb_version",
                column: "taskid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_version_version",
                table: "tb_version",
                column: "version");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_category");

            migrationBuilder.DropTable(
                name: "tb_command");

            migrationBuilder.DropTable(
                name: "tb_config");

            migrationBuilder.DropTable(
                name: "tb_error");

            migrationBuilder.DropTable(
                name: "tb_log");

            migrationBuilder.DropTable(
                name: "tb_node");

            migrationBuilder.DropTable(
                name: "tb_performance");

            migrationBuilder.DropTable(
                name: "tb_task");

            migrationBuilder.DropTable(
                name: "tb_tempdata");

            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "tb_version");
        }
    }
}
