using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Xml.Domain.MiddleMap
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public string Id { get; set; }

      
        /// <summary>
        /// 数据精度
        /// </summary>

        public string Precision { get; set; }

        /// <summary>
        /// 描述（电机功能参数）中文
        /// </summary>
        public string DescCn { get; set; }
        /// <summary>
        /// 描述（电机功能参数）英文
        /// </summary>
        public string DescEn { get; set; }

        /// <summary>
        /// 电机ID嵌入式所使用(!=MotorId)
        /// </summary>
        public string MachineId { get; set; }
        /// <summary>
        /// 数据值(董工使用，与我部无关数据)
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
        /// <summary>
        /// 起始位
        /// </summary>

        public string DatBitStart { get; set; }
        /// <summary>
        /// 结束位
        /// </summary>
        public string DatBitEnd { get; set; }
        /// <summary>
        /// 采集/控制设备ID
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 电机设备ID
        /// </summary>
        public int MotorId { get; set; }

        /// <summary>
        ///数据类型ID
        /// </summary>
        public int DataTypeId { get; set; }
        /// <summary>
        ///数据类型名称
        /// </summary>
        public string DataTypeName { get; set; }
        /// <summary>
        ///数据格式ID
        /// </summary>
        public int FormatId { get; set; }

        /// <summary>
        ///数据格式名称
        /// </summary>
        public string FormatName { get; set; }
        /// <summary>
        ///数据物理特性ID
        /// </summary>
        public int PhysicId { get; set; }
        /// <summary>
        ///数据物理特性名称
        /// </summary>
        public string PhysicName { get; set; }

        /// <summary>
        ///单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        ///数据范围
        /// </summary>
        public string Range { get; set; }
    }
}
