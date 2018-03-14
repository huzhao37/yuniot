//  SOLUTION  ： 云统物联网V3
//  PROJECT     ： Yunt.Redis
//  FILENAME   ： RedisClientSeting.cs
//  AUTHOR     ： soft-zh
//  CREATE TIME： 14:59
//  COPYRIGHT  ： 版权所有 (C) 云统信息科技有限公司 http://www.unitoon.cn/ 2017~2018

#region using namespace

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using NewLife.Xml;
using Yunt.Redis.Config;

#endregion

namespace Yunt.Redis.Config
{
    /// <summary>Redis</summary>
    [DisplayName("Redis")]
    [XmlConfigFile(@"Config\Redis.config", 15000)]
    [Serializable]
    public class RedisClientSeting : XmlConfig<RedisClientSeting>
    {
        public RedisClientSeting()
        {
            RedisServer = new List<HostItem>();
            Writes = new List<WriteHostItem>();
            Reads = new List<ReadHostItem>();

            SingleMode = true;
        }

        private bool _singleMode;
        /// <summary>
        ///     单一服务器模式，如果为False 将启用读取分离模式
        /// </summary>
        [Description("单一服务器模式，如果为False 将启用读取分离模式")]
        public bool SingleMode
        {
            get { return _singleMode; }
            set
            {
                _singleMode = value;
                if (_singleMode)
                {
                    RedisServer = new List<HostItem> { new HostItem( ) };
                    Writes.Clear();
                    Reads.Clear();
                }
                else
                {
                    Writes = new List<WriteHostItem> { new WriteHostItem( ) };
                    Reads = new List<ReadHostItem> { new ReadHostItem( ) };
                    RedisServer.Clear();
                }
            }
        }

        /// <summary>
        /// 第几个数据库
        /// </summary>
        public int DbIndex { get; set; }

        /// <summary>
        /// 单一服务器下配置
        /// </summary>
        public List<HostItem> RedisServer { get; set; }

        /// <summary>
        /// 读写分离模式下：写服务器
        /// </summary>
        public List<WriteHostItem> Writes { get; set; }

        /// <summary>
        /// 读写分离模式下：读服务器
        /// </summary>
        public List<ReadHostItem> Reads { get; set; }
    }
}