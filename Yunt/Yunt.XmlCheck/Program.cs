using System;
using System.ComponentModel;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using Yunt.Common;
using Yunt.Device.Domain.Model;
using Yunt.XmlCheck.Main;

namespace Yunt.XmlCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            XTrace.UseConsole(true,true);
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);

         
            //services.AddOptions();
            //  services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            //var providers = ServiceEx.StartServices(services, configuration);
            //services.AddAutoMapper();
      
            //var cy = ServiceProviderServiceExtensions.GetService<Motor>(providers["Device"]);

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
