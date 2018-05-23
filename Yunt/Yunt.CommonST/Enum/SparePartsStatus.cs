using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Common
{
    /// <summary>
    /// 备件状态
    /// </summary>
    public enum SparePartsStatus
    {
        /// <summary>
        /// 在库
        /// </summary>
        On=0,
        /// <summary>
        /// 出库
        /// </summary>
        Out,
        /// <summary>
        /// 在使用
        /// </summary>
        Using,
        /// <summary>
        /// 报废
        /// </summary>
        Useless
    }
}
