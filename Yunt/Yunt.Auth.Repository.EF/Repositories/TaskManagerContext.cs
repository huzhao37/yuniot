using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Yunt.Auth.Repository.EF.Models;

namespace Yunt.Auth.Repository.EF.Repositories
{
    public partial class TaskManagerContext : DbContext, IDisposable
    {
        public TaskManagerContext(DbContextOptions options) : base(options)
        {
            this.Database.SetCommandTimeout(60);
            //设置sql命令执行超时时间,合理设置时间可以最大化利用当前资源,
            //当前时间本地调试情况下，仅同时4个线程对10000条相同记录进行修改所需的时间，若出现命令执行超时的异常
            //，应该考虑从业务本身优化入手
        }
        public virtual DbSet<TbCategory> tb_category { get; set; }
        public virtual DbSet<TbCommand> tb_command { get; set; }
        public virtual DbSet<TbConfig> tb_config { get; set; }
        public virtual DbSet<TbError> tb_error { get; set; }
        public virtual DbSet<TbLog> tb_log { get; set; }
        public virtual DbSet<TbNode> tbnode { get; set; }
        public virtual DbSet<TbPerformance> tb_performance { get; set; }
        public virtual DbSet<TbTask> tb_task { get; set; }
        public virtual DbSet<TbTempdata> tb_tempdata { get; set; }
        public virtual DbSet<TbUser> tb_user { get; set; }
        public virtual DbSet<TbVersion> tb_version { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=10.1.5.25;port=3306;database=yunt_test;uid=root;pwd=unitoon2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.ToTable("tb_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categorycreatetime)
                    .HasColumnName("categorycreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasColumnName("categoryname")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbCommand>(entity =>
            {
                entity.ToTable("tb_command");

                entity.HasIndex(e => e.Nodeid);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Command)
                    .IsRequired()
                    .HasColumnName("command")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Commandcreatetime)
                    .HasColumnName("commandcreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Commandname)
                    .IsRequired()
                    .HasColumnName("commandname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Commandstate).HasColumnName("commandstate");

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");
            });

            modelBuilder.Entity<TbConfig>(entity =>
            {
                entity.ToTable("tb_config");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Configkey)
                    .IsRequired()
                    .HasColumnName("configkey")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Configvalue)
                    .IsRequired()
                    .HasColumnName("configvalue")
                    .IsUnicode(false);

                entity.Property(e => e.Istest).HasColumnName("istest");

                entity.Property(e => e.Lastupdatetime)
                    .HasColumnName("lastupdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasColumnName("remark")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbError>(entity =>
            {
                entity.ToTable("tb_error");

                entity.HasIndex(e => e.Errorcreatetime);

                entity.HasIndex(e => e.Errortype);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Errorcreatetime)
                    .HasColumnName("errorcreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Errortype).HasColumnName("errortype");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasColumnName("msg")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");
            });

            modelBuilder.Entity<TbLog>(entity =>
            {
                entity.ToTable("tb_log");

                entity.HasIndex(e => e.Logcreatetime);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Logcreatetime)
                    .HasColumnName("logcreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Logtype).HasColumnName("logtype");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasColumnName("msg")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");
            });

            modelBuilder.Entity<TbNode>(entity =>
            {
                entity.ToTable("tb_node");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ifcheckstate).HasColumnName("ifcheckstate");

                entity.Property(e => e.Nodecreatetime)
                    .HasColumnName("nodecreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nodeip)
                    .IsRequired()
                    .HasColumnName("nodeip")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nodelastupdatetime)
                    .HasColumnName("nodelastupdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nodename)
                    .IsRequired()
                    .HasColumnName("nodename")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPerformance>(entity =>
            {
                entity.ToTable("tb_performance");

                entity.HasIndex(e => e.Taskid);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cpu).HasColumnName("cpu");

                entity.Property(e => e.Installdirsize).HasColumnName("installdirsize");

                entity.Property(e => e.Lastupdatetime)
                    .HasColumnName("lastupdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Memory).HasColumnName("memory");

                entity.Property(e => e.Nodeid)
                    .HasColumnName("nodeid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");
            });

            modelBuilder.Entity<TbTask>(entity =>
            {
                entity.ToTable("tb_task");

                entity.HasIndex(e => e.Nodeid);

                entity.HasIndex(e => e.Taskstate);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Nodeid).HasColumnName("nodeid");

                entity.Property(e => e.Taskappconfigjson)
                    .IsRequired()
                    .HasColumnName("taskappconfigjson")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Taskcreatetime)
                    .HasColumnName("taskcreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Taskcreateuserid).HasColumnName("taskcreateuserid");

                entity.Property(e => e.Taskcron)
                    .IsRequired()
                    .HasColumnName("taskcron")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Taskerrorcount)
                    .HasColumnName("taskerrorcount");

                entity.Property(e => e.Tasklastendtime)
                    .HasColumnName("tasklastendtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tasklasterrortime)
                    .HasColumnName("tasklasterrortime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tasklaststarttime)
                    .HasColumnName("tasklaststarttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Taskmainclassdllfilename)
                    .IsRequired()
                    .HasColumnName("taskmainclassdllfilename")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Taskmainclassnamespace)
                    .IsRequired()
                    .HasColumnName("taskmainclassnamespace")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Taskname)
                    .IsRequired()
                    .HasColumnName("taskname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Taskremark)
                    .IsRequired()
                    .HasColumnName("taskremark")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Taskruncount)
                    .HasColumnName("taskruncount");

                entity.Property(e => e.Taskstate).HasColumnName("taskstate");

                entity.Property(e => e.Taskupdatetime)
                    .HasColumnName("taskupdatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Taskversion).HasColumnName("taskversion");
            });

            modelBuilder.Entity<TbTempdata>(entity =>
            {
                entity.ToTable("tb_tempdata");

                entity.HasIndex(e => e.Taskid);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Tempdatajson)
                    .IsRequired()
                    .HasColumnName("tempdatajson")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tempdatalastupdatetime)
                    .HasColumnName("tempdatalastupdatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tb_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Usercreatetime)
                    .HasColumnName("usercreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Useremail)
                    .IsRequired()
                    .HasColumnName("useremail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Userrole).HasColumnName("userrole");

                entity.Property(e => e.Userstaffno)
                    .IsRequired()
                    .HasColumnName("userstaffno")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Usertel)
                    .IsRequired()
                    .HasColumnName("usertel")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbVersion>(entity =>
            {
                entity.ToTable("tb_version");

                entity.HasIndex(e => e.Taskid);

                entity.HasIndex(e => e.Version);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.Versioncreatetime)
                    .HasColumnName("versioncreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Zipfile)
                    .IsRequired()
                    .HasColumnName("zipfile")
                    .HasColumnType("mediumblob ");

                entity.Property(e => e.Zipfilename)
                    .HasColumnName("zipfilename")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
