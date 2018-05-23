using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Quartz;
using Yunt.Common;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;

namespace Yunt.Demo.SendEmail
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendEmailJob : JobBase
    {
        public override string Cron => "0/2 * * * * ?";
        /// <summary>
        /// 是否为单次任务，默认为false
        /// </summary>
        public override bool IsSingle => false;

        /// <summary>
        /// Job的名称，默认为当前dll名
        /// </summary>
        public override string JobName => System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;

        /// <summary>
        /// 发布的版本号
        /// </summary>
        public override int Version => 1;
   
        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            //var jobId = context.JobDetail.JobDataMap?["jobid"];
            //if (jobId != null)
            //    JobId = Convert.ToInt32(jobId);
            //var job=TbJob.Find("JobID", JobId);
                 
            Start();
            
            //if (job == null) return;
            //job.Lastedend = DateTime.Now.TimeSpan();                   
            //job.SaveAsync();
        }

        private static void Start()
        {
            Logger.Info("v1:发送Email...");
        }
    }
}
