using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.XmlProtocol.Domain.MiddleMap
{
    /// <summary>
    /// 数据接收模型  ;
    /// </summary>
    public class DataGramModel
    {
        /// <summary>
        /// 设备ID;
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 采集设备Index;
        /// </summary>
        public string CollectdeviceIndex { get; set; }
        /// <summary>
        /// 数据字段个数;
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 数据表单值;
        /// </summary>
        public Dictionary<DateTime, List<int>> PValues { get; set; }
        
        public DataGramModel()
        {
            PValues = new Dictionary<DateTime, List<int>>();
        }
    }
}
