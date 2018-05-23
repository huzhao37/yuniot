using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Yunt.Common;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;

namespace Yunt.Dtsc.Core
{
    [DisallowConcurrentExecution()]
    public abstract class JobBase : ISchedulingJob
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
        /// Job的名称，默认为当前DLL名
        /// </summary>
        public virtual string JobName => System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;

        /// <summary>
        /// Job执行的超时时间(毫秒)，默认5分钟
        /// </summary>
        public virtual int JobTimeout => 5 * 60 * 1000;

        /// <summary>
        /// 发布的版本号
        /// </summary>
        public virtual int Version => 0;

        public int JobId;
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

            new TbError()
            {
                Createtime = DateTime.Now.TimeSpan(),
                JobID = TbJob.Find("Name", JobName)?.ID ?? 0,
                Msg = ($"Warn:excute time out，has canceled，wait for next operate...")
            }.SaveAsync();
            Logger.Warn(JobName + " excute time out，has canceled，wait for next operate...");
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
                var jobId = context.JobDetail.JobDataMap?["jobid"];
                if (jobId != null)
                    JobId = Convert.ToInt32(jobId);
                var job = TbJob.Find("ID", JobId);

                ExcuteJob(context, CancellationSource);

                if (job != null)
                {
                    job.Runcount++;
                    job.Lastedstart= Convert.ToInt64(context.FireTimeUtc.DateTime.ToLocalTime().TimeSpan());
                    job.Lastedend = DateTime.Now.TimeSpan();
                    job.Nextstart = Convert.ToInt64(context.NextFireTimeUtc?.DateTime.ToLocalTime().TimeSpan());
                    job.SaveAsync();
                }
             
            }
            catch (Exception ex)
            {
              new TbError()
              {
                  Createtime = DateTime.Now.TimeSpan(),
                  JobID = TbJob.Find("Name",JobName)?.ID??0,
                  Msg = ($"error:{JobName}-{ex.Message}")
              }.SaveAsync();
                
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
