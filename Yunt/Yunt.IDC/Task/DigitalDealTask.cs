using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Yunt.Bussiness;
using Yunt.Core;
using Yunt.FormBussiness;
using Yunt.FormBussiness.Dal;
using Yunt.FormModel;
using Yunt.IDC.Helper;
using Yunt.Model;
using Yunt.Mongo;
using Yunt.TaskManager.OpenOperator;
using Yunt.AppSetting;
using Yunt.FormModel.RedisModel;
using Yunt.MQ;
using Yunt.RedisV2;
using Yunt.RedisV2.Config;
using Yunt.TaskDriver;

namespace Yunt.IDC.Task
{
    /// <summary>
    /// 表单映射任务
    /// </summary>
    public class DigitalDealTask
    {
        private static readonly RedisClient RedisClientV2;
        static DigitalDealTask()
        {
            if (RedisClientV2 == null)
            {
                //var host = new HostItem() {Connections = 60, Host = TbConfig.Find("")};
                RedisClientV2 = RedisClient.DefaultDB;
                RedisClientV2.DB = 4;
            }
        }
        private static TaskOpenOperator opera;
        /// <summary>
        /// 千万级数据(对每一条需要查看DI等信息的原始字节进行处理加工)
        /// </summary>
        public static void SaveToLocalDb(TaskOpenOperator openOperator)
        {
            opera = openOperator;
            var interval = TaskSetting.Current.DigitalDealInterval;
#if DEBUG
            var s1 = new Stopwatch();
            s1.Start();
#endif
            var queueHost = AppSettings.GetConfigValue("QueueHost");
            var queuePort = Convert.ToInt32(AppSettings.GetConfigValue("QueuePort"));
            var queueUserName = AppSettings.GetConfigValue("QueueUser");
            var queuePassword = AppSettings.GetConfigValue("QueuePwd");

            var ccuri = "amqp://" + queueHost + ":" + queuePort;
            var queue = AppSettings.GetConfigValue("QueueBytesName"); //OriginBytes
            var errorQueue = queue + "Error"; //OriginBytesError
#if DEBUG
            s1.Stop();
            Logger.Info($"耗时{s1.ElapsedMilliseconds}ms");
#endif
            var rabbitHelper = new RabbitMqHelper();
            RabbitMqHelper.Cancelled = !TaskSetting.Current.DigitalDealEnable;
            rabbitHelper.Read(ccuri, queue, queueHost, queuePort, queueUserName, queuePassword, interval, errorQueue, Start, MQ.DataType.Integrate);
        }

