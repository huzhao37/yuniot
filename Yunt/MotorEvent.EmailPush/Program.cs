using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorEvent.EmailPush.Models;
using NewLife.Log;
using NewLife.Threading;
using MotorEvent.EmailPush.Email;

namespace MotorEvent.EmailPush
{
    class Program
    {
        private static TimerX _timerX;
        static void Main(string[] args)
        {
            XTrace.Log.Level=LogLevel.Info;
#if DEBUG
            XTrace.UseConsole(true, true);
#endif
            XTrace.Log.Info("任务启动...");
            try
            {
                while (true)
                {
                    if (_timerX == null)
                        _timerX = new TimerX(obj => { SendEmail(); }, null, 1000, 6000 * 1000);
                }
            }
            catch (Exception e)
            {
               XTrace.Log.Fatal(e.Message);
            }
           
        }

        private static void SendEmail()
        {
            var now = DateTime.Now;
            //if (!(now.Hour == 8 && now.Minute == 55))
            //    return;
            var end = TimeSpan(now);
            var start = TimeSpan(now.AddDays(-1));
            var where = "time < =" + end + " and time >" + start;
            var logs = Motoreventlog.FindAll(where, null, null, 0, 0);//.Take(10);//where, null, null, 0, 0

            var sb = new StringBuilder();
            if (logs?.Any()??false)
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
                mailToArray = new string[] { "wubo@unitoon.cn", "xuzh@unitoon.cn", "yujf@unitoon.cn","dongb@unitoon.cn",
                "huangdb@unitoon.cn", "lic@unitoon.cn"
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
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int TimeSpan(DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                return 0;
            }
            else
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (int)(time - startTime).TotalSeconds;
            }
        }
    }
}
