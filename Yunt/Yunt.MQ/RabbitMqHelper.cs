using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.MessagePatterns;
using Yunt.Common;
using System.Threading;

namespace Yunt.MQ
{
    public class RabbitMqHelper
    {
        public static bool Cancelled { get; set; }

        IConnection _connection = null;
        IModel _channel = null;
        Subscription _subscription = null;

        readonly RabbitMQClientHelper _rabbitMqClientHelper =
            new RabbitMQClientHelper("[RabbitMq]Yunt Data Parse");

        ConnectionFactory _factory = null;

        /// <summary>
        /// 数据队列读取
        /// </summary>
        /// <param name="brokerUri">连接字符串</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routKey">队列关键字</param>
        ///  <param name="exchange">交换机</param>
        /// <param name="hostName">主机名</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param> 
        ///  <param name="rabbitMqResolveInterval">解析间隔</param>
        ///  <param name="errorQueueName">错误数据队列名称</param>
        /// <param name="operation">匿名委托方法</param>
        /// <param name="type">数据类型</param>
        public void Read<T>(string brokerUri, string queueName,string routKey,string exchange,
            string hostName, int port, string username, string password, int rabbitMqResolveInterval,
            string errorQueueName, Func<byte[], T, bool> operation, T type)
        {

            if (_factory == null)
                _factory = new ConnectionFactory()
                {
                    Uri = new Uri(brokerUri),
                    RequestedHeartbeat = 30,
                    HostName = hostName,
                    Port = port,
                    UserName = username,
                    Password = password,
                };
            while (!Cancelled)
            {
                if (Cancelled)
                {
                    DisposeAllConnectionObjects();
                    break;
                }
                try
                {
                    if (_subscription == null)
                    {
                        try
                        {
                            _connection = _factory.CreateConnection();
                        }
                        catch (BrokerUnreachableException ex)
                        {
                            //You probably want to log the error and cancel after N tries, 
                            //otherwise start the loop over to try to connect again after a second or so.
                            Logger.Error("[RabbitMq]Create Connection Failed." + ex.Message);
                            continue;
                        }

                        _channel = _connection.CreateModel();
                        var declare = _channel.QueueDeclare(queueName, true, false, false, null);
                        _channel.QueueBind(queue: queueName, exchange: exchange,routingKey: routKey);
                        var messageCount = declare.MessageCount;//获取当前队列中的未读消息数
#if DEBUG
                        if (messageCount > 2)
                            Logger.Info($"[队列未读数量]:{messageCount}");
#endif
                        _subscription = new Subscription(_channel, queueName, false);
                    }

                    BasicDeliverEventArgs args;
                    var gotMessage = _subscription.Next(rabbitMqResolveInterval, out args); //250
                    if (!gotMessage) continue;
                    {
                        if (args == null)
                        {
                            //This means the connection is closed.
                            DisposeAllConnectionObjects();
                            Logger.Error("[RabbitMq]Connection is closed!");
                            continue;
                        }

                        var bytes = args.Body;
                        _subscription.Ack(args);

                        Write(brokerUri, args.Body, queueName + "BK", queueName + "BK",exchange, username, password);
                        try
                        {
#if DEBUG
                            var sw = new Stopwatch();
                            sw.Start();
#endif
                            var result = operation(bytes, type);
#if DEBUG
                            sw.Stop();
                            Logger.Info($"[RabbitMq]解析数据耗时:{sw.ElapsedMilliseconds}ms");
#endif
                            if (!result)
                            {
                                //再尝试解析两次
                                for (var i = 0; i < 2; i++)
                                {
#if DEBUG
                                    var sw1 = new Stopwatch();
                                    sw1.Start();
#endif
                                    result = operation(bytes, type);
#if DEBUG
                                    sw1.Stop();
                                    Logger.Info($"[RabbitMq]解析数据耗时:{sw1.ElapsedMilliseconds}ms");
#endif
                                    if (result) break;
                                }
                            }

                            if (!result)
                            {
                                Write(brokerUri, args.Body, errorQueueName, errorQueueName, exchange, username, password);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Update DB: " + ex.Message);
                        }
                    }
                }
                catch (OperationInterruptedException ex)
                {
                    DisposeAllConnectionObjects();
                    Logger.Error("[RabbitMq]获取数据错误： " + ex.Message);
                }
            }
            DisposeAllConnectionObjects();
        }
        /// <summary>
        /// 队列写入;
        /// </summary>
        /// <param name="uri">连接字符串</param>
        /// <param name="buffer">数据</param>
        /// <param name="queueName">队列名(默认与路由关键字一致)</param>
        /// <param name="routKey">队列关键字</param>
        ///  <param name="exchange">交换机</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        public void Write(string uri, byte[] buffer, string queueName, string routKey, string exchange, string userName, string pwd)
        {

            if (_factory == null)
                _factory = new ConnectionFactory()
                {
                    Uri = new Uri(uri),
                    //RequestedHeartbeat = 30,
                    UserName = userName,
                    Password = pwd,
                    Endpoint = new AmqpTcpEndpoint(new Uri(uri))
                };
            if (_connection == null)
                _connection = _factory.CreateConnection();
            if (_channel == null)
                _channel = _connection.CreateModel();
            //using (var conn = _factory.CreateConnection())
            {
                //using (var channel = conn.CreateModel())
                {
                    //定义一个持久化队列;
                    _channel.QueueDeclare(queueName, true, false, false, null);
                    _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routKey);
                    var properties = _channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;

                    _channel.BasicPublish(exchange, routKey, properties, buffer);
                }
            }

            //catch (Exception ex)
            //{
            //    Logger.Error("[RabbitMq]写入队列数据错误： " + ex.Message);
            //}
        }

