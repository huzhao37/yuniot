using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.Inventory.Domain.IRepository;
using Yunt.MQ;
using Yunt.WebApi.Models.ProductionLines;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
using System.Dynamic;
using Yunt.Device.Domain.MapModel;
using Microsoft.AspNetCore.Cors;
using Yunt.Device.Domain.Model;
using System.Diagnostics;
using System.Collections;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductionLine")]
    [Authorize]
    public class ProductionLineController : Controller
    {
        #region ctor & fields

        private readonly IConveyorByHourRepository _conveyorByHourRepository;
        private readonly IConveyorByDayRepository _conveyorByDayRepository;
        private readonly IMotorRepository _motorRepository;
        private readonly IMotortypeRepository _motortypeRepository;
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IProductionPlansRepository _productionPlansRepository;
        private readonly IMessagequeueRepository _messagequeueRepository;
        private readonly IMotorEventLogRepository _motorEventLogRepository;
        private readonly IOutHouseRepository _outHouseRepository;
        private readonly ISparePartsTypeRepository _sparePartsTypeRepository;
        private readonly IAlarmInfoRepository _alarmInfoRepository;
        private readonly ICollectdeviceRepository _collectRepo;
        public ProductionLineController(IConveyorByHourRepository conveyorByHourRepository
            , IConveyorByDayRepository conveyorByDayRepository,
            IMotorRepository motorRepository,
            IProductionLineRepository productionLineRepository,
             IMotortypeRepository motortypeRepository,
              IProductionPlansRepository productionPlansRepository,
              IMessagequeueRepository messagequeueRepository,
              IMotorEventLogRepository motorEventLogRepository,
              IOutHouseRepository outHouseRepository,
              ISparePartsTypeRepository sparePartsTypeRepository,
              IAlarmInfoRepository alarmInfoRepository,
              ICollectdeviceRepository collectRepo
            )
        {
            _conveyorByHourRepository = conveyorByHourRepository;
            _conveyorByDayRepository = conveyorByDayRepository;
            _motorRepository = motorRepository;
            _productionLineRepository = productionLineRepository;
            _motortypeRepository = motortypeRepository;
            _productionPlansRepository = productionPlansRepository;
            _messagequeueRepository = messagequeueRepository;
            _motorEventLogRepository = motorEventLogRepository;
            _outHouseRepository = outHouseRepository;
            _sparePartsTypeRepository = sparePartsTypeRepository;
            _alarmInfoRepository = alarmInfoRepository;
            _collectRepo = collectRepo;
        }
        #endregion

        #region obselete

        // GET: api/ProductionLine
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductionLine/5
        [HttpGet("{id}")]//, Name = "Get"
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductionLine
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProductionLine/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region ProductionLine_Ex
        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("ProductionLineSummary")]
        public ProductionLineStatus ProductionLineSummary(string productionlineId)
        {
            var dt = DateTime.Now.AddMinutes(-10).TimeSpan();
            var tuple = _productionLineRepository.GetMotors(productionlineId);
            var gprs = _productionLineRepository.GetStatus(productionlineId);
            var alarmMotors = _alarmInfoRepository.GetEntities(e => e.Time >= dt)?.GroupBy(e => e.MotorName)?.Count() ?? 0;
            //var loseMotors =0;
            //var collectDevices=_collectRepo.GetEntities(e => e.Productionline_Id.Equals(productionlineId))?.ToList();
            //if (collectDevices != null && collectDevices.Any())
            //    collectDevices.ForEach(c => {
            //        if (dt.Time()> c.Time)
            //        {
            //            loseMotors += _motorRepository.GetEntities(e => e.EmbeddedDeviceId == c.Id)?.ToList()?.Count??0;
            //        }
            //    });

            return new ProductionLineStatus()
            {
                Gprs = gprs,
                LoseMotors = (tuple.Item3 > 0 ? 0 : tuple.Item2),//loseMotors,//
                StopMotors = tuple.Item2,
                RunMotors = tuple.Item3,
                LineStatus = tuple.Item3 > 0,
                AlarmMotors = alarmMotors
            };
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("MainConveyorReal")]
        public MainConveyorReal MainConveyorReal(string productionlineId)
        {
            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsMainBeltWeight)?.FirstOrDefault();
            //var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(lineId) && e.IsBeltWeight&&!e.IsMainBeltWeight).ToList();
            if (motor == null) return new MainConveyorReal();
            // var motorId= _motorRepository.GetEntities(e=>e.ProductionLineId.Equals(productionlineId)&&e.MotorId).SingleOrDefault().MotorId
            var data = _conveyorByHourRepository.GetShiftRealData(motor, Startup.ShiftStartHour);
            var status = _productionLineRepository.GetStatus(productionlineId);
            var instant = _conveyorByHourRepository.GetInstantWeight(motor);
            var load = MathF.Round(data?.LoadStall ?? 0, 3);
            return new MainConveyorReal
            {
                AccumulativeWeight = data?.AccumulativeWeight ?? 0,
                InstantWeight = instant > 0 ? instant : 0,
                LoadStall = load > 0 ? load : 0,
                RunningTime = data?.RunningTime ?? 0,
                ProductionLineStatus = status
            };
        }

        // GET: api/ProductionLine
        [EnableCors("any")]
        [HttpGet]
        [Route("Alarms")]
        public dynamic Alarms(string productionlineId, int pageindex, int pagesize)
        {
            var data = new
            {
                Count = 0,
                Alarms = new PaginatedList<AlarmInfo>(0, 0, 0, new List<AlarmInfo>() { })
            };
            var alarmCount = 0;
            long start = DateTime.Now.Date.TimeSpan(), end = DateTime.Now.Date.AddDays(1).TimeSpan();
            var alarmInfo = _alarmInfoRepository.GetEntities(e => e.Time >= start && e.Time < end)
                            ?.ToList()?.OrderByDescending(e => e.Time);
            if (alarmInfo != null && alarmInfo.Any())
                alarmCount = alarmInfo.GroupBy(e => e.MotorName).Count();
            else
                return data;
            var list = new PaginatedList<AlarmInfo>(pageindex, pagesize, alarmInfo.Count(), alarmInfo.Skip((pageindex - 1) * pagesize).Take(pagesize));
            return new { Count = alarmCount, Alarms = list };
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("MainConveyorHistory")]
        public MainConveyorHository MainConveyorHistory(string productionlineId, DateTime start, DateTime end)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var resp = new MainConveyorHository();

            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsMainBeltWeight)?.FirstOrDefault();

            if (motor == null) return resp;
            var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsBeltWeight && !e.IsMainBeltWeight).ToList();
            List<dynamic> datas;

            var now = DateTime.Now.Date.TimeSpan();
            if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) || (start == end && start.Hour == DateTime.Now.Hour)
                     || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
            {
                //当天
                if ((start == end && start.Hour == DateTime.Now.Hour))
                {
                    datas = _productionLineRepository.MotorShiftHours(motor, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
     
                }
                //历史某一天
                else
                {
                    datas = _productionLineRepository.MotorShiftHours(motor, startT, endT, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
    
                }
            }
            else
            {
                datas = _productionLineRepository.MotorShifts(motor, startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour)?.OrderBy(e => (long)e.Time)?.ToList();
;
            }
            if (datas == null || !datas.Any()) return resp;

            float avgload = 0f, avgWeight = 0f;
            var source = datas?.Where(e => e.RunningTime > 0) ?? null;
            if (source != null && source.Any())
            {
                avgload = MathF.Round(source.Average(e => (float)e.LoadStall), 3);
                avgWeight = MathF.Round(source.Average(e => (float)e.AvgInstantWeight), 2);
            }
            resp.AvgInstantWeight = avgWeight;
            resp.AvgLoadStall = avgload;
            resp.RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2);
            resp.Output = MathF.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2);
            foreach (var d in datas)
            {
                resp.SeriesData.Add(new SeriesData()
                {
                    Output = d.AccumulativeWeight,
                    RunningTime = d.RunningTime,
                    UnixTime = d.Time
                });
            }

            if (!(finishCys?.Any() ?? true))
                return resp;
            finishCys.ForEach(cy =>
           {
               List<dynamic> list;
               //var list = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
               //          e.Time <= endT && e.MotorId.Equals(cy.MotorId))?.ToList();

               if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) || (start == end && start.Hour == DateTime.Now.Hour)
                    || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
               {
                   // float instantLoadStall=
                   //当天
                   if ((start == end && start.Hour == DateTime.Now.Hour))
                   {
                       list = _productionLineRepository.MotorShiftHours(cy, Startup.ShiftStartHour)?.ToList();
                       //resp.AvgLoadStall = _productionLineRepository.MotorIntantLoadStall(motor);
                   }
                   //历史某一天
                   else
                   {
                       list = _productionLineRepository.MotorShiftHours(cy, startT, endT, Startup.ShiftStartHour)?.ToList();
                       // var source = datas?.Where(e => e.RunningTime > 0)??null;
                       //if (source == null || !source.Any()) return resp;
                       //resp.AvgLoadStall = MathF.Round(source.Average(e => (float)e.LoadStall), 3);
                   }
                   float outPut = 0;
                   if (list != null && list.Any())
                   {
                       outPut = MathF.Round(list.Sum(e => (float)e.AccumulativeWeight), 2);
                   }
                   resp.FinishCy.Add(new FinishCy()
                   {
                       MotorId = cy.MotorId,
                       MotorName = cy.Name,
                       Output = outPut
                   });
               }
               else
               {
                   //list = _productionLineRepository.MotorShifts(cy, startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                   var data = _conveyorByHourRepository.GetEntities(e => e.Time >= startT &&
                                        e.Time <= endT && e.MotorId.Equals(cy.MotorId))?.OrderBy(e => e.Time)?.ToList();
                   float outPut = 0;
                   if (data != null && data.Any())
                   {
                       outPut = MathF.Round(data.Sum(e => (float)e.AccumulativeWeight), 2);
                   }
                   resp.FinishCy.Add(new FinishCy()
                   {
                       MotorId = cy.MotorId,
                       MotorName = cy.Name,
                       Output = outPut
                   });
               }

           });

            return resp;
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("Motors")]
        public IEnumerable<MotorSummary> Motors(string productionlineId)
        {
            var datas = new List<MotorSummary>();
            var motors = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.MotorTypeId != "MF"
                         && e.MotorTypeId != "VB" && e.MotorId != "WDD-P001-CC000001" && e.MotorId != "WDD-P001-PUL000004")?.ToList();
            if (motors == null || !motors.Any()) return datas;
            var gprs = _productionLineRepository.GetStatus(productionlineId);
            motors.ForEach(motor =>
            {
                if (motor.MotorTypeId == "CY" && !motor.IsBeltWeight)
                    return;
                var status = gprs ? _productionLineRepository.GetMotorStatusByMotorId(motor.MotorId) : MotorStatus.Lose;
                var detail = _productionLineRepository.MotorShiftDetails(motor, Startup.ShiftStartHour);
                datas.Add(new MotorSummary()
                {
                    MotorId = motor.MotorId,
                    MotorStatus = status,
                    MotorTypeId = motor.MotorTypeId,
                    Name = motor.Name,
                    RunningTime = detail?.RunningTime ?? 0,
                    LoadStall = detail?.LoadStall ?? 0,
                });
            });

            return datas;
        }

        #endregion

        #region motor_ex

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("MotorDetails")]
        public dynamic MotorDetails(DateTime start, DateTime end, string motorId)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();
            if (motor == null) return new CyDetail();
            List<dynamic> datas;
            var now = DateTime.Now.Date.TimeSpan();
            if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) || (start == end && start.Hour == DateTime.Now.Hour)
               || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
            {
                //当天
                if ((start == end && start.Hour == DateTime.Now.Hour))
                {
                    datas = _productionLineRepository.MotorShiftHours(motor, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
                    var series = _productionLineRepository.GetMotorSeries(datas, motor);
                    var details = _productionLineRepository.GetMotorInstantDetails(motor);
                    return new { details, series };
                }
                //历史某一天
                else
                {
                    datas = _productionLineRepository.MotorShiftHours(motor, startT, endT, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
                    var series = _productionLineRepository.GetMotorSeries(datas, motor);
                    var details = _productionLineRepository.GetMotorHistoryDetails(datas, motor);
                    return new { details, series };
                }
            }
            datas = _productionLineRepository.MotorShifts(motor, startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour)?.OrderBy(e => (long)e.Time)?.ToList();
            if (datas == null || !datas.Any())
            {
                var todayDatas = _productionLineRepository.MotorShiftDetails(motor, Startup.ShiftStartHour);
                todayDatas.Time = DateTime.Now.Date.TimeSpan();
                datas.Add(todayDatas);
            }

            return new
            {
                details = _productionLineRepository.GetMotorHistoryDetails(datas, motor),
                series = _productionLineRepository.GetMotorSeries(datas, motor)
            };
        }
        #endregion

        #region schedule

        [HttpPost]
        [Route("Plans")]
        [EnableCors("any")]
        public bool Schedule([FromBody]ProductionPlans plan)
        {
            if (plan == null)
                return false;
            var productionline =
                _productionLineRepository.GetEntities(e => e.ProductionLineId.Equals(plan.ProductionlineId))?.FirstOrDefault();
            if (productionline == null)
                return false;
            //28B+6+2+1=37B          
            var bytes = Extention.strToToHexByte("0102030405FF");
            //Array.Reverse(configBuff);//高低位互换
            var dataconfig = Extention.strToToHexByte("8010");
            Array.Reverse(dataconfig);//高低位互换
            bytes = Extention.CombomBinaryArray(bytes, dataconfig);

            bytes = Extention.CombomBinaryArray(bytes, new byte[1] { 01 });

            var startTime = plan.Start.Time();
            var endTime = plan.End.Time();
            // 年月日时 高低位
            var start = startTime.TimeTobyte4();
            Array.Reverse(start);//高低位互换
            bytes = Extention.CombomBinaryArray(bytes, start);
            var end = endTime.TimeTobyte4();
            Array.Reverse(end);//高低位互换
            bytes = Extention.CombomBinaryArray(bytes, end);
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy3));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy2));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.MainCy));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy1));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy4));

