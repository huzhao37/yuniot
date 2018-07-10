using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.MapModel;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Services;
using Yunt.Device.Repository.EF.Repositories;
using Yunt.Redis;

namespace Yunt.Device.Repository.EF.Services
{
   public class ProductionLineRepository : DeviceRepositoryBase<ProductionLine, Models.ProductionLine>, IProductionLineRepository
    {
        private readonly IMotorRepository _motorRep;
        private readonly IConveyorRepository _cyRep;
        private readonly IMaterialFeederRepository _mfRep;
        private readonly IJawCrusherRepository _jcRep;
        private readonly IConeCrusherRepository _ccRep;
        private readonly IVerticalCrusherRepository _vcRep;
        private readonly IVibrosieveRepository _vbRep;
        private readonly ISimonsConeCrusherRepository _sccRep;
        private readonly IPulverizerRepository _pulRep;
        private readonly IImpactCrusherRepository _icRep;
        private readonly IHVibRepository _hvbRep;
        
        private readonly IConveyorByHourRepository _cyByHourRep;
        private readonly IMaterialFeederByHourRepository _mfByHourRep;
        private readonly IJawCrusherByHourRepository _jcByHourRep;
        private readonly IConeCrusherByHourRepository _ccByHourRep;
        private readonly IVerticalCrusherByHourRepository _vcByHourRep;
        private readonly IVibrosieveByHourRepository _vbByHourRep;
        private readonly ISimonsConeCrusherByHourRepository _sccByHourRep;
        private readonly IPulverizerByHourRepository _pulByHourRep;
        private readonly IImpactCrusherByHourRepository _icByHourRep;
        private readonly IHVibByHourRepository _hvbByHourRep;

        private readonly IConveyorByDayRepository _cyByDayRep;
        private readonly IMaterialFeederByDayRepository _mfByDayRep;
        private readonly IJawCrusherByDayRepository _jcByDayRep;
        private readonly IConeCrusherByDayRepository _ccByDayRep;
        private readonly IVerticalCrusherByDayRepository _vcByDayRep;
        private readonly IVibrosieveByDayRepository _vbByDayRep;
        private readonly ISimonsConeCrusherByDayRepository _sccByDayRep;
        private readonly IPulverizerByDayRepository _pulByDayRep;
        private readonly IImpactCrusherByDayRepository _icByDayRep;
        private readonly IHVibByDayRepository _hvbByDayRep;
        public ProductionLineRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
            _cyRep = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(BootStrap.ServiceProvider);
            _mfRep= ServiceProviderServiceExtensions.GetService<IMaterialFeederRepository>(BootStrap.ServiceProvider);
            _jcRep = ServiceProviderServiceExtensions.GetService<IJawCrusherRepository>(BootStrap.ServiceProvider);
            _ccRep = ServiceProviderServiceExtensions.GetService<IConeCrusherRepository>(BootStrap.ServiceProvider);
            _vcRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherRepository>(BootStrap.ServiceProvider);
            _vbRep = ServiceProviderServiceExtensions.GetService<IVibrosieveRepository>(BootStrap.ServiceProvider);
            _sccRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherRepository>(BootStrap.ServiceProvider);
            _pulRep = ServiceProviderServiceExtensions.GetService<IPulverizerRepository>(BootStrap.ServiceProvider);
            _icRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherRepository>(BootStrap.ServiceProvider);
            _hvbRep = ServiceProviderServiceExtensions.GetService<IHVibRepository>(BootStrap.ServiceProvider);

            _cyByHourRep = ServiceProviderServiceExtensions.GetService<IConveyorByHourRepository>(BootStrap.ServiceProvider);
            _mfByHourRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederByHourRepository>(BootStrap.ServiceProvider);
            _jcByHourRep = ServiceProviderServiceExtensions.GetService<IJawCrusherByHourRepository>(BootStrap.ServiceProvider);
            _ccByHourRep = ServiceProviderServiceExtensions.GetService<IConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            _vcByHourRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByHourRepository>(BootStrap.ServiceProvider);
            _vbByHourRep = ServiceProviderServiceExtensions.GetService<IVibrosieveByHourRepository>(BootStrap.ServiceProvider);
            _sccByHourRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            _pulByHourRep = ServiceProviderServiceExtensions.GetService<IPulverizerByHourRepository>(BootStrap.ServiceProvider);
            _icByHourRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherByHourRepository>(BootStrap.ServiceProvider);
            _hvbByHourRep = ServiceProviderServiceExtensions.GetService<IHVibByHourRepository>(BootStrap.ServiceProvider);

            _cyByDayRep = ServiceProviderServiceExtensions.GetService<IConveyorByDayRepository>(BootStrap.ServiceProvider);
            _mfByDayRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederByDayRepository>(BootStrap.ServiceProvider);
            _jcByDayRep = ServiceProviderServiceExtensions.GetService<IJawCrusherByDayRepository>(BootStrap.ServiceProvider);
            _ccByDayRep = ServiceProviderServiceExtensions.GetService<IConeCrusherByDayRepository>(BootStrap.ServiceProvider);
            _vcByDayRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByDayRepository>(BootStrap.ServiceProvider);
            _vbByDayRep = ServiceProviderServiceExtensions.GetService<IVibrosieveByDayRepository>(BootStrap.ServiceProvider);
            _sccByDayRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByDayRepository>(BootStrap.ServiceProvider);
            _pulByDayRep = ServiceProviderServiceExtensions.GetService<IPulverizerByDayRepository>(BootStrap.ServiceProvider);
            _icByDayRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherByDayRepository>(BootStrap.ServiceProvider);
            _hvbByDayRep = ServiceProviderServiceExtensions.GetService<IHVibByDayRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 根据产线ID获取所有失联、停止、运行电机数目
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public virtual Tuple<int,int,int> GetMotors(string lineId)
        {      
            var lose = 0;
            var stop = 0;
            var run = 0;
            var motors = _motorRep.GetEntities(e => e.ProductionLineId.Equals(lineId))?.ToList();
            if (motors==null||!motors.Any()) return new Tuple<int, int, int>(0, 0, 0);
            foreach (var motor in motors)
            {
                MotorStatus status;
                switch (motor.MotorTypeId)
                {
                    case "CY":
                        //var x = _cyRep.GetEntities("WDD-P001-CY000047", DateTime.Now, false).ToList().OrderByDescending(e => e.Time);
                        status = _cyRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "MF":
                        status = _mfRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "JC":
                        status = _jcRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "CC":
                        status = _ccRep.GetCurrentStatus(motor.MotorId) ;
                        //var x = _ccRep.GetEntities("WDD-P001-CC000001", DateTime.Now, false).ToList().OrderByDescending(e => e.Time);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "VC":
                        status = _vcRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "VB":
                        status = _vbRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "SCC":
                        status = _sccRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "PUL":
                        status = _pulRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "IC":
                        status = _icRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    case "HVB":
                        status = _hvbRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1: run + 0;
                        break;
                    default:
                        break;
                }
            }
            var count = new Tuple<int, int, int>(lose, stop, run);
            return count;
        }


