using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.MQ;
using Yunt.WebApi.Models.ProductionLines;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;

namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductionLine")]
    [Authorize]
    public class ProductionLineController : Controller
    {

        private readonly IConveyorByHourRepository _conveyorByHourRepository;
        private readonly IConveyorByDayRepository _conveyorByDayRepository;
        private readonly IMotorRepository _motorRepository;
        private readonly IMotortypeRepository _motortypeRepository;
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IProductionPlansRepository _productionPlansRepository;
        private readonly IMessagequeueRepository _messagequeueRepository;
        public ProductionLineController(IConveyorByHourRepository conveyorByHourRepository
            , IConveyorByDayRepository conveyorByDayRepository,
            IMotorRepository motorRepository,
            IProductionLineRepository productionLineRepository,
             IMotortypeRepository motortypeRepository,
              IProductionPlansRepository productionPlansRepository,
              IMessagequeueRepository messagequeueRepository
            )
        {
            _conveyorByHourRepository = conveyorByHourRepository;
            _conveyorByDayRepository = conveyorByDayRepository;
            _motorRepository = motorRepository;
            _productionLineRepository = productionLineRepository;
            _motortypeRepository = motortypeRepository;
            _productionPlansRepository = productionPlansRepository;
            _messagequeueRepository = messagequeueRepository;
        }
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

        #region ProductionLine_Ex
        // GET: api/ProductionLine
        [HttpGet]
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
                RunMotors = tuple.Item3
            };
        }

        // GET: api/ProductionLine
        [HttpGet]
        [Route("MainConveyorReal")]
        public MainConveyorReal MainConveyorReal(string productionlineId)
        {
            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionlineId)&&e.IsMainBeltWeight)?.FirstOrDefault();
            //var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(lineId) && e.IsBeltWeight&&!e.IsMainBeltWeight).ToList();
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
        [HttpGet]
        public int AlarmMotors()
        {
            return 0;
        }

        // GET: api/ProductionLine
        [HttpGet]
        [Route("MainConveyorHistory")]
        public MainConveyorHository MainConveyorHistory(string productionlineId)
        {
            var resp = new MainConveyorHository();
          
            var motor = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionlineId) && e.IsMainBeltWeight)?.FirstOrDefault();
            
            if (motor == null) return resp;
            var finishCys = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionlineId) && e.IsBeltWeight&&!e.IsMainBeltWeight).ToList();
            var end = DateTime.Now.Date.TimeSpan();
            var start = DateTime.Now.Date.AddDays(-15).TimeSpan();
            var datas = _conveyorByDayRepository.GetEntities(e => e.Time>=start &&
                        e.Time<end && e.MotorId.Equals(motor.MotorId))?.ToList();
            if (datas==null||!datas.Any()) return resp;
            resp.AvgInstantWeight =(float)Math.Round(datas.Average(e => e.AvgInstantWeight),2);
            resp.AvgLoadStall = (float)Math.Round(datas.Average(e => e.LoadStall), 2);
            resp.RunningTime = (float)Math.Round(datas.Average(e => e.RunningTime), 2);
            resp.Output = (float)Math.Round(datas.Sum(e => e.AccumulativeWeight), 2);
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
                var list = _conveyorByDayRepository.GetEntities(e => e.Time >= start &&
                          e.Time < end && e.MotorId.Equals(cy.MotorId))?.ToList();
                float outPut = 0;
                if (list != null&& list.Any())
                {
                    outPut = (float)Math.Round(list.Sum(e => e.AccumulativeWeight), 2);
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
        [Route("Motors")]
        public IEnumerable<MotorSummary> Motors(string productionlineId)
        {
            var datas = new List<MotorSummary>();
            var motors = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionlineId))?.ToList();
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
        [Route("MotorDetails")]
        public dynamic MotorDetails(DateTime start,DateTime end,string motorId)
        {
            long startT = start.TimeSpan(), endT = end.TimeSpan();
            var motor = _motorRepository.GetEntities(e => e.MotorId.EqualIgnoreCase(motorId))?.FirstOrDefault();
            if (motor == null) return new CyDetail();
            List<dynamic> datas;
            //当天
            if (startT == endT)
            {
                datas = _productionLineRepository.MotorHours(motor).ToList();
                if (!datas?.Any() ?? true)
                    return new CyDetail();
                return _productionLineRepository.GetMotorDetails(datas, motor);

            }
            datas = _productionLineRepository.MotorDays(startT, endT, motor).ToList();
            if (!datas?.Any() ?? true)
                return new CyDetail();
            return _productionLineRepository.GetMotorDetails(datas, motor);
        }

        #endregion

        #region schedule


        [HttpPost]
        [Route("Plans")]
        public bool Schedule([FromBody]ProductionPlans plan)
        {
            var productionline =
                _productionLineRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(plan.ProductionlineId))?.FirstOrDefault();
            if (productionline == null)
                return false;
            //28B
            var bytes = Extention.CombomBinaryArray(Extention.TimeTobyte(plan.Start), Extention.TimeTobyte(plan.End));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy3));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy2));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.MainCy));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy1));
            bytes = Extention.CombomBinaryArray(bytes, Extention.IntToBytes4(plan.FinishCy4));

           var wddQueue =
      _messagequeueRepository.GetEntities(e => e.Route_Key.Equals("WUDDOUT")).FirstOrDefault();
            if (wddQueue == null)
                return false;
          
            var queueHost = wddQueue.Host;
            var queuePort = wddQueue.Port;
            var queueUserName = wddQueue.Username;
            var queuePassword = wddQueue.Pwd;

            var ccuri = "amqp://" + queueHost + ":" + queuePort;
            var queue = "WDDOUT";//wddQueue.Route_Key;
            var route = "WDDOUT"; //wddQueue.Route_Key; 
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
        
        public IEnumerable<dynamic> GetDateHistory( string motorId, DateTime dt,bool cache)
        {
            var datas=new List<dynamic>();
            var motor = _motorRepository.GetEntities(e => e.MotorId.EqualIgnoreCase(motorId))?.FirstOrDefault();
            if (motor == null)
                return datas;
            return _productionLineRepository.GetMotorHistoryByDate(motor, dt, cache);
        }


        #endregion
    }
}
