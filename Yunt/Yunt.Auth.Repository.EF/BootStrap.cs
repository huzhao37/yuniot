using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Auth.Domain;
using Yunt.Auth.Domain.BaseModel;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Repository.EF.Models;
using Yunt.Auth.Repository.EF.Repositories;
using Yunt.Redis.Config;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yunt.Redis;
using Yunt.Common;
using Microsoft.Extensions.Configuration;

namespace Yunt.Auth.Repository.EF
{
    [Service(ServiceType.Auth)]
    public class BootStrap //: IBootStrap
    {
       public void Start(IServiceCollection services, IConfigurationRoot configuration)
        {

            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfileConfiguration>();
            });
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            services.AddAutoMapper();

            var contextOptions = new DbContextOptionsBuilder().UseMySql("server=10.1.5.25;port=3306;database=yunt_test;uid=root;pwd=unitoon2017;").Options;
            services.AddSingleton(contextOptions)
              .AddTransient<TaskManagerContext>();
           
            var currentpath = AppDomain.CurrentDomain.BaseDirectory;
            var allTypes = Assembly.LoadFrom($"{currentpath}{ MethodBase.GetCurrentMethod().DeclaringType.Namespace}.dll").GetTypes();
            var type = typeof(ITaskRepositoryBase<>);

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
                option.RedisServer.Add(new HostItem() { Host = "127.0.0.1:6379" });
                option.SingleMode = true;
                //option.Password = "";
            });
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
