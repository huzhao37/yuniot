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
    //[Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class MobileController : Controller
    {
        #region Productionline 
        [HttpPost("[action]")]
        public ConveyorOutlineModel MainconveyorOutline([FromBody]RequestModel value)
        {
            ConveyorOutlineModel resData = new ConveyorOutlineModel { Output = 28470.1, RunningTime = 33.8, Load = 105.29, InstantOutput = 0 };
            return resData;
        }

        [HttpPost("[action]")]
        public List<ConveyorChartModel> OutputconveyorsOutline([FromBody]RequestModel value)
        {
            ConveyorChartModel res1 = new ConveyorChartModel { name = "C22(5-15mm)", y = 400, RunningTime = 240, InstantWeight = 40, MotorLoad = 50 };
            ConveyorChartModel res2 = new ConveyorChartModel { name = "C29(石粉)", y = 300, RunningTime = 180, InstantWeight = 40, MotorLoad = 60 };
            ConveyorChartModel res3 = new ConveyorChartModel { name = "C25(15-25mm)", y = 200, RunningTime = 120, InstantWeight = 40, MotorLoad = 70 };
            ConveyorChartModel res4 = new ConveyorChartModel { name = "C15(25-32mm)", y = 100, RunningTime = 60, InstantWeight = 40, MotorLoad = 80 };

            List<ConveyorChartModel> resData = new List<ConveyorChartModel>();
            resData.Add(res1);
            resData.Add(res2);
            resData.Add(res3);
            resData.Add(res4);

            return resData;
        }

        [HttpPost("[action]")]
        public ConveyorChartDataModel RecentMainconveyorOuputs([FromBody]RequestModel value)
        {
            ConveyorChartDataModel resData = new ConveyorChartDataModel();

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
            List<double> data = new List<double>();

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
            resData.xAxis = xAxis;
            resData.series = serieses;
            return resData;
        }
        #endregion


        #region MotorList 
        [HttpPost("[action]")]
        public List<MotorItemModel> MotorsOutline([FromBody]RequestModel value)
        {
            List<MotorItemModel> resData = new List<MotorItemModel>();

            MotorItemModel motor1 = new MotorItemModel { id = 1083, name = "C7(主皮带)", load = 33.3, runningtime = 120, type = "cy", status = false };
            MotorItemModel motor2 = new MotorItemModel { id = 1014, name = "28号皮带机", load = 22.3, runningtime = 220, type = "cc", status = true };
            MotorItemModel motor3 = new MotorItemModel { id = 1015, name = "28号皮带机", load = 13.3, runningtime = 110, type = "jc", status = true };
            MotorItemModel motor4 = new MotorItemModel { id = 1016, name = "28号皮带机", load = 42.3, runningtime = 140, type = "mf", status = true };
            MotorItemModel motor5 = new MotorItemModel { id = 1017, name = "28号皮带机", load = 23.3, runningtime = 240, type = "vc", status = false };
            resData.Add(motor1);
            resData.Add(motor2);
            resData.Add(motor3);
            resData.Add(motor4);
            resData.Add(motor5);

            return resData;
        }

        #endregion


        #region Motor 
        [HttpPost("[action]")]
        public MotorChartDataModel MotorDetail([FromBody]RequestModel value)
        {
            MotorChartDataModel resData = new MotorChartDataModel();

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
            List<double> data = new List<double>();

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
            return resData;

        }

        #endregion


        #region Event 
        [HttpPost("[action]")]
        public List<EventModel> EventsDetail([FromBody]RequestModel value)
        {
            List<EventModel> resData = new List<EventModel>();

            EventModel event1 = new EventModel { time = "2018-03-01 09:00", title = "Archery Training", motorname = "C7 主皮带", desc = "皮带机启动", status = "已处理", suspend = true, isalarm = false, lineColor = "darkturquoise", circleSize = 20, circleColor = "darkturquoise" };
            EventModel event2 = new EventModel { time = "2018-03-01 10:45", title = "Archery Training", motorname = "振动筛", desc = "给料电机故障", status = "已处理", suspend = true, isalarm = false, lineColor = "darkturquoise", circleSize = 20, circleColor = "darkturquoise" };
            EventModel event3 = new EventModel { time = "2018-03-01 19:00", title = "Archery Training", motorname = "C27皮带", desc = "皮带机停止", status = "未处理", suspend = false, isalarm = true, lineColor = "orange", circleSize = 20, circleColor = "orange" };
            EventModel event4 = new EventModel { time = "2018-03-01 15:00", title = "Archery Training", motorname = "颚破1", desc = "处理机启动", status = "未处理", suspend = false, isalarm = true, lineColor = "orange", circleSize = 20, circleColor = "orange" };
            EventModel event5 = new EventModel { time = "2018-03-01 12:00", title = "Archery Training", motorname = "C7 主皮带", desc = "皮带机启动", status = "已处理", suspend = true, isalarm = true, lineColor = "orange", circleSize = 20, circleColor = "orange" };

            resData.Add(event1);
            resData.Add(event2);
            resData.Add(event3);
            resData.Add(event4);
            resData.Add(event5);

            return resData;
        }
        #endregion



        public class ConveyorOutlineModel
        {
            public double Output { get; set; }
            public double RunningTime { get; set; }
            public double Load { get; set; }
            public double InstantOutput { get; set; }

        }
        public class ConveyorChartModel
        {
            public string name { get; set; }
            public double y { get; set; }
            public double RunningTime { get; set; }
            public double InstantWeight { get; set; }
            public double MotorLoad { get; set; }

        }
        public class RequestModel
        {
            public int motorId { get; set; }
            public int lineId { get; set; }
            public string startDatetime { get; set; }
            public string endDatetime { get; set; }

        }


        public class ConveyorChartDataModel
        {
            public xAxisModel xAxis { get; set; }
            public List<seriesModel> series { get; set; }

        }
        public class xAxisModel
        {
            public List<string> categories { get; set; }
        }
        public class seriesModel
        {
            public string name { get; set; }
            public List<double> data { get; set; }

        }


        public class MotorItemModel
        {
            public int id { get; set; }
            public double runningtime { get; set; }
            public string name { get; set; }
            public double load { get; set; }
            public string type { get; set; }
            public bool status { get; set; }
        }




        public class MotorChartDataModel
        {
            public ConveyorOutlineModel outline { get; set; }
            public xAxisModel xAxis { get; set; }
            public List<seriesModel> outputSeries { get; set; }
            public List<seriesModel> runningtimeSeries { get; set; }

        }


        public class EventModel
        {
            public string time { get; set; }
            public string title { get; set; }
            public string motorname { get; set; }
            public string desc { get; set; }
            public string status { get; set; }
            public bool suspend { get; set; }
            public bool isalarm { get; set; }
            public string lineColor { get; set; }
            public int circleSize { get; set; }
            public string circleColor { get; set; }
        }


    }
}