#if DEBUG
            Logger.Error(Extention.ByteArrayToHexString(bytes));
#endif
            var wddQueue =
      _messagequeueRepository.GetEntities(e => e.Route_Key.Equals("WUDDOUT")).FirstOrDefault();
            if (wddQueue == null)
                return false;

            var queueHost = wddQueue.Host;
            var queuePort = wddQueue.Port;
            var queueUserName = wddQueue.Username;
            var queuePassword = wddQueue.Pwd;

            var ccuri = "amqp://" + queueHost + ":" + queuePort;
            var queue = wddQueue.Route_Key;
            var route = wddQueue.Route_Key;
            var exchange = "amq.topic";
            plan.Time = DateTime.Now;
            var rabbitHelper = new RabbitMqHelper();
            var result = rabbitHelper.WriteCmd(ccuri, bytes, queue, route, exchange, queueUserName, queuePassword);
            if (result)
                _productionPlansRepository.Insert(plan);
            return result;
        }

        [HttpGet]
        [Route("GetSchedules")]
        [EnableCors("any")]
        public PaginatedList<dynamic> GetSchedules(int pagesize, int pageindex, DateTime start, DateTime end, string productionlineId, string motorId)
        {
            try
            {
                long startT = start.TimeSpan(), endT = end.TimeSpan();
                var resp = new List<dynamic>();
                var results = new PaginatedList<dynamic>(0, 0, 0, resp);
                var mainCy = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsMainBeltWeight)?.FirstOrDefault();
                var finishCy1 = _motorRepository.GetEntities(e => e.MotorId.Equals("WDD-P001-CY000054") && e.IsBeltWeight && !e.IsMainBeltWeight)?.FirstOrDefault();
                var finishCy2 = _motorRepository.GetEntities(e => e.MotorId.Equals("WDD-P001-CY000019") && e.IsBeltWeight && !e.IsMainBeltWeight)?.FirstOrDefault();
                var finishCy3 = _motorRepository.GetEntities(e => e.MotorId.Equals("WDD-P001-CY000041") && e.IsBeltWeight && !e.IsMainBeltWeight)?.FirstOrDefault();
                var finishCy4 = _motorRepository.GetEntities(e => e.MotorId.Equals("WDD-P001-CY000046") && e.IsBeltWeight && !e.IsMainBeltWeight)?.FirstOrDefault();
                //var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsBeltWeight && !e.IsMainBeltWeight).ToList();
                List<dynamic> mainCyList = null;
                List<dynamic> cy1List = null;
                List<dynamic> cy2List = null;
                List<dynamic> cy3List = null;
                List<dynamic> cy4List = null;
                //当天
                var now = DateTime.Now.Date.TimeSpan();
                var datas = _productionPlansRepository.GetEntities(e => e.ProductionlineId.EqualIgnoreCase(productionlineId)
                              && e.Start >= startT && e.End <= endT)?.ToList();
                if (datas != null && datas.Any())
                    datas.ForEach(x =>
                   {
                       long startTime = x.Start, endTime = x.End;
                       DateTime s = startTime.Time(), e = endTime.Time();
                       if ((e.Subtract(s).TotalHours <= 24 && s.Hour >= Startup.ShiftStartHour && e.Hour < Startup.ShiftEndHour) || (s == e && start.Hour == DateTime.Now.Hour)
                                || ((s.Hour >= Startup.ShiftStartHour && e.Hour >= Startup.ShiftEndHour || s.Hour < Startup.ShiftStartHour && e.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
                       {
                           //当天
                           if ((s == e && s.Hour == DateTime.Now.Hour))
                           {
                               mainCyList = _productionLineRepository.MotorShiftHours(mainCy, Startup.ShiftStartHour)?.ToList();
                               cy1List = _productionLineRepository.MotorShiftHours(finishCy1, Startup.ShiftStartHour)?.ToList();
                               cy2List = _productionLineRepository.MotorShiftHours(finishCy2, Startup.ShiftStartHour)?.ToList();
                               cy3List = _productionLineRepository.MotorShiftHours(finishCy3, Startup.ShiftStartHour)?.ToList();
                               cy4List = _productionLineRepository.MotorShiftHours(finishCy4, Startup.ShiftStartHour)?.ToList();

                           }
                           //历史某一天
                           else
                           {
                               mainCyList = _productionLineRepository.MotorShiftHours(mainCy,startTime ,endTime , Startup.ShiftStartHour)?.ToList();
                               cy1List = _productionLineRepository.MotorShiftHours(finishCy1,startTime, endTime, Startup.ShiftStartHour)?.ToList();
                               cy2List = _productionLineRepository.MotorShiftHours(finishCy2,startTime, endTime, Startup.ShiftStartHour)?.ToList();
                               cy3List = _productionLineRepository.MotorShiftHours(finishCy3,startTime, endTime, Startup.ShiftStartHour)?.ToList();
                               cy4List = _productionLineRepository.MotorShiftHours(finishCy4,startTime, endTime, Startup.ShiftStartHour)?.ToList();
                           }
                       }
                       else
                       {
                           mainCyList = _productionLineRepository.MotorShifts(mainCy, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                           cy1List = _productionLineRepository.MotorShifts(finishCy1, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                           cy2List = _productionLineRepository.MotorShifts(finishCy2, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                           cy3List = _productionLineRepository.MotorShifts(finishCy3, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                           cy4List = _productionLineRepository.MotorShifts(finishCy4, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                       }              
                       if (mainCyList != null && mainCyList.Any())
                           resp.Add(new
                           {
                               RealOutput = MathF.Round(mainCyList.Sum(m => (float)m.AccumulativeWeight), 2),
                               PlanOutput = x?.MainCy ?? 0,//datas?.Sum(e => e.MainCy) ?? 0,
                               mainCy.MotorId,
                               mainCy.Name,
                               Start = startTime,
                               End = endTime
                           });
                       if (cy1List != null && cy1List.Any())
                           resp.Add(new
                           {
                               RealOutput = MathF.Round(cy1List.Sum(m => (float)m.AccumulativeWeight), 2),
                               PlanOutput = x?.FinishCy1 ?? 0,//datas?.Sum(e => e.FinishCy1) ?? 0,
                               finishCy1.MotorId,
                               finishCy1.Name,
                               Start = startTime,
                               End = endTime
                           });
                       if (cy2List != null && cy2List.Any())
                           resp.Add(new
                           {
                               RealOutput = MathF.Round(cy2List.Sum(m => (float)m.AccumulativeWeight), 2),
                               PlanOutput = x?.FinishCy2 ?? 0,// datas?.Sum(e => e.FinishCy2) ?? 0,
                               finishCy2.MotorId,
                               finishCy2.Name,
                               Start = startTime,
                               End = endTime
                           });
                       if (cy3List != null && cy3List.Any())
                           resp.Add(new
                           {
                               RealOutput = MathF.Round(cy3List.Sum(m => (float)m.AccumulativeWeight), 2),
                               PlanOutput = x?.FinishCy3 ?? 0,//datas?.Sum(e => e.FinishCy3) ?? 0,
                               finishCy3.MotorId,
                               finishCy3.Name,
                               Start = startTime,
                               End = endTime
                           });
                       if (cy4List != null && cy4List.Any())
                           resp.Add(new
                           {
                               RealOutput = MathF.Round(cy4List.Sum(m => (float)m.AccumulativeWeight), 2),
                               PlanOutput = x?.FinishCy4 ?? 0,//datas?.Sum(e => e.FinishCy4) ?? 0,
                               finishCy4.MotorId,
                               finishCy4.Name,
                               Start = startTime,
                               End = endTime
                           });

                   });
                if (!motorId.IsNullOrWhiteSpace())
                    resp = resp.Where(e => ((string)e.MotorId).Equals(motorId)).ToList();
                var list = resp?.OrderByDescending(x => (long)(x?.Start ?? 0))?.Skip((pageindex - 1) * pagesize)?.Take(pagesize) ?? new List<dynamic>();
                return new PaginatedList<dynamic>(pageindex, pagesize, resp.Count(), list);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            }

        }
        [HttpGet]
        [Route("GetScheduleCys")]
        [EnableCors("any")]
        public dynamic GetScheduleCys(string productionlineId)
        {
            var results = new List<dynamic>();
            var cys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) &&
                        (e.MotorId.Equals("WDD-P001-CY000054") || e.MotorId.Equals("WDD-P001-CY000019") ||
                        e.MotorId.Equals("WDD-P001-CY000041") || e.MotorId.Equals("WDD-P001-CY000046") || e.IsMainBeltWeight))?.ToList();
            if (cys != null && cys.Any())
                cys.ForEach(cy =>
                {
                    results.Add(new { cy.MotorId, cy.Name });
                });
            return results;
        }
        #endregion

        #region instant_data_plc
        [HttpGet]
        [Route("MotorTypeList")]
        [EnableCors("any")]
        public dynamic MotorTypeList()
        {
            try
            {
                var data = _motortypeRepository.GetEntities(e => e.MotorTypeId != "SCC" && e.MotorTypeId != "RHC" && e.MotorTypeId != "DTRC" &&
                             e.MotorTypeId != "MS" && e.MotorTypeId != "Universal");
                if (data != null && data.Any())
                    return data.Select(e => new[] { e.MotorTypeId, e.MotorTypeName });
                return "no data";
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return ex.Message;
            }

        }
        [HttpGet]
        [Route("MotorListByType")]
        [EnableCors("any")]
        public dynamic MotorListByType(string motorTypeId, string productionLineId)
        {
            try
            {
                var data = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId) & e.MotorTypeId.Equals(motorTypeId));
                if (data != null && data.Any())
                    return data.Select(e => new[] { e.MotorId, e.Name });
                return "no data";
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return ex.Message;
            }
        }


        /// <summary>
        /// 获取电机设备原始数据
        /// </summary>
        /// <param name="motorId">电机设备</param>
        /// <param name="dt">日期</param>
        /// <param name="cache">是否从缓存获取</param>
        /// <returns></returns>
        [HttpGet]
        [Route("MotorHistory")]
        [EnableCors("any")]
        public IEnumerable<dynamic> GetDateHistory(string motorId, DateTime dt, bool cache)
        {
            try
            {
                var datas = new List<dynamic>();
                var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();
                if (motor == null)
                    return datas;
                return _productionLineRepository.GetMotorHistoryByDate(motor, dt, cache)?.OrderByDescending(e => (long)e.Time);
            }
            catch (Exception)
            {

                return new List<dynamic>();
            }

        }

        [HttpGet]
        [Route("MotorDiHistory")]
        [EnableCors("any")]
        public IEnumerable<dynamic> GetOtherHistory(string motorId, DateTime dt, bool isAlarm)
        {
            try
            {
                if (isAlarm)
                    return _motorEventLogRepository.GetDis(motorId, dt)?.Where(e => e.DataPhysic.Equals("报警"))?
                    .Select(e => new { e.Param, e.Value, Tim = e.Time })?.OrderByDescending(e => e.Tim)?.GroupBy(e => e.Tim);
                return _motorEventLogRepository.GetDis(motorId, dt)?.Where(e => e.DataPhysic.Equals("状态"))?
                  .Select(e => new { e.Param, e.Value, Tim = e.Time })?.OrderByDescending(e => e.Tim)?.GroupBy(e => e.Tim);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return new List<dynamic>();
            }

        }
        #endregion

        #region motor_event
        [HttpGet]
        [Route("MotorEvent")]
        [EnableCors("any")]
        public PaginatedList<dynamic> EventLogs(string motorId, string motorType, int pageindex, int pagesize, DateTime start, DateTime end)
        {
            var eventLogs = new List<MotorEventLog>();
            var alarms = new List<AlarmInfo>();
            Motor motor = null;
            List<Motor> motors = null;
            long startT = start.TimeSpan(), endT = end.AddDays(1).AddMilliseconds(-1).TimeSpan();
            if (!motorId.IsNullOrWhiteSpace())
            {
                motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();

                eventLogs = _motorEventLogRepository.GetEntities(e => e.MotorId.Equals(motorId) &&
                 e.Time >= startT && e.Time <= endT)?.ToList();

                alarms = _alarmInfoRepository.GetEntities(e => e.MotorId.Equals(motorId)
                           && e.Time >= startT && e.Time <= endT)?.ToList();
            }
            if (!motorType.IsNullOrWhiteSpace() && motorId.IsNullOrWhiteSpace())
            {
                motors = _motorRepository.GetEntities(e => e.MotorTypeId.Equals(motorType))?.ToList();
                var motorIds = motors?.Select(e => e.MotorId);
                if (motorIds != null && motorIds.Any())
                {
                    eventLogs = _motorEventLogRepository.GetEntities(e => motorIds.Contains(e.MotorId) &&
                                   e.Time >= startT && e.Time <= endT)?.ToList();

                    alarms = _alarmInfoRepository.GetEntities(e => motorIds.Contains(e.MotorId)
                               && e.Time >= startT && e.Time <= endT)?.ToList();
                }
            }
            if (motorType.IsNullOrWhiteSpace() && motorId.IsNullOrWhiteSpace())
            {
                eventLogs = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.Equals("WDD-P001") &&
                                e.Time >= startT && e.Time <= endT)?.ToList();
                alarms = _alarmInfoRepository.GetEntities(e => e.Time >= startT && e.Time <= endT)?.ToList();
            }

            //if (motor == null&&motors==null)
            //    return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>() { });     

            if ((eventLogs == null || !eventLogs.Any()) && (alarms == null || !alarms.Any()))
                return new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>() { });

            var datas = new List<dynamic>();
            //var x = new System.Collections.Concurrent.ConcurrentBag<dynamic>();
            //test sw
#if DEBUG
            var sw = new Stopwatch();
            sw.Start();
#endif
            if (eventLogs != null && eventLogs.Any())
                eventLogs.ForEach(e =>
                {
                    datas.Add(new
                    {
                        e.MotorId,
                        e.MotorName,
                        Desc = e.Description,
                        e.Time,
                        EventType = "Event"
                    });
                });
            if (alarms != null && alarms.Any())
                alarms.ForEach(a =>
               {
                   datas.Add(new
                   {
                       a.MotorId,
                       a.MotorName,
                       Desc = a.Content,
                       a.Time,
                       EventType = "Alarm"
                   });
               });
#if DEBUG
            sw.Stop();
            Logger.Warn($"耗时{sw.ElapsedMilliseconds}ms");
#endif
            var list = datas?.OrderByDescending(x => (long)(x?.Time ?? 0))?.Skip((pageindex - 1) * pagesize)?.Take(pagesize) ?? new List<dynamic>();
            return new PaginatedList<dynamic>(pageindex, pagesize, datas.Count(), list);
        }
        #endregion

        #region CostAccount
        [HttpGet]
        [EnableCors("any")]
        [Route("SpareCost")]
        public dynamic SpareAccounting(string productionLineId, DateTime start, DateTime end, int pageindex, int pagesize)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var line = _productionLineRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.FirstOrDefault();
            if (line == null) return new
            {
                SpareCount = 0,
                SpareCost = 0,
                SpareDetails = new { }
            };

            var allMotors = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
            if (allMotors == null || !allMotors.Any()) return new
            {
                SpareCount = 0,
                SpareCost = 0,
                SpareDetails = new { }
            };
            var allMotorIds = allMotors.Select(e => e.MotorId);
            var endTime = endT.Time().Date.AddDays(1).AddMilliseconds(-1).TimeSpan();
            var useLessSpares = _outHouseRepository.GetUseless(allMotorIds, startT, endTime);
            var spareCount = useLessSpares?.Count() ?? 0;
            var spareCost = _outHouseRepository.CalcCost(useLessSpares);
            var spareDetails = new List<dynamic>();
            if (useLessSpares == null || !useLessSpares.Any())
                return new
                {
                    SpareCount = spareCount,
                    SpareCost = spareCount,
                    SpareDetails = new { }
                };
            useLessSpares.ToList().ForEach(s =>
            {
                var motorName = _motorRepository.GetEntities(e => e.MotorId.Equals(s.MotorId))?.
                                FirstOrDefault()?.Name ?? "";
                var spareName = _sparePartsTypeRepository.GetEntities(e => e.Id == s.SparePartsTypeId)?.
                                FirstOrDefault()?.Name ?? "";
                spareDetails.Add(new { MotorName = motorName, SpareName = spareName, Start = s.OutTime.Time(), End = s.UselessTime.Time() });
            });
            var results = new PaginatedList<dynamic>(0, 0, 0, new List<dynamic>());
            if (spareDetails?.Any() ?? false)
            {
                var list = spareDetails.OrderBy(x => (DateTime)x.End).Skip((pageindex - 1) * pagesize).Take(pagesize);
                results = new PaginatedList<dynamic>(pageindex, pagesize, spareDetails.Count(), list);
            }

            return new
            {
                SpareCount = spareCount,
                SpareCost = spareCost,
                SpareDetails = results//spareDetails
            };


        }
        [HttpGet]
        [EnableCors("any")]
        [Route("PowersCost")]
        public dynamic PowerAccounting(string productionLineId, DateTime start, DateTime end)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var line = _productionLineRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.FirstOrDefault();
            if (line == null) return null;

            var allMotors = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
            if (allMotors == null || !allMotors.Any()) return null;
            var allMotorIds = allMotors.Select(e => e.MotorId);
            List<PowerCal> powers = null;
            float totalPower = 0, avgPower = 0;
            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) || (start == end && start.Hour == DateTime.Now.Hour)
               || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
            {
                if ((start == end && start.Hour == DateTime.Now.Hour))
                {
                    powers = _productionLineRepository.ShiftCalcMotorPowers(allMotors,Startup.ShiftStartHour);
                    totalPower = MathF.Round(powers.Sum(e => e.ActivePower), 2);
                    avgPower = totalPower;
                    return new { TotalPower = totalPower, AvgPower = avgPower, Powers = powers };
                }
                //历史某一天
                else
                {
                    powers = _productionLineRepository.ShiftCalcMotorPowers(allMotors, startT,Startup.ShiftStartHour);
                    totalPower = MathF.Round(powers.Sum(e => e.ActivePower), 2);
                    avgPower = totalPower;
                    return new { TotalPower = totalPower, AvgPower = avgPower, Powers = powers };
                }

            }
            //历史区间段
            var endTime = endT.Time().Date.AddDays(1).TimeSpan();//.AddMilliseconds(-1).TimeSpan();
            var times = end.Subtract(start).TotalDays + 1;
            powers = _productionLineRepository.ShiftCalcMotorPowers(allMotors, startT, endTime,Startup.ShiftStartHour,Startup.ShiftEndHour);
            totalPower = MathF.Round(powers.Sum(e => e.ActivePower), 2);
            avgPower = totalPower != 0 ? (float)Math.Round(totalPower / times, 2) : 0;
            return new { TotalPower = totalPower, AvgPower = avgPower, Powers = powers };
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("PowerAnaly")]
        public dynamic PowerAnalys(string productionlineId, DateTime start, DateTime end)
        {
            List<MainSeriesData> list = new List<MainSeriesData>();
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motors =
                       _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsBeltWeight
                       && !e.IsMainBeltWeight)?.ToList();
            if (motors == null) return list;
            motors.ForEach(m =>
            {
                List<dynamic> datas = null;
                if (m.MotorId == "WDD-P001-CY000054" || m.MotorId == "WDD-P001-CY000046" || m.MotorId == "WDD-P001-CY000019" || m.MotorId == "WDD-P001-CY000041")
                {
                    var msd = new MainSeriesData();
                    var now = DateTime.Now.Date.TimeSpan();
                    if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) || (start == end && start.Hour == DateTime.Now.Hour)
                         || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
                    {
                        //当天
                        if ((start == end && start.Hour == DateTime.Now.Hour))
                        {
                            datas = _productionLineRepository.MotorShiftHours(m, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();

                        }
                        //历史某一天
                        else
                        {
                            datas = _productionLineRepository.MotorShiftHours(m, startT, endT, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
                        }
                    }
                    else
                    {
                        datas = _productionLineRepository.MotorShifts(m, startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour)?.OrderBy(e => (long)e.Time)?.ToList();
                    }
                    float outPut = 0;
                    float sumRunningTime = 0;
                    var ActivePowers = MathF.Round(datas.Sum(e => (float)e.ActivePower), 2);
                    if (datas != null && datas.Any())
                    {
                        outPut = MathF.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2);
                        sumRunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2);
                    }
                    var source = new List<dynamic>();
                    datas.ForEach(d =>
                    {
                        msd.SeriesDatas.Add(new SeriesDatas
                        {
                            Output = d.AccumulativeWeight,
                            UnixTime = d.Time
                        });
                    });
                    msd.SumOutPut = outPut;
                    msd.SumRunningTime = sumRunningTime;
                    msd.AvgActivePower = (outPut != 0) ? MathF.Round(ActivePowers / outPut, 3) : 0f;
                    msd.MotorName = m.Name;
                    msd.MotorID = m.MotorId;
                    list.Add(msd);
                }
            });
            return list;
        }

        [HttpGet]
        [EnableCors("any")]
        [Route("Report")]
        public dynamic Reports(string productionlineId, DateTime start, DateTime end)
        {
            var report = new Report();
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motors =
                       _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsBeltWeight
                       && !e.IsMainBeltWeight)?.ToList();
            if (motors == null) return report;
            List<dynamic> datas;

            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((startT == endT) && (startT == now))
            {
                var detail1 = new List<Detail>();
                var detail2 = new List<Detail>();
                motors.ForEach(m =>
                {
                    datas = _productionLineRepository.MotorHours(m)?.OrderBy(e => (long)e.Time)?.ToList();
                    if (datas != null && datas.Any())
                    {
                        detail1.Add(new Detail() { Name = m.Name, OutPut = datas.Sum(e => (float)e.AccumulativeWeight) });
                        detail2.Add(new Detail() { Name = m.Name, OutPut = datas.Sum(e => (float)e.AccumulativeWeight) });
                    }

                });
                report.Row.Add(new Row()
                {
                    Detail = detail1,
                    Time = now.Time()
                });
                report.Detail = detail2;
                return report;
            }
            //历史某一天
            else if (startT == endT)
            {
                var detail1 = new List<Detail>();
                var detail2 = new List<Detail>();
                motors.ForEach(m =>
                {
                    datas = _productionLineRepository.MotorHours(m, startT)?.OrderBy(e => (long)e.Time)?.ToList();
                    if (datas != null && datas.Any())
                    {
                        detail1.Add(new Detail() { Name = m.Name, OutPut = datas.Sum(e => (float)e.AccumulativeWeight) });
                        detail2.Add(new Detail() { Name = m.Name, OutPut = datas.Sum(e => (float)e.AccumulativeWeight) });
                    }

                });
                report.Row.Add(new Row()
                {
                    Detail = detail1,
                    Time = startT.Time()
                });
                report.Detail = detail2;
                return report;
            }
            else
            {

                var detail2 = new List<Detail>();
                var rows = new List<Row>();
                motors.ForEach(m =>
                {
                    datas = _productionLineRepository.MotorDays(startT, endT, m)?.OrderBy(e => (long)e.Time)?.ToList();
                    if (datas != null && datas.Any())
                    {
                        datas.ForEach(d =>
                        {
                            var detail1 = new List<Detail>();
                            detail1.Add(new Detail() { Name = m.Name, OutPut = (float)d.AccumulativeWeight });
                            rows.Add(new Row()
                            {
                                Detail = detail1,
                                Time = ((long)d.Time).Time()
                            });
                        });
                        detail2.Add(new Detail() { Name = m.Name, OutPut = datas.Sum(e => (float)e.AccumulativeWeight) });
                    }

                });
                foreach (var row in rows.GroupBy(e => e.Time))
                {
                    var list = row.ToList();
                    var detail = new List<Detail>();
                    foreach (var item in list)
                    {
                        detail.Add(item.Detail[0]);
                    }
                    report.Row.Add(new Row() { Detail = detail, Time = row.Key });
                }
                report.Detail = detail2;
                return report;
            }
        }

        #endregion

        //test
        [HttpGet]
        [Route("test")]
        [EnableCors("any")]
        public dynamic test()
        {
            return _motorRepository.GetEntities();
            //_userRepository.GetPage(pageindex, pagesize, null, e => e.Time);
            // return 1;
        }
    }
}
