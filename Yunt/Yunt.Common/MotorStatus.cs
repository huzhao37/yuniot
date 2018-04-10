using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Common
{
    /// <summary>
    /// 电机设备状态
    /// </summary>
    public enum MotorStatus
    {
        /// <summary>
        /// 失联
        /// </summary>
        Lose=-1,
        /// <summary>
        /// 停止
        /// </summary>
        Stop=0,
        /// <summary>
        /// 运行
        /// </summary>
        Run
    }
}
