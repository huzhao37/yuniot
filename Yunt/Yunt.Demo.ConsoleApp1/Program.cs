using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewLife.Log;
using Quartz;
using Quartz.Impl;
using Yunt.Common;
using Yunt.Demo.ConsoleApp1.Migrations;
using Yunt.Demo.Core;
using Yunt.Redis;
using Yunt.Redis.Config;
namespace Yunt.Demo.ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            XTrace.UseConsole(true, false);

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


            //var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryRepository>(serviceProvider);

            //redis.Set("yunty", new Auth.Domain.Model.TbCategory() { Categoryname = "test1", Categorycreatetime = DateTime.Now }, DataType.Protobuf);
            // tbService.Insert(new Auth.Domain.Model.TbCategory() { Categoryname = "test1" });

            #endregion

            #region register
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            var providers = StartServices(services,configuration);

            //var authProvider=Register(services);
            //var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryRepository>(authProvider);
            //tbService.Insert(new Auth.Domain.Model.TbCategory() { Categoryname = "test1" });
            #endregion

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        /// <summary>
        /// 启动所有的服务插件
        /// </summary>
        /// <param name="services"></param>
        public static Dictionary<string, IServiceProvider> StartServices(IServiceCollection services,IConfigurationRoot configuration)
        {
            var providers = new Dictionary<string, IServiceProvider>();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            FileEx.MoveFolderTo(path + "commondll", path);

            //FileEx.TryLoadAssembly();
            var files = new DirectoryInfo(path).GetFiles();

            foreach (var f in files)
            {
                if (f.Name.Contains(".dll") && f.Name.Contains("Repository"))
                {
                    var dll = Assembly.LoadFrom(f.FullName);

                    var types = dll.GetTypes().Where(a => a.IsClass && a.Name.Equals("BootStrap"));

                    types.ToList().ForEach(d =>
                    {
                        var method = d.GetMethod("Start", BindingFlags.Instance | BindingFlags.Public);

                        var method2 = d.GetMethod("ContextInit", BindingFlags.Instance | BindingFlags.Public);
                        var obj = Activator.CreateInstance(d);
                        try
                        {
                            method.Invoke(obj, new object[] { services ,configuration});
                            var serviceProvider = services.BuildServiceProvider();
                            method2.Invoke(obj, new object[] { serviceProvider });

                            var attribute = d.GetCustomAttributes(typeof(ServiceAttribute), false).FirstOrDefault();

                            if (attribute != null)
                            {
                                providers.Add(((ServiceAttribute)attribute).Name.ToString(), serviceProvider); ;
                            }
                        }
                        catch (Exception ex)
                        {

                            Common.Logger.Exception(ex);
                        }
                        
                    });

                }
            }
            return providers;


        }

        /// <summary>
        /// auth初始化
        /// </summary>
        /// <param name="services"></param>
        public static IServiceProvider Register(IServiceCollection services)
        {
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

            //var currentpath = AppDomain.CurrentDomain.BaseDirectory;
            //var allTypes = Assembly.LoadFrom($"{currentpath}Yunt.Auth.Repository.EF.dll").GetTypes();
            //var type = typeof(ITaskRepositoryBase<>);

            //allTypes.Where(t =>  t.IsClass).ToList().ForEach(t =>
            //{
            //    var ins = t.GetInterfaces();
            //    if (!ins.Any(e => e.Name.Equals(type.Name))) return;
            //    if (ins.Length >= 2)
            //    {
            //        foreach (var i in ins)
            //        {
            //            if (!i.Name.Equals(type.Name))
            //            {
            //                //services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
            //                services.TryAddTransient(i,t );
            //            }

            //        }
            //    }
            //    else
            //    {
            //        foreach (var i in ins)
            //        {
            //            if (!i.Name.Equals(type.Name)) continue;
            //           // services.AddTransient<ITaskRepositoryBase<AggregateRoot>, TaskRepositoryBase<AggregateRoot, BaseModel>>();
            //            var arg =new Type[]{ typeof(AggregateRoot),typeof(BaseModel)};                          
            //            var tp= t.MakeGenericType(arg);                         
            //            services.TryAddTransient(i, tp);
            //        }
            //    }
            //});

            //services.AddDefaultRedisCache(option =>
            //{
            //    option.RedisServer.Add(new HostItem() { Host = "127.0.0.1:6379" });
            //    option.SingleMode = true;
            //    //option.Password = "";
            //});

            //构建容器
            var serviceProvider = services.BuildServiceProvider();
            //ContextFactory.Init(serviceProvider);
            return serviceProvider;
        }
    }
}
