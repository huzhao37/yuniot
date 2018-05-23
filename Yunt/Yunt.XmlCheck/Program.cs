using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Yunt.Common;
using Yunt.Device.Domain.Model;
using Yunt.XmlCheck.Main;
using Yunt.XmlProtocol.Domain.Models;

namespace Yunt.XmlCheck
{
    class Program
    {
        public static Dictionary<string, IServiceProvider> Providers;
        static void Main(string[] args)
        {
            XTrace.UseConsole(true, true);
            XTrace.Log.Level = LogLevel.Info;//打印错误级别的日志
            XCode.Setting.Current.Migration = XCode.DataAccessLayer.Migration.Off;//关闭反向工程
            XCode.Setting.Current.TraceSQLTime = 2000;//sql执行时间超过2s打印log
           //先将xcode所需mysql驱动加载进来，这样efcore的mysql驱动就不会与此发生冲突。。。等待xcode的驱动dnc更新
            Datatype.FindAll();
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            var providers = ServiceEx.StartServices(services, configuration);
            Providers = providers;
            services.AddAutoMapper();

            #region 基础xml初始化(初次使用)

            //XmlParse1.OtherXmlPersist();


            #endregion

            #region 解读主xml信息
            
            //获取主xml中相关信息
            //XmlParse.GetXmlInfo(xmlPath);
            var xmlInfo = XmlParse1.GetXmlInfo("XmlFile\\wdd.xml");
            //保存主xml信息
            XmlParse1.SaveXmlInfo(xmlInfo);

            #endregion



            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
