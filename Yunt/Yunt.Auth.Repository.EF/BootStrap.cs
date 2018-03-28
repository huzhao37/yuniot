using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Auth.Repository.EF.Models;
using Yunt.Auth.Repository.EF.Repositories;
using Yunt.Redis.Config;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yunt.Redis;
using Yunt.Common;
using Microsoft.Extensions.Configuration;
using Yunt.Auth.Domain.BaseModel;
using Yunt.Auth.Domain.IRepository;

namespace Yunt.Auth.Repository.EF
{
    [Service(ServiceType.Auth)]
    public class BootStrap //: IBootStrap
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
                //services.AddAutoMapper();

                var redisConn = configuration.GetSection("AppSettings").GetSection("Auth").GetValue<string>("RedisConn");
                var redisPwd = configuration.GetSection("AppSettings").GetSection("Auth").GetValue<string>("RedisPwd");
                var mySqlConn = configuration.GetSection("AppSettings").GetSection("Auth").GetValue<string>("MySqlConn");

                if (redisConn.IsNullOrWhiteSpace() || mySqlConn.IsNullOrWhiteSpace() || redisPwd.IsNullOrWhiteSpace())
                {
                    //todo 可写入初始配置
                    Logger.Error($"[Auth]:appsettings not entirely!");
                    Logger.Error($"please write Auth service's settings into appsettings! \n exp：\"Auth\":{{\"RedisConn\":\"***\"," +
                        $"\"MySqlConn\":\"***\"}}");
                }

                var contextOptions = new DbContextOptionsBuilder().UseMySql(mySqlConn).Options;
                services.AddSingleton(contextOptions)
                  .AddTransient<AuthContext>();

                var currentpath = AppDomain.CurrentDomain.BaseDirectory;
                var allTypes = Assembly.LoadFrom($"{currentpath}{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
                var type = typeof(IAuthRepositoryBase<>);

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
                            //services.AddTransient<IAuthRepositoryBase<BaseModel>, AuthRepositoryBase<BaseModel, BaseModel>>();
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
                Logger.Error($"[Auth]:start failed!");
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
