using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewLife.Log;
using NewLife.Serialization;
using NewLife.Threading;
using XCode.DataAccessLayer;
using Yunt.Common;
using Yunt.Common.Enviroment;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;
using Yunt.Redis;
using Yunt.Redis.Config;
using Logger = Yunt.Common.Logger;
using LogLevel = NewLife.Log.LogLevel;


namespace Dtsc.NodeService
{
    public class MainService //: IWin32Service
    {

        private static IRedisCachingProvider _redisProvider;
        private static TimerX _timer;
        private static TimerX _performanceTimer;
        private static TimeSpan _prevCpuTime = TimeSpan.Zero;//上次记录的CPU时间


        private static bool _isStop = false;


        public static void StartService()
        {
            try
            {
               // XTrace.UseConsole(true,false);

                #region init
                dynamic type = (new Program()).GetType();
                string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
                var services = new ServiceCollection();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(currentDirectory)
                    .AddJsonFile("appsettings.json", true, reloadOnChange: true);

                var configuration = builder.Build();
                services.AddSingleton<IConfiguration>(configuration);
                Logger.Create(configuration, new LoggerFactory(), "Dtsc.NodeService");
                var redisConn = configuration.GetSection("AppSettings").GetSection("Dtsc").GetValue<string>("RedisConn", "127.0.0.1");
                var redisPort = configuration.GetSection("AppSettings").GetSection("Dtsc").GetValue<int>("RedisPort", 6379);
                var redisPwd = configuration.GetSection("AppSettings").GetSection("Dtsc").GetValue<string>("RedisPwd", "Unitoon2018");
                var nodeId = configuration.GetSection("AppSettings").GetValue<int>("NodeId", 0);
                JobHelper.JobPath = configuration.GetSection("AppSettings").GetValue<string>("JobPath", "\\Yunt.Jobs");
                XTrace.Log.Level = (LogLevel)configuration.GetSection("AppSettings").GetValue<int>("LogLevel", 3);
                var performanceInverval = configuration.GetSection("AppSettings").GetValue<int>("PerformanceInverval", 60);
                if (redisConn.IsNullOrWhiteSpace() || redisPwd.IsNullOrWhiteSpace() || nodeId == 0)
                {
                    //todo 可写入初始配置
                    Logger.Error($"[Device]:appsettings not entirely!");
                    Logger.Error($"please write Device service's settings into appsettings! \n exp：\"Device\":{{\"RedisConn\":\"***\"," +
                        $"\"MySqlConn\":\"***\"}}");
                    new TbError()
                    {
                        Createtime = DateTime.Now.TimeSpan(),
                        JobID = 0,
                        Msg = $"Error:appsettings not entirely"
                    }.SaveAsync();
                }

                services.AddDefaultRedisCache(option =>
                {
                    option.RedisServer.Add(new HostItem() { Host = redisConn+":"+ redisPort });
                    option.SingleMode = true;
                    option.Password = redisPwd;
                });

                _redisProvider = ServiceProviderServiceExtensions.GetService<IRedisCachingProvider>(services.BuildServiceProvider());

                //#if DEBUG
                XTrace.UseConsole();
                //#endif

                #endregion

                var ip = HostHelper.GetExtenalIpAddress();
                var node = TbNode.Find("ID", nodeId);
                if (node != null)
                {
                    if (!ip.IsNullOrWhiteSpace())
                        node.Ip = ip;
                    node.SaveAsync();
                }
                //恢复所有运行job
                ResumeJob(nodeId);

                using (var p = Process.GetCurrentProcess())
                {
                    //断线重连
                    while (true)
                    {
                        if (_isStop)
                        return;
                    try
                    {
                        if (_timer == null)
                            _timer = new TimerX(obj => { ListenCmd(nodeId); }, null, 500, 100);
                        if (_performanceTimer == null)
                            _performanceTimer = new TimerX(obj => { Performance(p, performanceInverval, nodeId); }, null, 1000, performanceInverval * 1000);
                    }
                    catch (Exception e)
                    {
                        _timer?.Dispose();
                        _performanceTimer?.Dispose();
                        new TbError()
                        {
                            Createtime = DateTime.Now.TimeSpan(),
                            JobID = 0,
                            Msg = $"Error:{e.Message},错误详情，请查看节点服务日志！"
                        }.SaveAsync();
                        Logger.Exception(e);
                    }

                    }

                }

            }
            catch (Exception e)
            {
                new TbError()
                {
                    Createtime = DateTime.Now.TimeSpan(),
                    JobID = 0,
                    Msg = $"Error:{e.Message},错误详情，请查看节点服务日志！"
                }.SaveAsync();
                Logger.Exception(e);
            }

        }
        /// <summary>
        /// 监听redis_cmd
        /// </summary>
        /// <param name="nodeId">nodeId</param>
        private static void ListenCmd(int nodeId)
        {
            _redisProvider.DB = 0;
            var jobIds = _redisProvider.Keys("*");
            if (!jobIds?.Any() ?? false) return;
            jobIds.ForEach(jobId =>
            {
                if ((TbJob.Find("ID", jobId)?.NodeID ?? 0) != nodeId)
                    return;
                var cmd = _redisProvider.Get<string>(jobId, DataType.String).ToJsonEntity<TbCommand>();
                if (cmd == null) return;
                JobHelper.Excute(cmd);
                cmd.Time = DateTime.Now.TimeSpan();
                if (cmd.Success != 1) return;
                cmd.SaveAsync();
                _redisProvider.Delete(jobId);
            });
        }

        private static void ResumeJob(int nodeId)
        {
            var jobs = TbJob.FindAll("NodeID", nodeId).Where(e => e.State == 1);
            JobHelper.ResumeJob(jobs);

        }
        /// <summary>
        /// 性能监控
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="performanceInverval"></param>
        /// <param name="nodeId"></param>
        private static void Performance(Process pro, int performanceInverval, int nodeId = 0)
        {
#if DEBUG
            Logger.Info($"性能监控正在运行");
#endif
            var interval = performanceInverval * 1000;
            //当前时间
            var curTime = pro?.TotalProcessorTime??new TimeSpan(0,0,0);
            //间隔时间内的CPU运行时间除以逻辑CPU数量
            var value = (curTime - _prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
            var per = TbPerformance.Find("node_id", nodeId);
            if (per == null)
            {
                new TbPerformance()
                {
                    NodeID = nodeId,
                    JobID = 0,
                    Cpu = (float)Math.Round(value, 2),
                    // ReSharper disable once PossibleLossOfFraction
                    Memory = (int)Math.Round((double)(pro.PrivateMemorySize64 / 1024L / 1024L), 2),
                    Updatetime = DateTime.Now.TimeSpan(),
                    Installdirsize = pro.Threads.Count,
                }.SaveAsync();
            }
            else
            {
                per.NodeID = nodeId;
                //间隔时间内的CPU运行时间除以逻辑CPU数量  

                per.Cpu = (float)Math.Round(value, 2);
                // ReSharper disable once PossibleLossOfFraction
                per.Memory = (int)Math.Round((double)(pro.PrivateMemorySize64 / 1024L / 1024L), 2);
                per.Updatetime = DateTime.Now.TimeSpan();
                per.Installdirsize = pro.Threads.Count;
                per.SaveAsync();
            }
            _prevCpuTime = curTime;
        }


    }
}
