﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
namespace Yunt.DIDC.Tasks
{
    public class DayStatisticsTask : IJob
    {
        #region fields && ctor

        internal static Messagequeue WddQueue;
        private static readonly IMaterialFeederByDayRepository MfByDayRepository;
        private static readonly IConveyorByDayRepository CyByDayRepository;
        private static readonly IConeCrusherByDayRepository CcByDayRepository;
        private static readonly IJawCrusherByDayRepository JcByDayRepository;
        // private static IReverHammerCrusherByDayRepository rhcByDayRepository;
        // private static IDoubleToothRollCrusherByDayRepository dtrByDayRepository;
        private static readonly IVibrosieveByDayRepository VibByDayRepository;
        private static readonly IPulverizerByDayRepository PulByDayRepository;
        private static readonly IVerticalCrusherByDayRepository VcByDayRepository;
        private static readonly IImpactCrusherByDayRepository IcByDayRepository;
        private static readonly ISimonsConeCrusherByDayRepository SccByDayRepository;
        private static readonly IHVibByDayRepository HvibByDayRepository;

        private static readonly IMessagequeueRepository MessagequeueRepository;

        private static readonly IMotorRepository MotorRepository;
        private static readonly IEventKindRepository EventKindRepository;
        private static readonly IMotorEventLogRepository MotorEventLogRepository;

        static DayStatisticsTask()
        {
            //motorTypeRepository = ServiceProviderServiceExtensions.GetService<IMotorTypeRepository>(Program.Providers["Device"]);
            MfByDayRepository = ServiceProviderServiceExtensions.GetService<IMaterialFeederByDayRepository>(Program.Providers["Device"]);
            CyByDayRepository = ServiceProviderServiceExtensions.GetService<IConveyorByDayRepository>(Program.Providers["Device"]);
            CcByDayRepository = ServiceProviderServiceExtensions.GetService<IConeCrusherByDayRepository>(Program.Providers["Device"]);
            JcByDayRepository = ServiceProviderServiceExtensions.GetService<IJawCrusherByDayRepository>(Program.Providers["Device"]);
            // rhcByDayRepository = ServiceProviderServiceExtensions.GetService<IReverHammerCrusherByDayRepository>(Program.Providers["Device"]);
            //dtrByDayRepository = ServiceProviderServiceExtensions.GetService<IDoubleToothRollCrusherByDayRepository>(Program.Providers["Device"]);

            VibByDayRepository = ServiceProviderServiceExtensions.GetService<IVibrosieveByDayRepository>(Program.Providers["Device"]);
            PulByDayRepository = ServiceProviderServiceExtensions.GetService<IPulverizerByDayRepository>(Program.Providers["Device"]);
            VcByDayRepository = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByDayRepository>(Program.Providers["Device"]);
            IcByDayRepository = ServiceProviderServiceExtensions.GetService<IImpactCrusherByDayRepository>(Program.Providers["Device"]);
            SccByDayRepository = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByDayRepository>(Program.Providers["Device"]);
            HvibByDayRepository = ServiceProviderServiceExtensions.GetService<IHVibByDayRepository>(Program.Providers["Device"]);
            MessagequeueRepository = ServiceProviderServiceExtensions.GetService<IMessagequeueRepository>(Program.Providers["Xml"]);
            MotorRepository = ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
            EventKindRepository = ServiceProviderServiceExtensions.GetService<IEventKindRepository>(Program.Providers["Analysis"]);
            MotorEventLogRepository = ServiceProviderServiceExtensions.GetService<IMotorEventLogRepository>(Program.Providers["Analysis"]);
        }

        #endregion
        

