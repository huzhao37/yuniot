using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Yunt.Auth.Repository.EF.Models;
using Yunt.Auth.Repository.EF.Models.IdModel;
using Yunt.Common;

namespace Yunt.Demo.ConsoleApp1
{
   public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserIdFactories> UserIdFactories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.LoginAccount).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LoginPwd).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserRoleId).IsRequired();
            modelBuilder.Entity<User>().Property(u =>  u.UserId).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LoginAccount).HasMaxLength(10);
            modelBuilder.Entity<User>().Property(u => u.LoginPwd).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(u => u.Mail).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.MobileNo).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(u => u.Remark).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(10);

            modelBuilder.Entity<UserRole>().Property(u => u.Desc).IsRequired();
            modelBuilder.Entity<UserRole>().Property(u => u.Remark).HasMaxLength(100);
            modelBuilder.Entity<UserRole>().Property(u => u.Desc).HasMaxLength(20);
            modelBuilder.Entity<UserIdFactories>().Property(u => u.UserIndex).IsRequired();
        }


    }

    #region code first
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseMySql("server=rm-wz9mrn9kj0lt0r0i18o.mysql.rds.aliyuncs.com;port=3306;database=yunt_auth;uid=wdd;pwd=Unitoon2018;");

            return new AuthContext(optionsBuilder.Options);
        }
    }


    #endregion
}
