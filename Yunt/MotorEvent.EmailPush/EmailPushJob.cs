using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using MotorEvent.EmailPush.Models;
using NewLife.Log;
using Quartz;
using XCode.DataAccessLayer;
using Yunt.Common;
using Yunt.Common.Email;
using Yunt.Dtsc.Core;

namespace MotorEvent.EmailPush
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class EmailPushJob : JobBase
    {
        public override string Cron => "0 50 8 * * ? *";//0 50 8 * * ? *
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

        private static string[] MailList;
        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            Start();
        }

        private static void Start()
        {
            XTrace.Log.Info("mail push start ...");

            #region mail push       
            var now = DateTime.Now;
            //if (!(now.Hour == 8 && now.Minute == 55))
            //    return;
            var end = now.TimeSpan();
            var start = now.AddDays(-1).TimeSpan();
            var where = "time < =" + end + " and time >" + start;
            var logs = Motoreventlog.FindAll(where, null, null, 0, 0);//.Take(10);//where, null, null, 0, 0

            var sb = new StringBuilder();
            if (logs?.Any() ?? false)
                foreach (var log in logs)
                {
                    var lineName = ProductionLine.Find("Id", log.Productionline_ID).Name;
                    sb.AppendLine($"【产线】{log.Productionline_ID}_{lineName}【电机】{log.Motor_ID}_" +
                                      $"{log.Motorname}【事件内容】{log.Description}<br/>");
                }
            var content = sb.ToString();
            if (string.IsNullOrEmpty(content))
            //return;
            {
                content = "无事件";
            }
            // content += "\r\n详情请查看错误信息表!";

            var emailhelper = new EmailHelper
            {
                mailFrom = "zhaoh@unitoon.cn",
                mailPwd = "Zh199112",
                mailSubject = "云统设备监测平台之设备事件日报" + DateTime.Now.ToString("yyyyMMddHHmmss") + "【系统邮件】",
                mailBody = content,
                isbodyHtml = true,    //是否是HTML
                host = "smtp.exmail.qq.com",//如果是QQ邮箱则：smtp:qq.com,依次类推
                mailToArray = new string[] { "wubo@unitoon.cn", "xuzh@unitoon.cn", "yujf@unitoon.cn","yujf@sari.ac.cn","dongb@unitoon.cn",
                "huangdb@unitoon.cn", "lic@unitoon.cn","liumm@unitoon.cn"
                //"zhaoh@unitoon.cn"
                },//接收者邮件集合
                mailCcArray = new string[] { "zhaoh@unitoon.cn" }//抄送者邮件集合
            };
            try
            {
                emailhelper.Send();
            }
            catch (Exception exp)
            {
                XTrace.Log.Error("发送错误邮件错误", exp);
            }

            #endregion

            XTrace.Log.Info("mail push end ...");
        }
    }
}
