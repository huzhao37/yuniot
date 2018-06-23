using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
namespace Yunt.DIDC.Tasks
{
    public class DayStatisticsTask : IJob
    {
        #region fields && ctor

        internal static Messagequeue WddQueue;
        private static readonly IMaterialFeederByDayRepository MfByDayRepository;
        private static readonly IConveyorByDayRepository CyByDayRepository;
        private static readonly IConeCrusherByDayRepository CcByDayRepository;
        private static readonly IJawCrusherByDayRepository JcByDayRepository;
        // private static IReverHammerCrusherByDayRepository rhcByDayRepository;
        // private static IDoubleToothRollCrusherByDayRepository dtrByDayRepository;
        private static readonly IVibrosieveByDayRepository VibByDayRepository;
        private static readonly IPulverizerByDayRepository PulByDayRepository;
        private static readonly IVerticalCrusherByDayRepository VcByDayRepository;
        private static readonly IImpactCrusherByDayRepository IcByDayRepository;
        private static readonly ISimonsConeCrusherByDayRepository SccByDayRepository;
        private static readonly IHVibByDayRepository HvibByDayRepository;

        private static readonly IMessagequeueRepository MessagequeueRepository;
        

        static DayStatisticsTask()
        {
            //motorTypeRepository = ServiceProviderServiceExtensions.GetService<IMotorTypeRepository>(Program.Providers["Device"]);
            MfByDayRepository = ServiceProviderServiceExtensions.GetService<IMaterialFeederByDayRepository>(Program.Providers["Device"]);
            CyByDayRepository = ServiceProviderServiceExtensions.GetService<IConveyorByDayRepository>(Program.Providers["Device"]);
            CcByDayRepository = ServiceProviderServiceExtensions.GetService<IConeCrusherByDayRepository>(Program.Providers["Device"]);
            JcByDayRepository = ServiceProviderServiceExtensions.GetService<IJawCrusherByDayRepository>(Program.Providers["Device"]);
            // rhcByDayRepository = ServiceProviderServiceExtensions.GetService<IReverHammerCrusherByDayRepository>(Program.Providers["Device"]);
            //dtrByDayRepository = ServiceProviderServiceExtensions.GetService<IDoubleToothRollCrusherByDayRepository>(Program.Providers["Device"]);

            VibByDayRepository = ServiceProviderServiceExtensions.GetService<IVibrosieveByDayRepository>(Program.Providers["Device"]);
            PulByDayRepository = ServiceProviderServiceExtensions.GetService<IPulverizerByDayRepository>(Program.Providers["Device"]);
            VcByDayRepository = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByDayRepository>(Program.Providers["Device"]);
            IcByDayRepository = ServiceProviderServiceExtensions.GetService<IImpactCrusherByDayRepository>(Program.Providers["Device"]);
            SccByDayRepository = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByDayRepository>(Program.Providers["Device"]);
            HvibByDayRepository = ServiceProviderServiceExtensions.GetService<IHVibByDayRepository>(Program.Providers["Device"]);
            MessagequeueRepository = ServiceProviderServiceExtensions.GetService<IMessagequeueRepository>(Program.Providers["Xml"]);        
        }

        #endregion
        

