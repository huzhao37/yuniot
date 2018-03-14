using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Quartz;
using Quartz.Impl;
using Yunt.Auth.Domain.BaseModel;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Repository.EF;
using Yunt.Auth.Repository.EF.Models;
using Yunt.Auth.Repository.EF.Repositories;
using Yunt.Demo.Core;
using Yunt.Redis;
using Yunt.Redis.Config;
namespace Yunt.Demo.ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            XTrace.UseConsole(true,false);

            #region test

            //Action<Type> JoinToQuartz = (type) =>
            //{
            //    var obj = Activator.CreateInstance(type);
            //    string cron = type.GetProperty("Cron").GetValue(obj).ToString();
            //    var jobDetail = JobBuilder.Create(type)
            //                              .WithIdentity(type.Name)
            //                              .Build();

            //    var jobTrigger = TriggerBuilder.Create()
            //                                   .WithIdentity(type.Name + "Trigger")
            //                                   .StartNow()
            //                                   .WithCronSchedule(cron)
            //                                   .Build();

            //    StdSchedulerFactory.GetDefaultScheduler().Result.ScheduleJob(jobDetail, jobTrigger);
            //    StdSchedulerFactory.GetDefaultScheduler().Result.Start();
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.WriteLine($"新添加了一个服务{nameof(type)}，通过心跳Job自动被加载！");
            //};


            //var watcher = new FileSystemWatcher();
            //watcher.Path = AppDomain.CurrentDomain.BaseDirectory;
            //watcher.NotifyFilter = NotifyFilters.Attributes |
            //                       NotifyFilters.CreationTime |
            //                       NotifyFilters.DirectoryName |
            //                       NotifyFilters.FileName |
            //                       NotifyFilters.LastAccess |
            //                       NotifyFilters.LastWrite |
            //                       NotifyFilters.Security |
            //                       NotifyFilters.Size;
            //watcher.Filter = "*.dll";
            //// quartz运行时，可以添加新job，但不能覆盖，删除等
            //watcher.Changed += new FileSystemEventHandler((o, e) =>
            //{
            //    foreach (var module in Assembly.LoadFile(e.FullPath).GetModules())
            //    {
            //        foreach (var type in module.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
            //        {
            //            JoinToQuartz(type);
            //        }
            //    }
            //});

            ////Start monitoring.
            //watcher.EnableRaisingEvents = true;  

            #endregion

            #region mysql test

            //var dbconn = new yunt_testContext();
            #region add test

            //var count = 10_000;
            //var entities = new List<Auth.Domain.Model.TbCategory>();
            //for (var i = 0; i < count; i++)
            //{
            //    entities.Add(new Auth.Domain.Model.TbCategory()
            //    {
            //        Categoryname = i.ToString(),
            //        Categorycreatetime = DateTime.Now
            //    });
            //}

            //var entities2 = new List<TbCategory>();
            //for (var i = 0; i < 8_000; i++)
            //{
            //    entities2.Add(new TbCategory()
            //    {
            //        // Categoryname = i.ToString(),
            //        //Categorycreatetime = DateTime.Now
            //    });
            //}
            //var entities3 = new List<TbCategory>();
            //for (var i = 0; i < 8_000; i++)
            //{
            //    entities3.Add(new TbCategory()
            //    {
            //        //Categoryname = i.ToString(),
            //        //Categorycreatetime = DateTime.Now
            //    });
            //}
            //var entities4 = new List<TbCategory>();
            //for (var i = 0; i < 8_000; i++)
            //{
            //    entities4.Add(new TbCategory()
            //    {
            //        //Categoryname = i.ToString(),
            //        //Categorycreatetime = DateTime.Now
            //    });
            //}
            //var task1 = Task.Factory.StartNew(() => tbService.AddAsyn(entities));
            //var task2 = Task.Factory.StartNew(() => tbService.AddAsyn(entities2));
            //var task3 = Task.Factory.StartNew(() => tbService.AddAsyn(entities3));
            //var task4 = Task.Factory.StartNew(() => tbService.AddAsyn(entities4));
            #endregion

            #region update test
            //var entities = tbService.Get();
            //var updates=new List<TbCategory>();
            //var updates2 = new List<TbCategory>();
            //var updates3 = new List<TbCategory>();
            //var updates4 = new List<TbCategory>();
            //var count = 0;
            //foreach (var tbCategory in entities)
            //{
            //    tbCategory.Categorycreatetime=DateTime.Now;
            //    count++;
            //    if (count <=10_000)
            //    {
            //        updates.Add(tbCategory);
            //        updates2.Add(tbCategory);
            //        updates3.Add(tbCategory);
            //        updates4.Add(tbCategory);

            //    }
            //if (count> 10_000 && count <= 20_000)
            //{
            //    updates2.Add(tbCategory);
            //}

            //if (count > 20_000 && count <= 30_000)
            //{
            //    updates3.Add(tbCategory);
            //}

            //if (count > 30_000 && count <= 40_000)
            //{
            //    updates4.Add(tbCategory);
            //}
            //var task1 = Task.Factory.StartNew(() => tbService.Update(updates));
            //var task2 = Task.Factory.StartNew(() => tbService.Update(updates2));
            //var task3 = Task.Factory.StartNew(() => tbService.Update(updates3));
            //var task4 = Task.Factory.StartNew(() => tbService.Update(updates4));

            //}



            #endregion

            #region delete test
            //var entities = tbService.Get();
            //var deletes = new List<TbCategory>();
            //var deletes2 = new List<TbCategory>();
            //var deletes3 = new List<TbCategory>();
            //var deletes4 = new List<TbCategory>();
            //var count = 0;
            //foreach (var tbCategory in entities)
            //{
            //    count++;
            //    if (count <= 4_000)
            //    {
            //        deletes.Add(tbCategory);
            //        //deletes2.Add(tbCategory);
            //        deletes3.Add(tbCategory);
            //        deletes4.Add(tbCategory);
            //    }
            //    if (count > 4_000 && count <= 20_000)
            //    {
            //        deletes2.Add(tbCategory);
            //    }

            //    //if (count > 20_000 && count <= 30_000)
            //    //{
            //    //    deletes3.Add(tbCategory);
            //    //}

            //    //if (count > 30_000 && count <= 40_000)
            //    //{
            //    //    deletes4.Add(tbCategory);
            //    //}

            //}



            #endregion
        
            #region domain
            var services = new ServiceCollection();
            //注入
            //AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<AutoMapperProfileConfiguration>();
            //});
            //services.AddSingleton(config);
            //services.AddScoped<IMapper, Mapper>();

            //services.AddAutoMapper();

            //var contextOptions = new DbContextOptionsBuilder().UseMySql("server=10.1.5.25;port=3306;database=yunt_test;uid=root;pwd=unitoon2017;").Options;
            //services.AddSingleton(contextOptions)
            //  .AddTransient<TaskManagerContext>();
            //services.AddTransient<ITaskRepositoryBase<AggregateRoot>, TaskRepositoryBase<AggregateRoot, BaseModel>>();
            //services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
            ////services.AddTransient<TbCategory, TbCategoryService>();
            //Register(services);
            ////构建容器
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            //ContextFactory.Init(serviceProvider);
            //解析
            //var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryRepository>(serviceProvider);

            // var count = tbService.GetEntityById(5775);//tbService.GetEntities(e=>e.Categoryname.Equals("1"),e=>e.Categorycreatetime);
            #endregion

            // var sw = new Stopwatch();
            // sw.Start();
            //dbconn.tb_category.AddRange(entities);
            //dbconn.SaveChanges();
            //var task2 = Task.Factory.StartNew(() => dbconn.tb_category.DeleteAsync(deletes2));
            //var task3 = Task.Factory.StartNew(() => dbconn.tb_category.DeleteAsync(deletes3));
            //var task4 = Task.Factory.StartNew(() => dbconn.tb_category.DeleteAsync(deletes4));
            //Task.WaitAll(task1, task2, task3, task4);

            // tbService.GetEntities(e => e.Categoryname.Equals("1"));//tbService.Insert(entities);
            //sw.Stop();
            // Logger.Info($"add记录：{count:n},耗时：{sw.ElapsedMilliseconds:n}ms,qps:{count / sw.ElapsedMilliseconds * 1_000:n}q/s");


            #endregion

            #region config/redis test

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            services.AddSingleton<IConfiguration>(configuration);

            #region easycahing inspect

            //services.AddDefaultRedisCache(option =>
            //{
            //    option.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
            //    //option.Password = "";
            //});
            ////specify to use protobuf serializer
            //services.AddDefaultProtobufSerializer();

            #endregion





            #region yunt-redis register
            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfileConfiguration>();
            });
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            services.AddAutoMapper();

            var contextOptions = new DbContextOptionsBuilder().UseMySql("server=10.1.5.25;port=3306;database=yunt_test;uid=root;pwd=unitoon2017;").Options;
            services.AddSingleton(contextOptions)
              .AddTransient<TaskManagerContext>();
            services.AddTransient<ITaskRepositoryBase<AggregateRoot>, TaskRepositoryBase<AggregateRoot, BaseModel>>();
            services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
            //services.AddTransient<TbCategory, TbCategoryService>();
            services.AddDefaultRedisCache(option =>
            {
                //option.Endpoints.Add(new Redis.Config.ServerEndPoint("127.0.0.1", 6379));
                option.RedisServer.Add(new HostItem() { Host = "127.0.0.1:6379" });
                //option.Password = "";
            });
            //specify to use protobuf serializer
            //services.AddDefaultProtobufSerializer();
            Register(services);
            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ContextFactory.Init(serviceProvider);
          
            var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryRepository>(serviceProvider);

            //redis.Set("yunty", new Auth.Domain.Model.TbCategory() { Categoryname = "test1", Categorycreatetime = DateTime.Now }, DataType.Protobuf);
            tbService.Insert(new Auth.Domain.Model.TbCategory() { Categoryname = "test1" });

            #endregion


            #endregion

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        public static void Register(IServiceCollection services)
        {
            var allTypes = Assembly.GetCallingAssembly().GetTypes();
            var type = typeof(ITaskRepositoryBase<>);//IBaseEntityService


            allTypes.Where(t => !t.IsGenericType && t.IsClass).ToList().ForEach(t =>
            {
                //var ins = t.GetInterfaces();
                //if (ins.Any(i => i.Name.Equals(type.Name)))
                //{
                //    var tfrom = ins.FirstOrDefault(i => !i.Name.Equals(type.Name));
                //    //注册到容器中
                //    services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
                //    services.Contains().Builder.RegisterType(t).As(tfrom).SingleInstance().PropertiesAutowired();

                //    //LogHelper.Trace("AhnqIot.Bussiness.Register:{0}=>{1}", tfrom.Name, t.Name);
                //}

            });
        }
    }
}
