

#region using namespace

using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Yunt.Device.Repository.EF.Models;
using Yunt.Device.Repository.EF.Models.IdModel;

#endregion

namespace Yunt.Demo.ConsoleApp1
{
    /// <summary>
    ///      Iot DbContext
    /// </summary>
    public partial class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions options) : base(options)
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
                options.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_device;uid=wdd;pwd=Unitoon2018;");
            }

        }

        public virtual DbSet<ProductionLine> ProductionLine { get; set; }
        public virtual DbSet<HVib> HVib { get; set; }
        public virtual DbSet<HVibByHour> HVibByHour { get; set; }
        public virtual DbSet<HVibByDay> HVibByDay { get; set; }
        public virtual DbSet<MotorParams> MotorParams { get; set; }
        public virtual DbSet<MotorIdFactories> MotorIdFactories { get; set; }
        public virtual DbSet<Motor> Motor { get; set; }
        public virtual DbSet<MotorType> MotorType { get; set; }
        public virtual DbSet<ConeCrusher> ConeCrusher { get; set; }
        public virtual DbSet<ConeCrusherByDay> ConeCrusherByDay { get; set; }
        public virtual DbSet<ConeCrusherByHour> ConeCrusherByHour { get; set; }
        public virtual DbSet<Conveyor> Conveyor { get; set; }
        public virtual DbSet<ConveyorByDay> ConveyorByDay { get; set; }
        public virtual DbSet<ConveyorByHour> ConveyorByHour { get; set; }
        public virtual DbSet<DoubleToothRollCrusher> DoubleToothRollCrusher { get; set; }
        public virtual DbSet<DoubleToothRollCrusherByDay> DoubleToothRollCrusherByDay { get; set; }
        public virtual DbSet<DoubleToothRollCrusherByHour> DoubleToothRollCrusherByHour { get; set; }
        public virtual DbSet<ImpactCrusher> ImpactCrusher { get; set; }
        public virtual DbSet<ImpactCrusherByDay> ImpactCrusherByDay { get; set; }
        public virtual DbSet<ImpactCrusherByHour> ImpactCrusherByHour { get; set; }
        public virtual DbSet<JawCrusher> JawCrusher { get; set; }
        public virtual DbSet<JawCrusherByDay> JawCrusherByDay { get; set; }
        public virtual DbSet<JawCrusherByHour> JawCrusherByHour { get; set; }

        public virtual DbSet<MaterialFeeder> MaterialFeeder { get; set; }
        public virtual DbSet<MaterialFeederByDay> MaterialFeederByDay { get; set; }
        public virtual DbSet<MaterialFeederByHour> MaterialFeederByHour { get; set; }

        public virtual DbSet<Pulverizer> Pulverizer { get; set; }
        public virtual DbSet<PulverizerByDay> PulverizerByDay { get; set; }
        public virtual DbSet<PulverizerByHour> PulverizerByHour { get; set; }

        public virtual DbSet<ReverHammerCrusher> ReverHammerCrusher { get; set; }
        public virtual DbSet<ReverHammerCrusherByDay> ReverHammerCrusherByDay { get; set; }
        public virtual DbSet<ReverHammerCrusherByHour> ReverHammerCrusherByHour { get; set; }
        public virtual DbSet<SimonsConeCrusher> SimonsConeCrusher { get; set; }
        public virtual DbSet<SimonsConeCrusherByDay> SimonsConeCrusherByDay { get; set; }
        public virtual DbSet<SimonsConeCrusherByHour> SimonsConeCrusherByHour { get; set; }
        public virtual DbSet<VerticalCrusher> VerticalCrusher { get; set; }
        public virtual DbSet<VerticalCrusherByDay> VerticalCrusherByDay { get; set; }
        public virtual DbSet<VerticalCrusherByHour> VerticalCrusherByHour { get; set; }
        public virtual DbSet<Vibrosieve> Vibrosieve { get; set; }
        public virtual DbSet<VibrosieveByDay> VibrosieveByDay { get; set; }
        public virtual DbSet<VibrosieveByHour> VibrosieveByHour { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region 创建组合索引
            modelBuilder.Entity<MotorParams>().HasIndex(u => new { u.MotorTypeId, u.Param, u.Description });
            modelBuilder.Entity<MotorParams>().Property(u => u.MotorTypeId).HasMaxLength(4).IsRequired();
            modelBuilder.Entity<MotorParams>().Property(u => u.Param).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<MotorParams>().Property(u => u.Description).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<MotorIdFactories>().HasIndex(u => new { u.MotorIndex, u.MotorTypeId, u.ProductionLineId });
            modelBuilder.Entity<MotorIdFactories>().Property(u => u.MotorIndex).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<MotorIdFactories>().Property(u => u.MotorTypeId).HasMaxLength(4).IsRequired();
            modelBuilder.Entity<MotorIdFactories>().Property(u => u.ProductionLineId).HasMaxLength(15).IsRequired();

            modelBuilder.Entity<ConeCrusher>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ConeCrusher>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ConeCrusherByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ConeCrusherByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ConeCrusherByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ConeCrusherByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<HVib>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<HVib>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<HVibByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<HVibByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<HVibByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<HVibByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Motor>().HasIndex(u => new { u.MotorId, u.MotorTypeId,u.EmbeddedDeviceId });
            modelBuilder.Entity<Motor>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Motor>().Property(u => u.MotorTypeId).HasMaxLength(4).IsRequired();
            modelBuilder.Entity<Motor>().Property(u => u.EmbeddedDeviceId).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<MotorType>().HasIndex(u => u.MotorTypeId);
            modelBuilder.Entity<MotorType>().Property(u => u.MotorTypeId).HasMaxLength(4).IsRequired();
            modelBuilder.Entity<Conveyor>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<Conveyor>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ConveyorByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ConveyorByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ConveyorByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ConveyorByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();        
            modelBuilder.Entity<ImpactCrusher>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ImpactCrusher>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ImpactCrusherByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ImpactCrusherByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<ImpactCrusherByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<ImpactCrusherByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<JawCrusher>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<JawCrusher>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<JawCrusherByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<JawCrusherByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<JawCrusherByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<JawCrusherByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<MaterialFeeder>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<MaterialFeeder>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<MaterialFeederByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<MaterialFeederByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<MaterialFeederByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<MaterialFeederByDay>().HasIndex(u => new {u.Time , u.MotorId });
            //modelBuilder.Entity<Motor>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            //modelBuilder.Entity<MaterialFeederByHour>().HasIndex(u => new {u.Time , u.MotorId });
            //modelBuilder.Entity<Motor>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            //modelBuilder.Entity<MaterialFeederByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<Pulverizer>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Pulverizer>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<PulverizerByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<PulverizerByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<PulverizerByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<PulverizerByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<VerticalCrusher>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<VerticalCrusher>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<VerticalCrusherByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<VerticalCrusherByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<VerticalCrusherByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<VerticalCrusherByHour>().HasIndex(u => new {u.Time , u.MotorId });

            modelBuilder.Entity<Vibrosieve>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<Vibrosieve>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<VibrosieveByDay>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<VibrosieveByDay>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<VibrosieveByHour>().HasIndex(u => new {u.Time , u.MotorId });
            modelBuilder.Entity<VibrosieveByHour>().Property(u => u.MotorId).HasMaxLength(15).IsRequired();
            #endregion

            // var namespaces = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            //var typesToRegister = Assembly.Load("Yunt.Device.Repository.EF").GetTypes()
            //  .Where(type => !String.IsNullOrEmpty(type.Namespace))
            //  .Where(type => type.BaseType != null && type.BaseType.IsGenericType
            //      && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.ApplyConfiguration(configurationInstance);
            //}

        }

     


    }

    #region code first
    public class DeviceContextFactory : IDesignTimeDbContextFactory<DeviceContext>
    {
        public DeviceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>();
            optionsBuilder.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_device;uid=wdd;pwd=Unitoon2018;");

            return new DeviceContext(optionsBuilder.Options);
        }
    }


    #endregion
}