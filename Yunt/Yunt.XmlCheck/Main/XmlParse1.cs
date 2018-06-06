using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Log;
using XCode;
using Yunt.Common;
using Yunt.Common.Xml;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.XmlProtocol.Domain.MiddleMap;
using Yunt.XmlProtocol.Domain.Models;
using DataType = System.ComponentModel.DataAnnotations.DataType;
using Yunt.XmlProtocol.Domain.XmlMap;
using Yunt.XmlProtocol.Domain.Service;

namespace Yunt.XmlCheck.Main
{
    public class XmlParse1
    {
        public static string _collectDeviceIndex;
        //public static int _EmbeddedDataFormIndex;
        public static string _LineId = "WDD-P001";
        private static XmlProtocolHelper xmlHelper = new XmlProtocolHelper();

        private static IMotorRepository motorRepository;
        #region Xml主文件相关

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="xmlPath"></param>
        public static XmlInfo GetXmlInfo(string xmlPath)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + xmlPath;
                if (!File.Exists(path)) return new XmlInfo();
                var fs = File.Open(path, FileMode.Open, FileAccess.Read);
                var sr = new StreamReader(fs);
                var text = sr.ReadToEnd();
                return XmlHelper.XmlSerializeHelper.DeSerialize<XmlInfo>(text, Encoding.UTF8);
            }
            catch (Exception e)
            {
                XTrace.Log.Error(e.Message);
                // throw;
                return new XmlInfo();
            }


        }

        /// <summary>
        /// xml信息持久化
        /// </summary>
        public static void SaveXmlInfo(XmlInfo xmlInfo)
        {
            motorRepository= ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
            try
            {
                //嵌入式设备Index
                _collectDeviceIndex = xmlInfo.CollectDeviceIndex;

                var collectDevice = new Collectdevice()
                {
                    Index = _collectDeviceIndex,
                    ProductionlineID = _LineId,
                    Time = DateTime.Now
                };
                //collectDevice.Save();

                var inDataIndexList = new List<IndexModel>();
                var machineModelList = new List<MachineModel>();
                var dataModelList = new List<DataModel>();

                var tables = xmlInfo.Tables;
                if (tables.Any())
                    foreach (var table in tables)
                    {

                        //1.server-队列服务器
                        var server = table.Server;
                        if (server != null)
                        {
                            var queues = new List<Messagequeue>();
                            var outQueue = server.OutQueue;
                            var bindKeys = outQueue.Binds;
                            if (bindKeys.Any())
                            {
                                bindKeys.ForEach(q =>
                                {
                                    queues.Add(new Messagequeue()
                                    {
                                        Name = outQueue.Name,
                                        CollectdeviceIndex = _collectDeviceIndex,
                                        Host = server.Url,
                                        RouteKey = q.RouteKey,
                                        ComType = "",
                                        Port = 5672,
                                        Time = DateTime.Now,
                                        WriteRead = Convert.ToInt32(WriteOrRead.Write),
                                        Username = "guest",
                                        Pwd = "guest"
                                    });
                                });

                            }
                            var outQueues = server.Channel.Outdatas;
                            if (outQueues.Any())
                            {
                                outQueues.ForEach(q =>
                                {
                                    if (q.Timer.IsNullOrWhiteSpace())
                                        q.Timer = "0";
                                    var outQ = queues.FirstOrDefault(e => e.RouteKey.EqualIgnoreCase(q.RouteKey));
                                    outQ.ComType = q.Type;
                                    outQ.Timer = int.Parse(q.Timer);
                                });
                            }
                            var inQueues = server.Channel.Indatas;
                            if (inQueues.Any())
                            {
                                inQueues.ForEach(q =>
                                {
                                    if (q.Timer.IsNullOrWhiteSpace())
                                        q.Timer = "0";
                                    queues.Add(new Messagequeue()
                                    {
                                        Name = "",
                                        CollectdeviceIndex = _collectDeviceIndex,
                                        Host = server.Url,
                                        RouteKey = q.RouteKey,
                                        Timer = int.Parse(q.Timer),
                                        ComType = q.Type,
                                        WriteRead = Convert.ToInt32(WriteOrRead.Read),
                                        Port = 5672,
                                        Time = DateTime.Now,
                                        Username = "guest",
                                        Pwd = "guest"
                                    });
                                });
                            }
                            //queues.Insert();
                            //1.2.2.1---indata
                            if (server.Channel.Indatas.Any())
                            {                             
                                foreach (var inData in server.Channel.Indatas)
                                {
                                    if (inData.RouteKey.EqualIgnoreCase("STATUS"))
                                        continue;
                                    if (inData.Indexs.Any())
                                    {
                                        foreach (var index in inData.Indexs)
                                        {
                                            inDataIndexList.Add(new IndexModel()
                                            {
                                                DataId = index.Id,
                                                DataIndex = Convert.ToInt32(index.Index)
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        //2.device-采集/控制设备


                        //3.machine-电机
                        var machineList = table.Machines;
                        if (machineList.Any())
                        {
                            foreach (var machine in machineList)
                            {
                                machineModelList.Add(new MachineModel()
                                {
                                    MachineId = machine.Id,
                                    MachineName = machine.Name,
                                    MachineTypeId = machine.MachineType.MachineTypeId
                                });
                            }
                        }
                        //初始化Motor
                        if (machineModelList.Any())
                        {
                            var motors = new List<Motor>();
                            machineModelList.ForEach(machine =>
                            {
                                if(string.IsNullOrWhiteSpace(machine.MachineTypeId))
                                    return;
                                motors.Add(new Motor()
                                {
                                    EmbeddedDeviceId =1,//collectDevice.ID,
                                    ProductionLineId = collectDevice.ProductionlineID,
                                    FeedSize = 0,
                                    FinalSize = 0,
                                    MotorId = "0",
                                    MotorTypeId = machine.MachineTypeId,
                                    MotorPower = 0,
                                    Name = machine.MachineName,
                                    ProductSpecification = "",
                                    SerialNumber = "",
                                    StandValue = 0,
                                    Time = DateTime.Now.TimeSpan()
                                });
                                
                            });
                            if (motorRepository.GetEntities().ToList().Count == 0)
                            {
                                if (motors.Any())
                                {
                                    //Common.Logger.Info("正在初始化电机设备，请耐心等候...");
                                    //var re = motorRepository.Insert(motors);
                                    //Common.Logger.Info($"已完成初始化电机设备:{re}个");

                                }
                            }
                   
                              
                        }
                        //4.data-数据      
                        var dataList = table.Datas;
                        if (dataList.Any())
                        {
                            foreach (var data in dataList)
                            {
                                dataModelList.Add(new DataModel()
                                {
                                    Id = data.Id,
                                    MachineId = data.MachineId,
                                    Value = data.Value,
                                    Precision = data.Precision,
                                    DescCn = data.Desc.DescCn,
                                    DescEn = data.Desc.DescEn,
                                    DatBitStart = data.DatBitStart,
                                    DatBitEnd = data.DatBitEnd,
                                    DeviceId = data.DeviceId,
                                    Remark = data.Remark,
                                    FormatId = Convert.ToInt32(data.Format.FormatId),
                                    FormatName = data.Format.FormatName,
                                    PhysicName = data.Physic.PhysicName,
                                    PhysicId = Convert.ToInt32(data.Physic.PhysicId),
                                    DataTypeName = data.DataType,
                                    Unit = data.Unit,
                                    Range = data.Range,
                                });
                            }
                        }


                    }

                //保存数据至本地数据库
                var dataFormList = new List<Dataformmodel>();
                if (dataModelList.Any())
                {
                    foreach (var dataModel in dataModelList)
                    {
                        //生成数据表单记录
                        var index = inDataIndexList.SingleOrDefault(e => e.DataId == dataModel.Id); //数据index
                        var machine = machineModelList.SingleOrDefault(e => e.MachineId == dataModel.MachineId); //电机
                        //这种组成方式。。。（需要规范）
                        var bitDescStr = dataModel.FormatName.Split("bit");
                        var bit = "0";
                        var desc = "";

                        bit = bitDescStr[0];
                        if (bitDescStr.Length == 2)
                            desc = bitDescStr[1];
                        if (bit.Equals("LOGID"))
                        {
                            bit = "32";
                            desc = "整型模拟量";
                        }
                        if (bit.Equals("时间"))
                        {
                            bit = "32";
                            desc = "时间";
                        }
                        if (index == null)
                        {
                            continue;
                        }

                        var name = machine?.MachineName ?? "";
                        var typeName = machine?.MachineTypeId ?? "";
                        var motorId = "0";
                        if (!name.IsNullOrWhiteSpace()&& !typeName.IsNullOrWhiteSpace())
                        {
                            motorId = motorRepository.GetEntities(
                          e =>
                              e.Name.EqualIgnoreCase(name) &&
                              e.ProductionLineId.EqualIgnoreCase(_LineId) &&
                              e.MotorTypeId.EqualIgnoreCase(typeName))?.ToList()?.FirstOrDefault()?.MotorId;
                        }
                        if (!name.IsNullOrWhiteSpace() && typeName.IsNullOrWhiteSpace())
                        {
                            motorId = motorRepository.GetEntities(
                          e =>
                              e.Name.EqualIgnoreCase(name) &&
                              e.ProductionLineId.EqualIgnoreCase(_LineId))?.ToList().FirstOrDefault()?.MotorId;
                        }
                        if (name.IsNullOrWhiteSpace() && !typeName.IsNullOrWhiteSpace())
                        {
                            motorId = motorRepository.GetEntities(
                          e =>
                              e.ProductionLineId.EqualIgnoreCase(_LineId) &&
                              e.MotorTypeId.EqualIgnoreCase(typeName))?.ToList().FirstOrDefault().MotorId;
                        }

                        var dataForm = new Dataformmodel()
                        {

                            MachineId = dataModel?.MachineId ?? "",
                            DeviceId = dataModel?.DeviceId ?? "",
                            FieldParam = dataModel?.DescCn ?? "",
                            FieldParamEn = dataModel?.DescEn ?? "",
                            DataPhysicalAccuracy = dataModel?.Precision ?? "",
                            DataPhysicalFeature = dataModel?.PhysicName ?? "",
                            DataType = dataModel?.FormatName ?? "",
                            Index = index?.DataIndex ?? 0,
                            MotorTypeName = machine?.MachineTypeId ?? "",
                            MachineName = machine?.MachineName ?? "",
                            Bit = Convert.ToInt32(bit),
                            BitDesc = desc,
                            LineId = _LineId,
                            CollectdeviceIndex = _collectDeviceIndex,
                            DataPhysicalId = dataModel?.PhysicId ?? 0,
                            FormatId = dataModel?.FormatId ?? 0,
                            Unit = dataModel?.Unit ?? "",
                            MotorId = motorId,
                            Time = DateTime.Now
                        };
                       // Dataformmodel dataForm = null;
                        //if (index != null)
                        //{
                        //    dataForm = Dataformmodel.Find(new string[] { "Index" },
                        //   new object[] { index?.DataIndex ?? 0 });
                        //    dataForm.MotorId = motorId;
                        //}
                       
                        //dataForm.Insert();
                        if(dataForm!=null)
                            dataFormList.Add(dataForm);
                    }
                }

                var isValid = true;
                var paramList = Motorparams.FindAll();
                if (paramList.Any())
                {
                    foreach (var param in dataFormList)
                    {
                        if (!param.BitDesc.EqualIgnoreCase("整型模拟量") ||
                            (string.IsNullOrWhiteSpace(param.FieldParam) && string.IsNullOrWhiteSpace(param.FieldParamEn)) ||
                            param.FieldParam.Contains("皮带产量") || param.FieldParam.EqualIgnoreCase("起始时间") ||
                            param.FieldParam.EqualIgnoreCase("结束时间") || param.FieldParam.EqualIgnoreCase("记录ID"))
                        {
                            continue;
                        }
                        var isExist =
                            paramList.Where(
                                e => e.Description.Equals(param.FieldParam) && e.MotorTypeId.Equals(param.MotorTypeName)
                                && e.Param.Equals(param.FieldParamEn));

                        if (!isExist.Any())
                        {
                            isValid = false;
                            XTrace.Log.Error($"id:{param.DataPhysicalAccuracy},value:{param.Value}----设备类型ID：" +
                                             $"{param.MotorTypeName}，设备参数：{param.FieldParam},设备参数英文：{param.FieldParamEn}");
                        }

                    }
                }
              
                if (isValid)
                {
                    //生成数据配置项（DataConfig）记录
                    xmlHelper.SaveToDataConfigInfo(dataFormList);

                    //var result = dataFormList.Save();
                    //XTrace.Log.Info($"xml信息初始化完成，共有{result}记录入库！");
                }

            }
            catch (Exception ex)
            {
                XTrace.Log.Error($"[xml信息初始化]：出错{ex.Message}");
            }



        }

        #endregion

    }
}
