using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Xml.Domain;
using Yunt.Xml.Domain.BaseModel;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Repository.EF.Models;
using Yunt.Xml.Repository.EF.Repositories;
using Yunt.Redis.Config;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yunt.Redis;
using Yunt.Common;
using Microsoft.Extensions.Configuration;

namespace Yunt.Xml.Repository.EF
{
    [Service(ServiceType.Xml)]
    public class BootStrap : MarshalByRefObject//: IBootStrap
    {
        internal static IServiceProvider ServiceProvider;
        public void Start(IServiceCollection services, IConfigurationRoot configuration)
        {
            try
            {
                AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
                         {
                             cfg.AddProfile<AutoMapperProfileConfiguration>();
                         });
                services.AddSingleton(config);
                services.AddScoped<IMapper, Mapper>();
                // services.AddAutoMapper();

                var redisConn = configuration.GetSection("AppSettings").GetSection("Xml").GetValue<string>("RedisConn");
                var redisPwd = configuration.GetSection("AppSettings").GetSection("Xml").GetValue<string>("RedisPwd");
                var mySqlConn = configuration.GetSection("AppSettings").GetSection("Xml").GetValue<string>("MySqlConn");

                if (redisConn.IsNullOrWhiteSpace() || mySqlConn.IsNullOrWhiteSpace()|| redisPwd.IsNullOrWhiteSpace())
                {
                   //todo 可写入初始配置
                    Logger.Error($"[Xml]:appsettings not entirely!");
                    Logger.Error($"please write Xml service's settings into appsettings! \n exp：\"Xml\":{{\"RedisConn\":\"***\"," +
                        $"\"MySqlConn\":\"***\"}}");
                }

                var contextOptions = new DbContextOptionsBuilder().UseMySql(mySqlConn).Options;
                services.AddSingleton(contextOptions)
                  .AddTransient<XmlContext>();

                dynamic x = (new BootStrap()).GetType();
                string currentpath = Path.GetDirectoryName(x.Assembly.Location);
                // var currentpath = AppDomain.CurrentDomain.BaseDirectory;
                var allTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
                // Assembly.LoadFrom($"{currentpath}\\{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
            
                var type = typeof(IXmlRepositoryBase<>);
             
                allTypes.Where(t => t.IsClass).ToList().ForEach(t =>
                {
                    var ins = t.GetInterfaces();
                    if (!ins.Any(e => e.Name.Equals(type.Name))) return;
                    if (ins.Length >= 2)
                    {
                        foreach (var i in ins)
                        {
                            if (!i.Name.Equals(type.Name))
                            {
                                //services.AddTransient<ITbCategoryRepository, TbCategoryRepository>();
                                services.TryAddTransient(i, t);
                            }

                        }
                    }
                    else
                    {
                        foreach (var i in ins)
                        {
                            if (!i.Name.Equals(type.Name)) continue;
                            //services.AddTransient<IXmlRepositoryBase<BaseModel>, XmlRepositoryBase<BaseModel, BaseModel>>();
                            var arg = new Type[] { typeof(AggregateRoot), typeof(BaseModel) };
                            var tp = t.MakeGenericType(arg);
                            services.TryAddTransient(i, tp);
                        }
                    }
                });

                services.AddDefaultRedisCache(option =>
                {
                    option.RedisServer.Add(new HostItem() { Host = redisConn });
                    option.SingleMode = true;
                    option.Password = redisPwd;
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"[Xml]:start failed!");
                Logger.Exception(ex);
            }

        }
        /// <summary>
        /// 数据库上下文池初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void ContextInit(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ContextFactory.Init(serviceProvider);
        }
    }
}
