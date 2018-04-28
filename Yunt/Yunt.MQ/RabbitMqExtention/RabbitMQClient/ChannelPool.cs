using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using RabbitMQ.Client.Framing.Impl;
using Yunt.Common;

namespace Yunt.MQ
{
    public class ChannelPool
    {
        private List<IModelWrapper> ListChannel = new List<IModelWrapper>();
    
        private Connection Conn;

        /// <summary>
        /// 扫描周期，单位（秒），默认60秒
        /// </summary>
        private static readonly int ScanvageInterval = 60 * 1000;

        /// <summary>
        /// 最大闲置时间，单位（秒）,默认120秒
        /// </summary>
        internal static int MaxIdleTime = 120;


        private readonly System.Timers.Timer ScanTimer;

        /// <summary>
        /// 读写锁
        /// </summary>
        private ReaderWriterLockSlim GetChannellockObj = new ReaderWriterLockSlim();

       

        public ChannelPool(Connection conn)
        {
            if (conn == null)
               Logger.Error("参数conn不能为空，请检查参数");

            Conn = conn;

            //后台清理线程，定期清理整个应用域中每一个Channel中的每一个Channel项
            ScanTimer = new System.Timers.Timer(ScanvageInterval);
            ScanTimer.Elapsed += new System.Timers.ElapsedEventHandler(DoScavenging);
            ScanTimer.AutoReset = true;
            ScanTimer.Enabled = true;
        }
        private void DoScavenging(object sender, ElapsedEventArgs e)
        {
            GetChannellockObj.EnterUpgradeableReadLock();

            try
            {
                var keysToRemove = new List<IModelWrapper>();
               Logger.Info("当前ChannelPool："+this.GetHashCode() +" 扫描时间："+DateTime.Now);
                foreach (var item in ListChannel)
                {
                    var timeDur = TimeHelper.GetTimeDurationTotalSeconds(item.IdleTime, DateTime.Now);
                    if (item.IsBusy == false && timeDur >= MaxIdleTime)
                    {
                       Logger.Info(string.Format("Channel:{0}闲置时间超时,清除，最后使用时间{1},当时UTC时间{2}，时间差:{3}，大于MaxIdleTime:{4}"
                        ,item.GetHashCode() ,item.IdleTime , DateTime.Now, timeDur, MaxIdleTime));
                        keysToRemove.Add(item);
                    }
                }

                foreach (var item in keysToRemove)
                    InnerRemove(item);
            }
            finally
            {
                GetChannellockObj.ExitUpgradeableReadLock();
            }
        }

        internal void InnerRemove(IModelWrapper item)
        {
            GetChannellockObj.EnterWriteLock();
            try
            {
                ListChannel.Remove(item);

                item.Dispose();

               Logger.Info($"Channel:{item.GetHashCode()}闲置时间超时，清除成功,当前剩余连接数：{ListChannel.Count} ");
            }
            finally
            {
                GetChannellockObj.ExitWriteLock();
            }
        }
        private IModelWrapper CreateChannel(string routingKey, string exchange, string queueName)
        {
            GetChannellockObj.EnterWriteLock();
            try
            {
                var channel = Conn.CreateModel();
                channel.ExchangeDeclare(exchange, "topic", true);//direct
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(queueName, exchange, routingKey, null);

                var ModelWrapper = new IModelWrapper(channel, routingKey, exchange, queueName);

                ListChannel.Add(ModelWrapper);

               Logger.Info("当前Channel连接池数量：" + ListChannel.Count);

                return ModelWrapper;
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message);
                return null;
            }
            finally
            {
                GetChannellockObj.ExitWriteLock();
            }
        }
        public IModelWrapper GetOrCreateChannel(string queueName, string routingKey, string exchange)
        {
            GetChannellockObj.EnterUpgradeableReadLock();///若线程用光了，得写一个连接线程，所以这里用可升级锁
            try
            {
                var Channel = ListChannel.Find(p =>
                    p.IsBusy == false && p.Exchange == exchange && p.QueueName == queueName);

                if (Channel != null)
                {
                    Channel.IsBusy = true;
                    return Channel;
                }
                else if (Channel == null && Conn.KnownHosts.Length <Conn.ChannelMax)
                {
                    return CreateChannel(routingKey, exchange, queueName);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                GetChannellockObj.ExitUpgradeableReadLock();
            }
        }
    }
}
