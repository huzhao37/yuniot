using System.Threading;
using Quartz;

namespace Yunt.Dtsc.Core
{
    public interface ISchedulingJob:IJob
    {
        /// <summary>
        /// 取消资源
        /// </summary>
        CancellationTokenSource CancellationSource { get; }

        /// <summary>
        /// 执行计划，除了立即执行的JOB之后，其它JOB需要实现它
        /// </summary>
        string Cron { get; }

        /// <summary>
        /// 是否为单次任务，黑为false
        /// </summary>
        bool IsSingle { get;  }
        /// <summary>
        /// Job的名称，默认为当前类名
        /// </summary>
        string JobName { get;  }
        /// <summary>
        /// 发布版本号
        /// </summary>
        int Version { get; }
    }
}