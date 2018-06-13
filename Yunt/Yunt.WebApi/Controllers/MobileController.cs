﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    //[Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
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
        [HttpPost("[action]")]
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

        [HttpPost("[action]")]
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

        [HttpPost("[action]")]
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
        [HttpPost("[action]")]
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
        [HttpPost("[action]")]
        public MotorChartDataModel MotorDetail([FromBody]RequestModel value)
        {
            var resData = new MotorChartDataModel();

            #region bus

            //try
            //{
            //    var motor =
            //     _motorRepository.GetEntities(e => e.MotorId.Equals(value.motorId))?.FirstOrDefault();
            //    if (motor == null) return resData;
            //    var end = value.startDatetime.ToDateTime();
            //    var start = value.endDatetime.ToDateTime();
            //    //当日数据（24hours）
            //    if (start == end)
            //    {
            //        var data = _conveyorByHourRepository.GetRealData(motor.MotorId);
            //        return new MotorChartDataModel
            //        {
            //            Output = data.AccumulativeWeight,
            //            InstantOutput = data.AvgInstantWeight,
            //            Load = data.LoadStall,
            //            RunningTime = data.RunningTime
            //        };
            //    }
            //    //历史数据(阶段日期单位作为x轴)

            //    var datas = _conveyorByDayRepository.GetEntities(e => e.Time.CompareTo(start) >= 0 &&
            //                                                          e.Time.CompareTo(end) <= 0 && e.MotorId.Equals(motor.MotorId))?.ToList();
            //    if (datas == null || !datas.Any()) return resData;
            //}
            //catch (Exception e)
            //{
            //    Logger.Exception(e);
            //    return resData;
            //}


            #endregion
            //Business


            //输入
            //int value.motorId
            //DateTime DateTimeHelper(value.startDatetime)
            //DateTime DateTimeHelper(value.endDatetime)

            //业务
            //返回固定设备的今日或历史数据
            //传入的开始时间==结束时间 也返回今日统计数据，最多24条，categories与data长度一致
            //传入的开始时间!=结束时间 返回范围内历史数据
            //注意返回的数据结构已确定

            //{
            //    "outline": {
            //        "output": 28470.1,
            //        "runningTime": 33.8,
            //        "load": 105.29,
            //        "instantOutput": 0
            //    },
            //    "xAxis": {
            //                "categories": [
            //                    "2018-4-1",
            //                    "2018-4-2"
            //                ]
            //    },
            //    "outputSeries": [
            //        {
            //                    "name": "主皮带",
            //                    "data": [
            //                        11,
            //                        17
            //                    ]
            //        }
            //    ],
            //    "runningtimeSeries": [
            //        {
            //            "name": "主皮带",
            //            "data": [
            //                11,
            //                17
            //            ]
            //        }
            //    ]
            //}

            //Eg.
            //
            ConveyorOutlineModel outline = new ConveyorOutlineModel { Output = 28470.1, RunningTime = 33.8, Load = 105.29, InstantOutput = 0 };

            //
            xAxisModel xAxis = new xAxisModel();
            List<string> categories = new List<string>();

            for (int i = 1; i < 16; i++)
            {
                string str = "2018-4-" + i;
                categories.Add(str);
            }
            xAxis.categories = categories;

            //
            List<seriesModel> serieses = new List<seriesModel>();
            seriesModel series = new seriesModel();
            List<float> data = new List<float>();

            for (int i = 1; i < 16; i++)
            {
                Random random = new Random();
                int _value = random.Next(10, 20);
                data.Add(_value);
            }
            series.name = "主皮带";
            series.data = data;

            serieses.Add(series);

            //
            List<seriesModel> rtserieses = new List<seriesModel>();
            seriesModel rtseries = new seriesModel();
            List<double> rtdata = new List<double>();

            for (int i = 1; i < 16; i++)
            {
                Random random = new Random();
                int _value = random.Next(10, 20);
                rtdata.Add(_value);
            }
            rtseries.name = "主皮带";
            rtseries.data = data;

            rtserieses.Add(rtseries);

            //
            resData.outline = outline;
            resData.xAxis = xAxis;
            resData.outputSeries = serieses;
            resData.runningtimeSeries = rtserieses;
            //End Business



            return resData;

        }

        #endregion


        #region Event 
        [HttpPost("[action]")]
        public List<EventModel> EventsDetail([FromBody]RequestModel value)
        {
            var resData = new List<EventModel>();

            #region bus

            try
            {
                var lineId = value.lineId;
                var end = value.startDatetime.ToDateTime().TimeSpan();
                var start = value.endDatetime.ToDateTime().TimeSpan();
                //当日数据
                if (start == end)
                {
                    var datas = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(lineId) &&
                                                                          e.Time >= start && e.Time <= end);
                    if (datas == null || !datas.Any())
                        return resData;
                    Parallel.ForEach(datas, d =>
                    {
                        resData.Add(new EventModel()
                        {
                            time = d.Time.ToString(),
                            //title = "Archery Training",
                            motorname = d.MotorName,
                            desc = d.Description,
                            //isalarm = true,
                            lineColor = "darkturquoise",
                            circleSize = 20,
                            circleColor = "darkturquoise"
                        });
                    });
                    return resData;
                }
                var list = _motorEventLogRepository.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(lineId) &&
                                                                          e.Time >= start && e.Time <= end);
                if (list == null || !list.Any())
                    return resData;
                Parallel.ForEach(list, d =>
                {
                    resData.Add(new EventModel()
                    {
                        time = d.Time.ToString(),
                        //title = "Archery Training",
                        motorname = d.MotorName,
                        desc = d.Description,
                        //isalarm = true,
                        lineColor = "darkturquoise",
                        circleSize = 20,
                        circleColor = "darkturquoise"
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