        private static bool Start(byte[] buffer, MQ.DataType type)
        {
            #region 更新dataformModel表中的motorid字段
            //var forms = DataFormModel.FindAll();
            //foreach (var dataFormModel in forms)
            //{
            //    try
            //    {
            //        if (dataFormModel.MotorId > 0)
            //            continue;
            //        var motor = Container.motorContainer
            //                    .GetEntities(e => e.Name.Equals(dataFormModel.MachineName) &&
            //                                      e.ProductionLineId == dataFormModel.LineId)?.SingleOrDefault();
            //        //Debug.Assert(motor==null);
            //        dataFormModel.MotorId = motor?.Id ?? 0;
            //        if (dataFormModel.MotorId == 0)
            //        {
            //            Container.Info("不存在该motor");
            //            dataFormModel.MotorId = 0;
            //            //continue;
            //        }
            //        dataFormModel.SaveAsync();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }

            //}
            #endregion 

            var dis = new List<DIHistory>();
            var dos = new List<DOHistory>();
            var des = new List<DebugHistory>();
            var ws = new List<WarnHistory>();
            var ns = new List<NormalHistory>();
       

            var analyze = new ByteParse(buffer);
            var model = analyze.Parser();
            try
            {
                var configId = model.DataConfigId;
                var lineId = Container.emDeviceContainer.GetDevicesByIndex(model.DeviceId).ProductionLineId;
                var time = model.PValues.ToArray()[0].Key;

                var values = model.PValues.ToArray()[0].Value;
                var list = DataFormModel.FindByProLineId(lineId, configId).ToList().OrderBy(d => d.Index).ToList();
                //按照index排序
                var maxIndex = 0;
                if (list.Any()) maxIndex = list.Max(f => f.Index);
                if (maxIndex > values.Count || !list.Any() || !(values.Count > 0))
                {
                    Logger.Warn("[DigitalDealTask]表单结构与队列数据不一致，请检查表单或队列！");

                    return true; //表单结构与队列数据不一致，检查表单或队列
                }

                for (var i = 0; i < list.Count(); i++)
                {
                    var form = list[i];
                    //var motor = Container.motorContainer
                    //        .GetEntities(e => e.Name.Equals(form.MachineName) &&
                    //                          e.ProductionLineId == form.LineId)?.SingleOrDefault();
                    //if (motor == null) continue;
                    if(form.MotorId==0) continue;
                    var motor = new Motor() {Id = form.MotorId, ProductionLineId = form.LineId, Name = form.MachineName};
                    form.Time = time;
                    form.MotorId = motor.Id;
                    if (form.DataType.Contains("模拟量") || form.DataPhysicalFeature.Contains("AI"))
                    {                                           
                        form.Value = Normalize.ConvertToNormal(form.DataPhysicalAccuracy, form.FieldParam, values[form.Index], motor, list, values);
                        form.SaveAsync();//更新实时数据
                        //添加AI分析记录
                        var ai = new AiLog()
                        {
                            MotorId = motor.Id,
                            MotorName = motor.Name,
                            ProductionLineId = motor.ProductionLineId,
                            Param = form.FieldParam,
                            Value = form.Value,
                            Time = time
                        };
                        var isSuccess = RedisClientV2.Sadd(ai.MotorId.ToString(), ai, RedisV2.DataType.Protobuf)>0?"成功":"失败";
                        Container.Info($"[AILOG]-存入缓存{isSuccess}");

                        var data = new NormalHistory()
                        {
                            DataFormModelId = form.ID,
                            Value = values[form.Index],
                            FieldParam = form.FieldParam,
                            Time = time
                        };
                        // new MongoRepository<NormalHistory>().Insert(data);
                        ns.Add(data);

                    }
                    if (form.DataPhysicalFeature.Contains("DI") || form.DataPhysicalFeature.Contains("输入"))
                    {
                        form.DIValue = values[form.Index];
                       // form.Time = time;
                        form.SaveAsync();//更新实时数据

                        var data = new DIHistory()
                        {
                            DataFormModelId = form.ID,
                            Value = values[form.Index],
                            FieldParam = form.FieldParam,
                            Time = time
                        };
                        //new MongoRepository<DIHistory>().Insert(data);
                        dis.Add(data);
                    }
                    if (form.DataPhysicalFeature.Contains("DO") || form.DataPhysicalFeature.Contains("输出"))
                    {

                        form.DOValue = values[form.Index];
                       // form.Time = time;
                        form.SaveAsync();//更新实时数据

                        var data = new DOHistory()
                        {
                            DataFormModelId = form.ID,
                            Value = values[form.Index],
                            FieldParam = form.FieldParam,
                            Time = time
                        };
                        //new MongoRepository<DOHistory>().Insert(data);
                        dos.Add(data);
                    }
                    if (form.DataPhysicalFeature.Contains("状态"))
                    {
                        form.WarnValue = values[form.Index];
                      //  form.Time = time;
                        form.SaveAsync();//更新实时数据

                        var data = new WarnHistory()
                        {
                            DataFormModelId = form.ID,
                            Value = values[form.Index],
                            FieldParam = form.FieldParam,
                            Time = time
                        };
                        //  new MongoRepository<WarnHistory>().Insert(data);
                        ws.Add(data);
                    }
                    if (form.DataPhysicalFeature.Contains("调试"))
                    {

                        form.DebugValue = values[form.Index];
                       // form.Time = time;
                        form.SaveAsync();//更新实时数据

                        var data = new DebugHistory()
                        {
                            DataFormModelId = form.ID,
                            Value = values[form.Index],
                            FieldParam = form.FieldParam,
                            Time = time
                        };
                        //new MongoRepository<DebugHistory>().Insert(data);
                        des.Add(data);

                    }
                }

                if (dis.Any())
                    new FormService<DIHistory>().InsertAsync(dis);
                if (dos.Any())
                    new FormService<DOHistory>().InsertAsync(dos);
                if (ws.Any())
                    new FormService<WarnHistory>().InsertAsync(ws);
                if (des.Any())
                    new FormService<DebugHistory>().InsertAsync(des);
                if (ns.Any())
                    new FormService<NormalHistory>().InsertAsync(ns);
            }
            catch (Exception ex)
            {
                opera.Error("[DigitalDealTask]插入本地数据库出错:", ex);
                return false;
            }        
            //var result = redis.Srem("OriginalBytes", byteList, DataType.Protobuf);
            //Logger.Info($"[DigitalDealTask]-OriginalBytes delete to redis success:{result}!");
            return true;
        }
    }
}
