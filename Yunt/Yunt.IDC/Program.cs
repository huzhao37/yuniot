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
using Yunt.Xml.Domain.IRepository;
using Yunt.XmlProtocol.Domain.Models;
using LogLevel = NewLife.Log.LogLevel;

namespace Yunt.IDC
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        public static Dictionary<string, IServiceProvider> Providers;
        static void Main(string[] args)
        {
            #region init
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志
            XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;//关闭反向工程
            XCode.Setting.Current.TraceSQLTime = 2000;//sql执行时间超过2s打印log

            var services = new ServiceCollection();
            Init(services);          

            services.AddAutoMapper();



            #endregion

            #region test bacth
            //var rh = ServiceProviderServiceExtensions.GetService<IConeCrusherRepository>(services.BuildServiceProvider());
            //var rh2 = ServiceProviderServiceExtensions.GetService<IVerticalCrusherRepository>(services.BuildServiceProvider());
            //try
            //{
            //    var i = 0;
            //    while (true)
            //    {
            //        var model = new ConeCrusher()
            //        {
            //            MotorId = "00",AbsSpindleTravel = 0,ActivePower = 0,Current_A = 0,Current_B = 0,Current_C = 0,MovaStress = 0,OilFeedTempreature = 0,
            //            OilReturnTempreatur = 0,
            //            Time = 0
            //        };

            //        rh.InsertAsync(model);
            //        var model2 = new VerticalCrusher()
            //        {
            //            MotorId = "00",
            //            ActivePower = 0,
            //            Current_A = 0,
            //            Current_B = 0,
            //            Time = 0,Current_C = 0,PowerFactor = 0,Vibrate1 = 0,Vibrate2 = 0
            //        };

            //        rh2.InsertAsync(model2);
            //        System.Threading.Thread.Sleep(10);
            //        i++;
            //        if(i==1000)
            //            break;
            //    }
            //    rh.BatchSql();             
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
         
     

            #endregion

            MqDealTask.Start();


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
            Datatype.FindAll();
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            Common.Logger.Create(configuration, new LoggerFactory(), "service");
            services.AddSingleton<IConfiguration>(configuration);
         
            Configuration = configuration;
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
        }
        
    }
}
