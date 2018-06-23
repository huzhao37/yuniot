using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.Device.Repository.EF.Repositories;
using Yunt.IDC.Helper;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
using Yunt.Xml.Domain.Services;

namespace Yunt.IDC.Task
{
   /// <summary>
   /// 队列解析任务
   /// </summary>
  public  class MqDealTask
    {
        private static readonly IMessagequeueRepository MessagequeueRepository;
        private static readonly IBytesParseRepository BytesParseRepository;
        private static readonly IProductionLineRepository ProductionLineRepository;
        private static readonly IMotorRepository MotorRepository;
        static MqDealTask()
       {
            MessagequeueRepository = ServiceProviderServiceExtensions.GetService<IMessagequeueRepository>(Program.Providers["Xml"]);
            BytesParseRepository = ServiceProviderServiceExtensions.GetService<IBytesParseRepository>(Program.Providers["Xml"]);
            ProductionLineRepository = ServiceProviderServiceExtensions.GetService<IProductionLineRepository>(Program.Providers["Device"]);
            MotorRepository = ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
        }
       /// <summary>
       /// 所有队列集合
       /// </summary>
       //static readonly List<QueueModel> _queueList = new List<QueueModel>();
       internal static  Messagequeue WddQueue;
        /// <summary>
        /// 启动队列解析
        /// </summary>
        public static void Start()
        {
            #region 预热instancedata-3个月数据

            //if (!ProductionLineRepository.GetInstanceFromRedis("WDD-P001"))
            //{
            //    var motors = MotorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase("WDD-P001"))?.ToList();
            //    if (motors?.Any() ?? true)
            //    {
            //        motors.ForEach(e=>
            //        {
            //            var list = ProductionLineRepository.PreCache(e.MotorId, DateTime.Today.Date);

            //        });
            //    }
            //}


            #endregion
            var w_r =(int) WriteOrRead.Read;
            //var where = " Write_Read = '" + w_r + "' and  Route_Key != 'STATUS'";
            WddQueue =
               MessagequeueRepository.GetEntities(e => e.Write_Read.Equals(w_r) && !e.Route_Key.Equals("STATUS")).FirstOrDefault();
            if (WddQueue == null)
                return;
            var interval = WddQueue.Timer;
#if DEBUG
            var s1 = new Stopwatch();
            s1.Start();
#endif
            var queueHost = WddQueue.Host;
            var queuePort = WddQueue.Port;
            var queueUserName = WddQueue.Username;
            var queuePassword = WddQueue.Pwd;
            
            var ccuri = "amqp://" + queueHost + ":" + queuePort;
            var queue = WddQueue.Route_Key;//"FailedData";
            var route = WddQueue.Route_Key; //"0102030405FE.WUDD";//
            var exchange = "amq.topic";
            var errorQueue = queue+"Error"; //faileddata
#if DEBUG
            s1.Stop();
            Logger.Info($"耗时{s1.ElapsedMilliseconds}ms");
#endif
            var rabbitHelper = new RabbitMqHelper();
            RabbitMqHelper.Cancelled = !Program.Configuration.GetSection("AppSettings").GetValue<bool>("MqDealEnable");
            rabbitHelper.Read(ccuri, queue, route,exchange, queueHost, queuePort, queueUserName, queuePassword, interval, errorQueue,Deal,DataType.Integrate);
            //rabbitHelper.TaskForReadAllQueue(ccuri,queueUserName,queuePassword, Deal, DataType.Integrate);
        }
        /// <summary>
        /// 队列持久化
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="type"></param>
        public static bool Deal(byte[] buffer, DataType type)
        {
            switch (type)
            {
                case DataType.SimonsConeCrusher:
                    return BytesParseRepository.UniversalParser(buffer, "SimonsConeCrusher", BytesToDb.Saving);
                case DataType.Integrate:
                    return BytesParseRepository.UniversalParser(buffer, "Integrate", BytesToDb.Saving);
                case DataType.Locker:
                    return BytesParseRepository.UniversalParser(buffer, "Locker", BytesToDb.Saving);
                default:
                    return true;
            }
        }

    }
}
