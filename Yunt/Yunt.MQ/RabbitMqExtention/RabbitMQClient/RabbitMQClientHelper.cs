using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System.Threading;
using Yunt.Common;

namespace Yunt.MQ
{
    public class RabbitMQClientHelper
    {
        /// <summary>
        /// 会话实体列表
        /// </summary>
        public List<SessionModel> _sessionModels = new List<SessionModel>();
        public List<Subscription> _subscriptions = new List<Subscription>();
        Subscription _sub = null;


        public static string MsgSysName;

        public string MsgSys
        {
            get
            {
                return MsgSysName;
            }
            set
            {
                MsgSysName = value;
            }
        }
        public RabbitMQClientHelper(string _msgSys) //Constructor
        {
            Logger.Info("\nMsgSys: RabbitMQ");
            MsgSys = _msgSys;
        }


        /// <summary>
        /// 队列与通道绑定，并设定路由类型
        /// </summary>
        /// <param name="queueName">队列名</param>
        /// <param name="model">通道</param>
        public void PublishToQueue(string queueName, IModel model)
        {
            string finalQueueName = model.QueueDeclare(queueName, true, false, false, null);
            model.QueueBind(finalQueueName, "amq.topic", queueName, null);
        }
        /// <summary>
        /// 订阅集合列表
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="_channel">通道</param>
        public void SubscribeToQueue(string queueName, IModel _channel)
        {
            try
            {
                //channel = _channel;
                _sub = new Subscription(_channel, queueName);
                _subscriptions.Add(_sub);
                var session = new SessionModel() { Channel = _channel, Subscription = _sub };
                _sessionModels.Add(session);
            }
            catch (Exception ex)
            {

                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// 初始化主题订阅对象集合
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="queueDictionary">队列和路由关键字集合</param>
        /// <param name="exchange">交换机类型</param>
        /// <param name="userName">用户名</param>
        ///  <param name="pwd">密码</param>
        public void InitSubscribes(string url, List<QueueModel> queues, string exchange, string userName, string pwd)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    Logger.Error("参数url不能为空，请检查参数");
                if (!queues.Any())
                    Logger.Error("参数queues不能为空，请检查参数");

                var factory = new ConnectionFactory();

                factory = new ConnectionFactory();
                factory.Uri =new Uri(url);

                //factory.UserName = userName;
                //factory.Password = pwd;
                foreach (var queue in queues)
                {
                    var iModel = ConnectionPool.GetOrCreateChannel(factory, url, queue.QueueName, queue.Key, exchange);
                    SubscribeToQueue(queue.Key, iModel.Model);//加入到subscribes集合中去
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                //iModel.SetNotBusy();
            }
        }
        /// <summary>
        /// 写队列
        /// </summary>
        /// <param name="body">消息实体</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="model">通道</param>
        public void WriteMessage(byte[] body, string queueName, IModel model)
        {
            //declare a queue if it doesn't exist
            PublishToQueue(queueName, model);

            model.BasicPublish("amq.topic", queueName, null, body);
            Logger.Info("\n  [x] Sent to queue : "+ queueName);
        }
    }
}
