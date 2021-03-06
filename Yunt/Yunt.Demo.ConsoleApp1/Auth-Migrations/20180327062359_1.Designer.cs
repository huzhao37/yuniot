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
    [DbContext(typeof(AuthContext))]
    [Migration("20180327062359_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Yunt.Auth.Repository.EF.Models.IdModel.UserIdFactories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Time");

                    b.Property<int>("UserIndex");

                    b.HasKey("Id");

                    b.ToTable("UserIdFactories");
                });

            modelBuilder.Entity("Yunt.Auth.Repository.EF.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginAccount")
                        .IsRequired();

                    b.Property<string>("LoginPwd")
                        .IsRequired();

                    b.Property<string>("Mail");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Remark");

                    b.Property<long>("Time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserRoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Yunt.Auth.Repository.EF.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc")
                        .IsRequired();

                    b.Property<string>("Remark");

                    b.Property<long>("Time");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
