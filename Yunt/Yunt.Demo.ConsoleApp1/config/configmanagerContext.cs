using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class configmanagerContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Accountrole> Accountrole { get; set; }
        public virtual DbSet<Alarmtype> Alarmtype { get; set; }
        public virtual DbSet<Cabinetparamter> Cabinetparamter { get; set; }
        public virtual DbSet<Camera> Camera { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Dvr> Dvr { get; set; }
        public virtual DbSet<Embeddeddataform> Embeddeddataform { get; set; }
        public virtual DbSet<Embeddeddevice> Embeddeddevice { get; set; }
        public virtual DbSet<Messagetype> Messagetype { get; set; }
        public virtual DbSet<Motor> Motor { get; set; }
        public virtual DbSet<Motorctfactor> Motorctfactor { get; set; }
        public virtual DbSet<Motorparamters> Motorparamters { get; set; }
        public virtual DbSet<Motortype> Motortype { get; set; }
        public virtual DbSet<Productionline> Productionline { get; set; }
        public virtual DbSet<Standparamvalues> Standparamvalues { get; set; }
        public virtual DbSet<Timestatistics> Timestatistics { get; set; }
        public virtual DbSet<Workshiftinfo> Workshiftinfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=10.1.5.25;Port=3306;Database=configmanager;Uid=root;Pwd=unitoon2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId).HasColumnType("bigint(20)");

                entity.Property(e => e.AccountRoleId).HasColumnType("int(11)");

                entity.Property(e => e.Company).HasMaxLength(255);

                entity.Property(e => e.ProductionLineList).HasMaxLength(255);

                entity.Property(e => e.Pwd).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<Accountrole>(entity =>
            {
                entity.ToTable("accountrole");

                entity.Property(e => e.AccountRoleId).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RoleName).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Alarmtype>(entity =>
            {
                entity.ToTable("alarmtype");

                entity.Property(e => e.AlarmTypeId).HasColumnType("bigint(20)");

                entity.Property(e => e.AlarmTypeSerialnum).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Maintenance).HasMaxLength(255);

                entity.Property(e => e.Param).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Cabinetparamter>(entity =>
            {
                entity.ToTable("cabinetparamter");

                entity.Property(e => e.CabinetParamterId).HasColumnType("bigint(20)");

                entity.Property(e => e.CabinetParamterSerialnum).HasMaxLength(255);

                entity.Property(e => e.CabinetParamterType).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Param).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Camera>(entity =>
            {
                entity.ToTable("camera");

                entity.Property(e => e.CameraId).HasColumnType("bigint(20)");

                entity.Property(e => e.CameraSerialnum).HasMaxLength(255);

                entity.Property(e => e.ChannelName).HasMaxLength(255);

                entity.Property(e => e.ChannelNo).HasMaxLength(0);

                entity.Property(e => e.DvrSerialnum)
                    .HasMaxLength(0)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.Property(e => e.CarId).HasColumnType("bigint(20)");

                entity.Property(e => e.CarName).HasMaxLength(255);

                entity.Property(e => e.MotorSerialnum).HasMaxLength(255);

                entity.Property(e => e.PlateNo).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Dvr>(entity =>
            {
                entity.ToTable("dvr");

                entity.Property(e => e.DvrId).HasColumnType("bigint(20)");

                entity.Property(e => e.AccessToken).HasMaxLength(255);

                entity.Property(e => e.DvrSerialnum).HasMaxLength(255);

                entity.Property(e => e.ExpireTime).HasColumnType("bigint(20)");

                entity.Property(e => e.ProductionLineSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasMaxLength(255);
            });

            modelBuilder.Entity<Embeddeddataform>(entity =>
            {
                entity.ToTable("embeddeddataform");

                entity.Property(e => e.EmbeddedDataFormId).HasColumnType("bigint(20)");

                entity.Property(e => e.EmbeddedDataFormSerialnum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Embeddeddevice>(entity =>
            {
                entity.ToTable("embeddeddevice");

                entity.Property(e => e.EmbeddedDeviceId).HasColumnType("bigint(20)");

                entity.Property(e => e.EmbeddedDataFromSerialnum).HasMaxLength(255);

                entity.Property(e => e.EmbeddedDeviceName).HasMaxLength(255);

                entity.Property(e => e.EmbeddedDeviceSerialnum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OperationNumber).HasMaxLength(255);

                entity.Property(e => e.ProductionLineSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");

                entity.Property(e => e.WorkNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<Messagetype>(entity =>
            {
                entity.ToTable("messagetype");

                entity.Property(e => e.MessageTypeId).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Time).HasMaxLength(255);
            });

            modelBuilder.Entity<Motor>(entity =>
            {
                entity.ToTable("motor");

                entity.Property(e => e.MotorId).HasColumnType("bigint(20)");

                entity.Property(e => e.BuildTime).HasColumnType("bigint(20)");

                entity.Property(e => e.ControlDeviceId).HasMaxLength(255);

                entity.Property(e => e.EmbeddedDeviceSerialnum).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.IsBeltWeight).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsDisplay).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsMainBeltWeight).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsOutConveyor).HasColumnType("tinyint(1)");

                entity.Property(e => e.LatestMaintainTime).HasColumnType("bigint(20)");

                entity.Property(e => e.MotorSerialnum).HasMaxLength(255);

                entity.Property(e => e.MotorTypeSerialnum).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ProductionLineSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");

                entity.Property(e => e.TypeNo).HasMaxLength(255);
            });

            modelBuilder.Entity<Motorctfactor>(entity =>
            {
                entity.ToTable("motorctfactor");

                entity.Property(e => e.MotorCtfactorId).HasColumnType("bigint(20)");

                entity.Property(e => e.CurrentCt).HasMaxLength(255);

                entity.Property(e => e.MotorSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Motorparamters>(entity =>
            {
                entity.HasKey(e => e.MotorParamterId);

                entity.ToTable("motorparamters");

                entity.Property(e => e.MotorParamterId).HasColumnType("bigint(20)");

                entity.Property(e => e.AlarmTypeSerialnum).HasMaxLength(255);

                entity.Property(e => e.CabinetParamterSerialnum).HasMaxLength(255);

                entity.Property(e => e.EmbeddedDeviceSerialnum).HasMaxLength(255);

                entity.Property(e => e.Index).HasColumnType("int(11)");

                entity.Property(e => e.MotorParamterSerialnum).HasMaxLength(255);

                entity.Property(e => e.MotorSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Motortype>(entity =>
            {
                entity.ToTable("motortype");

                entity.Property(e => e.MotorTypeId).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.MotorTypeSerialnum).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Productionline>(entity =>
            {
                entity.ToTable("productionline");

                entity.Property(e => e.ProductionLineId).HasColumnType("bigint(20)");

                entity.Property(e => e.BuildTime).HasColumnType("bigint(20)");

                entity.Property(e => e.ExpiredTime).HasColumnType("bigint(20)");

                entity.Property(e => e.LatestDataTime).HasColumnType("bigint(20)");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.OreType).HasMaxLength(255);

                entity.Property(e => e.Production).HasMaxLength(255);

                entity.Property(e => e.ProductionLineSerialnum).HasMaxLength(255);

                entity.Property(e => e.PropertyRightPerson).HasMaxLength(255);

                entity.Property(e => e.ResponsiblePerson).HasMaxLength(255);

                entity.Property(e => e.ResponsiblePersonContact).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Standparamvalues>(entity =>
            {
                entity.HasKey(e => e.StandParamValueId);

                entity.ToTable("standparamvalues");

                entity.Property(e => e.StandParamValueId).HasColumnType("bigint(20)");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.MotorSerialnum).HasMaxLength(255);

                entity.Property(e => e.MotorTypeSerialnum).HasMaxLength(255);

                entity.Property(e => e.Parameter).HasMaxLength(255);

                entity.Property(e => e.StandParamValueSerialnum).HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Timestatistics>(entity =>
            {
                entity.HasKey(e => e.TimeStatisticId);

                entity.ToTable("timestatistics");

                entity.Property(e => e.TimeStatisticId).HasColumnType("bigint(20)");

                entity.Property(e => e.LastTime).HasColumnType("bigint(20)");

                entity.Property(e => e.NextTime).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Workshiftinfo>(entity =>
            {
                entity.ToTable("workshiftinfo");

                entity.Property(e => e.WorkShiftInfoId).HasColumnType("bigint(20)");

                entity.Property(e => e.AccountId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime).HasColumnType("bigint(20)");

                entity.Property(e => e.EndHour).HasColumnType("int(11)");

                entity.Property(e => e.Index).HasColumnType("int(11)");

                entity.Property(e => e.ProductionLineSerialnum).HasMaxLength(255);

                entity.Property(e => e.StartHour).HasColumnType("int(11)");

                entity.Property(e => e.WorkShiftInfoSerialnum).HasMaxLength(255);
            });
        }
    }
}
