﻿using System;
using System.Collections.Generic;
using System.Text;
using Yunt.Common;

namespace Yunt.Redis.Config
{
    /// <summary>
    /// Defines an endpoint.
    /// </summary>
    public class ServerEndPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EasyCaching.Redis.ServerEndPoint"/> class.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="port">Port.</param>
        public ServerEndPoint(string host, int port)
        {
            ArgumentCheck.NotNullOrWhiteSpace(host, nameof(host));

            Host = host;
            Port = port;
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }
    }
}
