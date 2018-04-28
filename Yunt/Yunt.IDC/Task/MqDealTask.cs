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
using Yunt.Common;
using Yunt.IDC.Helper;
using Yunt.MQ;
using Yunt.XmlProtocol.Domain.Models;
using Yunt.XmlProtocol.Domain.Service;

namespace Yunt.IDC.Task
{
   /// <summary>
   /// 队列解析任务
   /// </summary>
  public  class MqDealTask
    {  
        /// <summary>
        /// 所有队列集合
        /// </summary>
        static readonly List<QueueModel> _queueList = new List<QueueModel>();

        /// <summary>
        /// 启动队列解析
        /// </summary>
        public static void Start()
        {
            var wddQueue =
                Messagequeue.FindAll()
                    .Where(e => e.WriteRead.Equals(WriteOrRead.Read) && !e.RouteKey.EqualIgnoreCase("STATUS")).FirstOrDefault();
            var interval = wddQueue.Timer;
#if DEBUG
            var s1 = new Stopwatch();
            s1.Start();
#endif
            var queueHost = wddQueue.Host;
            var queuePort = wddQueue.Port;
            var queueUserName = wddQueue.Username;
            var queuePassword = wddQueue.Pwd;

            var ccuri = "amqp://" + queueHost + ":" + queuePort;
            var queue = wddQueue.RouteKey; //"FailedData";// //AppSettings.GetConfigValue("QueueName");// //sumin
            var errorQueue = queue+"Error"; //faileddata
#if DEBUG
            s1.Stop();
            Logger.Info($"耗时{s1.ElapsedMilliseconds}ms");
#endif
            var rabbitHelper = new RabbitMqHelper();
            RabbitMqHelper.Cancelled = !Program.Configuration.GetSection("AppSettings").GetValue<bool>("MqDealEnable");
            rabbitHelper.Read(ccuri, queue, queueHost, queuePort, queueUserName, queuePassword, interval, errorQueue,Deal,DataType.Integrate);



        
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
                    return MqHandler.UniversalParser(buffer, "SimonsConeCrusher", BytesToDb.Saving);
                case DataType.Integrate:
                    return MqHandler.UniversalParser(buffer, "Integrate", BytesToDb.Saving);
                case DataType.Locker:
                    return MqHandler.UniversalParser(buffer, "Locker", BytesToDb.Saving);
                default:
                    return true;
            }
        }

    }
}
