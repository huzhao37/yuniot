using RabbitMQ.Client;
using RabbitMQ.Client.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing.Impl;
using Yunt.Common;

namespace Yunt.MQ
{
    public class ConnectionWrapper:IDisposable
    {
        public ConnectionFactory ConnFactory { get; set; }

        public string Url { get; set; }

        public DateTime IdleTime { get; set; }

        protected Connection Conn { get; set; }

        protected ChannelPool ChannelPool { get; set; }


        public IModelWrapper GetOrCreateChannel(string queueName, string routingKey, string exchange)
        {
            IModelWrapper result= this.ChannelPool.GetOrCreateChannel(queueName, routingKey, exchange);

            if (result != null)
                IdleTime = default(DateTime);

            return result;
        }

        public void Dispose()
        {
            ((IConnection)Conn).Dispose();
            ChannelPool = null;
        }

        public bool HasSessionConn()
        {
            return this.Conn.KnownHosts.Length > 0;
        }

        public ConnectionWrapper(ConnectionFactory connFactory, string url, Connection conn)
        {
            if (conn == null)
                Logger.Error("参数conn不能为null，请检查参数");
            if (string.IsNullOrEmpty(url))
                Logger.Error("参数url不能为空，请检查参数");

            Url = url;
            Conn = conn;
            ConnFactory = connFactory;
            ChannelPool = new ChannelPool(conn);
        }

    
    }
}
