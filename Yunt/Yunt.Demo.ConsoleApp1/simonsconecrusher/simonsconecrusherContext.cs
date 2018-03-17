﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yunt.Demo.ConsoleApp1.simonsconecrusher
{
    public partial class simonsconecrusherContext : DbContext
    {
        public virtual DbSet<Simonsconecrusher> Simonsconecrusher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=10.1.5.25;Port=3306;Database=simonsconecrusher;Uid=root;Pwd=unitoon2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Simonsconecrusher>(entity =>
            {
                entity.ToTable("simonsconecrusher");

                entity.Property(e => e.SimonsConeCrusherId).HasColumnType("bigint(20)");

                entity.Property(e => e.MotorId).HasColumnType("bigint(20)");

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });
        }
    }
}
