using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.XmlCheck.XmlModel
{
    /// <summary>
    /// xml表单
    /// </summary>
    [Serializable]
    public class XmlInfo 
    {
     
        public int Id { get; set; }
   
        public bool IsDeleted { get; set; }
      
        public DateTime Time { get; set; }

        /// <summary>
        /// 产线ID
        /// </summary>
        public int ProductionLineId { get; set; }
        /// <summary>
        /// 嵌入式设备ID
        /// </summary>
        public string EmbeddedDeviceId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string url { get; set; }
    }
}
