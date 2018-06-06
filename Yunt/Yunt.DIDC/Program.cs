using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using NewLife.Threading;
using Yunt.Common;
using Yunt.DIDC.Task;
using Yunt.Xml.Domain.Model;

namespace Yunt.DIDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;

        public static TimerX _timerX;
        static void Main(string[] args)
        {
            #region init
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志

            var services = new ServiceCollection();
            Init(services);

            services.AddAutoMapper(typeof(Program).Assembly);



            #endregion

            while (true)
            {
                if (_timerX == null)
                    _timerX = new TimerX(obj =>
                    {
                        DayStatisticsTask.Start();

                    }, null, 1000, 24*60 * 60 * 1000);

                System.Threading.Thread.Sleep(10000);
            }



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
    }
}
