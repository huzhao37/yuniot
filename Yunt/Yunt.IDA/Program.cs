using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Quartz;
using Quartz.Impl;
using Yunt.Common;
using Yunt.IDA.Tasks;
using Yunt.Redis;

namespace Yunt.IDA
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;

        public static IScheduler Sched;
        static void Main(string[] args)
        {
            #region init
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志

            var services = new ServiceCollection();
            Init(services);

            services.AddAutoMapper(typeof(Program).Assembly);
            BufferPool.DEFAULT_BUFFERLENGTH = 2000 * 1024;//3M缓冲区
            //var day = new AnalysisTask();
            //day.ExcuteAnalysis();
            //day.MailPush();
        

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
            var job = JobBuilder.Create<AnalysisTask>()
                .WithIdentity("analysisJob", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("analysisTrigger", "group1")
                    .StartNow()
                    .WithCronSchedule("0 3 0 * * ? *")
                    .Build();

            await Sched.ScheduleJob(job, trigger);

            await Sched.Start();


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
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            Configuration = configuration;
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
        }

    }
}
