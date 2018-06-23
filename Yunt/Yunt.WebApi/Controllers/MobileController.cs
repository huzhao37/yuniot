using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Services;
using Yunt.WebApi.Models.Mobile;
using Yunt.WebApi.Models.ProductionLines;


namespace Yunt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Mobile")]
    [Authorize]
    public class MobileController : Controller
    {
        private readonly IConveyorByHourRepository _conveyorByHourRepository;
        private readonly IConveyorByDayRepository _conveyorByDayRepository;
        private readonly IMotorRepository _motorRepository;
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IMotorEventLogRepository _motorEventLogRepository;
        public MobileController(IConveyorByHourRepository conveyorByHourRepository
            , IConveyorByDayRepository conveyorByDayRepository,
            IMotorRepository motorRepository,
            IProductionLineRepository productionLineRepository,
            IMotorEventLogRepository motorEventLogRepository)
        {
            _conveyorByHourRepository = conveyorByHourRepository;
            _conveyorByDayRepository = conveyorByDayRepository;
            _motorRepository = motorRepository;
            _productionLineRepository = productionLineRepository;
            _motorEventLogRepository = motorEventLogRepository;
        }

        #region Productionline 
        [HttpPost]
        [EnableCors("any")]
        [Route("MainconveyorOutline")]
        public ConveyorOutlineModel MainconveyorOutline([FromBody]RequestModel value)
        {
            var resData = new ConveyorOutlineModel();

            #region bus
            try
            {
                var motor =
                    _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsMainBeltWeight)?.FirstOrDefault();
                if (motor == null) return resData;
                var end = value.startDatetime.ToDateTime();
                var start = value.endDatetime.ToDateTime();
                //当日数据
                if (start == end)
                {
                    var data = _conveyorByHourRepository.GetRealData(motor);
                    return new ConveyorOutlineModel
                    {
                        Output = data.AccumulativeWeight,
                        InstantOutput = data.AvgInstantWeight,
                        Load = data.LoadStall,
                        RunningTime = data.RunningTime
                    };
                }
                //历史数据

                var datas = _conveyorByDayRepository.GetEntities(e => e.Time.CompareTo(start) >= 0 &&
                                                                      e.Time.CompareTo(end) <= 0 && e.MotorId.Equals(motor.MotorId))?.ToList();
                if (datas == null || !datas.Any()) return resData;
                return new ConveyorOutlineModel
                {
                    Output = datas.Sum(e => e.AccumulativeWeight),
                    InstantOutput = datas.Average(e => e.AvgInstantWeight),
                    Load = datas.Average(e => e.LoadStall),
                    RunningTime = datas.Average(e => e.RunningTime)
                };
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return resData;
            }

            #endregion
        }

        [EnableCors("any")]
        [HttpPost]
        [Route("OutputconveyorsOutline")]
        public List<ConveyorChartModel> OutputconveyorsOutline([FromBody]RequestModel value)
        {
            var resData = new List<ConveyorChartModel>();

            #region bus
            try
            {
                var motors =
                       _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsBeltWeight
                       && !e.IsMainBeltWeight)?.ToList();
                if (motors == null || !motors.Any()) return resData;
                var end = value.startDatetime.ToDateTime();
                var start = value.endDatetime.ToDateTime();
                //当日数据
                if (start == end)
                {
                    Parallel.ForEach(motors, body: delegate (Motor motor)
                    {
                        var data = _conveyorByHourRepository.GetRealData(motor);
                        resData.Add(new ConveyorChartModel
                        {
                            y = data.AccumulativeWeight,
                            InstantWeight = data.AvgInstantWeight,
                            MotorLoad = data.LoadStall,
                            RunningTime = data.RunningTime,
                            name = motor.Name
                        });
                    });
                    return resData;
                }

                Parallel.ForEach(motors, motor =>
                {
                    //历史数据                 
                    var datas = _conveyorByDayRepository.GetEntities(e => e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) <= 0 && e.MotorId.Equals(motor.MotorId))?.ToList();
                    if (datas != null && datas.Any())
                    {
                        resData.Add(new ConveyorChartModel
                        {
                            y = datas.Sum(e => e.AccumulativeWeight),
                            InstantWeight = datas.Average(e => e.AvgInstantWeight),
                            MotorLoad = datas.Average(e => e.LoadStall),
                            RunningTime = datas.Average(e => e.RunningTime),
                            name = motor.Name
                        });
                    }

                });
                return resData;
            }
            catch (Exception e)
            {

                Logger.Exception(e);
                return resData;
            }

            #endregion
        }

        [EnableCors("any")]
        [HttpPost]
        [Route("RecentMainconveyorOuputs")]
        public ConveyorChartDataModel RecentMainconveyorOuputs([FromBody]RequestModel value)
        {
            var resData = new ConveyorChartDataModel();

            #region bus

            try
            {
                var motor =
                    _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsMainBeltWeight)?.FirstOrDefault();
                if (motor == null) return resData;
                var end = value.startDatetime.ToDateTime();
                var start = value.endDatetime.ToDateTime();
                long endTime = end.TimeSpan(), startTime = start.TimeSpan();
                //最近15日数据
                if (startTime == endTime)
                {
                    endTime = DateTime.Now.Date.TimeSpan();
                    startTime = DateTime.Now.Date.AddDays(-15).TimeSpan();
                }
                //历史数据
                var datas = _conveyorByDayRepository.GetEntities(e => e.Time.CompareTo(startTime) >= 0 &&
                               e.Time.CompareTo(endTime) < 0 && e.MotorId.Equals(motor.MotorId), e => e.Time)?.ToList();
                if (datas == null || !datas.Any()) return resData;
                var xList = datas.Select(e => e.Time.Time().ToShortDateString());
                resData.xAxis = new xAxisModel() { categories = xList };
                var yList = datas.Select(e => e.AccumulativeWeight);
                resData.series = new List<seriesModel>() { new seriesModel() { name = motor.Name, data = yList } };
                return resData;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return resData;
            }

            #endregion
        }
        #endregion

        #region MotorList 
        [EnableCors("any")]
        [HttpPost]
        [Route("MotorsOutline")]
        public List<MotorItemModel> MotorsOutline([FromBody]RequestModel value)
        {
            var resData = new List<MotorItemModel>();

            #region bus
            try
            {
                var motors =
                    _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId))?.ToList();
                if (motors == null || !motors.Any()) return resData;
                Parallel.ForEach(motors, motor =>
                {
                    var data = _conveyorByHourRepository.GetRealData(motor);
                    resData.Add(new MotorItemModel()
                    {
                        id = motor.MotorId,
                        load = data.LoadStall,
                        name = motor.Name,
                        runningtime = data.RunningTime,
                        type = motor.MotorTypeId,
                        status = _productionLineRepository.GetMotorStatusByMotorId(motor.MotorId).Equals(MotorStatus.Run)
                    });
                });
                return resData;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return resData;
            }

            #endregion

        }
        #endregion

        #region Motor 
        [EnableCors("any")]
        [Route("MotorDetails")]
        [HttpPost]
        public dynamic MotorDetail([FromBody]RequestModel value)
        {
            var resData = new MotorChartDataModel();
            //long startT = start.TimeSpan(), endT = end.TimeSpan();
            long endT = value.startDatetime.ToDateTime().TimeSpan();
            long startT = value.endDatetime.ToDateTime().TimeSpan();
            
            var motor = _motorRepository.GetEntities(e => e.MotorId.EqualIgnoreCase(value.motorId))?.FirstOrDefault();
            if (motor == null) return resData;
            List<dynamic> datas;
            //当天
            var now = DateTime.Now.Date.TimeSpan();
            if ((startT == endT) && (startT == now))
            {
                datas = _productionLineRepository.MotorHours(motor)?.ToList();
                return _productionLineRepository.GetMobileMotorDetails(datas, motor);
            }
            //历史某一天
            if (startT == endT)
            {
                datas = _productionLineRepository.MotorHours(motor, startT)?.ToList();
                return _productionLineRepository.GetMobileMotorDetails(datas, motor);
            }
            datas = _productionLineRepository.MotorDays(startT, endT, motor)?.ToList();
            return _productionLineRepository.GetMobileMotorDetails(datas, motor);
        }

        #endregion

        #region Event 
        [EnableCors("any")]
        [HttpPost]
        [Route("EventsDetail")]
        public List<EventModel> EventsDetail([FromBody]RequestModel value)
        {
            var resData = new List<EventModel>();

            #region bus

            try
            {
                var lineId = value.lineId;
                var end = value.startDatetime.ToDateTime().TimeSpan();
                var start = value.endDatetime.ToDateTime().TimeSpan();

                var datas = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(lineId) &&
                                                                      e.Time >= start && e.Time <= end);
                if (datas == null || !datas.Any())
                    return resData;
                Parallel.ForEach(datas, d =>
                {
                    resData.Add(new EventModel()
                    {
                        time = d.Time.ToString(),
                        motorname = d.MotorName,
                        desc = d.Description,
                    });
                });
                return resData;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return resData;
            }


            #endregion

        }
        #endregion

    }

}

