using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;

namespace Yunt.HIDC.Tasks
{
   public class HourStatisticsTask : IJob
    {
        #region fields && ctor
        internal static Messagequeue WddQueue;
        private static readonly IMaterialFeederByHourRepository MfByHourRepository;
        private static readonly IConveyorByHourRepository CyByHourRepository;
        private static readonly IConeCrusherByHourRepository CcByHourRepository;
        private static readonly IJawCrusherByHourRepository JcByHourRepository;
       // private static IReverHammerCrusherByHourRepository rhcByHourRepository;
       // private static IDoubleToothRollCrusherByHourRepository dtrByHourRepository;
        private static readonly IVibrosieveByHourRepository VibByHourRepository;
        private static readonly IPulverizerByHourRepository PulByHourRepository;
        private static readonly IVerticalCrusherByHourRepository VcByHourRepository;
        private static readonly IImpactCrusherByHourRepository IcByHourRepository;
        private static readonly ISimonsConeCrusherByHourRepository SccByHourRepository;
        private static readonly IHVibByHourRepository HvibByHourRepository;
        private static readonly IMessagequeueRepository MessagequeueRepository;
        static HourStatisticsTask()
        {
            //motorTypeRepository = ServiceProviderServiceExtensions.GetService<IMotorTypeRepository>(Program.Providers["Device"]);
            MfByHourRepository = ServiceProviderServiceExtensions.GetService<IMaterialFeederByHourRepository>(Program.Providers["Device"]);
            CyByHourRepository = ServiceProviderServiceExtensions.GetService<IConveyorByHourRepository>(Program.Providers["Device"]);
            CcByHourRepository = ServiceProviderServiceExtensions.GetService<IConeCrusherByHourRepository>(Program.Providers["Device"]);
            JcByHourRepository = ServiceProviderServiceExtensions.GetService<IJawCrusherByHourRepository>(Program.Providers["Device"]);
            // rhcByHourRepository = ServiceProviderServiceExtensions.GetService<IReverHammerCrusherByHourRepository>(Program.Providers["Device"]);
            //dtrByHourRepository = ServiceProviderServiceExtensions.GetService<IDoubleToothRollCrusherByHourRepository>(Program.Providers["Device"]);

            VibByHourRepository = ServiceProviderServiceExtensions.GetService<IVibrosieveByHourRepository>(Program.Providers["Device"]);
            PulByHourRepository = ServiceProviderServiceExtensions.GetService<IPulverizerByHourRepository>(Program.Providers["Device"]);
            VcByHourRepository = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByHourRepository>(Program.Providers["Device"]);
            IcByHourRepository = ServiceProviderServiceExtensions.GetService<IImpactCrusherByHourRepository>(Program.Providers["Device"]);
            SccByHourRepository = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByHourRepository>(Program.Providers["Device"]);
            HvibByHourRepository = ServiceProviderServiceExtensions.GetService<IHVibByHourRepository>(Program.Providers["Device"]);
            MessagequeueRepository = ServiceProviderServiceExtensions.GetService<IMessagequeueRepository>(Program.Providers["Xml"]);
        }

        #endregion
        /// <summary>
        /// 定时任务执行
        /// </summary>
        /// <param name="context"></param>
        public Task Execute(IJobExecutionContext context)
        {
            var startTime = DateTime.Now.AddHours(-1);
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
                var queue = WddQueue.Route_Key;//"FailedData";
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
                    startTime = DateTime.Now.AddHours(-1);
                    while (true)
                    {
                        Thread.Sleep(5000);
                        messageCount = rabbitHelper.GetMessageCount(ccuri, queue, route, exchange, queueHost, queuePort, queueUserName,
                    queuePassword) + rabbitHelper.GetMessageCount(ccuri, errorQueue, errorQueue, exchange, queueHost, queuePort, queueUserName,
                    queuePassword);
                        if (messageCount < 1) break;
#if DEBUG
                        Logger.Info($"[HourStatisticsTask]:当前队列还有{messageCount}个数据位解析，请等待");
#endif

                    }
                };
            }
             
          
            var dt = DateTime.Now.AddHours(-1);
            if (dt.CompareTo(startTime) == 0)
            {
                MainTask(dt);
                return Task.CompletedTask;
            }
            //自动恢复
            var hours = dt.Subtract(startTime).TotalHours;
            for (int i = 0; i < hours; i++)
            {
                var time = startTime.AddHours(i);
                MainTask(time);
            }

#if DEBUG
            Logger.Info("[HourStatisticsTask]Hour Statistics");
            Logger.Info($"耗时{context.JobRunTime.TotalMilliseconds}ms");
#endif
            //GC.Collect();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 主任务
        /// </summary>
        /// <param name="dt"></param>
        private void MainTask(DateTime dt)
        {
            CyByHourRepository.InsertHourStatistics(dt, "CY");
            SccByHourRepository.InsertHourStatistics(dt, "SCC");
            CcByHourRepository.InsertHourStatistics(dt, "CC");
            JcByHourRepository.InsertHourStatistics(dt, "JC");
            MfByHourRepository.InsertHourStatistics(dt, "MF");
            VcByHourRepository.InsertHourStatistics(dt, "VC");
            VibByHourRepository.InsertHourStatistics(dt, "VB");
            PulByHourRepository.InsertHourStatistics(dt, "PUL");
            IcByHourRepository.InsertHourStatistics(dt, "IC");
            HvibByHourRepository.InsertHourStatistics(dt, "HVB");
            HvibByHourRepository.Batch();
            //rhcByHourRepository.Insert(dt);
            //dtrByHourRepository.InsertAsync(t);

        }


        /// <summary>
        /// 统计恢复任务
        /// </summary>
        /// <param name="dt"></param>
        public static void RecoveryTask(DateTime dt)
        {
            CyByHourRepository.RecoveryHourStatistics(dt, "CY");
            SccByHourRepository.RecoveryHourStatistics(dt, "SCC");
            CcByHourRepository.RecoveryHourStatistics(dt, "CC");
            JcByHourRepository.RecoveryHourStatistics(dt, "JC");
            MfByHourRepository.RecoveryHourStatistics(dt, "MF");
            VcByHourRepository.RecoveryHourStatistics(dt, "VC");
            VibByHourRepository.RecoveryHourStatistics(dt, "VB");
            PulByHourRepository.RecoveryHourStatistics(dt, "PUL");
            IcByHourRepository.RecoveryHourStatistics(dt, "IC");
            HvibByHourRepository.RecoveryHourStatistics(dt, "HVB");
            HvibByHourRepository.Batch();

        }

        public static dynamic Test(DateTime dt,string motorId)
        {
           return CyByHourRepository.GetHour(dt, motorId);
        }
    }
}
