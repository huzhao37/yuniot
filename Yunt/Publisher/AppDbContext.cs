using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Publisher.Models;

namespace Publisher
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }
        public virtual DbSet<Person> Person { get; set; }
    

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Age)
                    .HasColumnName("Age")
                    .HasColumnType("int");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(25);

            });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("Server = 10.1.5.25, 1433; Database = test; Persist Security Info = True; User ID = sa; password = Password01!;");
        //}
    }
}
