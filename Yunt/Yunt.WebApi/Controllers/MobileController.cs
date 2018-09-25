using System;
using System.Collections;
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
                var start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long startT = start.TimeSpan(), endT = end.TimeSpan();
                //同一班次
                if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour)||(start==end&&start.Hour==DateTime.Now.Hour)
                     || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
                {
                    //今班次
                    if ((start == end && start.Hour == DateTime.Now.Hour))
                    {
                        var data = _conveyorByHourRepository.GetShiftRealData(motor, Startup.ShiftStartHour);
                        return new ConveyorOutlineModel
                        {
                            Output = data?.AccumulativeWeight ?? 0f,
                            InstantOutput = data?.AvgInstantWeight ?? 0f,
                            Load = data?.LoadStall ?? 0f,
                            RunningTime = data?.RunningTime ?? 0f
                        };
                    }
                    //历史某一班次
                    else
                    {
                        var data = _conveyorByHourRepository.GetHistoryShiftOneData(motor, startT, endT);
                        return new ConveyorOutlineModel
                        {
                            Output = data?.AccumulativeWeight ?? 0f,
                            InstantOutput = data?.AvgInstantWeight ?? 0f,
                            Load = data?.LoadStall ?? 0f,
                            RunningTime = data?.RunningTime ?? 0f
                        };
                    }
                }
                //历史2班次以上
                else
                {
                    var datas = _conveyorByHourRepository.GetHistoryShiftSomeData(motor, startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour);
                    //var datas = _conveyorByHourRepository.GetEntities(e => e.Time >= startT &&
                    //                  e.Time <= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                    var source = datas.Where(e => e.RunningTime > 0);
                    if(source==null||!source.Any())
                        return new ConveyorOutlineModel
                        {
                            Output = 0f,
                            InstantOutput = 0f,
                            Load = 0f,
                            RunningTime = 0f
                        };
                    return new ConveyorOutlineModel
                    {
                        Output = MathF.Round(datas?.Sum(e => e.AccumulativeWeight) ?? 0f, 2),
                        InstantOutput = MathF.Round(source?.Average(e => e.AvgInstantWeight) ?? 0f, 2),
                        Load = MathF.Round(source?.Average(e => e.LoadStall) ?? 0f, 3),
                        RunningTime = MathF.Round(datas?.Sum(e => e.RunningTime) ?? 0f, 2)
                    };
                }
                #region obselete
                ////当日数据
                //if ((startT == endT)&&(startT==DateTime.Now.Date.TimeSpan()))
                //{
                //    var data = _conveyorByHourRepository.GetRealData(motor);
                //    return new ConveyorOutlineModel
                //    {
                //        Output =MathF.Round(data.AccumulativeWeight,2),
                //        InstantOutput = data.AvgInstantWeight,
                //        Load =data.LoadStall,
                //        RunningTime = data.RunningTime
                //    };
                //}
                //var datas = new List<ConveyorByDay>();
                ////历史某一天
                //if (startT == endT)
                //{
                //    var endTime = end.Date.AddDays(1).TimeSpan();
                //    datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                //               e.Time < endTime && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                //}
                //else
                //{
                //    //历史数据
                //    datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                //               e.Time <= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                //}

                //if (datas == null || !datas.Any()) return resData;
                //var source = datas.Where(e => e.RunningTime > 0);
                //float instantOutPut = 0f, load = 0f;
                //if (source != null && source.Any())
                //{
                //    instantOutPut = MathF.Round(source.Average(e => e.AvgInstantWeight), 2);
                //    load = MathF.Round(source.Average(e => e.LoadStall), 3);
                //}
                //return new ConveyorOutlineModel
                //{
                //    Output = MathF.Round(datas.Sum(e => e.AccumulativeWeight),2),
                //    InstantOutput = instantOutPut,// MathF.Round(datas.Average(e => e.AvgInstantWeight),2),
                //    Load = load,//MathF.Round(datas.Average(e => e.LoadStall),3),
                //    RunningTime = MathF.Round(datas.Sum(e => e.RunningTime),2)
                //};
                #endregion
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
                       _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsBeltWeight)?.ToList();
                if (motors == null || !motors.Any()) return resData;
                var start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long startT = start.TimeSpan(), endT = end.TimeSpan();
                //同一班次
                if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour)|| (start == end && start.Hour == DateTime.Now.Hour)
                     || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
                {
                    //今班次
                    if ((start == end && start.Hour == DateTime.Now.Hour))
                    {
                        motors.ForEach(motor =>
                        {
                            var data = _conveyorByHourRepository.GetShiftRealData(motor, Startup.ShiftStartHour);
                            resData.Add(new ConveyorChartModel
                            {
                                y = data?.AccumulativeWeight ?? 0f,
                                InstantWeight = data?.AvgInstantWeight ?? 0f,
                                MotorLoad = data?.LoadStall ?? 0f,
                                RunningTime = data?.RunningTime ?? 0f,
                                name = motor.Name
                            });
                        });
                        return resData;
                    }
                    //历史某一班次
                    else
                    {
                        motors.ForEach(motor =>
                        {
                            var data = _conveyorByHourRepository.GetHistoryShiftOneData(motor, startT, endT);
                            if (data != null)
                            {
                                resData.Add(new ConveyorChartModel
                                {
                                    y = data?.AccumulativeWeight ?? 0f,
                                    InstantWeight = data?.AvgInstantWeight ?? 0f,
                                    MotorLoad = data?.LoadStall ?? 0f,
                                    RunningTime = data?.RunningTime ?? 0f,
                                    name = motor.Name
                                });
                            }

                        });
                        return resData;
                    }
                }
                //历史2班次以上
                else
                {
                    motors.ForEach(motor =>
                    {
                        var datas = _conveyorByHourRepository.GetEntities(e => e.Time >= startT &&
                                        e.Time <= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();                    
                        if (datas != null && datas.Any())
                        {
                            //var source = datas.Where(e => e.RunningTime > 0);
                            resData.Add(new ConveyorChartModel
                            {
                                y = MathF.Round(datas?.Sum(e => e.AccumulativeWeight) ?? 0f, 2),
                                InstantWeight = MathF.Round(datas?.Average(e => e.AvgInstantWeight) ?? 0f, 2),
                                MotorLoad = MathF.Round(datas?.Average(e => e.LoadStall) ?? 0f, 3),
                                RunningTime = MathF.Round(datas?.Average(e => e.RunningTime) ?? 0f, 2),
                                name = motor.Name
                            });
                        }                       
                    });
                    return resData;
                }

                #region obselete
                //当日数据
                //if ((startT == endT)&&(startT==DateTime.Now.Date.TimeSpan()))
                //{
                //    motors.ForEach( motor=>
                //    {
                //        var data = _conveyorByHourRepository.GetRealData(motor);
                //        resData.Add(new ConveyorChartModel
                //        {
                //            y = MathF.Round(data.AccumulativeWeight,2),
                //            InstantWeight = data.AvgInstantWeight,
                //            MotorLoad = data.LoadStall,
                //            RunningTime = data.RunningTime,
                //            name = motor.Name
                //        });
                //    });
                //    return resData;
                //}
                //var datas = new List<ConveyorByDay>();
                ////历史某一天
                //if (startT == endT)
                //{
                //    var endTime = end.Date.AddDays(1).TimeSpan();

                //    motors.ForEach(motor =>
                //    {
                //        datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                //                        e.Time < endTime && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                //        if (datas != null && datas.Any())
                //        {
                //            resData.Add(new ConveyorChartModel
                //            {
                //                y = MathF.Round(datas.Sum(e => e.AccumulativeWeight), 2),
                //                InstantWeight = MathF.Round(datas.Average(e => e.AvgInstantWeight), 2),
                //                MotorLoad = MathF.Round(datas.Average(e => e.LoadStall), 3),
                //                RunningTime = MathF.Round(datas.Average(e => e.RunningTime), 2),
                //                name = motor.Name
                //            });
                //        }

                //    });
                //    return resData;
                //}
                ////历史数据  
                //else
                //{
                //    motors.ForEach(motor =>
                //    {                              
                //        datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startT &&
                //                        e.Time <= endT && e.MotorId.Equals(motor.MotorId))?.OrderBy(e => e.Time)?.ToList();
                //        if (datas != null && datas.Any())
                //        {
                //            resData.Add(new ConveyorChartModel
                //            {
                //                y = MathF.Round(datas.Sum(e => e.AccumulativeWeight), 2),
                //                InstantWeight = MathF.Round(datas.Average(e => e.AvgInstantWeight), 2),
                //                MotorLoad = MathF.Round(datas.Average(e => e.LoadStall), 3),
                //                RunningTime = MathF.Round(datas.Average(e => e.RunningTime), 2),
                //                name = motor.Name
                //            });
                //        }

                //    });
                //    return resData;
                //}
                #endregion

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
        public List<ConveyorChartDataModel> RecentMainconveyorOuputs([FromBody]RequestModel value)
        {
            
            List<ConveyorChartDataModel> htdata = new List<ConveyorChartDataModel>();
            #region bus

            try
            {
                //var motor =
                //    _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsMainBeltWeight)?.FirstOrDefault();
                var motors =
                   _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.IsBeltWeight)?.ToList();
                if (motors == null) return htdata;
                var start = value.startDatetime.ToDateTime();
                var end = value.endDatetime.ToDateTime();
                long endTime = end.TimeSpan(), startTime = start.TimeSpan();
                //同一班次
                if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour)|| (start == end && start.Hour == DateTime.Now.Hour)
                     || ((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour || start.Hour < Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date == end.Date))
                {
                    //今班次
                    if (start == end && start.Hour == DateTime.Now.Hour)
                    {
                        return htdata;
                    }
                    //历史某一班次
                    else
                    {
                        return htdata;
                    }
                }
                //历史2班次以上
                else
                {
                    motors.ForEach(motor =>
                    {
                        var resData = new ConveyorChartDataModel();
                        var datas = _conveyorByHourRepository.GetHistoryShiftSomeData(motor, startTime, endTime, Startup.ShiftStartHour, Startup.ShiftEndHour)?.ToList();
                        //if (datas == null || !datas.Any()) return resData;
                        var xList = datas.Select(e => e.Time.Time().ToShortDateString());
                        resData.xAxis = new xAxisModel() { categories = xList };
                        var yList = datas.Select(e => e.AccumulativeWeight);
                        resData.series = new List<seriesModel>() { new seriesModel() { name = motor.Name, data = yList } };
                        htdata.Add(resData);
                    });
                    return htdata;
                }
                #region obselete
                ////最近15日数据
                //if ((startTime == endTime)&&(startTime==DateTime.Now.Date.TimeSpan()))
                //{
                //    endTime = DateTime.Now.Date.TimeSpan();
                //    startTime = DateTime.Now.Date.AddDays(-15).TimeSpan();
                //}
                ////历史某一天
                //else
                //{
                //    endTime = end.Date.AddDays(1).TimeSpan();
                //}
                ////历史数据          
                //var datas = _conveyorByDayRepository.GetEntities(e => e.Time >= startTime &&
                //               e.Time < endTime && e.MotorId.Equals(motor.MotorId), e => e.Time)?.ToList();
                //if (datas == null || !datas.Any()) return resData;
                //var xList = datas.Select(e => e.Time.Time().ToShortDateString());
                //resData.xAxis = new xAxisModel() { categories = xList };
                //var yList = datas.Select(e => e.AccumulativeWeight);
                //resData.series = new List<seriesModel>() { new seriesModel() { name = motor.Name, data = yList } };
                //return resData;

                #endregion
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return htdata;
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
                    _motorRepository.GetEntities(e => e.ProductionLineId.Equals(value.lineId) && e.MotorTypeId != "MF"
                        && e.MotorTypeId != "VB" && e.MotorId != "WDD-P001-CC000001" && e.MotorId != "WDD-P001-PUL000004")?.ToList();
                if (motors == null || !motors.Any()) return resData;
                motors.ForEach(motor =>
                {
                    if (motor.MotorTypeId == "CY" && !motor.IsBeltWeight)
                        return;
                    var data = _productionLineRepository.MotorShiftDetails(motor, Startup.ShiftStartHour);

                    #region obselete
                    //var data = _productionLineRepository.MotorDetails(motor);
                    #endregion

                    if (data == null)
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
                            load = data.LoadStall,
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
        public dynamic MotorDetail([FromBody]RequestModel value)
        {
            var resData = new MotorChartDataModel();
            var start = value.startDatetime.ToDateTime();
            var end = value.endDatetime.ToDateTime();
            long startT = start.TimeSpan();
            long endT = end.TimeSpan();

            var motor = _motorRepository.GetEntities(e => e.MotorId.Equals(value.motorId))?.FirstOrDefault();
            if (motor == null) return resData;
            List<dynamic> datas;
            //同一班次
            if ((end.Subtract(start).TotalHours <= 24 && start.Hour >= Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour)|| (start == end && start.Hour == DateTime.Now.Hour)
                ||((start.Hour >= Startup.ShiftStartHour && end.Hour >= Startup.ShiftEndHour|| start.Hour <Startup.ShiftStartHour && end.Hour < Startup.ShiftEndHour) && start.Date==end.Date))
            {
                //今班次
                if ((start == end && start.Hour == DateTime.Now.Hour))
                {
                    datas = _productionLineRepository.MotorShiftHours(motor,Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
                    return _productionLineRepository.GetMobileMotorDetails(datas, motor, true);
                }
                //历史某一班次
                else
                {
                    datas = _productionLineRepository.MotorShiftHours(motor, startT,endT, Startup.ShiftStartHour)?.OrderBy(e => (long)e.Time)?.ToList();
                    return _productionLineRepository.GetMobileMotorDetails(datas, motor, false);
                }
            }
            //历史2班次以上
            else
            {
                datas = _productionLineRepository.MotorShifts(motor,startT, endT, Startup.ShiftStartHour, Startup.ShiftEndHour)?.OrderBy(e => (long)e.Time)?.ToList();
                return _productionLineRepository.GetMobileMotorDetails(datas, motor, false);
            }

            #region obselete
            ////当天
            //var now = DateTime.Now.Date.TimeSpan();
            //if ((startT == endT) && (startT == now))
            //{
            //    datas = _productionLineRepository.MotorHours(motor)?.OrderBy(e => (long)e.Time)?.ToList();
            //    return _productionLineRepository.GetMobileMotorDetails(datas, motor, true);
            //}
            ////历史某一天
            //if (startT == endT)
            //{
            //    datas = _productionLineRepository.MotorHours(motor, startT)?.OrderBy(e => (long)e.Time)?.ToList();
            //    return _productionLineRepository.GetMobileMotorDetails(datas, motor, false);
            //}
            //datas = _productionLineRepository.MotorDays(startT, endT, motor)?.OrderBy(e => (long)e.Time)?.ToList();
            //return _productionLineRepository.GetMobileMotorDetails(datas, motor, false);
            #endregion
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
                var start = value.startDatetime.ToDateTime().TimeSpan();
                var end = value.endDatetime.ToDateTime().TimeSpan();

                var eventLogs = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.Equals(lineId) &&
                                                                      e.Time >= start && e.Time <= end)?.ToList();

                var alarms = _alarmInfoRepository.GetEntities(e => e.Time >= start && e.Time <= end)?.ToList();

                if ((eventLogs == null || !eventLogs.Any()) && (alarms == null || !alarms.Any()))
                    return resData;
                if (eventLogs != null && eventLogs.Any())
                    eventLogs.ForEach(e =>
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
                    alarms.ForEach(a =>
                    {
                        resData.Add(new EventModel
                        {
                            time = a.Time.ToString(),
                            motorname = a.MotorName,
                            desc = a.Content,
                            eventType = "Alarm"
                        });
                    });
                return resData?.OrderByDescending(e => e.time)?.ToList();
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

