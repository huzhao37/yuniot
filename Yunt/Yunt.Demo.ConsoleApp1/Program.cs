using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Quartz;
using Quartz.Impl;
using Yunt.Demo.Core;

namespace Yunt.Demo.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Action<Type> JoinToQuartz = (type) =>
            {
                var obj = Activator.CreateInstance(type);
                string cron = type.GetProperty("Cron").GetValue(obj).ToString();
                var jobDetail = JobBuilder.Create(type)
                                          .WithIdentity(type.Name)
                                          .Build();

                var jobTrigger = TriggerBuilder.Create()
                                               .WithIdentity(type.Name + "Trigger")
                                               .StartNow()
                                               .WithCronSchedule(cron)
                                               .Build();

                StdSchedulerFactory.GetDefaultScheduler().Result.ScheduleJob(jobDetail, jobTrigger);
                StdSchedulerFactory.GetDefaultScheduler().Result.Start();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"新添加了一个服务{nameof(type)}，通过心跳Job自动被加载！");
            };


            var watcher = new FileSystemWatcher();
            watcher.Path = AppDomain.CurrentDomain.BaseDirectory;
            watcher.NotifyFilter = NotifyFilters.Attributes |
                                   NotifyFilters.CreationTime |
                                   NotifyFilters.DirectoryName |
                                   NotifyFilters.FileName |
                                   NotifyFilters.LastAccess |
                                   NotifyFilters.LastWrite |
                                   NotifyFilters.Security |
                                   NotifyFilters.Size;
            watcher.Filter = "*.dll";
            // quartz运行时，可以添加新job，但不能覆盖，删除等
            watcher.Changed += new FileSystemEventHandler((o, e) =>
            {
                foreach (var module in Assembly.LoadFile(e.FullPath).GetModules())
                {
                    foreach (var type in module.GetTypes().Where(i => i.BaseType == typeof(JobBase)))
                    {
                        JoinToQuartz(type);
                    }
                }
            });

            //Start monitoring.
            watcher.EnableRaisingEvents = true;


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
