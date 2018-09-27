using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using NewLife.Serialization;

namespace Yunt.Common.Enviroment
{
    public class HostHelper

    {
        /// <summary>
        /// 获取外网ip地址
        /// </summary>
        public static string GetExtenalIpAddress()
        {
            String url = "http://hijoyusers.joymeng.com:8100/test/getNameByOtherIp";
            string IP = "未获取到外网ip";
            try
            {
                //从网址中获取本机ip数据  
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.Default;
                string str = client.DownloadString(url);
                client.Dispose();

                if (!str.Equals("")) IP = str;
                else IP = "";//GetExtenalIpAddress_0();
            }
            catch (Exception) { }

            return IP;
        }
        /// <summary>  
        /// 获取外网ip地址  
        /// </summary>  
        public static string GetExternalIp()
        {
            try
            {
                var client = new WebClient {Encoding = Encoding.Default};
                //string response = client.DownloadString("http://1212.ip138.com/ic.asp");//失效了
                //string response = client.DownloadString("http://icanhazip.com/");//可用，可能不稳定
                var response = client.DownloadString("http://ip.chinaz.com/");//站长之家
                const string myReg = @"<dd class=""fz24"">([\s\S]+?)<\/dd>";
                var mc = Regex.Match(response, myReg, RegexOptions.Singleline);
                if (mc.Success && mc.Groups.Count > 1)
                {
                    response = mc.Groups[1].Value;
                    return response;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
        /// <summary>
        /// 获取本地IP地址
        /// </summary>

        public static string GetLocalIp()
        {
            //获取本地的IP地址
            var addressIp = string.Empty;
            try
            {

                foreach (var ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        addressIp = ipAddress.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"[获取内网IP]：{ex.Message}");
            }
            return addressIp;
        }


        /// <summary>
        /// 通过IP得到IP所在地省市（Porschev）
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetAdrByIp(string ip)
        {
            var url = "http://www.cz88.net/ip/?ip=" + ip;
            const string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";
            //得到网页源码
            var html = GetHtml(url);
            var reg = new Regex(regStr, RegexOptions.None);
            var ma = reg.Match(html);
            html = ma.Value;
            var arr = html.Split(' ');
            return arr[0];
        }
        /// <summary>
        /// 获取HTML源码信息(Porschev)
        /// </summary>
        /// <param name="url">获取地址</param>
        /// <returns>HTML源码</returns>
        public static string GetHtml(string url)
        {
            var str = "";
            try
            {
                var uri = new Uri(url);
                var wr = WebRequest.Create(uri);
                var s = wr.GetResponse().GetResponseStream();
                var sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
               Logger.Exception(ex, $"[获取HTML源码信息]：{ex.Message}");
            }
            return str;
        }
        /// <summary>
        /// 得到真实IP以及所在地详细信息（Porschev）
        /// </summary>
        /// <returns></returns>
        public static string GetIpDetails(string ip)
        {
            try
            {
                var myWebClient = new WebClient {Credentials = CredentialCache.DefaultCredentials};
                //获取或设置用于向Internet资源的请求进行身份验证的网络凭据  
                var pageData = myWebClient.DownloadData("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip); //从指定网站下载数据  
                var pageHtml = Encoding.Default.GetString(pageData);
                var dic =pageHtml.ToJsonEntity<Dictionary<string, object>>();
                if (dic["code"].ToString() == "0")
                {
                    var dicData = (Dictionary<string, object>)dic["data"];
                    return dicData["country"] + dicData["city"].ToString();
                }
            }
            catch (WebException webEx)
            {
                Logger.Exception(webEx, $"[用户统计]-获取地理信息:{webEx.Message}！");
            }
            return null;

        }

    }
}
