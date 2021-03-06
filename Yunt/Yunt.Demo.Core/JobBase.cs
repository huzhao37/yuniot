﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Yunt.Common;

namespace Yunt.Demo.Core
{
    [DisallowConcurrentExecution()]
    public abstract class JobBase : MarshalByRefObject, ISchedulingJob
    {
        #region Properties

        /// <summary>
        /// 取消资源
        /// </summary>
        public CancellationTokenSource CancellationSource => new CancellationTokenSource();

        /// <summary>
        /// 执行计划，除了立即执行的JOB之后，其它JOB需要实现它
        /// </summary>
        public virtual string Cron => "* * * * * ?";

        /// <summary>
        /// 是否为单次任务，默认为false
        /// </summary>
        public virtual bool IsSingle => false;

        /// <summary>
        /// Job的名称，默认为当前类名
        /// </summary>
        public virtual string JobName => GetType().Name;

        /// <summary>
        /// Job执行的超时时间(毫秒)，默认5分钟
        /// </summary>
        public virtual int JobTimeout => 5 * 60 * 1000;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Job具体类去实现自己的逻辑
        /// </summary>
        protected abstract void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource);

        /// <summary>
        /// 当某个job超时时，它将被触发，可以发一些通知邮件等
        /// </summary>
        /// <param name="arg"></param>
        private void CancelOperation(object arg)
        {
            CancellationSource.Cancel();
            StdSchedulerFactory.GetDefaultScheduler().Result.Interrupt(new JobKey(JobName));
            Logger.Info(JobName + " excute time out，has canceled，wait for next operate...");
        }

        #endregion Methods

        #region IJob 成员

        public Task Execute(IJobExecutionContext context)
        {
            Timer timer = null;
            try
            {
                timer = new Timer(CancelOperation, null, JobTimeout, Timeout.Infinite);
                Logger.Info("{0} Start Excute", context.JobDetail.Key.Name);
                if (context.JobDetail.JobDataMap != null)
                {
                    foreach (var pa in context.JobDetail.JobDataMap)
                        Logger.Info($"JobDataMap，key：{pa.Key}，value：{pa.Value}");
                }
                ExcuteJob(context, CancellationSource);
            }
            catch (Exception ex)
            {
                Logger.Error(this.GetType().Name + "error:" + ex.Message);
            }
            finally
            {
                timer?.Dispose();
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
