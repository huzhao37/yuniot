using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewLife.Log;
using Yunt.Common;
using Yunt.Device.Domain.BaseModel;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.IDC.Helper;
using Yunt.IDC.Task;
using Yunt.Redis;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
using Yunt.Xml.Domain.Services;
using LogLevel = NewLife.Log.LogLevel;

namespace Yunt.IDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;
        static void Main(string[] args)
        {
            try
            {
                #region init
                XTrace.UseConsole(true, true);
                XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志
                                                 //XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;//关闭反向工程
                                                 //XCode.Setting.Current.TraceSQLTime = 2000;//sql执行时间超过2s打印log

                var services = new ServiceCollection();
                Init(services);

                services.AddAutoMapper(typeof(Program).Assembly);

                //BufferPool.DEFAULT_BUFFERLENGTH = 5 * 1024 * 1024;//5M缓冲区

                #endregion

                #region di recovery
                //long start = "2018-06-19 10:00:00".ToDateTime().TimeSpan(), end = "2018-06-26 23:59:00".ToDateTime().TimeSpan();
                //long lastTime = "2018-06-27 00:00:00".ToDateTime().TimeSpan();
                //var originBytesRepos = ServiceProviderServiceExtensions.GetService<IOriginalBytesRepository>(Providers["Device"]);
                //var bytesParseRepository = ServiceProviderServiceExtensions.GetService<IBytesParseRepository>(Providers["Xml"]);
                //var buffers = originBytesRepos.GetEntities(e => e.Time >= start &&
                //                e.Time <= end && e.ProductionLineId.Equals("WDD-P001"))?.OrderByDescending(e => e.Time)?.ToList();
                //if (buffers != null && buffers.Any())
                //    buffers.ForEach(buffer =>
                //    {
                //        if (lastTime == buffer.Time)
                //            return;
                //        lastTime = buffer.Time;
                //        var bytes = Extention.strToToHexByte(buffer.Bytes);
                //        var result = bytesParseRepository.UniversalParser(bytes, "Integrate", DiToRedis.Saving);
                //        if (!result)
                //            Common.Logger.Error("保存出错！");
                //    });

                //Common.Logger.Warn("Di recovery Finished!");
                #endregion

                MqDealTask.Start();
            }
            catch (Exception e)
            {
                Common.Logger.Error($"program1{e.Message + e.StackTrace}");
                Common.Logger.Error($"program2{e.InnerException.Message + e.InnerException.StackTrace}");
                Common.Logger.Error($"program3{e.InnerException.InnerException.Message + e.InnerException.InnerException.StackTrace}");
            }
       


            Console.ReadKey();
            Console.WriteLine("Hello World!");
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

            Common.Logger.Info($"currentDirectory:{currentDirectory}");
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            //Common.Logger.Create(configuration, new LoggerFactory(), "service");
            services.AddSingleton<IConfiguration>(configuration);
         
            Configuration = configuration;
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
        }
        
    }
}
