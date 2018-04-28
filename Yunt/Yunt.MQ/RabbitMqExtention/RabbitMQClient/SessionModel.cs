using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Yunt.MQ
{
   public class SessionModel
   {
        /// <summary>
        /// 通道
        /// </summary>
       public IModel Channel { get; set; }
        /// <summary>
        /// 订阅对象
        /// </summary>
        public Subscription Subscription { get; set; }
   }
}
