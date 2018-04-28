using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.MQ
{
   public class QueueModel
    {
        /// <summary>
        /// 队列名
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 路由关键字
        /// </summary>
        public string Key { get; set; }
    }
}
