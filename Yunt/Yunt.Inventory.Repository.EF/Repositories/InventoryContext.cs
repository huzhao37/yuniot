using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Yunt.Common;
using Yunt.Inventory.Repository.EF.Mappings;
using Yunt.Inventory.Repository.EF.Models;

namespace Yunt.Inventory.Repository.EF.Repositories
{
   public class InventoryContext : DbContext, IDisposable
    {
        public InventoryContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }
        public virtual DbSet<OutHouse> OutHouse { get; set; }
        public virtual DbSet<SparePartsType> SparePartsType { get; set; }
        public virtual DbSet<InHouse> InHouse { get; set; }
        public virtual DbSet<WareHouses> WareHouses { get; set; }
        public virtual DbSet<InventoryAlarmInfo> InventoryAlarmInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var namespaces = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            var typesToRegister = Assembly.Load("Yunt.Inventory.Repository.EF").GetTypes()
              .Where(type => !String.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                  && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            
        }
    }
}
