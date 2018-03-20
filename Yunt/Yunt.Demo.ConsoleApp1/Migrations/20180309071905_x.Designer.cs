﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Yunt.Demo.ConsoleApp1;

namespace Yunt.Demo.ConsoleApp1.Migrations
{
    [DbContext(typeof(yunt_testContext))]
    [Migration("20180309071905_x")]
    partial class x
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Categorycreatetime")
                        .HasColumnName("categorycreatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Categoryname")
                        .IsRequired()
                        .HasColumnName("categoryname")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("tb_category");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnName("command")
                        .HasMaxLength(400)
                        .IsUnicode(false);

                    b.Property<DateTime>("Commandcreatetime")
                        .HasColumnName("commandcreatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Commandname")
                        .IsRequired()
                        .HasColumnName("commandname")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<byte>("Commandstate")
                        .HasColumnName("commandstate");

                    b.Property<int>("Nodeid")
                        .HasColumnName("nodeid");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.HasKey("Id");

                    b.HasIndex("Nodeid");

                    b.ToTable("tb_command");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Configkey")
                        .IsRequired()
                        .HasColumnName("configkey")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Configvalue")
                        .IsRequired()
                        .HasColumnName("configvalue")
                        .IsUnicode(false);

                    b.Property<bool>("Istest")
                        .HasColumnName("istest");

                    b.Property<DateTime>("Lastupdatetime")
                        .HasColumnName("lastupdatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnName("remark")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("tb_config");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Errorcreatetime")
                        .HasColumnName("errorcreatetime")
                        .HasColumnType("datetime");

                    b.Property<byte>("Errortype")
                        .HasColumnName("errortype");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnName("msg")
                        .HasMaxLength(2000)
                        .IsUnicode(false);

                    b.Property<int>("Nodeid")
                        .HasColumnName("nodeid");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.HasKey("Id");

                    b.HasIndex("Errorcreatetime");

                    b.HasIndex("Errortype");

                    b.ToTable("tb_error");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Logcreatetime")
                        .HasColumnName("logcreatetime")
                        .HasColumnType("datetime");

                    b.Property<byte>("Logtype")
                        .HasColumnName("logtype");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnName("msg")
                        .HasMaxLength(2000)
                        .IsUnicode(false);

                    b.Property<int>("Nodeid")
                        .HasColumnName("nodeid");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.HasKey("Id");

                    b.HasIndex("Logcreatetime");

                    b.ToTable("tb_log");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Ifcheckstate")
                        .HasColumnName("ifcheckstate");

                    b.Property<DateTime>("Nodecreatetime")
                        .HasColumnName("nodecreatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Nodeip")
                        .IsRequired()
                        .HasColumnName("nodeip")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<DateTime>("Nodelastupdatetime")
                        .HasColumnName("nodelastupdatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Nodename")
                        .IsRequired()
                        .HasColumnName("nodename")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("tb_node");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbPerformance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<double>("Cpu")
                        .HasColumnName("cpu");

                    b.Property<double>("Installdirsize")
                        .HasColumnName("installdirsize");

                    b.Property<DateTime>("Lastupdatetime")
                        .HasColumnName("lastupdatetime")
                        .HasColumnType("datetime");

                    b.Property<double>("Memory")
                        .HasColumnName("memory");

                    b.Property<int>("Nodeid")
                        .HasColumnName("nodeid");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.HasKey("Id");

                    b.HasIndex("Taskid");

                    b.ToTable("tb_performance");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Categoryid")
                        .HasColumnName("categoryid");

                    b.Property<int>("Nodeid")
                        .HasColumnName("nodeid");

                    b.Property<string>("Taskappconfigjson")
                        .IsRequired()
                        .HasColumnName("taskappconfigjson")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<DateTime>("Taskcreatetime")
                        .HasColumnName("taskcreatetime")
                        .HasColumnType("datetime");

                    b.Property<int>("Taskcreateuserid")
                        .HasColumnName("taskcreateuserid");

                    b.Property<string>("Taskcron")
                        .IsRequired()
                        .HasColumnName("taskcron")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Taskerrorcount")
                        .HasColumnName("taskerrorcount");

                    b.Property<DateTime>("Tasklastendtime")
                        .HasColumnName("tasklastendtime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Tasklasterrortime")
                        .HasColumnName("tasklasterrortime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Tasklaststarttime")
                        .HasColumnName("tasklaststarttime")
                        .HasColumnType("datetime");

                    b.Property<string>("Taskmainclassdllfilename")
                        .IsRequired()
                        .HasColumnName("taskmainclassdllfilename")
                        .HasMaxLength(60)
                        .IsUnicode(false);

                    b.Property<string>("Taskmainclassnamespace")
                        .IsRequired()
                        .HasColumnName("taskmainclassnamespace")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Taskname")
                        .IsRequired()
                        .HasColumnName("taskname")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Taskremark")
                        .IsRequired()
                        .HasColumnName("taskremark")
                        .HasMaxLength(5000)
                        .IsUnicode(false);

                    b.Property<long>("Taskruncount")
                        .HasColumnName("taskruncount");

                    b.Property<byte>("Taskstate")
                        .HasColumnName("taskstate");

                    b.Property<DateTime>("Taskupdatetime")
                        .HasColumnName("taskupdatetime")
                        .HasColumnType("datetime");

                    b.Property<int>("Taskversion")
                        .HasColumnName("taskversion");

                    b.HasKey("Id");

                    b.HasIndex("Nodeid");

                    b.HasIndex("Taskstate");

                    b.ToTable("tb_task");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbTempdata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.Property<string>("Tempdatajson")
                        .IsRequired()
                        .HasColumnName("tempdatajson")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime>("Tempdatalastupdatetime")
                        .HasColumnName("tempdatalastupdatetime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Taskid");

                    b.ToTable("tb_tempdata");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Usercreatetime")
                        .HasColumnName("usercreatetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Useremail")
                        .IsRequired()
                        .HasColumnName("useremail")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<byte>("Userrole")
                        .HasColumnName("userrole");

                    b.Property<string>("Userstaffno")
                        .IsRequired()
                        .HasColumnName("userstaffno")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("Usertel")
                        .IsRequired()
                        .HasColumnName("usertel")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("tb_user");
                });

            modelBuilder.Entity("Yunt.Demo.ConsoleApp1.TbVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Taskid")
                        .HasColumnName("taskid");

                    b.Property<int>("Version")
                        .HasColumnName("version");

                    b.Property<DateTime>("Versioncreatetime")
                        .HasColumnName("versioncreatetime")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("Zipfile")
                        .IsRequired()
                        .HasColumnName("zipfile")
                        .HasColumnType("mediumblob ");

                    b.Property<string>("Zipfilename")
                        .HasColumnName("zipfilename")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Taskid");

                    b.HasIndex("Version");

                    b.ToTable("tb_version");
                });
#pragma warning restore 612, 618
        }
    }
}