using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yunt.Demo.ConsoleApp1.conveyor
{
    public partial class conveyorContext : DbContext
    {
        public virtual DbSet<Conveyor> Conveyor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=10.1.5.25;Port=3306;Database=conveyor;Uid=root;Pwd=unitoon2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conveyor>(entity =>
            {
                entity.ToTable("conveyor");

                entity.Property(e => e.ConveyorId).HasColumnType("bigint(20)");

                entity.Property(e => e.BootFlagBit).HasColumnType("tinyint(1)");

                entity.Property(e => e.MotorId).HasColumnType("bigint(20)");

                entity.Property(e => e.Time).HasColumnType("bigint(20)");

                entity.Property(e => e.Unit).HasColumnType("int(11)");

                entity.Property(e => e.ZeroCalibration).HasColumnType("tinyint(1)");
            });
        }
    }
}
