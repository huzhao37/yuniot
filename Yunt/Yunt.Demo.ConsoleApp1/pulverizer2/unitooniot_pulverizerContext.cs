﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yunt.Demo.ConsoleApp1.pulverizer2
{
    public partial class unitooniot_pulverizerContext : DbContext
    {
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Pulverizer> Pulverizer { get; set; }
        public virtual DbSet<PulverizerByDay> PulverizerByDay { get; set; }
        public virtual DbSet<PulverizerByHour> PulverizerByHour { get; set; }
        public virtual DbSet<PulverizerByWorkShift> PulverizerByWorkShift { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=rm-bp1e7cw4xl2ns45aoo.sqlserver.rds.aliyuncs.com,3433; Database=unitooniot_pulverizer;Persist Security Info=True;User ID=xenomorph;password=U2n0i1t7oon;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
}