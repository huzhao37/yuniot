using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Quartz;
using Yunt.Common;
using Yunt.Common.Email;
using Yunt.Dtsc.Core;
using Yunt.Dtsc.Domain.Model;

namespace Dtsc.MailPush
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class EmailPushJob : JobBase
    {
        public override string Cron => "0/59 * * * * ?";//0 30 8 * * ? *
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
            Start();
        }

        private static void Start()
        {
            #region main

            var now = DateTime.Now;
            var end = now.TimeSpan();
            var start = now.AddMinutes(-1).TimeSpan();
            var where = "createtime <=" + end + " and createtime >" + start;
            var logs = TbError.FindAll(where, null, null, 0, 0);

            var sb = new StringBuilder();
            if (logs?.Any() ?? false)
                foreach (var log in logs)
                {
                    var jobName = TbJob.Find("ID", log.JobID).Name;
                    sb.AppendLine($"【任务】{jobName}_{log.JobID}【日志内容】{log.Msg}<br/>");
                }
            var content = sb.ToString();
            if (string.IsNullOrEmpty(content))
                return;
           
             content += "\r\n详情请查看错误信息表!";
            var recver = TbUser.Find("name", "admin")?.Email?? "zhaoh@unitoon.cn";
            var emailhelper = new EmailHelper
            {
                mailFrom = "zhaoh@unitoon.cn",
                mailPwd = "Zh199112",
                mailSubject = "云统任务调度中心之错误日志" + DateTime.Now.ToString("yyyyMMddHHmmss") + "【系统邮件】",
                mailBody = content,
                isbodyHtml = true,    //是否是HTML
                host = "smtp.exmail.qq.com",//如果是QQ邮箱则：smtp:qq.com,依次类推
                mailToArray = new string[] {recver
                },//接收者邮件集合
               // mailCcArray = new string[] { "zhaoh@unitoon.cn" }//抄送者邮件集合
            };
            try
            {
                emailhelper.Send();
            }
            catch (Exception exp)
            {
               Logger.Error("发送错误邮件错误", exp);
            }

            #endregion
            
        }
    }
}
