using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.ProductionLines
{
    /// <summary>
    /// 成品皮带历史数据
    /// </summary>
    public class MainSeriesData
    {
        public MainSeriesData()
        {
            SeriesDatas = new List<SeriesDatas>();
        }
        public string MotorID { set; get; }
        public string MotorName { get; set; }
        /// <summary>
        /// 总运行时间
        /// </summary>
        public float SumRunningTime { get; set; }

        /// <summary>
        /// 总产量
        /// </summary>
        public float SumOutPut { get; set; }

        /// <summary>
        /// 每吨耗电量
        /// </summary>
        public float AvgActivePower { get; set; }
        public List<SeriesDatas> SeriesDatas { get; set; }
        

    }
  


    public class SeriesDatas
    {
        /// <summary>
        /// 时间
        /// </summary>
        public long UnixTime { get; set; }

        /// <summary>
        /// 产量
        /// </summary>
        public float Output { get; set; }
    }
    public class Report
    {
        public Report()
        {
            Row = new List<Row>();
            Detail = new List<Detail>();
        }
        public List<Detail> Detail { get; set; }
        public List<Row> Row { get; set; }
       
    }
    public class Row
    {
        public Row()
        {
            Detail = new List<Detail>();
        }
        public List<Detail> Detail { get; set; }
        public DateTime Time { get; set; }
    }
    public class Detail
    {
        public float OutPut { get; set; }
        public string Name { get; set; }
    }
 

}
