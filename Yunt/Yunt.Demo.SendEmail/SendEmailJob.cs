using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Yunt.Demo.Core;

namespace Yunt.Demo.SendEmail
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendEmailJob : JobBase
    {
        public override string Cron => "0/2 * * * * ?";

        protected override void ExcuteJob(IJobExecutionContext context)
        {
            Console.WriteLine("发送Email");
            Console.Write("发送Email:" + DateTime.Now);
        }
    }
}
