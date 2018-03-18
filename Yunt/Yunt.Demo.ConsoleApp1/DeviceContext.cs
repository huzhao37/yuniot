

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
                options.UseMySql("server=10.1.5.25;port=3306;database=yunt_device;uid=root;pwd=unitoon2017;");
            }

        }
        public virtual DbSet<MotorIdFactories> MotorIdFactories { get; set; }
        public virtual DbSet<Motor> Motor { get; set; }
        public virtual DbSet<Motortype> Motortype { get; set; }
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

            //base.OnModelCreating(modelBuilder);
            //var namespaces = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
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
            optionsBuilder.UseMySql("server=10.1.5.25;port=3306;database=yunt_device;uid=root;pwd=unitoon2017;");

            return new DeviceContext(optionsBuilder.Options);
        }
    }


    #endregion
}