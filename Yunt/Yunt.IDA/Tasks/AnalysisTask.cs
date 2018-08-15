using System;
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
using Yunt.Common.Email;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;

namespace Yunt.IDA.Tasks
{
    public class AnalysisTask : IJob
    {
        #region fields && ctor
        
        private static readonly IEventKindRepository EventKindRepository;
        private static readonly IMotorEventLogRepository MotorEventLogRepository;
        private static readonly IProductionLineRepository ProductionLineRepository;
        static AnalysisTask()
        {         
            EventKindRepository = ServiceProviderServiceExtensions.GetService<IEventKindRepository>(Program.Providers["Analysis"]);
            MotorEventLogRepository = ServiceProviderServiceExtensions.GetService<IMotorEventLogRepository>(Program.Providers["Analysis"]);
            ProductionLineRepository= ServiceProviderServiceExtensions.GetService<IProductionLineRepository>(Program.Providers["Device"]);
        }

        #endregion
        

        /// <summary>
        /// 定时任务执行
        /// </summary>
        /// <param name="context"></param>
        public Task Execute(IJobExecutionContext context)
        {
          
            ExcuteAnalysis();
            Thread.Sleep(8*60*60*1000);
            MailPush();
#if DEBUG
            Common.Logger.Info("[AnalysisTask]Analysis Statistics");
            Common.Logger.Info($"耗时{context.JobRunTime.TotalMilliseconds}ms");
#endif
            return Task.CompletedTask;
            //GC.Collect();
        }

        #region mail push

