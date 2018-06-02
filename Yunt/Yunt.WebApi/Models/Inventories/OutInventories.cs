using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.Inventories
{
    /// <summary>
    /// 出库对象
    /// </summary>
    public class OutInventories
    {
        [DisplayName("入库编号")]
        public int InHouseId { get; set; }
        //[DisplayName("批次")]
        //public string BatchNo { get; set; }
        /// <summary>
        /// 出库
        /// </summary>
        [DisplayName("出库操作员")]
        public string OutOperator { get; set; }
        ///// <summary>
        ///// 备件类型编号
        ///// </summary>
        //[DisplayName("备件类型编号")]
        //public string SparePartsTypeId { get; set; }
        /// <summary>
        /// 电机设备编号
        /// </summary>
        [DisplayName("电机设备编号")]
        public string MotorId { get; set; } = "Invalid";

        /// <summary>
        /// 出库时间
        /// </summary>
        [DisplayName("出库时间")]
        public long OutTime { get; set; }
    }
}
