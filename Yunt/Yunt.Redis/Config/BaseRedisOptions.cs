using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Redis.Config
{
    /// <summary>
    /// Base redis options.
    /// </summary>
    public class BaseRedisOptions
    {
        /// <summary>
        /// 单一服务器模式，如果为False 将启用读取分离模式"
        /// </summary>
        public bool SingleMode { get; set; } = true;

        /// <summary>
        /// 第几个数据库
        /// </summary>
        public int DbIndex { get; set; } = 0;
        /// <summary>
        /// Gets or sets the password to be used to connect to the Redis server.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to use SSL encryption.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is SSL; otherwise, <c>false</c>.
        /// </value>
        public bool IsSsl { get; set; } = false;

        /// <summary>
        /// Gets or sets the SSL Host.
        /// If set, it will enforce this particular host on the server's certificate.
        /// </summary>
        /// <value>
        /// The SSL host.
        /// </value>
        public string SslHost { get; set; } = null;

        /// <summary>
        /// Gets or sets the timeout for any connect operations.
        /// </summary>
        /// <value>
        /// The connection timeout.
        /// </value>
        public int ConnectionTimeout { get; set; } = 5000;

        /// <summary>
        /// Gets the list of endpoints to be used to connect to the Redis server.
        /// </summary>
        /// <value>
        /// The endpoints.
        /// </value>
        public IList<ServerEndPoint> Endpoints { get; } = new List<ServerEndPoint>();

        public IList<WriteHostItem> Writes { get; } = new List<WriteHostItem> {  };
        public IList<ReadHostItem> Reads { get; } = new List<ReadHostItem> {  };
        public IList<HostItem> RedisServer { get; } = new List<HostItem> {  };

        public int Connections { get; set; } = 60;

    }
}
