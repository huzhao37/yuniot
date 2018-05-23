using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Dtsc.Domain.Model
{
    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 暂停
        /// </summary>
        Pause=-2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete=-1,
        /// <summary>
        /// 停止
        /// </summary>
        Stop=0,
        /// <summary>
        /// 启动
        /// </summary>
        Start=1,
        /// <summary>
        /// 重启
        /// </summary>
        ReStart=2,
       
    }
}
