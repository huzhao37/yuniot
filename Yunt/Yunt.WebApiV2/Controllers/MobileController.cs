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
using Yunt.WebApiV2.Models.Mobile;
using Yunt.WebApiV2.Models.ProductionLines;


namespace Yunt.WebApiV2.Controllers
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
        private readonly IAlarmInfoRepository _alarmInfoRepository;
        public MobileController(IConveyorByHourRepository conveyorByHourRepository
            , IConveyorByDayRepository conveyorByDayRepository,
            IMotorRepository motorRepository,
            IProductionLineRepository productionLineRepository,
            IMotorEventLogRepository motorEventLogRepository,
            IAlarmInfoRepository alarmInfoRepository)
        {
            _conveyorByHourRepository = conveyorByHourRepository;
            _conveyorByDayRepository = conveyorByDayRepository;
            _motorRepository = motorRepository;
            _productionLineRepository = productionLineRepository;
            _motorEventLogRepository = motorEventLogRepository;
            _alarmInfoRepository = alarmInfoRepository;
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
                var  start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long startT = start.TimeSpan(), endT = end.TimeSpan();
                //当日数据
                if (startT == endT)
                {
                    var data = _conveyorByHourRepository.GetRealData(motor);
                    return new ConveyorOutlineModel
                    {
                        Output =(float)Math.Round(data.AccumulativeWeight,2),
                        InstantOutput = data.AvgInstantWeight,
                        Load = (float)Math.Round(data.LoadStall,3),
                        RunningTime = data.RunningTime
                    };
                }
                //历史数据

                var datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                                                                      e.Time<= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                if (datas == null || !datas.Any()) return resData;
                return new ConveyorOutlineModel
                {
                    Output = (float)Math.Round(datas.Sum(e => e.AccumulativeWeight),2),
                    InstantOutput = (float)Math.Round(datas.Average(e => e.AvgInstantWeight),2),
                    Load =(float)Math.Round(datas.Average(e => e.LoadStall),3),
                    RunningTime = (float)Math.Round(datas.Average(e => e.RunningTime),2)
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
                var  start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long startT = start.TimeSpan(), endT = end.TimeSpan();
                //当日数据
                if (startT == endT)
                {
                    Parallel.ForEach(motors, body: delegate (Motor motor)
                    {
                        var data = _conveyorByHourRepository.GetRealData(motor);
                        resData.Add(new ConveyorChartModel
                        {
                            y = (float)Math.Round(data.AccumulativeWeight,2),
                            InstantWeight = data.AvgInstantWeight,
                            MotorLoad = (float)Math.Round(data.LoadStall,3),
                            RunningTime = data.RunningTime,
                            name = motor.Name
                        });
                    });
                    return resData;
                }

                Parallel.ForEach(motors, motor =>
                {
                    //历史数据                 
                    var datas = _conveyorByDayRepository.GetEntities(e => e.Time>= startT &&
                                    e.Time <= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                    if (datas != null && datas.Any())
                    {
                        resData.Add(new ConveyorChartModel
                        {
                            y = (float)Math.Round(datas.Sum(e => e.AccumulativeWeight),2),
                            InstantWeight = (float)Math.Round(datas.Average(e => e.AvgInstantWeight),2),
                            MotorLoad = (float)Math.Round(datas.Average(e => e.LoadStall),3),
                            RunningTime = (float)Math.Round(datas.Average(e => e.RunningTime),2),
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
                var  start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long endTime = end.TimeSpan(), startTime = start.TimeSpan();
                //最近15日数据
                if (startTime == endTime)
                {
                    endTime = DateTime.Now.Date.TimeSpan();
                    startTime = DateTime.Now.Date.AddDays(-15).TimeSpan();
                }
                //历史数据
                var datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startTime &&
                               e.Time < endTime && e.MotorId.Equals(motor.MotorId), e => e.Time)?.ToList();
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
                Parallel.ForEach(motors,motor =>
                {
                    var data = _productionLineRepository.MotorDetails(motor);//_conveyorByHourRepository.GetRealData(motor);
                    if(data==null)
                        resData.Add(new MotorItemModel()
                        {
                            id = motor.MotorId,
                            load = 0,
                            name = motor.Name,
                            runningtime = 0,
                            type = motor.MotorTypeId,
                            status = _productionLineRepository.GetMotorStatusByMotorId(motor.MotorId).Equals(MotorStatus.Run)
                        });
                    else
                        resData.Add(new MotorItemModel()
                        {
                            id = motor.MotorId,
                            load = (float)Math.Round(data.LoadStall,3),
                            name = motor.Name,
                            runningtime = (float)data.RunningTime,
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
        //public dynamic MotorDetail([FromBody]RequestModel value)
        {
            //var resData = new MotorChartDataModel();
            ////long startT = start.TimeSpan(), endT = end.TimeSpan();
            //long  startT = value.startDatetime.ToDateTime().TimeSpan();
            //long endT = value.endDatetime.ToDateTime().TimeSpan();
            
            //var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(value.motorId))?.FirstOrDefault();
            //if (motor == null) return resData;
            //List<dynamic> datas;
            ////当天
            //var now = DateTime.Now.Date.TimeSpan();
            //if ((startT == endT) && (startT == now))
            //{
            //    datas = _productionLineRepository.MotorHours(motor)?.OrderBy(e => (long)e.Time)?.ToList();
            //    return _productionLineRepository.GetMobileMotorDetails(datas, motor);
            //}
            ////历史某一天
            //if (startT == endT)
            //{
            //    datas = _productionLineRepository.MotorHours(motor, startT)?.OrderBy(e => (long)e.Time)?.ToList();
            //    return _productionLineRepository.GetMobileMotorDetails(datas, motor);
            //}
            //datas = _productionLineRepository.MotorDays(startT, endT, motor)?.OrderBy(e => (long)e.Time)?.ToList();
            //return _productionLineRepository.GetMobileMotorDetails(datas, motor);
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
                var  start = value.startDatetime.ToDateTime().TimeSpan();
                var end = value.endDatetime.ToDateTime().TimeSpan();

                var eventLogs = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.Equals(lineId) &&
                                                                      e.Time >= start && e.Time <= end);

                var alarms = _alarmInfoRepository.GetEntities(e => e.Time >= start && e.Time <= end)?.ToList();

                if ((eventLogs == null || !eventLogs.Any())&&(alarms == null || !alarms.Any()))
                    return resData;
                if (eventLogs != null && eventLogs.Any())
                    Parallel.ForEach(eventLogs, e =>
                    {
                        resData.Add(new EventModel
                        {
                            time = e.Time.ToString(),
                            motorname = e.MotorName,
                            desc = e.Description,
                            eventType = "Event"
                        });
                    });
                if (alarms != null && alarms.Any())
                    Parallel.ForEach(alarms, a =>
                    {
                        resData.Add(new EventModel
                        {
                            time = a.Time.ToString(),
                            motorname = a.MotorName,
                            desc = a.Content,
                            eventType = "Alarm"
                        });
                    });
                return resData?.OrderByDescending(e=>e.time)?.ToList();
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

