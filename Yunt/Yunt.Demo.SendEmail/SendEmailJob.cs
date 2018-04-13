using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Quartz;
using Yunt.Common;
using Yunt.Dtsc.Core;

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
        /// Job的名称，默认为当前类名
        /// </summary>
        public override string JobName => GetType().Name;

        /// <summary>
        /// 发布的版本号
        /// </summary>
        public override int Version => 1;

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
           Start();
        }

        private static void Start()
        {
            Logger.Info("v1:发送Email...");
        }
    }
}
