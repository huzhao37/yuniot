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
using Yunt.Device.Domain.Model.IdModel;

namespace Yunt.Device.Repository.EF
{
    [Service(ServiceType.Device)]
    public class BootStrap:MarshalByRefObject 
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

                var redisConn = configuration.GetSection("AppSettings").GetSection("Device").GetValue<string>("RedisConn");
                var redisPwd = configuration.GetSection("AppSettings").GetSection("Device").GetValue<string>("RedisPwd");
                var mySqlConn = configuration.GetSection("AppSettings").GetSection("Device").GetValue<string>("MySqlConn");

                if (redisConn.IsNullOrWhiteSpace() || mySqlConn.IsNullOrWhiteSpace()|| redisPwd.IsNullOrWhiteSpace())
                {
                   //todo 可写入初始配置
                    Logger.Error($"[Device]:appsettings not entirely!");
                    Logger.Error($"please write Device service's settings into appsettings! \n exp：\"Device\":{{\"RedisConn\":\"***\"," +
                        $"\"MySqlConn\":\"***\"}}");
                }

                var contextOptions = new DbContextOptionsBuilder().UseMySql(mySqlConn).EnableSensitiveDataLogging().Options;
                services.AddSingleton(contextOptions)
                  .TryAddTransient<DeviceContext>();

                //services.AddDbContext<DeviceContext>(options => options.UseMySql(mySqlConn));

                //services.AddScoped(s => new DeviceContext(contextOptions));
                //services.AddDbContextPool<DeviceContext>(options =>
                //                  options.UseMySql(mySqlConn).EnableSensitiveDataLogging());
                dynamic x = (new BootStrap()).GetType();
                string currentpath = Path.GetDirectoryName(x.Assembly.Location);
                // var currentpath = AppDomain.CurrentDomain.BaseDirectory;
                var allTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
                // Assembly.LoadFrom($"{currentpath}\\{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
          
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
                            //services.AddTransient<IDeviceRepositoryBase<AggregateRoot>, DeviceRepositoryBase<AggregateRoot, BaseModel>>();
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
            ServiceProvider = serviceProvider;
            ContextFactory.Init();
        }
    }
}
