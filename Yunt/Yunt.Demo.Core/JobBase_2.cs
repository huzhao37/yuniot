using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Yunt.Demo.Core
{
    [DisallowConcurrentExecution()]
    public abstract class JobBase_2 : IJob//:IJob
    {

        #region IJob 成员

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now + "{0}这个Job开始执行", context.JobDetail.Key.Name);
                ExcuteJob(context);
                return Task.CompletedTask; ;
            }
            catch (Exception ex)
            {
               Console.WriteLine(this.GetType().Name + "error:" + ex.Message);
                throw;
            }
        }

        #endregion
        ///// <summary>
        ///// 执行计划，子类可以重写
        ///// </summary>
        //public virtual string Cron => "0/5 * * * * ?";

        /// <summary>
        /// 执行计划，除了立即执行的JOB之后，其它JOB需要实现它
        /// </summary>
        public abstract string Cron { get; }

        /// <summary>
        /// 是否为单次任务，默认为false
        /// </summary>
        public virtual bool IsSingle => false;

        /// <summary>
        /// Job的名称，默认为当前类名
        /// </summary>
        public virtual string JobName => GetType().Name;
        /// <summary>
        /// Job具体类去实现自己的逻辑
        /// </summary>
        protected abstract void ExcuteJob(IJobExecutionContext context);

        /// <summary>
        /// 当某个job超时时，它将被触发，可以发一些通知邮件等
        /// </summary>
        /// <param name="arg"></param>
        private void CancelOperation(object arg)
        {
            CancellationSource.Cancel();
            StdSchedulerFactory.GetDefaultScheduler().Result.Interrupt(new JobKey(JobName));
            Console.WriteLine(JobName + "Job执行超时，已经取消，等待下次调度...");
        }
        /// <summary>
        /// 取消资源
        /// </summary>
        public CancellationTokenSource CancellationSource => new CancellationTokenSource();
        /// <summary>
        /// Job执行的超时时间(毫秒)，默认5分钟
        /// </summary>
        public virtual int JobTimeout => 5 * 60 * 1000;

   
    }
}
