using System;
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
            MotorChartDataModel resData = new MotorChartDataModel(); 
            switch(value.motorId)
            {
                case "1":
                resData = genCY();
                break;
                case "2":
                resData = genJC();
                break;
                case "3":
                resData = genIC();
                break;
                case "4":
                resData = genVB();
                break;
                case "5":
                resData = genHFV();
                break;
                case "6":
                resData = genSCC();
                break;
                case "7":
                resData = genVC();
                break;
                case "8":
                resData = genPUL();
                break;
                case "9":
                resData = genMF();
                break;
            }
            return resData;
        }
 
        public MotorChartDataModel genCY()
        {
            MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel { Output = 28470.1, RunningTime = 33.8, Load = 105.29, Current = 230.2};
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.outputSeries = genSeries("主皮带");
            resData.runningtimeSeries = genSeries("主皮带");
            resData.currentSeries = genSeries("主皮带");

            return resData;
        }
        public MotorChartDataModel genJC()
        {
            MotorChartDataModel resData = new MotorChartDataModel();  
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                MST = 105.29,
                MST2 = 105.29,
                RST = 105.29,
                RST2 = 105.29,
                VIB = 105.29,
                VIB2 = 105.29,
                WR = 105.29,
                WR2 = 105.29, 
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("颚破");
            resData.mstSeries = genSeries("颚破");
            resData.mst2Series = genSeries("颚破");
            resData.rstSeries = genSeries("颚破");
            resData.rst2Series = genSeries("颚破");
            resData.vibSeries = genSeries("颚破");
            resData.vib2Series = genSeries("颚破");
            resData.wrSeries = genSeries("颚破");
            resData.wr2Series = genSeries("颚破");
            resData.currentSeries = genSeries("颚破");

            return resData;
        }
        public MotorChartDataModel genIC()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                ST = 105.29,
                ST2 = 105.29,
                VIB = 105.29,
                VIB2 = 105.29,
                WR = 105.29,
                WR2 = 105.29, 
                Current = 230.2,
                Current2 = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("反击破");
            resData.stSeries = genSeries("反击破");
            resData.st2Series = genSeries("反击破");
            resData.vibSeries = genSeries("反击破");
            resData.vib2Series = genSeries("反击破");
            resData.wrSeries = genSeries("反击破");
            resData.wr2Series = genSeries("反击破");
            resData.currentSeries = genSeries("反击破");    
            resData.current2Series = genSeries("反击破");          
            return resData;
        }
        public MotorChartDataModel genVB()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                ST = 105.29,
                ST2 = 105.29,
                ST3 = 105.29,
                ST4 = 105.29,
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("振动筛");
            resData.stSeries = genSeries("振动筛");
            resData.st2Series = genSeries("振动筛");
            resData.st3Series = genSeries("振动筛");
            resData.st4Series = genSeries("振动筛");
            resData.currentSeries = genSeries("振动筛");  
            return resData;
        }
        public MotorChartDataModel genHFV()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                ST = 105.29,
                ST2 = 105.29,
                ST3 = 105.29,
                ST4 = 105.29,
                OFS = 105.29,
                ORS = 105.29,
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("高频筛");
            resData.stSeries = genSeries("高频筛");
            resData.st2Series = genSeries("高频筛");
            resData.st3Series = genSeries("高频筛");
            resData.st4Series = genSeries("高频筛");
            resData.ofsSeries = genSeries("高频筛");
            resData.orsSeries = genSeries("高频筛");
            resData.currentSeries = genSeries("高频筛");  
            return resData;
        }
        public MotorChartDataModel genSCC()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                TT = 105.29,
                OFT = 105.29,
                ORT = 105.29,
                STV = 105.29,
                MS = 105.29,
                VIB = 105.29,
                VIB2 = 105.29,
                WR = 105.29,
                WR2 = 105.29, 
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("圆锥破");
            resData.ttSeries = genSeries("圆锥破");
            resData.oftSeries = genSeries("圆锥破");
            resData.ortSeries = genSeries("圆锥破");
            resData.stvSeries = genSeries("圆锥破");
            resData.msSeries = genSeries("圆锥破");
            resData.vibSeries = genSeries("圆锥破");
            resData.vib2Series = genSeries("圆锥破");
            resData.wrSeries = genSeries("圆锥破");
            resData.wr2Series = genSeries("圆锥破");
            resData.currentSeries = genSeries("圆锥破");  
            return resData;
        }
        public MotorChartDataModel genVC()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                VIB = 105.29,
                VIB2 = 105.29,
                WR = 105.29,
                WR2 = 105.29, 
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("立轴破");
            resData.vibSeries = genSeries("立轴破");
            resData.vib2Series = genSeries("立轴破");
            resData.wrSeries = genSeries("立轴破");
            resData.wr2Series = genSeries("立轴破");
            resData.currentSeries = genSeries("立轴破");  
            return resData;
        }
        public MotorChartDataModel genPUL()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                VIB = 105.29,
                VIB2 = 105.29,
                WR = 105.29,
                WR2 = 105.29, 
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("磨粉机");
            resData.vibSeries = genSeries("磨粉机");
            resData.vib2Series = genSeries("磨粉机");
            resData.wrSeries = genSeries("磨粉机");
            resData.wr2Series = genSeries("磨粉机");
            resData.currentSeries = genSeries("磨粉机");   
            return resData;
        }
        public MotorChartDataModel genMF()
        {
             MotorChartDataModel resData = new MotorChartDataModel();
            OutlineModel outline = new OutlineModel 
            {
                RunningTime = 33.8, 
                Load = 105.29, 
                Frequency = 105.29,
                Current = 230.2
            };
 
            resData.outline = outline;
            resData.xAxis = genTime();
            resData.runningtimeSeries = genSeries("给料机");
            resData.freqSeries = genSeries("给料机");
            resData.currentSeries = genSeries("给料机");  
            return resData;
        }
        public xAxisModel genTime()
        {
            xAxisModel xAxis = new xAxisModel();
            List<string> categories = new List<string>();
 
            for (int i = 1; i < 16; i++)
            {
                string str = "2018-4-" + i;
                categories.Add(str);
            }
            xAxis.categories = categories;
            return xAxis;
        }

        public List<seriesModel> genSeries(string name)
        {
            List<seriesModel> serieses = new List<seriesModel>();
            seriesModel series = new seriesModel();
            List<double> data = new List<double>();
 
            for (int i = 1; i < 16; i++)
            {
                Random random = new Random();
                int _value = random.Next(10, 20);
                data.Add(_value);
            }
            series.name = name;
            series.data = data;
 
            serieses.Add(series);

            return serieses;
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
                            // lineColor = "darkturquoise",
                            // circleSize = 20,
                            // circleColor = "darkturquoise"
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
                        // lineColor = "darkturquoise",
                        // circleSize = 20,
                        // circleColor = "darkturquoise"
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







        #region ViewModels
        public class OutlineModel
        {
            public double RunningTime { get; set; }
            public double Load { get; set; }

            public double Output { get; set; }
            public double MST { get; set; }
            public double MST2 { get; set; }
            public double RST { get; set; }
            public double RST2 { get; set; }
            public double ST { get; set; }
            public double ST2 { get; set; }
            public double ST3 { get; set; }
            public double ST4 { get; set; } 
            public double TT { get; set; }
            public double OFT { get; set; }           
            public double ORT { get; set; }
            public double STV { get; set; }
            public double MS { get; set; } 
            public double OFS { get; set; }
            public double ORS { get; set; }
            public double VIB { get; set; }
            public double VIB2 { get; set; }
            public double WR { get; set; }
            public double WR2 { get; set; }
            public double Current {get;set;}
            public double Current2 {get;set;}
            public double Frequency {get;set;}


        }
        public class MotorChartDataModel
        {
            public OutlineModel outline { get; set; }
            public xAxisModel xAxis { get; set; }
            public List<seriesModel> outputSeries { get; set; }
            public List<seriesModel> runningtimeSeries { get; set; }
            public List<seriesModel> currentSeries { get; set; }
            public List<seriesModel> current2Series { get; set; }
            public List<seriesModel> freqSeries { get; set; }
            public List<seriesModel> vibSeries { get; set; }
            public List<seriesModel> vib2Series { get; set; }
            public List<seriesModel> wrSeries { get; set; }
            public List<seriesModel> wr2Series { get; set; }
            public List<seriesModel> mstSeries { get; set; }
            public List<seriesModel> mst2Series { get; set; }
            public List<seriesModel> rstSeries { get; set; }
            public List<seriesModel> rst2Series { get; set; }
            public List<seriesModel> ttSeries { get; set; }
            public List<seriesModel> oftSeries { get; set; }            
            public List<seriesModel> ortSeries { get; set; }
            public List<seriesModel> stSeries { get; set; }
            public List<seriesModel> st2Series { get; set; }
            public List<seriesModel> st3Series { get; set; }
            public List<seriesModel> st4Series { get; set; }
            public List<seriesModel> ofsSeries { get; set; }
            public List<seriesModel> orsSeries { get; set; }
            public List<seriesModel> stvSeries { get; set; }
            public List<seriesModel> msSeries { get; set; }            
        }
 
        #endregion




}