        /// <summary>
        /// 定时任务执行
        /// </summary>
        /// <param name="context"></param>
        public Task Execute(IJobExecutionContext context)
        {
            var w_r = (int)WriteOrRead.Read;
            //var where = " Write_Read = '" + w_r + "' and  Route_Key != 'STATUS'";
            WddQueue =
               MessagequeueRepository.GetEntities(e => e.Write_Read.Equals(w_r) && !e.Route_Key.Equals("STATUS")).FirstOrDefault();
            if (WddQueue != null)
            {
                var queueHost = WddQueue.Host;
                var queuePort = WddQueue.Port;
                var queueUserName = WddQueue.Username;
                var queuePassword = WddQueue.Pwd;

                var ccuri = "amqp://" + queueHost + ":" + queuePort;
                var queue = WddQueue.Route_Key; //"FailedData";
                var route = WddQueue.Route_Key;
                var exchange = "amq.topic";
                var errorQueue = queue + "Error"; //faileddata
                var rabbitHelper = new RabbitMqHelper();
                //等待队列消息消费完后再执行统计
                var messageCount = rabbitHelper.GetMessageCount(ccuri, queue, route, exchange, queueHost, queuePort, queueUserName,
                    queuePassword) + rabbitHelper.GetMessageCount(ccuri, errorQueue, errorQueue, exchange, queueHost, queuePort, queueUserName,
                    queuePassword);
                if (messageCount >= 1)
                {
                    while (true)
                    {
                        Thread.Sleep(5000);
                        messageCount = rabbitHelper.GetMessageCount(ccuri, queue, route, exchange, queueHost, queuePort, queueUserName,
                    queuePassword) + rabbitHelper.GetMessageCount(ccuri, errorQueue, errorQueue, exchange, queueHost, queuePort, queueUserName,
                    queuePassword);
                        if (messageCount < 1) break;
#if DEBUG
                        Common.Logger.Info($"[DayStatisticsTask]:当前队列还有{messageCount}个数据位解析，请等待");
#endif

                    }
                };

           
            }
            var dt = DateTime.Now.AddDays(-1);
            CyByDayRepository.InsertDayStatistics(dt, "CY");
            SccByDayRepository.InsertDayStatistics(dt, "SCC");
            CcByDayRepository.InsertDayStatistics(dt, "CC");
            JcByDayRepository.InsertDayStatistics(dt, "JC");
            MfByDayRepository.InsertDayStatistics(dt, "MF");
            VcByDayRepository.InsertDayStatistics(dt, "VC");
            VibByDayRepository.InsertDayStatistics(dt, "VB");
            PulByDayRepository.InsertDayStatistics(dt, "PUL");
            IcByDayRepository.InsertDayStatistics(dt, "IC");
            HvibByDayRepository.InsertDayStatistics(dt, "HVB");

            HvibByDayRepository.Batch();
            //rhcByDayRepository.Insert(dt);
            //dtrByDayRepository.InsertAsync(t);
            ExcuteAnalysis();

#if DEBUG
            Common.Logger.Info("[DayStatisticsTask]Day Statistics");
            Common.Logger.Info($"耗时{context.JobRunTime.TotalMilliseconds}ms");
#endif
            return Task.CompletedTask;
            //GC.Collect();
        }

        #region event
        private int _count = 0;
        public void ExcuteAnalysis()
        {
            var keys = MotorEventLogRepository.GetRedisAllKeys();
            keys.ForEach(key =>
            {
                var logs = MotorEventLogRepository.GetAiLogs(key);
                //todo
                var time = DateTime.Now.TimeSpan();
                if (logs.Any())
                {
                    var result = Statistics(logs.OrderBy(e => e.Time), key, time);
                    MotorEventLogRepository.Batch();
#if DEBUG
                    if (result)
                        Common.Logger.Info("分析完毕！");
#endif
                }


            });

        }

        private bool Statistics(IEnumerable<AiLog> logs, string MotorId, long start)
        {
            try
            {
                var single = logs.FirstOrDefault();
                var MotorName = single.MotorName;
                var lineId = single.ProductionLineId;

                var kinds = EventKindRepository.GetEntities(e => e.MotorTypeId.EqualIgnoreCase(single.MotorTypeId));
                if (single.MotorTypeId.EqualIgnoreCase("HVB"))
                    kinds = EventKindRepository.GetEntities(e => e.MotorTypeId.EqualIgnoreCase("VB"));
                if (kinds != null && kinds.Any())
                {
                    Parallel.ForEach(kinds, kind =>
                    {
                        switch (single.MotorTypeId)
                        {
                            case "CY":
                                CyEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "JC":
                                JcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "CC":
                                CcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "VC":
                                VcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "VB":
                                VibEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "SCC":
                                SccEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "PUL":
                                PulEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "IC":
                                IcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "RHC":
                                RhcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "DTRC":
                                DtrcEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                            case "HVB":
                                HVibEvent(logs, kind, MotorId, start, lineId, MotorName);
                                break;
                        };
                    });

                }
                return true;
            }
            catch (Exception e)
            {
                Common.Logger.Error(e.Message);
                return false;
            }


        }

