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
              IAlarmInfoRepository alarmInfoRepository
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
            var tuple = _productionLineRepository.GetMotors(productionlineId);
            var gprs = _productionLineRepository.GetStatus(productionlineId);
            return new ProductionLineStatus()
            {
                Gprs = gprs,
                LoseMotors = tuple.Item1,
                StopMotors = tuple.Item2,
                RunMotors = tuple.Item3,
                LineStatus= tuple.Item3>0
            };
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("MainConveyorReal")]
        public MainConveyorReal MainConveyorReal(string productionlineId)
        {
            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId)&&e.IsMainBeltWeight)?.FirstOrDefault();
            //var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(lineId) && e.IsBeltWeight&&!e.IsMainBeltWeight).ToList();
            if (motor == null) return new MainConveyorReal();
            // var motorId= _motorRepository.GetEntities(e=>e.ProductionLineId.Equals(productionlineId)&&e.MotorId).SingleOrDefault().MotorId
            var data = _conveyorByHourRepository.GetRealData(motor);
            var status = _productionLineRepository.GetStatus(productionlineId);
            return new MainConveyorReal
            {
                AccumulativeWeight = data?.AccumulativeWeight??0,
                InstantWeight = data?.AvgInstantWeight ?? 0,
                LoadStall = data?.LoadStall ?? 0,
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
            var alarmInfo = _alarmInfoRepository.GetEntities(e =>e.Time >= start && e.Time < end)
                            ?.ToList()?.OrderByDescending(e => e.Time);
            if (alarmInfo != null && alarmInfo.Any())
                alarmCount = alarmInfo.GroupBy(e => e.MotorName).Count();
            else
                return data;
            var list = new PaginatedList<AlarmInfo>(pageindex, pagesize, alarmInfo.Count(), alarmInfo.Skip((pageindex - 1) * pagesize).Take(pagesize));
            return new {Count=alarmCount,Alarms= list};
        }

        // GET: api/ProductionLine
        [HttpGet]
        [EnableCors("any")]
        [Route("MainConveyorHistory")]
        public MainConveyorHository MainConveyorHistory(string productionlineId,DateTime start,DateTime end)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var resp = new MainConveyorHository();
          
            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsMainBeltWeight)?.FirstOrDefault();
            
            if (motor == null) return resp;
            var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId) && e.IsBeltWeight&&!e.IsMainBeltWeight).ToList();
            List<dynamic> datas;
            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((startT == endT) && (startT == now))
            {
                datas = _productionLineRepository.MotorHours(motor)?.ToList();              
            }
            //历史某一天
            else if (startT == endT)
            {
                datas = _productionLineRepository.MotorHours(motor, startT)?.ToList();
            }
            else
            {
                datas = _productionLineRepository.MotorDays(startT, endT, motor)?.ToList();
            }
            //var datas = _conveyorByDayRepository.GetEntities(e => e.Time>= startT &&
            //            e.Time<=endT && e.MotorId.Equals(motor.MotorId))?.ToList();
            if (datas==null||!datas.Any()) return resp;
            resp.AvgInstantWeight =(float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight),2);
            resp.AvgLoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2);
            resp.RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2);
            resp.Output = (float)Math.Round(datas.Sum(e =>(float) e.AccumulativeWeight), 2);
            foreach (var d in datas)
            {
                resp.SeriesData.Add(new SeriesData()
                {
                    Output = d.AccumulativeWeight,
                    RunningTime = d.RunningTime,
                    UnixTime = d.Time
                });
            }

            if (!finishCys?.Any() ?? true)
                return resp;
            Parallel.ForEach(finishCys, cy =>
            {
                List<dynamic> list;
                //var list = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                //          e.Time <= endT && e.MotorId.Equals(cy.MotorId))?.ToList();

                if ((startT == endT) && (startT == now))
                {
                    list = _productionLineRepository.MotorHours(cy)?.ToList();
                }
                //历史某一天
                else if(startT == endT)
                {
                    list = _productionLineRepository.MotorHours(cy, startT)?.ToList();
                }
                else
                {
                    list = _productionLineRepository.MotorDays(startT, endT, cy)?.ToList();
                }

                float outPut = 0;
                if (list != null&& list.Any())
                {
                    outPut = (float)Math.Round(list.Sum(e =>(float) e.AccumulativeWeight), 2);
                }
                resp.FinishCy.Add(new FinishCy()
                {
                    MotorId = cy.MotorId,
                    MotorName = cy.Name,
                    Output = outPut
                });
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
            var motors = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionlineId))?.ToList();
            if (motors==null||!motors.Any()) return datas;
            Parallel.ForEach(motors, motor =>
            {
                var status = _productionLineRepository.GetMotorStatusByMotorId(motor.MotorId);
                var detail = _productionLineRepository.MotorDetails(motor);              
                datas.Add(new MotorSummary()
                {
                    MotorId = motor.MotorId,
                    MotorStatus = status,
                    MotorTypeId = motor.MotorTypeId,
                    Name = motor.Name,
                    RunningTime = detail?.RunningTime??0,
                    LoadStall = detail?.LoadStall??0,
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
        public dynamic MotorDetails(DateTime start,DateTime end,string motorId)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();
            if (motor == null) return new CyDetail();
            List<dynamic> datas;
            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((startT == endT)&&(startT==now))
            {
                datas = _productionLineRepository.MotorHours(motor)?.ToList();
                return _productionLineRepository.GetMotorDetails(datas, motor);
            }
            //历史某一天
            if (startT == endT)
            {
                datas = _productionLineRepository.MotorHours(motor,startT)?.ToList();
                return _productionLineRepository.GetMotorDetails(datas, motor);
            }
            datas = _productionLineRepository.MotorDays(startT, endT, motor)?.ToList();          
            return _productionLineRepository.GetMotorDetails(datas, motor);
        }

        #endregion

        #region schedule

        [HttpPost]
        [Route("Plans")]
        [EnableCors("any")]
        public bool Schedule([FromBody]ProductionPlans plan)
        {
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

            bytes = Extention.CombomBinaryArray(bytes,new byte[1] { 01});

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
            plan.Time=DateTime.Now;
            var rabbitHelper = new RabbitMqHelper();
            var result=rabbitHelper.WriteCmd(ccuri, bytes, queue, route, exchange,queueUserName, queuePassword);
            if(result)
                _productionPlansRepository.Insert(plan);
            return result;
        }
        #endregion

        #region instant_data_plc
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
        public IEnumerable<dynamic> GetDateHistory( string motorId, DateTime dt,bool cache)
        {
            var datas=new List<dynamic>();
            var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();
            if (motor == null)
                return datas;
            return _productionLineRepository.GetMotorHistoryByDate(motor, dt, cache);
        }


        #endregion

        #region motor_event
        [HttpGet]
        [Route("MotorEvent")]
        [EnableCors("any")]
        public PaginatedList<MotorEventLog> EventLogs(string motorId,int pageindex, int pagesize,DateTime start,DateTime end)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(motorId))?.FirstOrDefault();
            if (motor == null)
                return new PaginatedList<MotorEventLog>(0, 0, 0, new List<MotorEventLog>() { });
            var datas = _motorEventLogRepository.GetEntities(e=>e.MotorId.Equals(motorId)&&
                        e.Time>=startT&&e.Time<=endT)?.ToList();
            if (datas==null||!datas.Any())
                return new PaginatedList<MotorEventLog>(0, 0, 0, new List<MotorEventLog>() {});
            var list = datas.OrderByDescending(x => x.Time).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return new PaginatedList<MotorEventLog>(pageindex, pagesize, datas.Count(), list);
        }
        #endregion

        #region CostAccount
        [HttpGet]
        [EnableCors("any")]
        [Route("SpareCost")]
        public dynamic SpareAccounting(string productionLineId, DateTime start, DateTime end)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var line = _productionLineRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.FirstOrDefault();
            if (line == null)return new
            {
                SpareCount = 0,
                SpareCost = 0,
                SpareDetails = new { }
            }; 

            var allMotors = _motorRepository.GetEntities(e => e.ProductionLineId.Equals(productionLineId))?.ToList();
            if (allMotors == null||!allMotors.Any()) return new
            {
                SpareCount = 0,
                SpareCost = 0,
                SpareDetails = new { }
            }; 
            var allMotorIds = allMotors.Select(e => e.MotorId);
                var endTime = startT.Time().Date.AddDays(1).AddMilliseconds(-1).TimeSpan();                
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
                                    FirstOrDefault()?.Name??"";
                    var spareName= _sparePartsTypeRepository.GetEntities(e => e.Id==s.SparePartsTypeId)?.
                                    FirstOrDefault()?.Name ?? "";
                    spareDetails.Add(new { MotorName =motorName,SpareName=spareName,Start=s.OutTime.Time(),End= s.UselessTime.Time() });
                });
                return new
                {
                    SpareCount = spareCount,
                    SpareCost = spareCount,
                    SpareDetails = spareDetails
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
            List<PowerCal> powers;
            float totalPower = 0, avgPower=0;
            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((startT == endT) && (startT == now))
            {
                powers = _productionLineRepository.CalcMotorPowers(allMotors);
                totalPower = (float)Math.Round(powers.Sum(e =>e.ActivePower),2);
                avgPower= totalPower;
                return new { TotalPower=totalPower,AvgPower=avgPower,Powers=powers};
            }
            //历史某一天
            if (startT == endT)
            {
                powers = _productionLineRepository.CalcMotorPowers(allMotors,startT);         
                totalPower = (float)Math.Round(powers.Sum(e => e.ActivePower), 2);
                avgPower = totalPower;
                return new { TotalPower = totalPower, AvgPower = avgPower, Powers = powers };
            }
            //历史区间段
            var endTime = endT.Time().Date.AddDays(1).TimeSpan();//.AddMilliseconds(-1).TimeSpan();
            var times =end.Subtract(start).TotalDays+1;
            powers = _productionLineRepository.CalcMotorPowers(allMotors,startT,endTime);
            totalPower = (float)Math.Round(powers.Sum(e =>e.ActivePower), 2);
            avgPower = totalPower!=0?(float)Math.Round(totalPower/ times,2):0;
            return new { TotalPower = totalPower, AvgPower = avgPower, Powers = powers };
        }

        #endregion
    }
}
