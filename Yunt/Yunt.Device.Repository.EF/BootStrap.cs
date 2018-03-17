using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Device.Domain;
using Yunt.Device.Domain.BaseModel;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Repository.EF.Models;
using Yunt.Device.Repository.EF.Repositories;
using Yunt.Redis.Config;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yunt.Redis;
using Yunt.Common;
using Microsoft.Extensions.Configuration;

namespace Yunt.Device.Repository.EF
{
    [Service(ServiceType.Device)]
    public class BootStrap //: IBootStrap
    {
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

                var redisConn = configuration.GetSection("AppSettings").GetSection("Device").GetValue<string>("RedisConn");
                var mySqlConn = configuration.GetSection("AppSettings").GetSection("Device").GetValue<string>("MySqlConn");

                if (redisConn.IsNullOrWhiteSpace() || mySqlConn.IsNullOrWhiteSpace())
                {
                    Logger.Error($"[Device]:appsettings not entirely!");
                    Logger.Error($"please write Device service's settings into appsettings! \n exp：\"Device\":{{\"RedisConn\":\"***\"," +
                        $"\"MySqlConn\":\"***\"}}");
                }

                var contextOptions = new DbContextOptionsBuilder().UseMySql(mySqlConn).Options;
                services.AddSingleton(contextOptions)
                  .AddTransient<DeviceContext>();

                var currentpath = AppDomain.CurrentDomain.BaseDirectory;
                var allTypes = Assembly.LoadFrom($"{currentpath}{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
                var type = typeof(IDeviceRepositoryBase<>);

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
                            // services.AddTransient<ITaskRepositoryBase<AggregateRoot>, TaskRepositoryBase<AggregateRoot, BaseModel>>();
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
                    //option.Password = "";
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"[Device]:start failed!");
                Logger.Exception(ex);
            }


        }
        /// <summary>
        /// 数据库上下文池初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void ContextInit(IServiceProvider serviceProvider)
        {
            ContextFactory.Init(serviceProvider);
        }
    }
}
