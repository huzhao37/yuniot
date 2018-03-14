
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace Yunt.Redis
{
   public class InitRedis 
    {
        #region Redis

        /// <summary>
        /// REDIS序列化协议
        /// </summary>
        /// 
        protected const DataType SerializeType = DataType.Protobuf;

        /// <summary>
        /// Redis Key
        /// </summary>
       // protected static readonly string RedisKey = typeof(TEntity).Name;

        /// <summary>
        /// Redis Client
        /// </summary>
        public static readonly RedisCachingProvider RedisClient;

        #endregion

       static InitRedis()
       {
            RedisClient = RedisCachingProvider.DefaultDB;
        }
    }
}
