using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
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
                        status = _cyRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "MF":
                        status = _mfRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "JC":
                        status = _jcRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "CC":
                        status = _ccRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "VC":
                        status = _vcRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "VB":
                        status = _vbRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "SCC":
                        status = _sccRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "PUL":
                        status = _pulRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "IC":
                        status = _icRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
                        break;
                    case "HVB":
                        status = _hvbRep.GetCurrentStatus(motor.MotorId) ;
                        lose = status.Equals(MotorStatus.Lose) ? lose + 0 : lose + 1;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 0 : stop + 1;
                        run = status.Equals(MotorStatus.Run) ? run + 0 : run + 1;
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
            var motor = _motorRep.GetEntities(e => e.MotorId.EqualIgnoreCase(motorId)).SingleOrDefault();
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
        /// 根据电机设备ID和时间节点获取电机设备详情
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorDetails(DateTime startTime, DateTime endTime, string motorId)
        {
            long start = startTime.TimeSpan(), end = endTime.TimeSpan();
            var motor = _motorRep.GetEntities(e => e.MotorId.EqualIgnoreCase(motorId)).SingleOrDefault();
            if (motor == null) return new List<dynamic>();
            dynamic list=new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                      list = _cyByDayRep.GetEntities(e=>e.MotorId.Equals(motor.MotorId)&&e.Time>=start&&
                      e.Time<=end).ToList();//?.ToList()??new List<ConveyorByDay>();
                      var cy = _cyByHourRep.GetRealData(motor.MotorId);
                      if(cy!=null)
                        list.Add(cy);
                      return list;
                case "MF":
                    list = _mfByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<MaterialFeederByDay>();                   
                    var mf = _mfByHourRep.GetRealData(motor.MotorId);
                    if (mf != null)
                        list.Add(mf);
                    return list;

                case "JC":
                    list = _jcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<JawCrusherByDay>();
                    var jc = _jcByHourRep.GetRealData(motor.MotorId);
                    if (jc != null)
                        list.Add(jc);
                    return list;

                case "CC":
                    list = _ccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<ConeCrusherByDay>();
                    //list.Add(_ccByHourRep.GetRealData(motor.MotorId));
                    var cc = _ccByHourRep.GetRealData(motor.MotorId);
                    if (cc != null)
                        list.Add(cc);
                    return list;

                case "VC":
                    list = _vcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                      e.Time<=end).ToList();//?.ToList() ?? new List<VerticalCrusherByDay>();
                                   //list.Add(_vcByHourRep.GetRealData(motor.MotorId));
                    var vc = _vcByHourRep.GetRealData(motor.MotorId);
                    if (vc != null)
                        list.Add(vc);
                    return list;

                case "VB":
                    list = _vbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<VibrosieveByDay>();
                                  //list.Add(_vbByHourRep.GetRealData(motor.MotorId));
                    var vb = _vbByHourRep.GetRealData(motor.MotorId);
                    if (vb != null)
                        list.Add(vb);
                    return list;

                case "SCC":
                    list = _sccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();
                    //list.Add(_sccByHourRep.GetRealData(motor.MotorId));
                    var scc = _sccByHourRep.GetRealData(motor.MotorId);
                    if (scc != null)
                        list.Add(scc);
                    return list;

                case "PUL":
                    list = _pulByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<PulverizerByDay>();
                    //list.Add(_pulByHourRep.GetRealData(motor.MotorId));
                    var pul = _pulByHourRep.GetRealData(motor.MotorId);
                    if (pul != null)
                        list.Add(pul);
                    return list;

                case "IC":
                    list = _icByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<ImpactCrusherByDay>();
                                  //list.Add(_icByHourRep.GetRealData(motor.MotorId));
                    var ic = _icByHourRep.GetRealData(motor.MotorId);
                    if (ic != null)
                        list.Add(ic);
                    return list;

                case "HVB":
                    list = _hvbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time>=start &&
                     e.Time<=end).ToList();//?.ToList() ?? new List<HVibByDay>();
                                  //list.Add(_hvbByHourRep.GetRealData(motor.MotorId));
                    var hvb = _hvbByHourRep.GetRealData(motor.MotorId);
                    if (hvb != null)
                        list.Add(hvb);
                    return list;

                default:
                    return list;
            }
        }
        #endregion

    }
}
