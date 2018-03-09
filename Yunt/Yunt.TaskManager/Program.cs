using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Yunt.TaskManager.Model;
using Yunt.TaskManager.Repository.Contract;
using Yunt.TaskManager.Repository.EF;
using Yunt.TaskManager.Repository.EF.Core;
using Yunt.TaskManager.Service;
using Yunt.TaskManager.Service.Contract;
using Logger = Yunt.TaskManager.Core.Logger;

namespace Yunt.TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            XTrace.UseConsole(true, false);

            //using (var db=new TaskManagerContext())
            //{

            //}


            //var contextOptionsBuilder = new DbContextOptionsBuilder<TaskManagerContext>();
            //contextOptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConsoleApp;Trusted_Connection=True;MultipleActiveResultSets=true;");

            //// 注入配置选项
            //using (var context = new TaskManagerContext(contextOptionsBuilder.Options))
            //{
            //    // TODO
            //}


            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDbContext<TaskManagerContext>(c =>
            //{
            //    c.UseSqlServer("Server=rm-bp1e7cw4xl2ns45aoo.sqlserver.rds.aliyuncs.com,3433; Database=dyd_bs_task;Persist Security Info=True;User ID=xenomorph;password=U2n0i1t7oon;");
            //});

            //var serviceProvider = serviceCollection.BuildServiceProvider();

            //using (var context = serviceProvider.GetService<TaskManagerContext>())
            //{
            //    //context.Database.Migrate();
            //    var list = context.TbTask.ToList();
            //}


            var services = new ServiceCollection();
            //注入

            //services.AddDbContextPool<TaskManagerContext>(c =>
            //{
            //    c.UseSqlServer("Server=rm-bp1e7cw4xl2ns45aoo.sqlserver.rds.aliyuncs.com,3433; Database=yunt_test;Persist Security Info=True;User ID=xenomorph;password=U2n0i1t7oon;");
            //});
            var contextOptions = new DbContextOptionsBuilder().UseSqlServer("Server=rm-bp1e7cw4xl2ns45aoo.sqlserver.rds.aliyuncs.com,3433; Database=yunt_test;Persist Security Info=True;User ID=xenomorph;password=U2n0i1t7oon;Connection Timeout=60;").Options;
            services.AddSingleton(contextOptions)
              .AddTransient<TaskManagerContext>();
            services.AddTransient<ITaskRepositoryBase<BaseModel>, TaskRepositoryBase<BaseModel>>();
            services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
            services.AddTransient<ITbCategoryService, TbCategoryService>();

            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ContextFactory.Init(serviceProvider);
            //解析
            var tbService = ServiceProviderServiceExtensions.GetService<ITbCategoryService>(serviceProvider);

            #region add test

            //var entities = new List<TbCategory>();
            //for (var i = 0; i < 8_000; i++)
            //{
            //    entities.Add(new TbCategory()
            //    {
            //        //Categoryname = i.ToString(),
            //        //Categorycreatetime = DateTime.Now
            //    });
            //}

            //var entities2 = new List<TbCategory>();
            //for (var i = 0; i < 8_000; i++)
            //{
            //    entities2.Add(new TbCategory()
            //    {
            //       // Categoryname = i.ToString(),
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
            var entities = tbService.Get();
            var deletes = new List<TbCategory>();
            var deletes2 = new List<TbCategory>();
            var deletes3 = new List<TbCategory>();
            var deletes4 = new List<TbCategory>();
            var count = 0;
            foreach (var tbCategory in entities)
            {
                count++;
                if (count <= 4_000)
                {
                    deletes.Add(tbCategory);
                    //deletes2.Add(tbCategory);
                    deletes3.Add(tbCategory);
                    deletes4.Add(tbCategory);
                }
                if (count > 4_000 && count <= 20_000)
                {
                    deletes2.Add(tbCategory);
                }

                //if (count > 20_000 && count <= 30_000)
                //{
                //    deletes3.Add(tbCategory);
                //}

                //if (count > 30_000 && count <= 40_000)
                //{
                //    deletes4.Add(tbCategory);
                //}

            }



            #endregion
            var sw = new Stopwatch();
            sw.Start();
            var task1 = Task.Factory.StartNew(() => tbService.DeleteAsync(deletes));
            var task2 = Task.Factory.StartNew(() => tbService.DeleteAsync(deletes2));
            var task3 = Task.Factory.StartNew(() => tbService.DeleteAsync(deletes3));
            var task4 = Task.Factory.StartNew(() => tbService.DeleteAsync(deletes4));
            Task.WaitAll(task1, task2, task3, task4);
            sw.Stop();
            Logger.Info($"删除记录：{10_000:n},耗时：{sw.ElapsedMilliseconds:n}ms");
            //var list = tbService.GetTbCategories(new System.DateTime(2017, 1, 1, 0, 0, 0), new System.DateTime(2018, 3, 1, 0, 0, 0), 1,
            //    5);
            //Console.WriteLine("Hello World!");
            //var test = tbService.Get();
            Console.ReadKey();

        }
    }
}
