using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Xml.Linq;
using NewLife.Log;
using NewLife.Reflection;

namespace Yunt.XmlCheck.Main
{
    public class XmlParse
    {
        public static void GetXmlInfo(string xmlPath)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + xmlPath;
                if (!File.Exists(path)) return;

                var xe = XElement.Load(path);
                //嵌入式设备index
                var embeddedDeviceIndex = xe.Attribute("dev_id").Value;

                var configs = from ele in xe.Elements("table")
                              select ele;
                //1.服务器
                var serverTable = configs.ToList()[0];


                var server = from ele in serverTable.Elements("server")
                             select ele;
                //服务器Id
                var serverId = server.FirstOrDefault().Attribute("id").Value;
                //服务器url
                var serverUrl = (from ele in server.Elements("url")
                    select ele).FirstOrDefault().Value;
                //上行队列（需要部门这边写入的队列信息）
                var outQueue = from ele in server.Elements("outqueue")
                    select ele;
                //上行队列配置
                var outConfig = outQueue.FirstOrDefault().Attribute("config").Value;
                //上行队列名称
                var outName= outQueue.FirstOrDefault().Attribute("name").Value;
                //上行队列Bind
                var bind=from ele in outQueue.Elements("bind")
                    select ele;
                //上行队列bind的交换机
                var outExchange = bind.FirstOrDefault().Attribute("exchange").Value;
                //上行队列bind的队列关键字
                var bindRoute_key =bind.FirstOrDefault().Attribute("route_key").Value;


                //**channel部分**

                //**channel部分--indata部分**

                //**channel部分--outdata部分**


                var channel = from ele in server.FirstOrDefault().Elements("channel") select ele;

                var outdata = from ele in channel.FirstOrDefault().Elements("outdata") select ele;

                var DataConfigId = outdata.FirstOrDefault().Attribute("id").Value.Substring(2);

             




                //2.设备
                var deviceTable = from ele in configs.ToList()[1].Elements("device")
                    select ele;

                //3.电机
                var machineTable = from ele in configs.ToList()[2].Elements("machine")
                    select ele;

                //4.数据
                var dataTable = from ele in configs.ToList()[3].Elements("data")
                    select ele;
                var DeviceControlId1 = dataTable.ToList()[dataTable.Count() - 2].Attribute("id").Value.Substring(2);
                var DeviceControlId2 = dataTable.ToList()[dataTable.Count() - 1].Attribute("id").Value.Substring(2);
            }
            catch (Exception ex)
            {
               XTrace.Log.Error($"[XML]解析出错：{ex.Message}");
            }
        }


    }
}
