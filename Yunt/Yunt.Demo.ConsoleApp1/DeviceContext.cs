

#region using namespace

using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var namespaces = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            var typesToRegister = Assembly.Load("Yunt.Device.Repository.EF").GetTypes()
              .Where(type => !String.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                  && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            //base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new ConeCrusherMapping());
        }

     


    }
}