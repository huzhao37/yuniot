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
    [DbContext(typeof(AnalysisContext))]
    [Migration("20180623060038_4")]
    partial class _4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("Yunt.Analysis.Repository.EF.Models.AlarmInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(50);

                    b.Property<string>("MotorId")
                        .HasMaxLength(20);

                    b.Property<string>("MotorName")
                        .HasMaxLength(20);

                    b.Property<string>("Remark")
                        .HasMaxLength(50);

                    b.Property<long>("Time");

                    b.HasKey("Id");

                    b.ToTable("AlarmInfo");
                });

            modelBuilder.Entity("Yunt.Analysis.Repository.EF.Models.EventKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("MotorTypeId")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Regulation")
                        .HasMaxLength(200);

                    b.Property<long>("Time");

                    b.HasKey("Id");

                    b.HasIndex("Code", "MotorTypeId");

                    b.ToTable("EventKind");
                });

            modelBuilder.Entity("Yunt.Analysis.Repository.EF.Models.MotorEventLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("EventCode")
                        .HasMaxLength(12);

                    b.Property<string>("MotorId")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("MotorName")
                        .HasMaxLength(20);

                    b.Property<string>("ProductionLineId")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<long>("Time");

                    b.HasKey("Id");

                    b.HasIndex("ProductionLineId", "MotorId");

                    b.ToTable("MotorEventLog");
                });
#pragma warning restore 612, 618
        }
    }
}
