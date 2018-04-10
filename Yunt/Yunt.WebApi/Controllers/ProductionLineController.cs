using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.WebApi.Models.ProductionLines;

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
        private readonly IProductionLineRepository _productionLineRepository;
        public ProductionLineController(IConveyorByHourRepository conveyorByHourRepository
            , IConveyorByDayRepository conveyorByDayRepository,
            IMotorRepository motorRepository,
            IProductionLineRepository productionLineRepository)
        {
            _conveyorByHourRepository = conveyorByHourRepository;
            _conveyorByDayRepository = conveyorByDayRepository;
            _motorRepository = motorRepository;
            _productionLineRepository = productionLineRepository;
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
        public MainConveyorReal MainConveyorReal(string motorId)
        {
            // var motorId= _motorRepository.GetEntities(e=>e.ProductionLineId.Equals(productionlineId)&&e.MotorId).SingleOrDefault().MotorId
            var data = _conveyorByHourRepository.GetRealData(motorId);
            return new MainConveyorReal
            {
                AccumulativeWeight = data.AccumulativeWeight,
                InstantWeight = data.AvgInstantWeight,
                LoadStall = data.LoadStall,
                RunningTime = data.RunningTime
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
        public IEnumerable<MainConveyorHository> MainConveyorHistory(string motorId)
        {
            var resp = new List<MainConveyorHository>();
            var end = DateTime.Now.Date;
            var start = end.AddDays(-15);
            var datas = _conveyorByDayRepository.GetEntities(e => e.Time.CompareTo(start) >= 0 &&
                        e.Time.CompareTo(end) <= 0 && e.MotorId.Equals(motorId));
            if (!datas.Any()) return resp;
            foreach (var d in datas)
            {
                resp.Add(new MainConveyorHository()
                {
                    AccumulativeWeight = d.AccumulativeWeight,
                    Time = d.Time
                });
            }
            return resp;
        }

        // GET: api/ProductionLine
        [HttpGet]
        [Route("Motors")]
        public IEnumerable<MotorSummary> Motors(string productionlineId)
        {
            var datas = new List<MotorSummary>();
            var motors = _motorRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionlineId));
            if (!motors.Any()) return datas;
            foreach (var motor in motors)
            {
                var status = _productionLineRepository.GetMotorStatusByMotorId(motor.MotorId);
                datas.Add(new MotorSummary()
                {
                    MotorId = motor.MotorId,
                    MotorStatus = status,
                    MotorTypeId = motor.MotorTypeId,
                    Name = motor.Name
                });
            }
            return datas;
        }

        #endregion

        #region motor_ex

        // GET: api/ProductionLine
        [HttpGet]
        [Route("MotorDetails")]
        public IEnumerable<dynamic> MotorDetails(DateTime start,DateTime end,string motorId)
        {
            return _productionLineRepository.MotorDetails(start, end, motorId);
        }

        #endregion
    }
}
