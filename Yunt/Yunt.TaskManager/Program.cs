using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.TaskManager.Model;
using Yunt.TaskManager.Repository.Contract;
using Yunt.TaskManager.Repository.EF;
using Yunt.TaskManager.Repository.EF.Core;
using Yunt.TaskManager.Service;
using Yunt.TaskManager.Service.Contract;

namespace Yunt.TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {

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
            services.AddDbContext<TaskManagerContext>(c =>
            {
                c.UseSqlServer("Server=rm-bp1e7cw4xl2ns45aoo.sqlserver.rds.aliyuncs.com,3433; Database=dyd_bs_task;Persist Security Info=True;User ID=xenomorph;password=U2n0i1t7oon;");
            });
            services.AddTransient<ITaskRepositoryBase<BaseModel>, TaskRepositoryBase<BaseModel>>();
            services.AddTransient<ITbCategoryRepository, TbCategoryRepository<TbCategory>>();
            services.AddTransient<ITbCategoryService, TbCategoryService>();
            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            //解析
            var tbService = serviceProvider.GetService<ITbCategoryService>();

            var list = tbService.GetTbCategories(new DateTime(2017, 1, 1, 0, 0, 0), new DateTime(2018, 3, 1, 0, 0, 0), 1,
                5);
            Console.WriteLine("Hello World!");
            var test = tbService.Get();
            Console.ReadKey();
            
        }
    }
}
