

#region using namespace

using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Yunt.Analysis.Repository.EF.Models;
using Yunt.Device.Repository.EF.Models;
using Yunt.Device.Repository.EF.Models.IdModel;

#endregion

namespace Yunt.Demo.ConsoleApp1
{
    /// <summary>
    ///      Iot DbContext
    /// </summary>
    public partial class AnalysisContext : DbContext
    {
        public AnalysisContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured == false)
            {
                options.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_analysis;uid=wdd;pwd=Unitoon2018;");
            }

        }

        public virtual DbSet<EventKind> EventKind { get; set; }
        public virtual DbSet<MotorEventLog> MotorEventLog { get; set; }
        public virtual DbSet<AlarmInfo> AlarmInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region 创建组合索引
            modelBuilder.Entity<EventKind>().HasIndex(u => new { u.Code, u.MotorTypeId });
            modelBuilder.Entity<EventKind>().Property(u => u.MotorTypeId).HasMaxLength(5).IsRequired();
            modelBuilder.Entity<EventKind>().Property(u => u.Code).HasMaxLength(12).IsRequired();
            modelBuilder.Entity<EventKind>().Property(u => u.Regulation).HasMaxLength(200);
            modelBuilder.Entity<EventKind>().Property(u => u.Description).HasMaxLength(200);

            modelBuilder.Entity<MotorEventLog>().HasIndex(u => new { u.ProductionLineId, u.MotorId });
            modelBuilder.Entity<MotorEventLog>().Property(u => u.ProductionLineId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<MotorEventLog>().Property(u => u.MotorId).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<MotorEventLog>().Property(u =>u.EventCode ).HasMaxLength(12);
            modelBuilder.Entity<MotorEventLog>().Property(u => u.Description).HasMaxLength(200);
            modelBuilder.Entity<MotorEventLog>().Property(u => u.MotorName).HasMaxLength(20);
            modelBuilder.Entity<AlarmInfo>().Property(u => u.MotorName).HasMaxLength(20);
            modelBuilder.Entity<AlarmInfo>().Property(u => u.MotorId).HasMaxLength(20);
            modelBuilder.Entity<AlarmInfo>().Property(u => u.Content).HasMaxLength(50);
            modelBuilder.Entity<AlarmInfo>().Property(u => u.Remark).HasMaxLength(50);
            #endregion



        }

     


    }

    #region code first
    public class DeviceContextFactory : IDesignTimeDbContextFactory<AnalysisContext>
    {
        public AnalysisContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnalysisContext>();
            optionsBuilder.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_analysis;uid=wdd;pwd=Unitoon2018;");

            return new AnalysisContext(optionsBuilder.Options);
        }
    }


    #endregion
}