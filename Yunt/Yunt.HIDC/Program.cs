using System;
using System.Collections.Generic;
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
using Yunt.HIDC.Tasks;
using Yunt.Xml.Domain.Model;
using LogLevel = NewLife.Log.LogLevel;

namespace Yunt.HIDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;
        public static IScheduler sched;
        //public static TimerX _timerX;
        static void Main(string[] args)
        {
            #region init
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志
            //XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;//关闭反向工程
            //XCode.Setting.Current.TraceSQLTime = 2000;//sql执行时间超过2s打印log

            var services = new ServiceCollection();
            Init(services);

            services.AddAutoMapper(typeof(Program).Assembly);



            #endregion

            #region test
            //DateTime dt = "2018-07-22 11:00:00".ToDateTime();

            //var x = HourStatisticsTask.Test(dt, "WDD-P001-CY000021");


            //Common.Logger.Error("测试完毕！");
            //Console.ReadKey();
            #endregion

            #region recovery
            try
            {
                DateTime start = "2018-08-22 14:00:00".ToDateTime(), end = "2018-08-22 14:00:00".ToDateTime();
                //HourStatisticsTask.UpdatePowers(start,end);
                HourStatisticsTask.RecoveryTask(start,end);
                //HourStatisticsTask.RecoveryTask("2018-06-25 13:00:00".ToDateTime());
                //HourStatisticsTask.RecoveryTask("2018-07-06 12:00:00".ToDateTime());
                Common.Logger.Error("恢复完毕！");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Common.Logger.Exception(ex);
            }

            #endregion

            //while (true)
            //{
            //    if (sched?.IsShutdown ?? false)
            //        break;
            //    if (sched == null)
            //        Start();
            //    Thread.Sleep(60 * 1000);
            //}
        }
        public static async Task Start()
        {
            var sf = new StdSchedulerFactory();
            sched = await sf.GetScheduler();
            var job = JobBuilder.Create<HourStatisticsTask>()
                .WithIdentity("hourJob", "group2")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("hourTrigger", "group2")
                    .StartNow()
                    .WithCronSchedule("0 0 * * * ? *")
                    .Build();

            await sched.ScheduleJob(job, trigger);

            await sched.Start();
        }

        public static async Task Stop()
        {
            await sched.Shutdown(true);

        }
        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <returns></returns>
        private static void Init(ServiceCollection services)
        {
            //先将xcode所需mysql驱动加载进来，这样efcore的mysql驱动就不会与此发生冲突。。。等待xcode的驱动dnc更新
            //Datatype.FindAll();
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
    }
}
