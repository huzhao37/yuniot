using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Yunt.Demo.Core;

namespace Yunt.Demo.SendEmail
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendEmailJob_2 : JobBase_2
    {
        public override string Cron => "0/2 * * * * ?";

   

        protected override void ExcuteJob(IJobExecutionContext context)
        {
            Start();
   
        }
    
        private static void Start()
        {
            Console.WriteLine("发送Email任务启动");
            Console.Write("发送Email:" + DateTime.Now);
        }

        private static void Stop(IJobExecutionContext context)
        {
            Console.WriteLine("发送Email任务停止");

            ((ICancellableJobExecutionContext)context).Cancel();
            Console.WriteLine("发送Email任务已停止");
        }
    }
}