        /// <summary>
        /// 定时任务执行
        /// </summary>
        /// <param name="context"></param>
        public Task Execute(IJobExecutionContext context)
        {
            var startTime = DateTime.Now.AddDays(-1);
            var w_r = (int)WriteOrRead.Read;
            //var where = " Write_Read = '" + w_r + "' and  Route_Key != 'STATUS'";
            WddQueue =
               MessagequeueRepository.GetEntities(e => e.Write_Read.Equals(w_r) && !e.Route_Key.Equals("STATUS")).FirstOrDefault();
            if (WddQueue != null)
            {
                var queueHost = WddQueue.Host;
                var queuePort = WddQueue.Port;
                var queueUserName = WddQueue.Username;
                var queuePassword = WddQueue.Pwd;

                var ccuri = "amqp://" + queueHost + ":" + queuePort;
                var queue = WddQueue.Route_Key; //"FailedData";
                var route = WddQueue.Route_Key;
                var exchange = "amq.topic";
                var errorQueue = queue + "Error"; //faileddata
                var rabbitHelper = new RabbitMqHelper();
                //等待队列消息消费完后再执行统计
                var messageCount = rabbitHelper.GetMessageCount(ccuri, queue, route, exchange, queueHost, queuePort, queueUserName,
                    queuePassword) + rabbitHelper.GetMessageCount(ccuri, errorQueue, errorQueue, exchange, queueHost, queuePort, queueUserName,
                    queuePassword);
                if (messageCount >= 1)
                { 
                    //自动恢复统计数据-起始时间
                    startTime = DateTime.Now.AddDays(-1);
                    while (true)
                    {
                        Thread.Sleep(5000);
                        messageCount = rabbitHelper.GetMessageCount(ccuri, queue, route, exchange, queueHost, queuePort, queueUserName,
                    queuePassword) + rabbitHelper.GetMessageCount(ccuri, errorQueue, errorQueue, exchange, queueHost, queuePort, queueUserName,
                    queuePassword);
                        if (messageCount < 1) break;
#if DEBUG
                        Common.Logger.Info($"[DayStatisticsTask]:当前队列还有{messageCount}个数据位解析，请等待");
#endif
                    }
                };
           
            }
            var dt = DateTime.Now.AddDays(-1);

            if (dt.CompareTo(startTime) == 0)
            {
                MainTask(dt);
                return Task.CompletedTask;
            }
            //自动恢复
            var days = dt.Subtract(startTime).TotalDays;
            for (int i = 0; i < days; i++)
            {
                var time = startTime.AddDays(i);
                MainTask(time);
            }

#if DEBUG
            Common.Logger.Info("[DayStatisticsTask]Day Statistics");
            Common.Logger.Info($"耗时{context.JobRunTime.TotalMilliseconds}ms");
#endif
            return Task.CompletedTask;
            //GC.Collect();
        }

        /// <summary>
        /// 主任务
        /// </summary>
        /// <param name="dt"></param>
        private void MainTask(DateTime dt)
        {
            CyByDayRepository.InsertDayStatistics(dt, "CY");
            SccByDayRepository.InsertDayStatistics(dt, "SCC");
            CcByDayRepository.InsertDayStatistics(dt, "CC");
            JcByDayRepository.InsertDayStatistics(dt, "JC");
            MfByDayRepository.InsertDayStatistics(dt, "MF");
            VcByDayRepository.InsertDayStatistics(dt, "VC");
            VibByDayRepository.InsertDayStatistics(dt, "VB");
            PulByDayRepository.InsertDayStatistics(dt, "PUL");
            IcByDayRepository.InsertDayStatistics(dt, "IC");
            HvibByDayRepository.InsertDayStatistics(dt, "HVB");

            HvibByDayRepository.Batch();
            //rhcByDayRepository.Insert(dt);
            //dtrByDayRepository.InsertAsync(t);
            //ExcuteAnalysis();

        }

        /// <summary>
        /// 统计恢复任务
        /// </summary>
        /// <param name="dt"></param>
        public static void RecoveryTask(DateTime dt)
        {
            CyByDayRepository.RecoveryDayStatistics(dt, "CY");
            SccByDayRepository.RecoveryDayStatistics(dt, "SCC");
            CcByDayRepository.RecoveryDayStatistics(dt, "CC");
            JcByDayRepository.RecoveryDayStatistics(dt, "JC");
            MfByDayRepository.RecoveryDayStatistics(dt, "MF");
            VcByDayRepository.RecoveryDayStatistics(dt, "VC");
            VibByDayRepository.RecoveryDayStatistics(dt, "VB");
            PulByDayRepository.RecoveryDayStatistics(dt, "PUL");
            IcByDayRepository.RecoveryDayStatistics(dt, "IC");
            HvibByDayRepository.RecoveryDayStatistics(dt, "HVB");

            HvibByDayRepository.Batch();
        }
    }
}
