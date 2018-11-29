﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using gbat.Common;
using gbat.Demo.ConsoleApp1;

namespace gbat.Demo.ConsoleApp1.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20180612060853_6")]
    partial class _6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("gbat.Inventory.Repository.EF.Models.InHouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchNo")
                        .HasMaxLength(20);

                    b.Property<int>("Count");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<string>("FactoryInfo")
                        .HasMaxLength(50);

                    b.Property<string>("InOperator")
                        .HasMaxLength(10);

                    b.Property<long>("InTime");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("SparePartsTypeId");

                    b.Property<float>("UnitPrice");

                    b.Property<int>("WareHousesId");

                    b.HasKey("Id");

                    b.ToTable("InHouse");
                });

            modelBuilder.Entity("gbat.Inventory.Repository.EF.Models.InventoryAlarmInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreateTime");

                    b.Property<int>("InventoryBalance");

                    b.Property<int>("SparePartsTypeId");

                    b.HasKey("Id");

                    b.ToTable("InventoryAlarmInfo");
                });

            modelBuilder.Entity("gbat.Inventory.Repository.EF.Models.OutHouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchNo")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("MotorId")
                        .HasMaxLength(20);

                    b.Property<string>("OutOperator")
                        .HasMaxLength(10);

                    b.Property<long>("OutTime");

                    b.Property<int>("SparePartsStatus");

                    b.Property<int>("SparePartsTypeId");

                    b.Property<float>("UnitPrice");

                    b.Property<long>("UselessTime");

                    b.HasKey("Id");

                    b.ToTable("OutHouse");
                });

            modelBuilder.Entity("gbat.Inventory.Repository.EF.Models.SparePartsType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreateTime");

                    b.Property<int>("InventoryAlarmLimits");

                    b.Property<string>("Name")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("SparePartsType");
                });

            modelBuilder.Entity("gbat.Inventory.Repository.EF.Models.WareHouses", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreateTime");

                    b.Property<string>("Keeper")
                        .HasMaxLength(10);

                    b.Property<string>("MotorTypeId")
                        .HasMaxLength(4);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Remark")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("WareHouses");
                });
#pragma warning restore 612, 618
        }
    }
}