        /// <summary>
        /// 获取产线状态
        /// </summary>
        /// <param name="lineId">产线Id</param>
        /// <returns></returns>
        public bool GetStatus(string lineId)
        {
            var now = DateTime.Now.TimeSpan();
            var line = GetEntities(e => e.ProductionLineId.EqualIgnoreCase(lineId)).FirstOrDefault();
            return line != null && now-line.Time <= 10 * 60;
        }

        /// <summary>
        /// 根据电机设备ID获取设备状态
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public virtual MotorStatus GetMotorStatusByMotorId(string motorId)
        {
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            if (motor==null) return MotorStatus.Lose;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyRep.GetCurrentStatus(motor.MotorId);
                   
                case "MF":
                   return _mfRep.GetCurrentStatus(motor.MotorId);
                   
                case "JC":
                   return _jcRep.GetCurrentStatus(motor.MotorId);
                   
                case "CC":
                   return _ccRep.GetCurrentStatus(motor.MotorId);
                   
                case "VC":
                   return _vcRep.GetCurrentStatus(motor.MotorId);
                   
                case "VB":
                   return _vbRep.GetCurrentStatus(motor.MotorId);
                   
                case "SCC":
                   return _sccRep.GetCurrentStatus(motor.MotorId);
                   
                case "PUL":
                   return _pulRep.GetCurrentStatus(motor.MotorId);
                   
                case "IC":
                   return _icRep.GetCurrentStatus(motor.MotorId);
                   
                case "HVB":
                   return _hvbRep.GetCurrentStatus(motor.MotorId);
                   
                default:
                    return MotorStatus.Lose;
            }          
        }
        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual dynamic MotorDetails(Motor motor)
        {
           // var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motor.motorId)).SingleOrDefault();
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetRealData(motor);
                case "MF":
                    return _mfByHourRep.GetRealData(motor);
                case "JC":
                    return _jcByHourRep.GetRealData(motor);

                case "CC":
                    return _ccByHourRep.GetRealData(motor);

                case "VC":
                    return _vcByHourRep.GetRealData(motor);

                case "VB":
                    return _vbByHourRep.GetRealData(motor);

                case "SCC":
                    return _sccByHourRep.GetRealData(motor);

                case "PUL":
                    return _pulByHourRep.GetRealData(motor);

                case "IC":
                    return _icByHourRep.GetRealData(motor);

                case "HVB":
                    return _hvbByHourRep.GetRealData(motor);

                default:
                    return list;
            }
        }
        /// <summary>
        /// 根据电机设备ID和时间节点获取电机设备详情
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorDetails(DateTime startTime, DateTime endTime, string motorId)
        {
            long start = startTime.TimeSpan(), end = endTime.TimeSpan();
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).FirstOrDefault();
            if (motor == null) return new List<dynamic>();
            dynamic list=new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                      list = _cyByDayRep.GetEntities(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&
                      e.Time<=end).ToList();//?.ToList()??new List<ConveyorByDay>();
                      var cy = _cyByHourRep.GetRealData(motor);
                      if(cy!=null)
                        list.Add(cy);
                      return list;
                case "MF":
                    list = _mfByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<MaterialFeederByDay>();                   
                    var mf = _mfByHourRep.GetRealData(motor);
                    if (mf != null)
                        list.Add(mf);
                    return list;

                case "JC":
                    list = _jcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<JawCrusherByDay>();
                    var jc = _jcByHourRep.GetRealData(motor);
                    if (jc != null)
                        list.Add(jc);
                    return list;

                case "CC":
                    list = _ccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<ConeCrusherByDay>();
                    //list.Add(_ccByHourRep.GetRealData(motor.MotorId));
                    var cc = _ccByHourRep.GetRealData(motor);
                    if (cc != null)
                        list.Add(cc);
                    return list;

                case "VC":
                    list = _vcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                      e.Time<=end).ToList();//?.ToList() ?? new List<VerticalCrusherByDay>();
                                   //list.Add(_vcByHourRep.GetRealData(motor.MotorId));
                    var vc = _vcByHourRep.GetRealData(motor);
                    if (vc != null)
                        list.Add(vc);
                    return list;

                case "VB":
                    list = _vbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<VibrosieveByDay>();
                                  //list.Add(_vbByHourRep.GetRealData(motor.MotorId));
                    var vb = _vbByHourRep.GetRealData(motor);
                    if (vb != null)
                        list.Add(vb);
                    return list;

                case "SCC":
                    list = _sccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();
                    //list.Add(_sccByHourRep.GetRealData(motor.MotorId));
                    var scc = _sccByHourRep.GetRealData(motor);
                    if (scc != null)
                        list.Add(scc);
                    return list;

                case "PUL":
                    list = _pulByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<PulverizerByDay>();
                    //list.Add(_pulByHourRep.GetRealData(motor.MotorId));
                    var pul = _pulByHourRep.GetRealData(motor);
                    if (pul != null)
                        list.Add(pul);
                    return list;

                case "IC":
                    list = _icByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<ImpactCrusherByDay>();
                                  //list.Add(_icByHourRep.GetRealData(motor.MotorId));
                    var ic = _icByHourRep.GetRealData(motor);
                    if (ic != null)
                        list.Add(ic);
                    return list;

                case "HVB":
                    list = _hvbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<HVibByDay>();
                                  //list.Add(_hvbByHourRep.GetRealData(motor.MotorId));
                    var hvb = _hvbByHourRep.GetRealData(motor);
                    if (hvb != null)
                        list.Add(hvb);
                    return list;

                default:
                    return list;
            }
        }

        /// <summary>
        /// 根据电机设备ID和时间节点获取电机设备详情(不包括今天)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorDays(long start, long end, Motor motor)
        {
           // long start = startTime.TimeSpan(), end = endTime.TimeSpan();
            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).FirstOrDefault();
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    list = _cyByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                    e.Time <= end).ToList();//?.ToList()??new List<ConveyorByDay>();
                    return list;
                case "MF":
                    list = _mfByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<MaterialFeederByDay>();                   

                    return list;

                case "JC":
                    list = _jcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<JawCrusherByDay>();
 
                    return list;

                case "CC":
                    list = _ccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<ConeCrusherByDay>();
         
                    return list;

                case "VC":
                    list = _vcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                      e.Time <= end).ToList();//?.ToList() ?? new List<VerticalCrusherByDay>();
       
                    return list;

                case "VB":
                    list = _vbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<VibrosieveByDay>();
 
                    return list;

                case "SCC":
                    list = _sccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();
   
                    return list;

                case "PUL":
                    list = _pulByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<PulverizerByDay>();
  
                    return list;

                case "IC":
                    list = _icByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<ImpactCrusherByDay>();
       
                    return list;

                case "HVB":
                    list = _hvbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();//?.ToList() ?? new List<HVibByDay>();
   
                    return list;

                default:
                    return list;
            }
        }
        /// <summary>
        /// 根据动态数据获取设备详情
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        public dynamic GetMotorDetails(IEnumerable<dynamic> datas,Motor motor)
        {
            if (datas == null||!datas.Any())
                return GetMotorDetails(motor.MotorTypeId);
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return new
                    {
                        AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                        AccumulativeWeight = (float)Math.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            Output = e.AccumulativeWeight,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                case "MF":
                    return new
                    {
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        AvgFrequency = (float)Math.Round((datas.Average(e => (float)e.AvgFrequency)), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.AvgFrequency, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };
                case "JC":
                    return new
                    {
                        AvgMotiveSpindleTemperature1 = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                        AvgMotiveSpindleTemperature2 = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                        AvgRackSpindleTemperature1 = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                        AvgRackSpindleTemperature2 = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                        AvgVibrate1 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                        WearValue1 = (float)Math.Round(datas.Average(e => (float)e.WearValue1), 2),
                        AvgVibrate2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                        WearValue2 = (float)Math.Round(datas.Average(e => (float)e.WearValue2), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            e.AvgMotiveSpindleTemperature1,
                            e.AvgMotiveSpindleTemperature2,
                            e.AvgRackSpindleTemperature1,
                            e.AvgRackSpindleTemperature2,
                            e.AvgVibrate1,
                            e.AvgVibrate2,
                            e.WearValue2,
                            e.WearValue1,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                case "CC":
                    return new
                    {
                        AvgMovaStress = (float)Math.Round(datas.Average(e => (float)e.AvgMovaStress), 2),
                        AvgOilReturnTempreatur = (float)Math.Round((double)(datas.Average(e => (float)e.AvgOilReturnTempreatur)), 2),
                        AvgOilFeedTempreature = (float)Math.Round(datas.Average(e => (float)e.AvgOilFeedTempreature), 2),
                        AvgTankTemperature = (float)Math.Round((double)(datas.Average(e => (float)e.AvgTankTemperature)), 2),
                        AvgSpindleTravel = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTravel), 2),
                        AvgVibrate1 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgVibrate1)), 2),
                        AvgVibrate2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgVibrate2)), 2),
                        WearValue1 = (float)Math.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = (float)Math.Round((double)(datas.Average(e => (float)e.WearValue2)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            e.AvgMovaStress,
                            e.AvgOilReturnTempreatur,
                            e.AvgOilFeedTempreature,
                            e.AvgTankTemperature,
                            e.AvgSpindleTravel,
                            e.AvgVibrate1,
                            e.AvgVibrate2,
                            e.WearValue1,
                            e.WearValue2,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };

                case "VC":
                    return new
                    {
                        WearValue1 = (float)Math.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = (float)Math.Round(datas.Average(e => (float)e.WearValue2), 2),
                        AvgVibrate1 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgVibrate2)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.WearValue2, e.WearValue1, e.AvgVibrate1, e.AvgVibrate2, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };
                case "VB":
                    return new
                    {
                        AvgSpindleTemperature4 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature4), 2),
                        AvgSpindleTemperature2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgSpindleTemperature2)), 2),
                        AvgSpindleTemperature1 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature1), 2),
                        AvgSpindleTemperature3 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgSpindleTemperature3)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            e.AvgSpindleTemperature1,
                            e.AvgSpindleTemperature4,
                            e.AvgSpindleTemperature2,
                            e.AvgSpindleTemperature3,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };

                case "SCC":
                    return new
                    {
                        AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                        AccumulativeWeight = (float)Math.Round((double)(datas.Sum(e => (float)e.AccumulativeWeight)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { Output = e.AccumulativeWeight, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };

                case "PUL":
                    return new
                    {
                        AvgVibrate1 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgVibrate2)), 2),
                        WearValue1 = (float)Math.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = (float)Math.Round((double)(datas.Average(e => (float)e.WearValue2)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.WearValue2, e.WearValue1, e.AvgVibrate1, e.AvgVibrate2, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };

                case "IC":
                    return new
                    {
                        AvgMotor2Current_B = (float)Math.Round(datas.Average(e => (float)e.AvgMotor2Current_B), 2),
                        AvgMotor1Current_B = (float)Math.Round((double)(datas.Average(e => (float)e.AvgMotor1Current_B)), 2),
                        AvgSpindleTemperature1 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature1), 2),
                        AvgSpindleTemperature2 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature2), 2),
                        AvgVibrate1 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgVibrate2)), 2),
                        WearValue1 = (float)Math.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = (float)Math.Round((double)(datas.Average(e => (float)e.WearValue2)), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            e.WearValue2,
                            e.WearValue1,
                            e.AvgVibrate1,
                            e.AvgVibrate2,
                            e.AvgMotor2Current_B,
                            e.AvgMotor1Current_B,
                            e.AvgSpindleTemperature1,
                            e.AvgSpindleTemperature2,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                case "HVB":
                    return new
                    {
                        AvgSpindleTemperature4 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature4), 2),
                        AvgSpindleTemperature2 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgSpindleTemperature2)), 2),
                        AvgSpindleTemperature3 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature3), 2),
                        AvgSpindleTemperature1 = (float)Math.Round((double)(datas.Average(e => (float)e.AvgSpindleTemperature1)), 2),
                        AvgOilReturnStress = (float)Math.Round(datas.Average(e => (float)e.AvgOilReturnStress), 2),
                        AvgOilFeedStress = (float)Math.Round((double)(datas.Average(e => (float)e.AvgOilFeedStress)), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new {
                            e.AvgSpindleTemperature4,
                            e.AvgSpindleTemperature2,
                            e.AvgSpindleTemperature3,
                            e.AvgSpindleTemperature1,
                            e.AvgOilFeedStress,
                            e.AvgOilReturnStress,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                default:
                    return  new
                    {
                        AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                        AccumulativeWeight = (float)Math.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                        AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),                    
                    };
            }
        }

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="motorTypeId"></param>
        /// <returns></returns>
        private dynamic GetMotorDetails(string motorTypeId)
        {
            switch (motorTypeId)
            {
                case "CY":
                    return new
                    {
                        AvgInstantWeight = 0,
                        AccumulativeWeight = 0,
                        AvgCurrent = 0,
                        LoadStall =0,
                        RunningTime = 0,
                        SeriesData =  new {
                            Output = 0,
                            Current =0,
                            RunningTime=0,
                            UnixTime = 0
                        }
                    };
                case "MF":
                    return new
                    {
                        AvgCurrent = 0,
                        AvgFrequency = 0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData =  new { AvgFrequency = 0, Current = 0,AvgCurrent_B = 0, RunningTime=0, UnixTime =0 }
                    };
                case "JC":
                    return new
                    {
                        AvgMotiveSpindleTemperature1 = 0,
                        AvgMotiveSpindleTemperature2  = 0,
                        AvgRackSpindleTemperature1 = 0,
                        AvgRackSpindleTemperature2 = 0,
                        AvgVibrate1 = 0,
                        WearValue1 =0,
                        AvgVibrate2 = 0,
                        WearValue2 =0,
                        AvgCurrent = 0,
                        LoadStall =0,
                        RunningTime = 0,
                        SeriesData = new {
                           AvgMotiveSpindleTemperature1 = 0,
                            AvgMotiveSpindleTemperature2 = 0,
                            AvgRackSpindleTemperature1 = 0,
                            AvgRackSpindleTemperature2 = 0,
                           AvgVibrate1 = 0,
                          AvgVibrate2 = 0,
                           WearValue2 = 0,
                           WearValue = 0,
                           AvgCurrent_B = 0,
                           RunningTime = 0,
                            UnixTime =0
                        }
                    };
                case "CC":
                    return new
                    {
                        AvgMovaStress =  0,
                        AvgOilReturnTempreatur =0,
                        AvgOilFeedTempreature = 0,
                        AvgTankTemperature = 0,
                        AvgSpindleTravel = 0,
                        AvgVibrate1 =0,
                        AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 =0,
                        AvgCurrent = 0,
                        LoadStall =0,
                        RunningTime =0,
                        SeriesData = new {
                           AvgMovaStress = 0,
                           AvgOilReturnTempreatur = 0,
                           AvgOilFeedTempreature = 0,
                           AvgTankTemperature = 0,
                           AvgSpindleTravel = 0,
                           AvgVibrate1 = 0,
                           AvgVibrate2 = 0,
                           WearValue1 = 0,
                           WearValue2 = 0,
                            Current = 0,
                           RunningTime = 0,
                            UnixTime =0
                        }
                    };

                case "VC":
                    return new
                    {
                        WearValue1 = 0,
                        WearValue2 = 0,
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData =new {
                           WearValue2=0,
                           WearValue1=0,
                           AvgVibrate=01,
                           AvgVibrate2=0,
                            Current =0,
                            RunningTime=0,
                            UnixTime = 0 }
                    };
                case "VB":
                    return new
                    {
                        AvgSpindleTemperature4 = 0,
                        AvgSpindleTemperature2 = 0,
                        AvgSpindleTemperature1 = 0,
                        AvgSpindleTemperature3 = 0,
                        AvgCurrent =0,
                        LoadStall =0, 
                        RunningTime =0,   
                        SeriesData = new {
                          AvgSpindleTemperature1=0,
                          AvgSpindleTemperature4=0,
                          AvgSpindleTemperature2=0,
                          AvgSpindleTemperature3=0,
                            Current = 0,
                            RunningTime=0,
                            UnixTime =0
                        }
                    };

                case "SCC":
                    return new
                    {
                        AvgInstantWeight =0,
                        AccumulativeWeight=0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                        RunningTime = 0,                     
                        SeriesData = new {
                            Output =0,
                            Current = 0,
                            RunningTime=0,
                            UnixTime =0 }
                    };

                case "PUL":
                    return new
                    {
                        AvgVibrate1 =0,
                        AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new {
                           WearValue2=0,
                           WearValue1=0,
                           AvgVibrate1=0,
                           AvgVibrate2=0,
                            Current = 0,
                            RunningTime=0,
                            UnixTime = 0 }
                    };

                case "IC":
                    return new
                    {
                        AvgMotor2Current_B = 0,
                        AvgMotor1Current_B = 0,
                        AvgSpindleTemperature1 =0 ,
                        AvgSpindleTemperature2 =0 ,
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 = 0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new {
                           WearValue2=0,
                           WearValue1 = 0,
                           AvgVibrate1 = 0,
                           AvgVibrate2 = 0,
                            AvgMotor2Current_B = 0,
                            AvgMotor1Current_B = 0,
                           AvgSpindleTemperature1 = 0,
                           AvgSpindleTemperature2 = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };
                case "HVB":
                    return new
                    {
                        AvgSpindleTemperature4 =0,
                        AvgSpindleTemperature2 = 0,
                        AvgSpindleTemperature3 = 0,
                        AvgSpindleTemperature1 = 0,
                        AvgOilReturnStress = 0,
                        AvgOilFeedStress =0,
                        AvgCurrent =0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new {
                           AvgSpindleTemperature4=0,
                           AvgSpindleTemperature2 = 0,
                           AvgSpindleTemperature3 = 0,
                           AvgSpindleTemperature1 = 0,
                           AvgOilFeedStress = 0,
                           AvgOilReturnStress = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };
                default:
                    return new
                    {
                        AvgInstantWeight = 0,
                        AccumulativeWeight = 0,
                        AvgCurrent = 0,
                        LoadStall =0,
                        RunningTime = 0,
                    };
            }
        }
        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorHours(Motor motor)
        {
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetRealDatas(motor);
                case "MF":
                    return _mfByHourRep.GetRealDatas(motor);
                case "JC":
                    return _jcByHourRep.GetRealDatas(motor);

                case "CC":
                    return _ccByHourRep.GetRealDatas(motor);

                case "VC":
                    return _vcByHourRep.GetRealDatas(motor);

                case "VB":
                    return _vbByHourRep.GetRealDatas(motor);

                case "SCC":
                    return _sccByHourRep.GetRealDatas(motor);

                case "PUL":
                    return _pulByHourRep.GetRealDatas(motor);

                case "IC":
                    return _icByHourRep.GetRealDatas(motor);

                case "HVB":
                    return _hvbByHourRep.GetRealDatas(motor);

                default:
                    return list;
            }
        }
        /// <summary>
        /// 根据电机设备ID获取历史某一天电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorHours(Motor motor,long date)
        {
            long startUnix = date, endUnix = date.Time().AddDays(1).TimeSpan();
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();
                case "MF":
                    return _mfByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();
                case "JC":
                    return _jcByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "CC":
                    return _ccByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "VC":
                    return _vcByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "VB":
                    return _vbByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "SCC":
                    return _sccByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "PUL":
                    return _pulByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "IC":
                    return _icByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                case "HVB":
                    return _hvbByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < endUnix).ToList();

                default:
                    return list;
            }
        }


        /// <summary>
        /// 缓存中是否存在该产线下的设备原始数据库
        /// </summary>
        /// <param name="productionLineId"></param>
        /// <returns></returns>
        public bool GetInstanceFromRedis(string productionLineId)
        {
            RedisProvider.DB = 15;
            var motors = _motorRep.GetEntities(e=>e.ProductionLineId.EqualIgnoreCase(productionLineId));
            if (motors==null||!motors.Any()) return true;
            var redisKeys = RedisProvider.Keys("*");
            if (redisKeys.Select(e => e.Split("_")[1]).Contains(motors.Select(e => e.MotorId).FirstOrDefault()))
                return true;
            return false;
        }

        /// <summary>
        /// 删除缓存（慎用！）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motor">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        [Obsolete]
        public int DelCache(Motor motor, DateTime dt)
        {
            var dayUnix = dt.Date.TimeSpan();
            RedisProvider.DB = 15;
            var result = RedisProvider.Delete(dayUnix + "_" + motor.MotorId);
            return result;
        }
        /// <summary>
        /// 缓存预热（慎用！）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motor">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        [Obsolete]
        public int PreCache(Motor motor, DateTime dt)
        {
            var result = 0;      
            switch (motor.MotorTypeId)
            {
                case "CY":
                     result =_cyRep.PreCache(motor,dt);
                     break;
                case "MF":
                    result = _mfRep.PreCache(motor, dt);
                    break;
                case "JC":
                    result = _jcRep.PreCache(motor, dt);
                    break;
                case "CC":
                    result = _ccRep.PreCache(motor, dt);
                    break;
                case "VC":
                    result = _vcRep.PreCache(motor, dt);
                    break;
                case "VB":
                    result = _vbRep.PreCache(motor, dt);
                    break;
                case "SCC":
                    result = _sccRep.PreCache(motor, dt);
                    break;
                case "PUL":
                    result = _pulRep.PreCache(motor, dt);
                    break;
                case "IC":
                    result = _icRep.PreCache(motor, dt);
                    break;
                case "HVB":
                    result = _hvbRep.PreCache(motor, dt);
                    break;
                default:
                    break;
                    
            }            
            return result;
        }

        /// <summary>
        ///  缓存预热（慎用！）同一天内的某段时间数据
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="dayUnix"></param>
        /// <param name="start"></param>
        /// <param name="end">不包括</param>
        /// <returns></returns>
        [Obsolete]
        public int PreCache2(Motor motor,long dayUnix, long start,long end)
        {
            var result = 0;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    var list = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if(list!=null&&list.Any())
                        result = _cyRep.Cache(motor.MotorId, dayUnix,list);
                    break;
                case "MF":
                    var list2 = _mfRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list2 != null && list2.Any())
                        result = _mfRep.Cache(motor.MotorId, dayUnix, list2);
                    break;
                case "JC":
                    var list3 = _jcRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list3 != null && list3.Any())
                        result = _jcRep.Cache(motor.MotorId, dayUnix, list3);
                    break;
                case "CC":
                    var list4 = _ccRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list4 != null && list4.Any())
                        result = _ccRep.Cache(motor.MotorId, dayUnix, list4);
                    break;
                case "VC":
                    var list5 = _vcRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list5 != null && list5.Any())
                        result = _vcRep.Cache(motor.MotorId, dayUnix, list5);
                    break;
                case "VB":
                    var list6 = _vbRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list6 != null && list6.Any())
                        result = _vbRep.Cache(motor.MotorId, dayUnix, list6);
                    break;
                case "SCC":
                    var list7 = _sccRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list7 != null && list7.Any())
                        result = _sccRep.Cache(motor.MotorId, dayUnix, list7);
                    break;
                case "PUL":
                    var list8 = _pulRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list8 != null && list8.Any())
                        result = _pulRep.Cache(motor.MotorId, dayUnix, list8);
                    break;
                case "IC":
                    var list9 = _icRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list9 != null && list9.Any())
                        result = _icRep.Cache(motor.MotorId, dayUnix, list9);
                    break;
                case "HVB":
                    var list10 = _hvbRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list10 != null && list10.Any())
                        result = _hvbRep.Cache(motor.MotorId, dayUnix, list10);
                    break;
                default:
                    break;

            }
            return result;
        }
        /// <summary>
        /// 获取设备原始数据
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="date">日期</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetMotorHistoryByDate(Motor motor, DateTime date, bool cache)
        {
            var datas=new List<dynamic>();
            if (cache)
            {
                switch (motor.MotorTypeId)
                {
                    case "CY":
                        return _cyRep.GetEntities(motor.MotorId,date,false,null,e=>e.Time);
                    case "MF":
                        return _mfRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);
                    case "JC":
                        return _jcRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "CC":
                        return _ccRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "VC":
                        return _vcRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);
                    case "VB":
                        return _vbRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "SCC":
                        return _sccRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "PUL":
                        return _pulRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "IC":
                        return _icRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "HVB":
                        return _hvbRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    default:
                        return datas;
                }
            }
            long start = date.TimeSpan(), end = date.AddDays(1).TimeSpan();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);
                case "MF":
                    return _mfRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);
                case "JC":
                    return _jcRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "CC":
                    return _ccRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "VC":
                    return _vcRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);
                case "VB":
                    return _vbRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "SCC":
                    return _sccRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "PUL":
                    return _pulRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "IC":
                    return _icRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                case "HVB":
                    return _hvbRep.GetFromSqlDb(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&e.Time<end, e => e.Time);

                default:
                    return datas;
            }
        }

        /// <summary>
        /// 获取设备原始数据
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="date">日期</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetMotorHistoryByDate(Motor motor, DateTime date, string dataType)
        {
            var datas = new List<dynamic>();
    
                switch (motor.MotorTypeId)
                {
                    case "CY":
                        return _cyRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);
                    case "MF":
                        return _mfRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);
                    case "JC":
                        return _jcRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "CC":
                        return _ccRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "VC":
                        return _vcRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);
                    case "VB":
                        return _vbRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "SCC":
                        return _sccRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "PUL":
                        return _pulRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "IC":
                        return _icRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    case "HVB":
                        return _hvbRep.GetEntities(motor.MotorId, date, false, null, e => e.Time);

                    default:
                        return datas;
                }
        }
        /// <summary>
        /// 计算当日产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        public List<PowerCal> CalcMotorPowers(List<Motor> motors)
        {
            var now = DateTime.Now.Date;
            if (motors == null || !motors.Any())
                return null;
            var resp = new List<PowerCal>();
            var list = new List<PowerCal>();
            motors.ForEach(motor =>
            {
                if (motor.MotorTypeId.EqualIgnoreCase("IC"))
                {
                    var datas = MotorHours(motor);
                    var times = new List<long>();
                    for (int i = 0; i < 23; i++)
                    {
                        var time = now.AddHours(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, ActivePower = (e.Motor1ActivePower + e.Motor2ActivePower) })
                                .Where(e => (long)e.Time == time);
                        var t = d.FirstOrDefault();
                        var item = new PowerCal { ActivePower = d.Any() ? t.ActivePower : 0f, Time = time };
                        list.Add(item);
                    }
                }
                else
                {
                    var datas = MotorHours(motor);
                    var times = new List<long>();
                    for (int i = 0; i < 23; i++)
                    {
                        var time = now.AddHours(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, e.ActivePower })
                                .Where(e => (long)e.Time == time);
                        var t = d.FirstOrDefault();
                        var item = new PowerCal { ActivePower = d.Any() ? t.ActivePower : 0f, Time = time };
                        list.Add(item);
                    }
                }

            });
            var groups = list.GroupBy(e => e.Time);
            foreach (var item in groups)
            {
                var time = item.Key;
                var powerSum = (float)Math.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
                resp.Add(new PowerCal { ActivePower = powerSum, Time = (long)time });
            }
            return resp;
        }

            /// <summary>
            /// 计算历史区间内产线耗电量明细
            /// </summary>
            /// <param name="motors"></param>
            /// <returns></returns>
            public List<PowerCal> CalcMotorPowers(List<Motor> motors,long start,long end)
        {
            var length = end.Time().Subtract(start.Time()).TotalDays/1;
            var endT = end.Time().AddMilliseconds(-1).TimeSpan();
            var startT = start.Time().Date;
            if (motors == null || !motors.Any())
                return null;
            var resp = new List<PowerCal>();
            var list = new List<PowerCal>();
            motors.ForEach(motor =>
            {
                if (motor.MotorTypeId.EqualIgnoreCase("IC"))
                {
                    var datas = MotorDays(start, endT, motor);
                    var times = new List<long>();
                    for (int i = 0; i < length; i++)
                    {
                        var time = startT.AddDays(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, ActivePower = (e.Motor1ActivePower + e.Motor2ActivePower) })
                                .Where(e => (long)e.Time == time);
                        var t = d.FirstOrDefault();
                        var item = new PowerCal { ActivePower = d.Any() ? t.ActivePower : 0f, Time = time };
                        list.Add(item);
                    }
                }
                else
                {
                    var datas = MotorDays(start, endT, motor);
                    var times = new List<long>();
                    for (int i = 0; i < length; i++)
                    {
                        var time = startT.AddDays(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, e.ActivePower })
                                .Where(e => (long)e.Time == time);
                        var t = d.FirstOrDefault();
                        var item = new PowerCal { ActivePower = d.Any() ? t.ActivePower : 0f, Time = time };
                        list.Add(item);
                    }
                }

            });
            var groups = list.GroupBy(e => e.Time);
            foreach (var item in groups)
            {
                var time = item.Key;
                var powerSum = (float)Math.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
                resp.Add(new PowerCal { ActivePower = powerSum, Time = (long)time });
            }
            return resp;
        }

        /// <summary>
        /// 计算历史某一天产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        public List<PowerCal> CalcMotorPowers(List<Motor> motors, long date)
        {
            var dt = date.Time();
            if (motors == null || !motors.Any())
                return null;
            var resp = new List<PowerCal>();
            var list = new List<PowerCal>();
            motors.ForEach(motor =>
            {
                if (motor.MotorTypeId.EqualIgnoreCase("IC"))
                {
                    var datas = MotorHours(motor, date);
                    var times = new List<long>();
                    for (int i = 0; i < 23; i++)
                    {
                        var time = dt.AddHours(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, ActivePower= (e.Motor1ActivePower+e.Motor2ActivePower) })
                                .Where(e => (long)e.Time == time);
                        var t = d.FirstOrDefault();
                        var item = new PowerCal { ActivePower = d.Any() ? t.ActivePower : 0f, Time = time };
                        list.Add(item);
                    }             
                }
                else
                {
                    var datas = MotorHours(motor, date);
                    var times = new List<long>();
                    for (int i = 0; i < 23; i++)
                    {
                        var time = dt.AddHours(i).TimeSpan();
                        times.Add(time);
                        var d = datas.Select(e => new { e.Time, e.ActivePower })
                                .Where(e => (long)e.Time == time);
                        var t=d.FirstOrDefault();
                        var item = new PowerCal { ActivePower= d.Any() ? t.ActivePower:0f,Time=time};
                        list.Add(item);
                    }
                }

            });
            var groups = list.GroupBy(e => e.Time);
            foreach (var item in groups)
            {
                var time = item.Key;
                var powerSum = (float)Math.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
                resp.Add(new PowerCal{ ActivePower = powerSum, Time = (long)time });
            }
            return resp;
        }

        /// <summary>
        /// 根据动态数据获取移动端设备详情
        /// </summary>
        /// <param name="datas">需要先排序</param>
        /// <param name="motor"></param>
        /// <returns></returns>
        public dynamic GetMobileMotorDetails(IEnumerable<dynamic> datas, Motor motor)
        {
            if (datas==null||!datas.Any())
                return GetMobileMotorDetails(motor.MotorTypeId);
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return new
                    {
                        outline=new
                        {
                            // AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                            Output = (float)Math.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis=new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        outputSeries=new {name=motor.Name, data =datas.Select(e=>new { e.AccumulativeWeight})},
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },              
                    };
                case "MF":
                    return new
                    {
                        outline = new
                        {
                            Frequency = (float)Math.Round(datas.Average(e => (float)e.AvgFrequency), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        freqSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgFrequency }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                case "JC":
                    return new
                    {
                        outline = new
                        {
                            MST = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                            MST2 = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                            RST = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                            RST2 = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        mstSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotiveSpindleTemperature1 }) },
                        mst2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotiveSpindleTemperature2 }) },
                        rstSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgRackSpindleTemperature1 }) },
                        rst2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgRackSpindleTemperature2 }) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                case "CC":
                    return new
                    {
                        outline = new
                        {
                            TT = (float)Math.Round(datas.Average(e => (float)e.AvgTankTemperature), 2),
                            OFT = (float)Math.Round(datas.Average(e => (float)e.AvgOilFeedTempreature), 2),
                            ORT = (float)Math.Round(datas.Average(e => (float)e.AvgOilReturnTempreatur), 2),
                            STV = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTravel), 2),
                            MS = (float)Math.Round(datas.Average(e => (float)e.AvgMovaStress), 2),
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        ttSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgTankTemperature }) },
                        oftSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilFeedTempreature }) },
                        ortSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilReturnTempreatur }) },
                        stvSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTravel }) },
                        msSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgMovaStress }) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },           
                    };

                case "VC":
                    return new
                    {
                        outline = new
                        {
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },                   
                    };
                case "VB":
                    return new
                    {
                        outline = new
                        {
                            ST = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            ST3 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature3), 2),
                            ST4 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature4), 2),                       
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                        st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                        st3Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature3 }) },
                        st4Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature4 }) },                     
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };

                case "SCC":
                    return new
                    {
                        outline = new
                        {
                            TT = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                            OFT = (float)Math.Round(datas.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                            ORT = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                            STV = (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            MS= (float)Math.Round(datas.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        mstSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotiveSpindleTemperature1 }) },
                        mst2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotiveSpindleTemperature2 }) },
                        rstSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgRackSpindleTemperature1 }) },
                        rst2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgRackSpindleTemperature2 }) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
           
                    };

                case "PUL":
                    return new
                    {
                        outline = new
                        {
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };

                case "IC":
                    return new
                    {
                        outline = new
                        {
                            ST= (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2= (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            Current2= (float)Math.Round(datas.Average(e => (float)e.AvgMotor2Current_B), 2),
                            VIB = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = (float)Math.Round(datas.Average(e => (float)e.AvgVibrate2), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgMotor1Current_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                        st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                        current2Series= new { name = motor.Name, data = datas.Select(e => new { e.AvgMotor2Current_B }) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotor1Current_B }) },
                       
                    };
                case "HVB":
                    return new
                    {
                        outline = new
                        {
                            ST = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            ST3 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature3), 2),
                            ST4 = (float)Math.Round(datas.Average(e => (float)e.AvgSpindleTemperature4), 2),
                            OFS = (float)Math.Round(datas.Average(e => (float)e.AvgOilFeedStress), 2),
                            ORS = (float)Math.Round(datas.Average(e => (float)e.AvgOilReturnStress), 2),
                            Current = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                            RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                        st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                        st3Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature3 }) },
                        st4Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature4 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        ofsSeries= new { name = motor.Name, data = datas.Select(e => new { e.AvgOilFeedStress }) },
                        orsSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilReturnStress }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                default:
                    return new
                    {
                        //AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                        //AccumulativeWeight = (float)Math.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                        //AvgCurrent = (float)Math.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        //LoadStall = (float)Math.Round(datas.Average(e => (float)e.LoadStall), 2),
                        //RunningTime = (float)Math.Round(datas.Sum(e => (float)e.RunningTime), 2),
                    };
            }
        }

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="motorTypeId"></param>
        /// <returns></returns>
        private dynamic GetMobileMotorDetails(string motorTypeId)
        {
            switch (motorTypeId)
            {
                case "CY":
                    return new
                    {
                        outline = new
                        {
                            // AvgInstantWeight = (float)Math.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                            Output =0,
                            Current = 0,
                            Load = 0,
                            RunningTime =0,
                        },
                        xAxis = new { },
                        outputSeries = new {},
                        runningtimeSeries = new {  },
                        currentSeries = new {},
                    };
                case "MF":
                    return new
                    {
                        outline = new
                        {
                            Frequency =0, 
                            Current = 0,
                            Load = 0,
                            RunningTime =0,   
                        },
                        xAxis = new { },
                        freqSeries = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };
                case "JC":
                    return new
                    {
                        outline = new
                        {
                            MST = 0,
                            MST2 = 0,
                            RST = 0,
                            RST2 = 0,
                            VIB = 0,
                            VIB2 = 0,
                            Current = 0,
                            Load =0,
                            RunningTime =0,
                        },
                        xAxis = new { },
                        mstSeries = new { },
                        mst2Series = new { },
                        rstSeries = new { },
                        rst2Series = new { },
                        vibSeries = new { },
                        vib2Series = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };
                case "CC":
                    return new
                    {
                        outline = new
                        {
                            TT = 0,
                            OFT = 0,
                            ORT = 0,
                            STV = 0,
                            MS = 0,
                            VIB = 0,
                            VIB2 = 0,
                            Current =0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        ttSeries = new { },
                        oftSeries = new { },
                        ortSeries = new { },
                        stvSeries = new { },
                        msSeries = new { },
                        vibSeries = new { },
                        vib2Series = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };

                case "VC":
                    return new
                    {
                        outline = new
                        {
                            VIB = 0,
                            VIB2 = 0,
                            Current = 0,
                            Load =0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        vibSeries = new {},
                        vib2Series = new {  },
                        runningtimeSeries = new {  },
                        currentSeries = new { },
                    };
                case "VB":
                    return new
                    {
                        outline = new
                        {
                            ST = 0,
                            ST2 = 0,
                            ST3 = 0,
                            ST4 =0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        stSeries = new {  },
                        st2Series = new {  },
                        st3Series = new {  },
                        st4Series = new { },
                        runningtimeSeries = new {  },
                        currentSeries = new {  },
                    };

                case "SCC":
                    return new
                    {
                        outline = new
                        {
                            TT = 0,
                            OFT = 0,
                            ORT =0,
                            STV = 0,
                            MS = 0,
                            VIB = 0,
                            VIB2 =0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        mstSeries = new {  },
                        mst2Series = new {  },
                        rstSeries = new {  },
                        rst2Series = new {  },
                        vibSeries = new {  },
                        vib2Series = new {  },
                        runningtimeSeries = new { },
                        currentSeries = new { },

                    };

                case "PUL":
                    return new
                    {
                        outline = new
                        {
                            VIB = 0,
                            VIB2 = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        vibSeries = new {  },
                        vib2Series = new {  },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };

                case "IC":
                    return new
                    {
                        outline = new
                        {
                            ST =0,
                            ST2 = 0,
                            Current2 = 0,
                            VIB = 0,
                            VIB2 = 0,
                            Current =0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        stSeries = new { },
                        st2Series = new {  },
                        current2Series = new { },
                        vibSeries = new {  },
                        vib2Series = new {  },
                        runningtimeSeries = new {  },
                        currentSeries = new { },

                    };
                case "HVB":
                    return new
                    {
                        outline = new
                        {
                            ST = 0,
                            ST2 =0,
                            ST3 =0,
                            ST4 =0,
                            OFS =0,
                            ORS =0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new {  },
                        stSeries = new {  },
                        st2Series = new { },
                        st3Series = new {  },
                        st4Series = new {  },
                        runningtimeSeries = new {  },
                        ofsSeries = new {  },
                        orsSeries = new { },
                        currentSeries = new {  },
                    };
                default:
                    return new
                    {
                    };
            }
        }
        #endregion

    }
}
