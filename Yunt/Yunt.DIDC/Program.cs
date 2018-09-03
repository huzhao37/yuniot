using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using NewLife.Threading;
using Quartz;
using Quartz.Impl;
using Yunt.Common;
using Yunt.DIDC.Tasks;
using Yunt.Xml.Domain.Model;

namespace Yunt.DIDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;

        public static IScheduler Sched;
        //public static TimerX _timerX;
        static  void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            #region init
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志

            var services = new ServiceCollection();
            Init(services);

            services.AddAutoMapper(typeof(Program).Assembly);
            // BufferPool.DEFAULT_BUFFERLENGTH = 2000 * 1024;//2M缓冲区
            //var day = new DayStatisticsTask();
            //day.ExcuteAnalysis();

            #endregion

            #region recovery
            try
            {
                DateTime start = "2018-09-2 0:00:00".ToDateTime(), end = "2018-09-2 0:00:00".ToDateTime();
                //DayStatisticsTask.UpdatePowers("2018-06-20 00:00:00".ToDateTime());
                //DayStatisticsTask.UpdatePowers("2018-06-25 00:00:00".ToDateTime());
                //DayStatisticsTask.UpdatePowers("2018-07-06 00:00:00".ToDateTime());
                //DayStatisticsTask.UpdatePowers("2018-07-22 00:00:00".ToDateTime());
                //DayStatisticsTask.UpdateRunLoads(start, end);
                DayStatisticsTask.RecoveryTask(start, end);


            }
            catch (Exception ex)
            {
                Common.Logger.Exception(ex);
            }

            Common.Logger.Error("所有恢复完毕！");
            Console.ReadKey();
            #endregion

            #region test

            //DayStatisticsTask.Test("WDD-P001-CC000001", new DateTime(2018, 7, 25));
            #endregion

            while (true)
            {
                if (Sched?.IsShutdown ?? false)
                    break;
                if (Sched == null)
                    Start();
                Thread.Sleep(60 * 1000);
            }
        }
        public static async Task Start()
        {
            var sf = new StdSchedulerFactory();
            Sched = await sf.GetScheduler();
            var job = JobBuilder.Create<DayStatisticsTask>()
                .WithIdentity("dayJob", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("dayTrigger", "group1")
                    .StartNow()
                    .WithCronSchedule("0 2 0 * * ? *")
                    .Build();

            await Sched.ScheduleJob(job, trigger);

            await Sched.Start();



            //while (true)
            //{
            //    if (_timerX == null)
            //        _timerX = new TimerX(obj =>
            //        {
            //            task.Start();

            //        }, null, 1000, 24*60 * 60 * 1000);

            //    System.Threading.Thread.Sleep(10000);
            //}

        }

        public static async Task Stop()
        {
            await Sched.Shutdown(true);

        }
        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <returns></returns>
        private static void Init(ServiceCollection services)
        {
            //先将xcode所需mysql驱动加载进来，这样efcore的mysql驱动就不会与此发生冲突。。。等待xcode的驱动dnc更新
           // Datatype.FindAll();
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            // Common.Logger.Create(configuration, new LoggerFactory(), "service");
            services.AddSingleton<IConfiguration>(configuration);

            Configuration = configuration;
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
        }

        /// <summary>
        /// 删除配置文件
        /// </summary>
        private static void DeleteConfigFile()
        {
            //var file = AppDomain.CurrentDomain.BaseDirectory + @"Config/Redis.config";
            //if (File.Exists(file))//判断文件是不是存在
            //{
            //    File.Delete(file);//如果存在则删除
            //}

        }
    }


}
