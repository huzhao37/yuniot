using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Yunt.Common;
using Yunt.Inventory.Repository.EF.Mappings;
using Yunt.Inventory.Repository.EF.Models;
using Yunt.Inventory.Repository.EF.Models.IdModel;

namespace Yunt.Demo.ConsoleApp1
{
   public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }
        public virtual DbSet<SpareParts> SpareParts { get; set; }
        public virtual DbSet<SparePartsType> SparePartsType { get; set; }
        public virtual DbSet<SparePartsIdFactories> SparePartsIdFactories { get; set; }
        public virtual DbSet<WareHouses> WareHouses { get; set; }
        public virtual DbSet<InventoryAlarmInfo> InventoryAlarmInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryAlarmInfo>().Property(u => u.SparePartsId).HasMaxLength(20);

            modelBuilder.Entity<SpareParts>().Property(u => u.Description).HasMaxLength(50);
            modelBuilder.Entity<SpareParts>().Property(u => u.FactoryInfo).HasMaxLength(50);
            modelBuilder.Entity<SpareParts>().Property(u => u.InOperator).HasMaxLength(10);
            modelBuilder.Entity<SpareParts>().Property(u => u.MotorId).HasMaxLength(20);
            modelBuilder.Entity<SpareParts>().Property(u => u.OutOperator).HasMaxLength(10);
            modelBuilder.Entity<SpareParts>().Property(u => u.SparePartsId).HasMaxLength(20);
            modelBuilder.Entity<SpareParts>().Property(u => u.SparePartsName).HasMaxLength(20);
            modelBuilder.Entity<SpareParts>().Property(u => u.SparePartsTypeId).HasMaxLength(10);
            modelBuilder.Entity<SpareParts>().Property(u => u.WareHousesId).HasMaxLength(20);
            modelBuilder.Entity<SparePartsIdFactories>().Property(u => u.SparePartsTypeId).HasMaxLength(10);
            modelBuilder.Entity<SparePartsType>().Property(u => u.SparePartsTypeId).HasMaxLength(10);
            modelBuilder.Entity<SparePartsType>().Property(u => u.Name).HasMaxLength(10);
            modelBuilder.Entity<WareHouses>().Property(u => u.Keeper).HasMaxLength(10);
            modelBuilder.Entity<WareHouses>().Property(u => u.MotorTypeId).HasMaxLength(4);
            modelBuilder.Entity<WareHouses>().Property(u => u.Name).HasMaxLength(20);
            modelBuilder.Entity<WareHouses>().Property(u => u.Remark).HasMaxLength(50);
            modelBuilder.Entity<WareHouses>().Property(u => u.WareHousesId).HasMaxLength(20);
        }


    }

    #region code first
    public class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryContext>
    {
        public InventoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();
            optionsBuilder.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_inventory;uid=wdd;pwd=Unitoon2018;");

            return new InventoryContext(optionsBuilder.Options);
        }
    }


    #endregion
}