        /// <summary>
        /// 控制命令队列写入
        /// </summary>
        /// <param name="brokerUri"></param>
        /// <param name="queueName"></param>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool Write(string brokerUri, string queueName,
            string hostName, int port, string username, string password, byte[] buffer)
        {
            try
            {


                if (_factory == null)
                    _factory = new ConnectionFactory()
                    {
                        Uri = new Uri(brokerUri) ,
                        RequestedHeartbeat = 30,
                        HostName = hostName,
                        Port = port,
                        UserName = username,
                        Password = password,
                    };
                if (_connection == null)
                    _connection = _factory.CreateConnection();
                if (_channel == null)
                    _channel = _connection.CreateModel();
                // using (var conn = factory.CreateConnection())
                {
                    // using (var channel = conn.CreateModel())
                    {
                        _channel.QueueDeclare(queueName, true, false, false, null);
                        var properties = _channel.CreateBasicProperties();
                        properties.DeliveryMode = 2;
                        _channel.BasicPublish("amq.topic", queueName, properties, buffer);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取队列中数据数量
        /// </summary>
        /// <param name="brokerUri">连接字符串</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routKey">队列关键字</param>
        ///  <param name="exchange">交换机</param>
        /// <param name="hostName">主机名</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param> 
        /// <returns></returns>
        public uint GetMessageCount(string brokerUri, string queueName, string routKey, string exchange,
            string hostName, int port, string username, string password)
        {

            if (_factory == null)
                _factory = new ConnectionFactory()
                {
                    Uri = new Uri(brokerUri),
                    RequestedHeartbeat = 30,
                    HostName = hostName,
                    Port = port,
                    UserName = username,
                    Password = password,
                };

            try
            {
                if (_subscription == null)
                {
                    try
                    {
                        if(_connection==null)
                            _connection = _factory.CreateConnection();
                    }
                    catch (BrokerUnreachableException ex)
                    {
                        Logger.Error("[RabbitMq]Create Connection Failed." + ex.Message);
                    }
                    if (_channel == null)
                        _channel = _connection.CreateModel();
                    var declare = _channel.QueueDeclare(queueName, true, false, false, null);
                    _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routKey);
                    return declare.MessageCount; //获取当前队列中的未读消息数
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[RabbitMq]获取数据错误： " + ex.Message);
            }
            return 0;
        }

        public void DisposeAllConnectionObjects()
        {
            if (_subscription != null)
            {
                //IDisposable is implemented explicitly for some reason.
                ((IDisposable)_subscription).Dispose();
                _subscription = null;
            }

            if (_channel != null)
            {
                _channel.Dispose();
                _channel = null;
            }

            if (_connection != null)
            {
                try
                {
                    _connection.Dispose();
                }
                catch (EndOfStreamException)
                {
                }
                _connection = null;
            }
        }



        #region New Data Parse

        /// <summary>
        ///  开启读取所有队列的任务
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="queueList"></param>
        /// <param name="operation"></param>

        public void TaskForReadAllQueue<T>(string uri, string username, string password, List<QueueModel> queueList, Func<byte[], T, bool> operation, T type)
        {
            try
            {
                _rabbitMqClientHelper.InitSubscribes(uri, queueList, "amq.topic", username, password);

                if (!_rabbitMqClientHelper._sessionModels.Any()) return;

                foreach (var session in _rabbitMqClientHelper._sessionModels)
                {
                    Thread.Sleep(50);
                    Task.Factory.StartNew(() => ReadMessage(session, operation,type));
               
                }

                Logger.Info("[RabbitMq]No message in the queue(s) at this time.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// 读取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="operation"></param>
        /// <param name="type"></param>
        private void ReadMessage<T>(SessionModel element, Func<byte[], T, bool> operation, T type)
        {
            try
            {
                byte[] buff = null;
                string queueName;
                while (true)
                {
                    if (element.Subscription.QueueName == null)
                    {
                        break;
                    }
                    BasicDeliverEventArgs ev;
                    var gotMessage = element.Subscription.Next(250, out ev); //250
                    if (gotMessage)
                    {
                        if (ev == null)
                        {
                            //This means the connection is closed.
                            DisposeAll(element);
                            Logger.Error("[RabbitMq]Connection is closed!");
                            continue;
                        }

                        buff = ev.Body;
                        queueName = element.Subscription.QueueName;
                        element.Subscription.Ack(ev);

                        _rabbitMqClientHelper.WriteMessage(buff, queueName + "BK", element.Channel);
                        //Thread.Sleep(3000);
                        //try
                        //{
                        //    var thread = new Thread(() =>
                        //    {
                        var result = operation(buff, type);
                        if (!result)
                        {
                            //再尝试解析两次
                            for (int i = 0; i < 2; i++)
                            {
#if DEBUG
                                var sw1 = new Stopwatch();
                                sw1.Start();
#endif
                                result = operation(buff, type);
#if DEBUG
                                sw1.Stop();
                                Logger.Info($"[RabbitMq]解析数据耗时:{sw1.ElapsedMilliseconds}ms");
#endif
                                if (result) break;
                            }
                        }
                        if (!result)
                        {
                            _rabbitMqClientHelper.WriteMessage(buff, "FailedTestData", element.Channel);
                        }
                        //    });
                        //    thread.Start();
                        //    thread.Join(60000);
                        //    thread.Abort();
                        //}
                        //catch (Exception ex)
                        //{
                        //    Container.Error("Update DB: " + ex.Message);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                DisposeAll(element);
                Logger.Error("[RabbitMq]获取数据错误： " + ex.Message);
            }
        }

        /// <summary>
        /// 释放会话
        /// </summary>
        /// <param name="element">会话实体</param>
        public void DisposeAll(SessionModel element)
        {
            element.Subscription.Close();
            Logger.Info("[RabbitMq]\nDestroying RABBIT");
        }

        #endregion

    }
}
