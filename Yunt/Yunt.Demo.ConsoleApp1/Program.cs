using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewLife;
using NewLife.Log;
using NewLife.Reflection;
using Quartz;
using Quartz.Impl;
//using Yunt.Auth.Domain.IRepository;
using Yunt.Common;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;
//using Yunt.Device.Domain.IRepository;
//using Yunt.Device.Domain.Model;
//using Yunt.Redis;
//using Yunt.Redis.Config;
namespace Yunt.Demo.ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            XTrace.UseConsole(true, true);
            
            //var services = new ServiceCollection();
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            //var configuration = builder.Build();
            //services.AddSingleton<IConfiguration>(configuration);

       

            #region dtsc data init
          


            #endregion

          

            #region test
            //var services = new ServiceCollection();
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            //var configuration = builder.Build();
            //services.AddSingleton<IConfiguration>(configuration);

            #region obselete

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


            #endregion

            var watcher = new FileSystemWatcher
            {
                Path = Path.GetFullPath(".\\..\\Yunt.Jobs"),
                NotifyFilter = NotifyFilters.Attributes |
                               NotifyFilters.CreationTime |
                               NotifyFilters.DirectoryName |
                               NotifyFilters.FileName |
                               NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.Security |
                               NotifyFilters.Size,
                Filter = "*.dll"
            };

            var dic = new Dictionary<string, object>();
            //dic.Add("x1",false);
            // quartz运行时，可以添加新job，但不能覆盖，删除等
            watcher.Created += new FileSystemEventHandler((o, e) =>
            {

                //var upTime=File.GetLastWriteTime(e.FullPath);
                // var asm = Assembly.LoadFile(e.FullPath);
                //创建dll文件（覆盖模式）
                try
                {
                    using (var fs = new FileStream(e.FullPath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        var asm = Assembly.Load(fs.ReadBytes());

                        foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                        {
                            JobHelper.JoinToQuartz(type, DateTimeOffset.Now, dic);
                        }
                    }
                }
                catch (Exception ex)//可预见异常
                {
                    using (var fs = new FileStream(e.FullPath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        var asm = Assembly.Load(fs.ReadBytes());

                        foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                        {
                            JobHelper.JoinToQuartz(type, DateTimeOffset.Now, dic);
                        }
                    }
                }

            });
            watcher.Renamed += new RenamedEventHandler((o, e) =>
            {
                try
                {
                    using (var fs = new FileStream(e.FullPath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        var asm = Assembly.Load(fs.ReadBytes());

                        foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                        {
                            JobHelper.DelteToQuartz(type);
                        }
                    }
                }
                catch (Exception ex)//可预见异常
                {
                    using (var fs = new FileStream(e.FullPath, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        var asm = Assembly.Load(fs.ReadBytes());

                        foreach (var type in asm.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                        {
                            JobHelper.DelteToQuartz(type);
                        }
                    }
                }

                File.Delete(e.FullPath);

            });



            //Start monitoring.
            watcher.EnableRaisingEvents = true;

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
            //var services = new ServiceCollection();
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
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            //var configuration = builder.Build();
            //services.AddSingleton<IConfiguration>(configuration);

            ////services.AddOptions();
            ////  services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            //var providers = ServiceEx.StartServices(services, configuration);
            //services.AddAutoMapper();
            ////var authProvider=Register(services);
            //// var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryRepository>(providers["Device"]);
            ////tbService.Insert(new Auth.Domain.Model.TbCategory() { Categoryname = "test1" });

            //// var x2 = tbService.GetEntities();
            //var cy = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(providers["Device"]);

            #region 时序数据缓存        

            //var list = new List<Conveyor>();
            //var time = DateTime.Now.Date;
            //var t = time;
            //for (var i = 0; i < 60; i++)
            //{
            //    for (var j = 0; j < 30; j++)
            //    {
            //        t = time.AddDays(j);
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = i.ToString(), Time = t.TimeSpan() });
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = i.ToString(), Time = t.TimeSpan() });
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = i.ToString(), Time = t.TimeSpan() });
            //    }

            //}

            //var x = cy.GetLatestRecord("7");
            //Console.WriteLine("ok");
            //Console.ReadKey();
            //var thread1 = new Thread(() =>
            //  {
            //      for (var i = 0; i < 10000; i++)
            //      {
            //          cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "1", Time = DateTime.Now.TimeSpan() });
            //          System.Threading.Thread.Sleep(1);
            //      }
            //  });
            //thread1.Start();
            //var thread2 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "2", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread2.Start();
            //var thread3 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "3", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread3.Start();
            //var thread4 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "4", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread4.Start();
            //var thread5 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "5", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread5.Start();
            //var thread6 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "6", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread6.Start();
            //var thread7 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "7", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread7.Start();
            //var thread8 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "8", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread8.Start();
            //var thread9 = new Thread(() =>
            //{
            //    for (var i = 0; i < 10000; i++)
            //    {
            //        cy.InsertAsync(new Conveyor() { Current_B = i, MotorId = "9", Time = DateTime.Now.TimeSpan() });
            //        System.Threading.Thread.Sleep(1);
            //    }
            //});
            //thread9.Start();

            #endregion

            ////var m = ServiceProviderServiceExtensions.GetService<IMotorRepository>(providers["Device"]);

            //try
            //{
            //    //m.Insert(new Motor() { MotorTypeId = "CY", ProductionLineId = "WDD-P000001" });
            //    //cy.Insert(new Conveyor() {Current = 8, MotorId = "1"});
            //    //var x = cy.GetEntities();
            //    // cy.GetEntities(e => e.IsDeleted, e => e.MotorId == "");
            //    //var x = m.GetEntities();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}

            #endregion

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }




        #region obselete

        /// <summary>
        /// 启动所有的服务插件
        /// </summary>
        /// <param name="services"></param>
        public static Dictionary<string, IServiceProvider> StartServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            var providers = new Dictionary<string, IServiceProvider>();
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                FileEx.CopyFolderTo(path + "commondll", path);

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

                            method.Invoke(obj, new object[] { services, configuration });
                            var serviceProvider = services.BuildServiceProvider();
                            method2.Invoke(obj, new object[] { serviceProvider });

                            var attribute = d.GetCustomAttributes(typeof(ServiceAttribute), false).FirstOrDefault();

                            if (attribute != null)
                            {
                                providers.Add(((ServiceAttribute)attribute).Name.ToString(), serviceProvider); ;
                            }

                        });

                    }
                }

            }
            catch (Exception ex)
            {
                Common.Logger.Exception(ex);
            }
            return providers;


        }


        #endregion


    }
}
