using RabbitMQ.Client;
using RabbitMQ.Client.Impl;
using System;
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
    public static class ConnectionPool
    {
        private static readonly List<ConnectionWrapper> ConnList = new List<ConnectionWrapper>();///连接池里的链接
        /// <summary>
        /// 读写锁
        /// </summary>
        private static ReaderWriterLockSlim GetConnlockObj = new ReaderWriterLockSlim();



        /// <summary>
        /// 扫描周期，单位（秒），默认60秒
        /// </summary>
        private static readonly int ScanvageInterval = 60*1000;

        /// <summary>
        /// 最大闲置时间，单位（秒）,默认120秒
        /// </summary>
        internal static int MaxIdleTime = 120;

        private static readonly System.Timers.Timer ScanTimer;

        static ConnectionPool()
        {
            //后台清理线程，定期清理整个应用域中每一个SocketQueue中的每一个Socket项
            ScanTimer = new System.Timers.Timer(ScanvageInterval);
            ScanTimer.Elapsed += new System.Timers.ElapsedEventHandler(DoScavenging);
            ScanTimer.AutoReset = true;
            ScanTimer.Enabled = true;

        }

        /// <summary>
        /// 清理方法，清理本CacheQueue中过期的cache项
        /// </summary>
        private static void DoScavenging(object sender, ElapsedEventArgs e)
        {
            List<ConnectionWrapper> keysToRemove = new List<ConnectionWrapper>();

            GetConnlockObj.EnterUpgradeableReadLock();
            Logger.Info("当前ConnectionPool:扫描时间：" + DateTime.Now);
            try
            {
                foreach (var item in ConnList)
                {
                    if (item.HasSessionConn() == false)
                    {
                        if (item.IdleTime == default(DateTime))
                            item.IdleTime = DateTime.UtcNow;
                        else
                        {
                            var timeDur = TimeHelper.GetTimeDurationTotalSeconds(item.IdleTime, DateTime.Now);
                            if (timeDur > MaxIdleTime)
                            {
                                Logger.Info(string.Format("Connection:{0}闲置时间超时，清除，最后使用时间{1},当时UTC时间{2}，时间差:{3}，大于MaxIdleTime:{4}"
                                    , item.GetHashCode(), item.IdleTime, DateTime.Now, timeDur, MaxIdleTime));
                                keysToRemove.Add(item);
                            }
                        }
                    }
                }

                foreach (var item in keysToRemove)
                    InnerRemove(item);
            }
            finally
            {
                GetConnlockObj.ExitUpgradeableReadLock();
            }
        }

        internal static void InnerRemove(ConnectionWrapper item)
        {
            GetConnlockObj.EnterWriteLock();
            try
            {
                ConnList.Remove(item);
                item.Dispose();
                item = null;

                Logger.Info($"Connection:{item.GetHashCode()}闲置时间超时，清除成功,当前剩余连接数：{ConnList.Count} ");
            }
            finally
            {
                GetConnlockObj.ExitWriteLock();
            }
        }

        public static IModelWrapper GetOrCreateChannel(ConnectionFactory connFactory, string url, string queueName, string routingKey
            , string exchange)
        {
            if (connFactory == null)
                Logger.Error("参数connFactory不能为空，请检查参数");
            if (string.IsNullOrEmpty(url))
                Logger.Error("参数url不能为空，请检查参数");

            GetConnlockObj.EnterUpgradeableReadLock();///若线程用光了，得写一个连接线程，所以这里用可升级锁
            IModelWrapper model = null;
            try
            {
                foreach (var item in ConnList)
                {
                    if (string.Compare(item.Url, url, true) == 0)
                    {
                        model = item.GetOrCreateChannel(queueName, routingKey, exchange);
                        if (model != null)
                            return model;
                    }
                }
                if (model == null)
                {
                    //创建connection对象
                    var conn = CreateConnection(connFactory, url);
                    return conn.GetOrCreateChannel(queueName, routingKey, exchange);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                GetConnlockObj.ExitUpgradeableReadLock();
            }
            return null;
        }

        private static ConnectionWrapper CreateConnection(ConnectionFactory connFactory, string url)
        {
            GetConnlockObj.EnterWriteLock();
            try
            {
                Connection newConn = (Connection)connFactory.CreateConnection();
                ConnectionWrapper connWrapper = new ConnectionWrapper(connFactory, url, newConn);
                ConnList.Add(connWrapper);

                Logger.Info("当前Connection连接池数量：" + ConnList.Count);
                return connWrapper;
            }
            finally
            {
                GetConnlockObj.ExitWriteLock();
            }
        }

    }
}
