using System.Collections.Generic;

namespace Yunt.WebApi.Models.Mobile
{


    #region ViewModels

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
        public string lineId { get; set; }
        public string motorId { get; set; }
        public string startDatetime { get; set; }
        public string endDatetime { get; set; }

    }


    public class ConveyorChartDataModel
    {
        public ConveyorChartDataModel()
        {
            series = new List<seriesModel>();
        }
        public xAxisModel xAxis { get; set; }
        public IEnumerable<seriesModel> series { get; set; }

    }
    public class xAxisModel
    {
        public xAxisModel()
        {
            categories = new List<string>();
        }
        public IEnumerable<string> categories { get; set; }
    }
    public class seriesModel
    {
        public seriesModel()
        {
            data = new List<float>();
        }
        public string name { get; set; }
        public IEnumerable<float> data { get; set; }

    }


    public class MotorItemModel
    {
        public string id { get; set; }
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
    #endregion
}
