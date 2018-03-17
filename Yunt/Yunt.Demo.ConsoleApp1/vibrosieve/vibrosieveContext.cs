using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yunt.Demo.ConsoleApp1.vibrosieve
{
    public partial class vibrosieveContext : DbContext
    {
        public virtual DbSet<Vibrosieve> Vibrosieve { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=10.1.5.25;Port=3306;Database=vibrosieve;Uid=root;Pwd=unitoon2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vibrosieve>(entity =>
            {
                entity.ToTable("vibrosieve");

                entity.Property(e => e.VibrosieveId).HasColumnType("bigint(20)");

                entity.Property(e => e.MotorId).HasColumnType("bigint(20)");

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });
        }
    }
}
