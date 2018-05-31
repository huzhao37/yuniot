using System;
using System.IO;
using NewLife.Log;

namespace MailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");
            try
            {
             
                var emailhelper = new EmailHelper
                {
                    mailFrom = "zhaoh@unitoon.cn",
                    mailPwd = "Zh199112",
                    mailSubject = "云统任务调度中心之错误日志" + DateTime.Now.ToString("yyyyMMddHHmmss") + "【系统邮件】",
                    mailBody = "test",
                    isbodyHtml = true,    //是否是HTML
                    host = "smtp.exmail.qq.com",//如果是QQ邮箱则：smtp:qq.com,依次类推
                    mailToArray = new string[] {"zhaoh@unitoon.cn"
                },//接收者邮件集合
                  // mailCcArray = new string[] { "zhaoh@unitoon.cn" }//抄送者邮件集合
                };

                emailhelper.Send();
            }
            catch (Exception exp)
            {
                XTrace.Log.Error(exp.Message, exp.StackTrace+"source:"+exp.Source);
                Console.WriteLine(exp.Message);
            }
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
