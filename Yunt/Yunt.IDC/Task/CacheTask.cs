using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Services;
using Yunt.Device.Repository.EF.Repositories;
using Yunt.IDC.Helper;
using Yunt.MQ;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;
using Yunt.Xml.Domain.Services;

namespace Yunt.IDC.Task
{
   /// <summary>
   /// 缓存任务
   /// </summary>
  public  class CacheTask
    {
        private static readonly IMessagequeueRepository MessagequeueRepository;
        private static readonly IBytesParseRepository BytesParseRepository;
        private static readonly IProductionLineRepository ProductionLineRepository;
        private static readonly IMotorRepository MotorRepository;
        static CacheTask()
       {
            MessagequeueRepository = ServiceProviderServiceExtensions.GetService<IMessagequeueRepository>(Program.Providers["Xml"]);
            BytesParseRepository = ServiceProviderServiceExtensions.GetService<IBytesParseRepository>(Program.Providers["Xml"]);
            ProductionLineRepository = ServiceProviderServiceExtensions.GetService<IProductionLineRepository>(Program.Providers["Device"]);
            MotorRepository = ServiceProviderServiceExtensions.GetService<IMotorRepository>(Program.Providers["Device"]);
        }
        /// <summary>
        /// 启动队列解析
        /// </summary>
        public static void Start()
        {
            #region 预热instancedata-3个月数据
            var motors = MotorRepository.GetEntities(e => e.ProductionLineId.Equals("WDD-P001"))?.ToList();
            DateTime start = "2018-06-20 00:00:00".ToDateTime(), end = "2018-07-20 0:0:00".ToDateTime();
            //var motors = MotorRepository.GetEntities(e => e.ProductionLineId.Equals("WDD-P001"))?.ToList();
            //if (motors?.Any() ?? true)
            //{
            //    motors.ForEach(e =>
            //    {
            //        var results = ProductionLineRepository.PreCache2(e, DateTime.Now.Date.TimeSpan(), start.TimeSpan(), end.TimeSpan());
            //        Logger.Info($"[{DateTime.Now.Date}]{e.MotorId}:初始化{results}个记录...");
            //    });
            //}

            var startT = start;
            var days = (int)end.Subtract(start).TotalDays+1;
            for (int i = 0; i < days; i++)
            {
                var time = startT.AddDays(i);
            
                if (motors?.Any() ?? true)
                {
                    motors.ForEach(e =>
                    {
                        var rel = ProductionLineRepository.DelCache(e, time);
                        if (rel > 0)
                        {
                            var results = ProductionLineRepository.PreCache(e, time);
                            Logger.Info($"[{time}]_{e.MotorId}:初始化{results}个记录...");
                        }

                    });
                }
            }
            Logger.Warn("缓存完毕！");
            Console.ReadKey();

            #endregion

        }
   

    }
}
