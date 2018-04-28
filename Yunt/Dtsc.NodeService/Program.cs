using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Threading;
using Yunt.Common;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;
using Yunt.Redis;
using Yunt.Redis.Config;

namespace Dtsc.NodeService
{
    class Program
    {
        private static IRedisCachingProvider _redisProvider;
        private static TimerX _timer;
        static void Main(string[] args)
        {
            #region init
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            var redisConn = configuration.GetSection("AppSettings").GetSection("Dtsc").GetValue<string>("RedisConn");
            var redisPwd = configuration.GetSection("AppSettings").GetSection("Dtsc").GetValue<string>("RedisPwd");

            if (redisConn.IsNullOrWhiteSpace() || redisPwd.IsNullOrWhiteSpace())
            {
                //todo 可写入初始配置
                Logger.Error($"[Device]:appsettings not entirely!");
                Logger.Error($"please write Device service's settings into appsettings! \n exp：\"Device\":{{\"RedisConn\":\"***\"," +
                    $"\"MySqlConn\":\"***\"}}");
            }

            services.AddDefaultRedisCache(option =>
            {
                option.RedisServer.Add(new HostItem() { Host = redisConn });
                option.SingleMode = true;
                option.Password = redisPwd;
            });

            _redisProvider = ServiceProviderServiceExtensions.GetService<IRedisCachingProvider>(services.BuildServiceProvider());


            #endregion

            while (true)
            {
                if(_timer==null)
                    _timer=new TimerX(obj=> { ListenCmd(); },null,500,100);
            }          ;         
        }
        /// <summary>
        /// 监听redis_cmd
        /// </summary>
        /// <returns></returns>
        private static void ListenCmd()
        {          
            _redisProvider.DB = 15;
            var jobIds = _redisProvider.Keys("*");
            if (!jobIds.Any()) return;
            jobIds.ForEach(jobId =>
            {
                var state = _redisProvider.Get<int>(jobId, DataType.Json);
                if (state != null)
                {
                    var cmd=new TbCommand()
                    {
                        Commandtype = state,
                        Jobid =Convert.ToInt32(jobId)
                    };
                    JobHelper.Excute(cmd); 
                }
              
            });           
        }
    }
}
