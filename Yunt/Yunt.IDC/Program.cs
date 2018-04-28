using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;

namespace Yunt.IDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;
        static void Main(string[] args)
        {
            #region init
            var services = new ServiceCollection();
           Init(services);
            services.AddAutoMapper();
           
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志
            XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;//关闭反向工程
            XCode.Setting.Current.TraceSQLTime = 2000;//sql执行时间超过2s打印log

            #endregion

            Console.WriteLine("Hello World!");
        }
        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <returns></returns>
        private static void Init(ServiceCollection services)
        {
          
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            Configuration = configuration;
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
        }
    }
}