        private void CyEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000001":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000001",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000002":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        //motorLogs.Add(new MotorEventLog()
                        //{
                        //    MotorId = MotorId,
                        //    EventCode = "EVT00000002",
                        //    Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                        //    ProductionLineId = lineId,
                        //    MotorName = MotorName,
                        //    Time = start
                        //});
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000002",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000003":
                    if (logs == null || !logs.Any()) return;
                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000003"));
                        if (exist.Any()) return;//排除该时间段重复事件报告

                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000003",
                            Description = $"[{e.Key}]:{kind.Description}",//-{e.ToList()[0].Param}({e.ToList()[0].Value})
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000004":
                    //判断开机标志位
                    var boots = logs?.Where(e => e.Param.Contains("运行") && e.Value == -1).ToList();
                    if (boots != null && boots.Any())
                    {
                        boots.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000004"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000004",
                                Description = $"[{e.Time}]:{kind.Description}",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000005":
                    //判断累计称重
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机

                    var weighs = logs?.Where(e => e.Param.Contains("累计称重") && e.Value == -1).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000005"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000005",
                                Description = $"[{e.Time}]:{kind.Description}",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000006":
                    //判断累计称重
                    var faccumlateWeigh = logs?.Where(e => e.Param.Contains("累计称重")).ToList();
                    if (faccumlateWeigh != null && faccumlateWeigh.Any())
                    {
                        var last = 0.0;
                        for (var i = 0; i < faccumlateWeigh.Count(); i++)
                        {
                            var weigh = faccumlateWeigh[i].Value;
                            if (i == 0)
                            {
                                last = weigh;
                                continue;
                            }
                            //清零
                            if (weigh < last)
                            {
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000006",
                                    Description = $"[{faccumlateWeigh[i].Time}]:{kind.Description}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = faccumlateWeigh[i].Time
                                });

                                last = weigh;
                            }
                        }
                    }
                    break;
                case "EVT00000007":
                    //判断累计称重
                    var accumlateWeigh = logs?.Where(e => e.Param.Contains("累计称重")).ToList();
                    if (accumlateWeigh != null && accumlateWeigh.Any())
                    {
                        var last = 0.0;
                        for (var i = 0; i < accumlateWeigh.Count(); i++)
                        {
                            var weigh = accumlateWeigh[i].Value;
                            if (i == 0)
                            {
                                last = weigh;
                                continue;
                            }
                            //跑偏
                            if (weigh - last >= 100)
                            {
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000007",
                                    Description = $"[{accumlateWeigh[i].Time}]:{kind.Description}-本次称重（{weigh}）-上次称重（{last}）",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = accumlateWeigh[i].Time
                                });

                                last = weigh;
                            }
                        }
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }

        private void JcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000008":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000008",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000009":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000009",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000009",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000010":
                    if (logs == null || !logs.Any()) return;
                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000010"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000010",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000011":
                    //判断温度
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100 || (e.Value >= -30 && e.Value <= -20))).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000011"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000011",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void CcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000012":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000012",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000013":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000013",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000013",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000014":
                    if (logs == null || !logs.Any()) return;
                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000014"));
                        if (exist.Any()) return;//排除该时间段重复事件报告

                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000014",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000015":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100 || (e.Value >= -30 && e.Value <= -20))).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000015"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000015",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000016":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断圆锥压力
                    var press = logs?.Where(e => e.Param.Contains("压力") && (e.Value > 10 || e.Value < 1)).ToList();
                    if (press != null && press.Any())
                    {
                        press.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000016"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000016",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void VcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT000000017":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT000000017",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT000000018":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT000000018",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT000000018",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT000000019":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT000000019"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT000000019",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT000000020":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100 || (e.Value >= -30 && e.Value <= -20))).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000020"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT000000020",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;

                case "EVT00000021":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断圆锥压力
                    var press = logs?.Where(e => e.Param.Contains("压力") && (e.Value > 10 || e.Value < 1)).ToList();
                    if (press != null && press.Any())
                    {
                        press.ForEach(e =>
                        {
                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = MotorId,
                                EventCode = "EVT00000021",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void VibEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000022":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000022",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000023":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000023",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000023",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000024":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000024"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000024",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;

            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }

        private void HVibEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000022":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000022",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000023":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000023",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000023",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000024":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000024"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000024",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;

            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void SccEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000025":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000025",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000026":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000026",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000026",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000027":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000027"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000027",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000028":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000028"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000028",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void PulEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000029":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000029",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000030":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000030",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000030",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000031":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000031"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000031",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000032":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000032"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000032",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void IcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000033":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000033",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000034":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000034",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000034",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000035":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000035"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000035",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000036":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100 || (e.Value >= -30 && e.Value <= -20))).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000036"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000036",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void RhcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000037":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000037",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000038":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000038",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000038",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000039":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000039"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000039",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000040":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000040"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000040",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000041":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断轴承速度
                    var press = logs?.Where(e => e.Param.Contains("轴承速度") && e.Value == -1).ToList();
                    if (press != null && press.Any())
                    {
                        press.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000041"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000041",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }
        private void DtrcEvent(IEnumerable<AiLog> logs, EventKind kind, string MotorId, long start, string lineId, string MotorName)
        {
            var motorLogs = new List<MotorEventLog>();
            switch (kind.Code)
            {
                case "EVT00000042":
                    if (logs == null || !logs.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000042",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-离线",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        //不予判断
                    }
                    break;
                case "EVT00000043":
                    //判断开机标志位
                    var flags = logs?.Where(e => e.Param.Contains("运行")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = MotorId,
                            EventCode = "EVT00000043",
                            Description = $"[{Unix.ConvertLongDateTime(start)}]:{kind.Description}-停止",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = start
                        });
                    }
                    else
                    {
                        var boot = true;
                        for (var i = 0; i < flags.Count(); i++)
                        {
                            var isStop = flags[i].Value <= 0;
                            if (isStop == boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000043",
                                    Description = $"[{flags[i].Time}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = !isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000044":
                    if (logs == null || !logs.Any()) return;

                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000044"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000044",
                            Description = $"[{e.Key}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000045":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100 || (e.Value >= -30 && e.Value <= -20))).ToList();
                    if (weighs != null && weighs.Any())
                    {
                        weighs.ForEach(e =>
                        {
                            var id = MotorId;
                            var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000045"));
                            if (exist.Any()) return;//排除该时间段重复事件报告

                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = id,
                                EventCode = "EVT00000045",
                                Description = $"[{e.Time}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
            }
            MotorEventLogRepository.InsertAsync(motorLogs);

        }

        #region EventKind Init

        /// <summary>
        /// 事件类型初始化
        /// </summary>
        public void EventKindInit()
        {
            var kinds = new List<EventKind>
            {
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "CY",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "皮带机启停",
                    MotorTypeId = "CY",
                    Regulation = "“开机标志位”，由0-1启动，1-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "CY",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "测速传感器断开",
                    MotorTypeId = "CY",
                    Regulation = "开机标志位出现-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "称重传感器断开",
                    MotorTypeId = "CY",
                    Regulation = "累计称重出现-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "皮带清零",
                    MotorTypeId = "CY",
                    Regulation = "下一分钟比前一分钟“累计称重”低",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "皮带跑偏或者未校准",
                    MotorTypeId = "CY",
                    Regulation = "每分钟“累计称重”差额超过100吨，同时在数据统计中需要剔除该脏数据。",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "JC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "JC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "JC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "JC",
                    Regulation = "温度值在-25度左右或者100度以上",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "CC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "CC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "CC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "CC",
                    Regulation = "温度值在-25度左右或者100度以上",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "压力传感器断开",
                    MotorTypeId = "CC",
                    Regulation = "动锥压力低于1或者高于10（经验值）",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "VC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "VC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "VC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "VC",
                    Regulation = "温度值在-25度左右或者100度以上",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "压力传感器断开",
                    MotorTypeId = "VC",
                    Regulation = "动锥压力低于1或者高于10（经验值）",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "VB",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "VB",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "VB",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "SCC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "SCC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "SCC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "SCC",
                    Regulation = "有一个温度为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "PUL",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "PUL",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "PUL",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "PUL",
                    Regulation = "有一个温度为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "IC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "IC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "IC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "IC",
                    Regulation = "温度值在-25度左右或者100度以上",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "RHC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "RHC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "RHC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "RHC",
                    Regulation = "有一个温度为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "测速传感器断开",
                    MotorTypeId = "RHC",
                    Regulation = "轴承速度为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "嵌入式设备通讯状态",
                    MotorTypeId = "DTRC",
                    Regulation = "连续10分钟之内收不到原始数据",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "主机启停",
                    MotorTypeId = "DTRC",
                    Regulation = "“主机电流”，由0-X启动，X-0 停止",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "传感器损坏或者接线断开",
                    MotorTypeId = "DTRC",
                    Regulation = "Normal所有参数都为-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "温度传感器断开",
                    MotorTypeId = "DTRC",
                    Regulation = "温度值在-25度左右或者100度以上",
                    Time = DateTime.Now.TimeSpan()
                }
            };

            #endregion
       
        EventKindRepository.Insert(kinds);
        EventKindRepository.Batch();
         Console.ReadKey();
        }
        /// <summary>
        /// 生成事件代码
        /// </summary>
        private string GenerateEventCode()
        {
            _count++;          
            return "EVT" + Math.Round(Convert.ToDouble( _count) / 10000000, 7).ToString("N7").Replace(".", "");
        }

        #endregion

    }
}