        public void MailPush()
        {
            var now = DateTime.Now;
            var end = now.TimeSpan();
            var start = now.AddDays(-1).TimeSpan();
            var logs = MotorEventLogRepository.GetEntities(e=>e.Time>start&&e.Time<=end);
            var sb = new StringBuilder();
            if (logs?.Any() ?? false)
                foreach (var log in logs)
                {
                    var lineName =ProductionLineRepository.GetEntities(e=>e.ProductionLineId.Equals(
                                    log.ProductionLineId))?.FirstOrDefault()?.Name??"";
                    sb.AppendLine($"【产线】{log.ProductionLineId}_{lineName}【电机】{log.MotorId}_" +
                                      $"{log.MotorName}【事件内容】{log.Description}<br/>");
                }
            var content = sb.ToString();
            if (string.IsNullOrEmpty(content))
            //return;
            {
                content = "无事件";
            }

            var emailhelper = new EmailHelper
            {
                mailFrom = "zhaoh@unitoon.cn",
                mailPwd = "Zh199112",
                mailSubject = "云统设备监测平台之设备事件日报" + DateTime.Now.ToString("yyyyMMddHHmmss") + "【系统邮件】",
                mailBody = content,
                isbodyHtml = true,    //是否是HTML
                host = "smtp.exmail.qq.com",//如果是QQ邮箱则：smtp:qq.com,依次类推
                mailToArray = new string[] { "wubo@unitoon.cn", "xuzh@unitoon.cn", "yujf@sari.ac.cn","dongb@unitoon.cn","shuhr@unitoon.cn","liz@unitoon.cn",
                "huangdb@unitoon.cn", "lic@unitoon.cn"
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
                Common.Logger.Error("发送错误邮件错误", exp);
            }
        }
        

        #endregion

        #region event
        private int _count = 0;
        public void ExcuteAnalysis()
        {
            var keys = MotorEventLogRepository.GetRedisAllKeys();
            keys.ForEach(key =>
            {
                var logs = MotorEventLogRepository.GetAiLogsByKey(key);
                //todo
                var time = DateTime.Now.TimeSpan();
                //test
                //var time = DateTime.Now.Date.AddDays(-1).TimeSpan();
                if (logs.Any())
                {
                    string motorId = "";
                    var strs = key.Split('|');
                    if (strs.Length == 2)
                        motorId = strs[1];
                    if (motorId.IsNullOrWhiteSpace())
                        return;
                    var result = Statistics(logs.OrderBy(e => e.Time), motorId, time);
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

                var kinds = EventKindRepository.GetEntities(e => e.MotorTypeId.Equals(single.MotorTypeId));
                if (single.MotorTypeId.Equals("HVB"))
                    kinds = EventKindRepository.GetEntities(e => e.MotorTypeId.Equals("VB"));
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                    //判断每秒脉冲数
                    var flags = logs?.Where(e => e.Param.Contains("每秒脉冲数")).ToList();
                    if (flags == null || !flags.Any())
                    {
                        //motorLogs.Add(new MotorEventLog()
                        //{
                        //    MotorId = MotorId,
                        //    EventCode = "EVT00000002",
                        //    Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000002",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000003":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f)?.GroupBy(e => e.Time)?.ToList();
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
                            Description = $"[{e.Key.Time()}]:{kind.Description}",//-{e.ToList()[0].Param}({e.ToList()[0].Value})
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000004":
                    //判断每秒脉冲数
                    var boots = logs?.Where(e => e.Param.Contains("每秒脉冲数") && e.Value ==-1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000005":
                    //判断累计产量
                    if (logs?.Any(e => e.Param.Contains("每秒脉冲数") && e.Value <= 0f) ?? false) return;//判断是否关机

                    var weighs = logs?.Where(e => e.Param.Contains("累计产量") && e.Value == -1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000006":
                    //判断累计产量
                    var faccumlateWeigh = logs?.Where(e => e.Param.Contains("累计产量")).ToList();
                    if (faccumlateWeigh != null && faccumlateWeigh.Any())
                    {
                        float last = 0f;
                        for (var i = 0; i < faccumlateWeigh.Count(); i++)
                        {
                            var weigh = faccumlateWeigh[i].Value;
                            if (i == 0)
                            {
                                last = weigh;
                                continue;
                            }
                            //清零
                            if (weigh < last&&Math.Abs(weigh-last)<100)
                            {
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000006",
                                    Description = $"[{faccumlateWeigh[i].Time.Time()}]:{kind.Description}",
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
                    //判断累计产量
                    var accumlateWeigh = logs?.Where(e => e.Param.Contains("累计产量")).ToList();
                    if (accumlateWeigh != null && accumlateWeigh.Any())
                    {
                        float last = 0f;
                        for (var i = 0; i < accumlateWeigh.Count(); i++)
                        {
                            var weigh = accumlateWeigh[i].Value;
                            if (i == 0)
                            {
                                last = (float)weigh;
                                continue;
                            }                         
                            //跑偏
                            if (Math.Abs(weigh - last) >= 100)
                            {
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000007",
                                    Description = $"[{accumlateWeigh[i].Time.Time()}]:{kind.Description}-本次称重（{weigh}）-上次称重（{last}）",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = accumlateWeigh[i].Time
                                });                        
                            }
                            last = weigh;
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000009",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000010":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
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
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000011":
                    //判断温度
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100f || (e.Value >= -30f && e.Value <= -20f))).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000013",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000014":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000014"));
                        if (exist.Any()) return;//排除该时间段重复事件报告

                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000014",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000015":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100f || (e.Value >= -30f && e.Value <= -20f))).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000016":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断圆锥压力
                    var press = logs?.Where(e => e.Param.Contains("压力") && (e.Value > 10 || e.Value < 1f)).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
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

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT000000019":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT000000019"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT000000019",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT000000020":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100f || (e.Value >= -30f && e.Value <= -20f))).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;

                case "EVT00000021":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断圆锥压力
                    var press = logs?.Where(e => e.Param.Contains("压力") && (e.Value > 10f || e.Value < 1f)).ToList();
                    if (press != null && press.Any())
                    {
                        press.ForEach(e =>
                        {
                            motorLogs.Add(new MotorEventLog()
                            {
                                MotorId = MotorId,
                                EventCode = "EVT00000021",
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000023",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000024":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000024"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000024",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
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

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000024":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000024"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000024",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000026",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000027":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000027"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000027",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000028":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000030",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000031":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000031"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000031",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000032":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000034",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000035":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000035"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000035",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000036":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100f || (e.Value >= -30f && e.Value <= -20f))).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000038",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000039":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000039"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000039",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000040":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && e.Value == -1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
                                ProductionLineId = lineId,
                                MotorName = MotorName,
                                Time = e.Time
                            });
                        });
                    }
                    break;
                case "EVT00000041":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断轴承速度
                    var press = logs?.Where(e => e.Param.Contains("轴承速度") && e.Value == -1f).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                            Description = $"[{start.Time()}]:{kind.Description}-离线",
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
                            Description = $"[{start.Time()}]:{kind.Description}-停止",
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
                            if (i == 0)
                            {
                                boot = flags[0].Value > 0f;
                                continue;
                            }
                            var isStop = flags[i].Value <= 0f;
                            if (isStop != boot)
                            {
                                var bootDesc = isStop ? "停止" : "启动";
                                motorLogs.Add(new MotorEventLog()
                                {
                                    MotorId = MotorId,
                                    EventCode = "EVT00000043",
                                    Description = $"[{flags[i].Time.Time()}]:{kind.Description}-{bootDesc}",
                                    ProductionLineId = lineId,
                                    MotorName = MotorName,
                                    Time = flags[i].Time
                                });

                                boot = isStop;
                            }
                        }
                    }
                    break;

                case "EVT00000044":
                    if (logs == null || !logs.Any()) return;
                    var existNo = logs.Where(e => e.Value != -1f)?.ToList();
                    if (existNo != null && existNo.Any())
                        break;
                    var allParamEx = logs.Where(e => e.Value == -1f).GroupBy(e => e.Time).ToList();
                    if (!allParamEx.Any()) return;
                    allParamEx.ForEach(e =>
                    {
                        if (e.Any(item => item.Value != -1f))
                            return;
                        var id = MotorId;
                        var exist = motorLogs.Where(x => x.MotorId == id && x.EventCode.Equals("EVT00000044"));
                        if (exist.Any()) return;//排除该时间段重复事件报告
                        motorLogs.Add(new MotorEventLog()
                        {
                            MotorId = id,
                            EventCode = "EVT00000044",
                            Description = $"[{e.Key.Time()}]:{kind.Description}",
                            ProductionLineId = lineId,
                            MotorName = MotorName,
                            Time = e.Key
                        });
                    });


                    break;
                case "EVT00000045":
                    if (logs?.Any(e => e.Param.Contains("运行") && e.Value == -1f) ?? false) return;//判断是否关机
                    //判断温度
                    var weighs = logs?.Where(e => e.Param.Contains("温度") && (e.Value >= 100f || (e.Value >= -30f && e.Value <= -20f))).ToList();
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
                                Description = $"[{e.Time.Time()}]:{kind.Description}-{e.Param}({e.Value})",
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
                    Regulation = "累计产量出现-1",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "皮带清零",
                    MotorTypeId = "CY",
                    Regulation = "下一分钟比前一分钟“累计产量”低",
                    Time = DateTime.Now.TimeSpan()
                },
                new EventKind()
                {
                    Code = GenerateEventCode(),
                    Description = "皮带跑偏或者未校准",
                    MotorTypeId = "CY",
                    Regulation = "每分钟“累计产量”差额超过100吨，同时在数据统计中需要剔除该脏数据。",
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

        #endregion
    }
}
