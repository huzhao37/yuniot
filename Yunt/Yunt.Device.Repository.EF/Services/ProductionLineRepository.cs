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
            if(_motorRep==null)
                _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
            if (_cyRep == null)
                _cyRep = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(BootStrap.ServiceProvider);
            if (_mfRep == null)
                _mfRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederRepository>(BootStrap.ServiceProvider);
            if (_jcRep == null)
                _jcRep = ServiceProviderServiceExtensions.GetService<IJawCrusherRepository>(BootStrap.ServiceProvider);
            if (_ccRep == null)
                _ccRep = ServiceProviderServiceExtensions.GetService<IConeCrusherRepository>(BootStrap.ServiceProvider);
            if (_vcRep == null)
                _vcRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherRepository>(BootStrap.ServiceProvider);
            if (_vbRep == null)
                _vbRep = ServiceProviderServiceExtensions.GetService<IVibrosieveRepository>(BootStrap.ServiceProvider);
            if (_sccRep == null)
                _sccRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherRepository>(BootStrap.ServiceProvider);
            if (_pulRep == null)
                _pulRep = ServiceProviderServiceExtensions.GetService<IPulverizerRepository>(BootStrap.ServiceProvider);
            if (_icRep == null)
                _icRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherRepository>(BootStrap.ServiceProvider);
            if (_hvbRep == null)
                _hvbRep = ServiceProviderServiceExtensions.GetService<IHVibRepository>(BootStrap.ServiceProvider);

            if (_cyByHourRep == null)
                _cyByHourRep = ServiceProviderServiceExtensions.GetService<IConveyorByHourRepository>(BootStrap.ServiceProvider);
            if (_mfByHourRep == null)
                _mfByHourRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederByHourRepository>(BootStrap.ServiceProvider);
            if (_jcByHourRep == null)
                _jcByHourRep = ServiceProviderServiceExtensions.GetService<IJawCrusherByHourRepository>(BootStrap.ServiceProvider);
            if (_ccByHourRep == null)
                _ccByHourRep = ServiceProviderServiceExtensions.GetService<IConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            if (_vcByHourRep == null)
                _vcByHourRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByHourRepository>(BootStrap.ServiceProvider);
            if (_vbByHourRep == null)
                _vbByHourRep = ServiceProviderServiceExtensions.GetService<IVibrosieveByHourRepository>(BootStrap.ServiceProvider);
            if (_sccByHourRep == null)
                _sccByHourRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            if (_pulByHourRep == null)
                _pulByHourRep = ServiceProviderServiceExtensions.GetService<IPulverizerByHourRepository>(BootStrap.ServiceProvider);
            if (_icByHourRep == null)
                _icByHourRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherByHourRepository>(BootStrap.ServiceProvider);
            if (_hvbByHourRep == null)
                _hvbByHourRep = ServiceProviderServiceExtensions.GetService<IHVibByHourRepository>(BootStrap.ServiceProvider);
            if (_cyByDayRep == null)
                _cyByDayRep = ServiceProviderServiceExtensions.GetService<IConveyorByDayRepository>(BootStrap.ServiceProvider);
            if (_mfByDayRep == null)
                _mfByDayRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederByDayRepository>(BootStrap.ServiceProvider);
            if (_jcByDayRep == null)
                _jcByDayRep = ServiceProviderServiceExtensions.GetService<IJawCrusherByDayRepository>(BootStrap.ServiceProvider);
            if (_ccByDayRep == null)
                _ccByDayRep = ServiceProviderServiceExtensions.GetService<IConeCrusherByDayRepository>(BootStrap.ServiceProvider);
            if (_vcByDayRep == null)
                _vcByDayRep = ServiceProviderServiceExtensions.GetService<IVerticalCrusherByDayRepository>(BootStrap.ServiceProvider);
            if (_vbByDayRep == null)
                _vbByDayRep = ServiceProviderServiceExtensions.GetService<IVibrosieveByDayRepository>(BootStrap.ServiceProvider);
            if (_sccByDayRep == null)
                _sccByDayRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByDayRepository>(BootStrap.ServiceProvider);
            if (_pulByDayRep == null)
                _pulByDayRep = ServiceProviderServiceExtensions.GetService<IPulverizerByDayRepository>(BootStrap.ServiceProvider);
            if (_icByDayRep == null)
                _icByDayRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherByDayRepository>(BootStrap.ServiceProvider);
            if (_hvbByDayRep == null)
                _hvbByDayRep = ServiceProviderServiceExtensions.GetService<IHVibByDayRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 根据产线ID获取所有失联、停止、运行电机数目
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public virtual Tuple<int, int, int> GetMotors(string lineId)
        {
            var lose = 0;
            var stop = 0;
            var run = 0;
            var motors = _motorRep.GetEntities(e => e.ProductionLineId.Equals(lineId))?.ToList();
            if (motors == null || !motors.Any()) return new Tuple<int, int, int>(0, 0, 0);
            foreach (var motor in motors)
            {
                MotorStatus status;
                switch (motor.MotorTypeId)
                {
                    case "CY":
                        //var x = _cyRep.GetEntities("WDD-P001-CY000047", DateTime.Now, false).ToList().OrderByDescending(e => e.Time);
                        status = _cyRep.GetCurrentStatus(motor.MotorId,motor.IsBeltWeight);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "MF":
                        status = _mfRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "JC":
                        status = _jcRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "CC":
                        status = _ccRep.GetCurrentStatus(motor.MotorId);
                        //var x = _ccRep.GetEntities("WDD-P001-CC000001", DateTime.Now, false).ToList().OrderByDescending(e => e.Time);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "VC":
                        status = _vcRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "VB":
                        status = _vbRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "SCC":
                        status = _sccRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "PUL":
                        status = _pulRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "IC":
                        status = _icRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
                        break;
                    case "HVB":
                        status = _hvbRep.GetCurrentStatus(motor.MotorId);
                        lose = status.Equals(MotorStatus.Lose) ? lose + 1 : lose + 0;
                        stop = status.Equals(MotorStatus.Stop) ? stop + 1 : stop + 0;
                        run = status.Equals(MotorStatus.Run) ? run + 1 : run + 0;
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
            return line != null && now - line.Time <= 10 * 60;
        }

        /// <summary>
        /// 根据电机设备ID获取设备状态
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public virtual MotorStatus GetMotorStatusByMotorId(string motorId)
        {
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            if (motor == null) return MotorStatus.Lose;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyRep.GetCurrentStatus(motor.MotorId,motor.IsBeltWeight);

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
                    return MotorStatus.Stop;
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
        /// 根据动态数据获取设备历史详情项
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        public dynamic GetMotorHistoryDetails(IEnumerable<dynamic> datas, Motor motor)
        {
            var source = datas?.Where(e => e.RunningTime > 0) ?? null;
            if (source == null || !source.Any())
                return GetMotorDetails(motor.MotorTypeId);
            var loadStall = MathF.Round(source.Average(e => (float)e.LoadStall), 3);
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return new
                    {
                        AvgInstantWeight = MathF.Round(source.Average(e => (float)e.AvgInstantWeight), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };
                case "MF":
                    return new
                    {
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        AvgFrequency = MathF.Round((source.Average(e => (float)e.AvgFrequency)), 2),
                        LoadStall = loadStall,
                    };
                case "JC":
                    return new
                    {
                        AvgMotiveSpindleTemperature1 = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                        AvgMotiveSpindleTemperature2 = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                        AvgRackSpindleTemperature1 = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                        AvgRackSpindleTemperature2 = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                        AvgVibrate1 = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };
                case "CC":
                    return new
                    {
                        AvgMovaStress = MathF.Round(source.Average(e => (float)e.AvgMovaStress), 2),
                        AvgOilReturnTempreatur = MathF.Round((source.Average(e => (float)e.AvgOilReturnTempreatur)), 2),
                        AvgOilFeedTempreature = MathF.Round(source.Average(e => (float)e.AvgOilFeedTempreature), 2),
                        AvgTankTemperature = MathF.Round((source.Average(e => (float)e.AvgTankTemperature)), 2),
                        AvgSpindleTravel = MathF.Round(source.Average(e => (float)e.AvgSpindleTravel), 2),
                        AvgVibrate1 = MathF.Round((source.Average(e => (float)e.AvgVibrate1)), 2),
                        AvgVibrate2 = MathF.Round((source.Average(e => (float)e.AvgVibrate2)), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };

                case "VC":
                    return new
                    {
                        AvgVibrate1 = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = MathF.Round((source.Average(e => (float)e.AvgVibrate2)), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };
                case "VB":
                    return new
                    {
                        AvgSpindleTemperature4 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature4), 2),
                        AvgSpindleTemperature2 = MathF.Round((source.Average(e => (float)e.AvgSpindleTemperature2)), 2),
                        AvgSpindleTemperature1 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature1), 2),
                        AvgSpindleTemperature3 = MathF.Round((source.Average(e => (float)e.AvgSpindleTemperature3)), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };

                case "SCC":
                    return new
                    {
                        AvgCurrent = 0,
                        LoadStall = loadStall,
                    };

                case "PUL":
                    return new
                    {
                        AvgVibrate1 = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = MathF.Round((source.Average(e => (float)e.AvgVibrate2)), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };

                case "IC":
                    return new
                    {
                        AvgCurrent_B = MathF.Round((source.Average(e => (float)e.AvgCurrent_B)), 2),
                        //AvgMotor2Current_B = MathF.Round(source.Average(e => (float)e.AvgMotor2Current_B), 2),
                        //AvgMotor1Current_B = MathF.Round((source.Average(e => (float)e.AvgMotor1Current_B)), 2),
                        AvgSpindleTemperature1 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature1), 2),
                        AvgSpindleTemperature2 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature2), 2),
                        AvgVibrate1 = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                        AvgVibrate2 = MathF.Round((source.Average(e => (float)e.AvgVibrate2)), 2),
                        LoadStall = loadStall,
                    };
                case "HVB":
                    return new
                    {
                        AvgSpindleTemperature4 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature4), 2),
                        AvgSpindleTemperature2 = MathF.Round((source.Average(e => (float)e.AvgSpindleTemperature2)), 2),
                        AvgSpindleTemperature3 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature3), 2),
                        AvgSpindleTemperature1 = MathF.Round((source.Average(e => (float)e.AvgSpindleTemperature1)), 2),
                        AvgOilReturnStress = MathF.Round(source.Average(e => (float)e.AvgOilReturnStress), 2),
                        AvgOilFeedStress = MathF.Round((source.Average(e => (float)e.AvgOilFeedStress)), 2),
                        AvgCurrent = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                        LoadStall = loadStall,
                    };
                default:
                    return new
                    {
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
            }
        }
        /// <summary>
        /// 获取设备瞬时详情项
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public dynamic GetMotorInstantDetails(Motor motor)
        {
            dynamic lastRecord = null;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    lastRecord = _cyRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgInstantWeight = lastRecord?.InstantWeight ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.InstantWeight ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.InstantWeight / motor.StandValue, 3),
                    };
                case "MF":
                    lastRecord = _mfRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        AvgFrequency = lastRecord?.Frequency ?? 0,
                        LoadStall = (lastRecord?.Frequency ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Frequency / motor.StandValue, 3),
                    };
                case "JC":
                    lastRecord = _jcRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgMotiveSpindleTemperature1 = lastRecord?.MotiveSpindleTemperature1 ?? 0,
                        AvgMotiveSpindleTemperature2 = lastRecord?.MotiveSpindleTemperature2 ?? 0,
                        AvgRackSpindleTemperature1 = lastRecord?.RackSpindleTemperature1 ?? 0,
                        AvgRackSpindleTemperature2 = lastRecord?.RackSpindleTemperature2 ?? 0,
                        AvgVibrate1 = lastRecord?.Vibrate1 ?? 0,
                        AvgVibrate2 = lastRecord?.Vibrate2 ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };
                case "CC":
                    lastRecord = _ccRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgMovaStress = lastRecord?.MovaStress ?? 0,
                        AvgOilReturnTempreatur = lastRecord?.OilReturnTempreatur ?? 0,
                        AvgOilFeedTempreature = lastRecord?.OilFeedTempreature ?? 0,
                        AvgTankTemperature = lastRecord?.TankTemperature ?? 0,
                        AvgSpindleTravel = lastRecord?.SpindleTravel ?? 0,
                        AvgVibrate1 = lastRecord?.Vibrate1 ?? 0,
                        AvgVibrate2 = lastRecord?.Vibrate2 ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };

                case "VC":
                    lastRecord = _vcRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgVibrate1 = lastRecord?.Vibrate1 ?? 0,
                        AvgVibrate2 = lastRecord?.Vibrate2 ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };
                case "VB":
                    lastRecord = _vbRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgSpindleTemperature4 = lastRecord?.SpindleTemperature4 ?? 0,
                        AvgSpindleTemperature2 = lastRecord?.SpindleTemperature2 ?? 0,
                        AvgSpindleTemperature1 = lastRecord?.SpindleTemperature1 ?? 0,
                        AvgSpindleTemperature3 = lastRecord?.SpindleTemperature3 ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };

                case "SCC":
                    lastRecord = _sccRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };

                case "PUL":
                    lastRecord = _pulRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgVibrate1 = lastRecord?.Vibrate1 ?? 0,
                        AvgVibrate2 = lastRecord?.Vibrate2 ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };

                case "IC":
                    lastRecord = _icRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgCurrent_B = lastRecord?.Current_B ?? 0,
                        //AvgMotor2Current_B = lastRecord?.Motor2Current_B ?? 0,
                        //AvgMotor1Current_B = lastRecord?.Motor1Current_B ?? 0,
                        AvgSpindleTemperature1 = lastRecord?.SpindleTemperature1 ?? 0,
                        AvgSpindleTemperature2 = lastRecord?.SpindleTemperature2 ?? 0,
                        AvgVibrate1 = lastRecord?.Vibrate1 ?? 0,
                        AvgVibrate2 = lastRecord?.Vibrate2 ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };
                case "HVB":
                    lastRecord = _hvbRep.GetLatestRecord(motor.MotorId);
                    return new
                    {
                        AvgSpindleTemperature4 = lastRecord?.SpindleTemperature4 ?? 0,
                        AvgSpindleTemperature2 = lastRecord?.SpindleTemperature2 ?? 0,
                        AvgSpindleTemperature3 = lastRecord?.SpindleTemperature3 ?? 0,
                        AvgSpindleTemperature1 = lastRecord?.SpindleTemperature1 ?? 0,
                        AvgOilReturnStress = lastRecord?.OilReturnStress ?? 0,
                        AvgOilFeedStress = lastRecord?.OilFeedStress ?? 0,
                        AvgCurrent = lastRecord?.Current_B ?? 0,
                        LoadStall = (lastRecord?.Current_B ?? 0) * motor.StandValue == 0 ? 0 :
                                                MathF.Round(lastRecord.Current_B / motor.StandValue, 3),
                    };
                default:
                    return new
                    {
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
            }
        }
        /// <summary>
        /// 根据动态数据获取设备详情图表
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        public dynamic GetMotorSeries(IEnumerable<dynamic> datas, Motor motor)
        {
            if (datas == null || !datas.Any())
                return GetMotorSeries(motor.MotorTypeId);
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return new
                    {
                        AccumulativeWeight = MathF.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
                            Output = e.AccumulativeWeight,
                            Current = e.AvgCurrent_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                case "MF":
                    return new
                    {
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.AvgFrequency, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };
                case "JC":
                    return new
                    {
                        WearValue1 = MathF.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = MathF.Round(datas.Average(e => (float)e.WearValue2), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
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
                        WearValue1 = MathF.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = MathF.Round(datas.Average(e => (float)e.WearValue2), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
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
                        WearValue1 = MathF.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = MathF.Round(datas.Average(e => (float)e.WearValue2), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.WearValue2, e.WearValue1, e.AvgVibrate1, e.AvgVibrate2, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };
                case "VB":
                    return new
                    {
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
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
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { Output = e.AccumulativeWeight, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };

                case "PUL":
                    return new
                    {
                        WearValue1 = MathF.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = MathF.Round(datas.Average(e => (float)e.WearValue2), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new { e.WearValue2, e.WearValue1, e.AvgVibrate1, e.AvgVibrate2, Current = e.AvgCurrent_B, e.RunningTime, UnixTime = e.Time })
                    };

                case "IC":
                    return new
                    {
                        WearValue1 = MathF.Round(datas.Average(e => (float)e.WearValue1), 2),
                        WearValue2 = MathF.Round(datas.Average(e => (float)e.WearValue2), 2),
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
                            e.WearValue2,
                            e.WearValue1,
                            e.AvgVibrate1,
                            e.AvgVibrate2,
                            //e.AvgMotor2Current_B,
                            e.AvgCurrent_B,
                            e.AvgSpindleTemperature1,
                            e.AvgSpindleTemperature2,
                            //e.AvgMotor1Current_B,
                            e.RunningTime,
                            UnixTime = e.Time
                        })
                    };
                case "HVB":
                    return new
                    {
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                        SeriesData = datas.Select(e => new
                        {
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
                    return new
                    {
                        RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
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
        public virtual IEnumerable<dynamic> MotorHours(Motor motor, long date)
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
            var motors = _motorRep.GetEntities(e => e.ProductionLineId.EqualIgnoreCase(productionLineId));
            if (motors == null || !motors.Any()) return true;
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
                    result = _cyRep.PreCache(motor, dt);
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
        public int PreCache2(Motor motor, long dayUnix, long start, long end)
        {
            var result = 0;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    var list = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
                    if (list != null && list.Any())
                        result = _cyRep.Cache(motor.MotorId, dayUnix, list);
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
            var datas = new List<dynamic>();
            if (cache)
            {
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
            long start = date.TimeSpan(), end = date.AddDays(1).TimeSpan();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);
                case "MF":
                    return _mfRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);
                case "JC":
                    return _jcRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "CC":
                    return _ccRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "VC":
                    return _vcRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);
                case "VB":
                    return _vbRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "SCC":
                    return _sccRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "PUL":
                    return _pulRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "IC":
                    return _icRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

                case "HVB":
                    return _hvbRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end, e => e.Time);

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
            int length = (int)DateTime.Now.Subtract(now).TotalHours+1;
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
                    for (int i = 0; i < length; i++)
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
                    for (int i = 0; i < length; i++)
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
                var powerSum = MathF.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
                resp.Add(new PowerCal { ActivePower = powerSum, Time = (long)time });
            }
            return resp;
        }

        /// <summary>
        /// 计算历史区间内产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        public List<PowerCal> CalcMotorPowers(List<Motor> motors, long start, long end)
        {
            var length = end.Time().Subtract(start.Time()).TotalDays / 1;
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
            var groups = list?.OrderBy(e => e.Time)?.GroupBy(e => e.Time);
            if (groups != null && groups.Any())
                foreach (var item in groups)
                {
                    var time = item.Key;
                    var powerSum = MathF.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
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
                    for (int i = 0; i < 24; i++)
                    {
                        var time = dt.AddHours(i).TimeSpan();
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
                    var datas = MotorHours(motor, date);
                    var times = new List<long>();
                    for (int i = 0; i < 24; i++)
                    {
                        var time = dt.AddHours(i).TimeSpan();
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
                var powerSum = MathF.Round(item?.OrderBy(e => e.Time)?.ToList().Sum(e => (float)e.ActivePower) ?? 0, 2);
                resp.Add(new PowerCal { ActivePower = powerSum, Time = (long)time });
            }
            return resp;
        }

        /// <summary>
        /// 根据动态数据获取移动端设备详情
        /// </summary>
        /// <param name="datas">需要先排序</param>
        /// <param name="motor"></param>
        ///  <param name="isInstant"></param>
        /// <returns></returns>
        public dynamic GetMobileMotorDetails(IEnumerable<dynamic> datas, Motor motor, bool isInstant)
        {           
                //return GetMobileMotorDetails(motor.MotorTypeId);          
            var source = datas?.Where(e => e.RunningTime > 0)?.ToList() ?? new List<dynamic>();       
            float loadStall = 0;
            if (!isInstant)
            {
                if (source == null || !source.Any())
                    loadStall = 0;
                else
                    loadStall = MathF.Round(source.Average(e => (float)e.LoadStall), 3);
            }
            //add version-2018.8.3
            dynamic lastRecord = null;
            switch (motor.MotorTypeId)
            {
                case "CY":
                    if (datas == null || !datas.Any())
                        datas = new List<ConveyorByHour>() { new ConveyorByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new ConveyorByHour() { MotorId=motor.MotorId});
                    if (isInstant)
                    {
                        lastRecord = _cyRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.InstantWeight * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.InstantWeight / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                // AvgInstantWeight = MathF.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                                Output = MathF.Round(source.Sum(e => (float)e.AccumulativeWeight), 2),
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            outputSeries = new { name = motor.Name, data = datas.Select(e => new { e.AccumulativeWeight }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            // AvgInstantWeight = MathF.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                            Output = MathF.Round(source.Sum(e => (float)e.AccumulativeWeight), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        outputSeries = new { name = motor.Name, data = datas.Select(e => new { e.AccumulativeWeight }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                case "MF":
                    if (datas == null || !datas.Any())
                        datas = new List<MaterialFeederByHour>() { new MaterialFeederByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new MaterialFeederByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _mfRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Frequency * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Frequency / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                Frequency = lastRecord?.Frequency ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            freqSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgFrequency }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            Frequency = MathF.Round(source.Average(e => (float)e.AvgFrequency), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        freqSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgFrequency }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                case "JC":
                    if (datas == null || !datas.Any())
                        datas = new List<JawCrusherByHour>() { new JawCrusherByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new JawCrusherByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _jcRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                MST = lastRecord?.MotiveSpindleTemperature1 ?? 0,
                                MST2 = lastRecord?.MotiveSpindleTemperature2 ?? 0,
                                RST = lastRecord?.RackSpindleTemperature1 ?? 0,
                                RST2 = lastRecord?.RackSpindleTemperature2 ?? 0,
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    }
                    return new
                    {
                        outline = new
                        {
                            MST = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                            MST2 = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                            RST = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                            RST2 = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    if (datas == null || !datas.Any())
                        datas = new List<ConeCrusherByHour>() { new ConeCrusherByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new ConeCrusherByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _ccRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                TT = lastRecord?.TankTemperature ?? 0,
                                OFT = lastRecord?.OilFeedTempreature ?? 0,
                                ORT = lastRecord?.OilReturnTempreatur ?? 0,
                                STV = lastRecord?.SpindleTravel ?? 0,
                                MS = lastRecord?.MovaStress ?? 0,
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    }
                    return new
                    {
                        outline = new
                        {
                            TT = MathF.Round(source.Average(e => (float)e.AvgTankTemperature), 2),
                            OFT = MathF.Round(source.Average(e => (float)e.AvgOilFeedTempreature), 2),
                            ORT = MathF.Round(source.Average(e => (float)e.AvgOilReturnTempreatur), 2),
                            STV = MathF.Round(source.Average(e => (float)e.AvgSpindleTravel), 2),
                            MS = MathF.Round(source.Average(e => (float)e.AvgMovaStress), 2),
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    if (datas == null || !datas.Any())
                        datas = new List<VerticalCrusherByHour>() { new VerticalCrusherByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new VerticalCrusherByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _vcRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                            vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                case "VB":
                    if (datas == null || !datas.Any())
                        datas = new List<VibrosieveByHour>() { new VibrosieveByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new VibrosieveByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _vbRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                ST = lastRecord?.SpindleTemperature1 ?? 0,
                                ST2 = lastRecord?.SpindleTemperature2 ?? 0,
                                ST3 = lastRecord?.SpindleTemperature3 ?? 0,
                                ST4 = lastRecord?.SpindleTemperature4 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                            st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                            st3Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature3 }) },
                            st4Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature4 }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            ST = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            ST3 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature3), 2),
                            ST4 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature4), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    if (datas == null || !datas.Any())
                        datas = new List<SimonsConeCrusherByHour>() { new SimonsConeCrusherByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new SimonsConeCrusherByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _sccRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                TT = lastRecord?.MotiveSpindleTemperature1 ?? 0,
                                OFT = lastRecord?.MotiveSpindleTemperature2 ?? 0,
                                ORT = lastRecord?.RackSpindleTemperature1 ?? 0,
                                STV = lastRecord?.RackSpindleTemperature2 ?? 0,
                                MS = lastRecord?.RackSpindleTemperature2 ?? 0,
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
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
                    }
                    return new
                    {
                        outline = new
                        {
                            TT = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature1), 2),
                            OFT = MathF.Round(source.Average(e => (float)e.AvgMotiveSpindleTemperature2), 2),
                            ORT = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature1), 2),
                            STV = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            MS = MathF.Round(source.Average(e => (float)e.AvgRackSpindleTemperature2), 2),
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
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
                    if (datas == null || !datas.Any())
                        datas = new List<PulverizerByHour>() { new PulverizerByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new PulverizerByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _pulRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                            vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };

                case "IC":
                    if (datas == null || !datas.Any())
                        datas = new List<ImpactCrusherByHour>() { new ImpactCrusherByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new ImpactCrusherByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _icRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                ST = lastRecord?.SpindleTemperature1 ?? 0,
                                ST2 = lastRecord?.SpindleTemperature2 ?? 0,
                                //Current2 = lastRecord?.Motor2Current_B ?? 0,
                                VIB = lastRecord?.Vibrate1 ?? 0,
                                VIB2 = lastRecord?.Vibrate2 ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                            st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                            //current2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotor2Current_B }) },
                            vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                            vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },

                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            ST = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            Current2 = MathF.Round(source.Average(e => (float)e.AvgMotor2Current_B), 2),
                            VIB = MathF.Round(source.Average(e => (float)e.AvgVibrate1), 2),
                            VIB2 = MathF.Round(source.Average(e => (float)e.AvgVibrate2), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                        st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                        current2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgMotor2Current_B }) },
                        vibSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate1 }) },
                        vib2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgVibrate2 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },

                    };
                case "HVB":
                    if (datas == null || !datas.Any())
                        datas = new List<HVibByHour>() { new HVibByHour() { MotorId = motor.MotorId } };
                    if (source == null || !source.Any())
                        source.Add(new HVibByHour() { MotorId = motor.MotorId });
                    if (isInstant)
                    {
                        lastRecord = _hvbRep.GetLatestRecord(motor.MotorId);
                        loadStall = lastRecord == null ? 0 : (lastRecord.Current_B * motor.StandValue == 0 ? 0 :
                                            MathF.Round(lastRecord.Current_B / motor.StandValue, 3));
                        return new
                        {
                            outline = new
                            {
                                ST = lastRecord?.SpindleTemperature1 ?? 0,
                                ST2 = lastRecord?.SpindleTemperature2 ?? 0,
                                ST3 = lastRecord?.SpindleTemperature3 ?? 0,
                                ST4 = lastRecord?.SpindleTemperature4 ?? 0,
                                OFS = lastRecord?.OilFeedStress ?? 0,
                                ORS = lastRecord?.OilReturnStress ?? 0,
                                Current = lastRecord?.Current_B ?? 0,
                                Load = loadStall,
                                RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                            },
                            xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                            stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                            st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                            st3Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature3 }) },
                            st4Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature4 }) },
                            runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                            ofsSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilFeedStress }) },
                            orsSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilReturnStress }) },
                            currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                        };
                    }
                    return new
                    {
                        outline = new
                        {
                            ST = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature1), 2),
                            ST2 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature2), 2),
                            ST3 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature3), 2),
                            ST4 = MathF.Round(source.Average(e => (float)e.AvgSpindleTemperature4), 2),
                            OFS = MathF.Round(source.Average(e => (float)e.AvgOilFeedStress), 2),
                            ORS = MathF.Round(source.Average(e => (float)e.AvgOilReturnStress), 2),
                            Current = MathF.Round(source.Average(e => (float)e.AvgCurrent_B), 2),
                            Load = loadStall,
                            RunningTime = MathF.Round(source.Sum(e => (float)e.RunningTime), 2),
                        },
                        xAxis = new { categories = datas.Select(e => ((long)e.Time).Time()) },
                        stSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature1 }) },
                        st2Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature2 }) },
                        st3Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature3 }) },
                        st4Series = new { name = motor.Name, data = datas.Select(e => new { e.AvgSpindleTemperature4 }) },
                        runningtimeSeries = new { name = motor.Name, data = datas.Select(e => new { e.RunningTime }) },
                        ofsSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilFeedStress }) },
                        orsSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgOilReturnStress }) },
                        currentSeries = new { name = motor.Name, data = datas.Select(e => new { e.AvgCurrent_B }) },
                    };
                default:
                    return new
                    {
                        //AvgInstantWeight = MathF.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                        //AccumulativeWeight = MathF.Round(datas.Sum(e => (float)e.AccumulativeWeight), 2),
                        //AvgCurrent = MathF.Round(datas.Average(e => (float)e.AvgCurrent_B), 2),
                        //LoadStall = MathF.Round(datas.Average(e => (float)e.LoadStall), 2),
                        //RunningTime = MathF.Round(datas.Sum(e => (float)e.RunningTime), 2),
                    };
            }
        }


        #endregion

        #region version 18.07.17
        /// <summary>
        /// 根据电机设备获取电机设备瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual float MotorIntantLoadStall(Motor motor)
        {
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetInstantLoadStall(motor);
                case "MF":
                    return _mfByHourRep.GetInstantLoadStall(motor);
                case "JC":
                    return _jcByHourRep.GetInstantLoadStall(motor);

                case "CC":
                    return _ccByHourRep.GetInstantLoadStall(motor);

                case "VC":
                    return _vcByHourRep.GetInstantLoadStall(motor);

                case "VB":
                    return _vbByHourRep.GetInstantLoadStall(motor);

                case "SCC":
                    return _sccByHourRep.GetInstantLoadStall(motor);

                case "PUL":
                    return _pulByHourRep.GetInstantLoadStall(motor);

                case "IC":
                    return _icByHourRep.GetInstantLoadStall(motor);

                case "HVB":
                    return _hvbByHourRep.GetInstantLoadStall(motor);

                default:
                    return 0f;
            }
        }
        #endregion

        #region private method
        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="motorTypeId"></param>
        /// <returns></returns>
        private dynamic GetMotorSeries(string motorTypeId)
        {
            switch (motorTypeId)
            {
                case "CY":
                    return new
                    {
                        //AvgInstantWeight = 0,
                        AccumulativeWeight = 0,
                        //AvgCurrent = 0,
                        // LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            Output = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };
                case "MF":
                    return new
                    {
                        //AvgCurrent = 0,
                        //AvgFrequency = 0,
                        LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new { AvgFrequency = 0, Current = 0, AvgCurrent_B = 0, RunningTime = 0, UnixTime = 0 }
                    };
                case "JC":
                    return new
                    {
                        // AvgMotiveSpindleTemperature1 = 0,
                        // AvgMotiveSpindleTemperature2 = 0,
                        // AvgRackSpindleTemperature1 = 0,
                        // AvgRackSpindleTemperature2 = 0,
                        //  AvgVibrate1 = 0,
                        WearValue1 = 0,
                        // AvgVibrate2 = 0,
                        WearValue2 = 0,
                        //  AvgCurrent = 0,
                        //LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
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
                            UnixTime = 0
                        }
                    };
                case "CC":
                    return new
                    {
                        // AvgMovaStress = 0,
                        //  AvgOilReturnTempreatur = 0,
                        // AvgOilFeedTempreature = 0,
                        // AvgTankTemperature = 0,
                        //  AvgSpindleTravel = 0,
                        //   AvgVibrate1 = 0,
                        //  AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 = 0,
                        //  AvgCurrent = 0,
                        // LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
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
                            UnixTime = 0
                        }
                    };

                case "VC":
                    return new
                    {
                        WearValue1 = 0,
                        WearValue2 = 0,
                        //AvgVibrate1 = 0,
                        // AvgVibrate2 = 0,
                        // AvgCurrent = 0,
                        // LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            WearValue2 = 0,
                            WearValue1 = 0,
                            AvgVibrate = 01,
                            AvgVibrate2 = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };
                case "VB":
                    return new
                    {
                        //AvgSpindleTemperature4 = 0,
                        //AvgSpindleTemperature2 = 0,
                        //  AvgSpindleTemperature1 = 0,
                        //  AvgSpindleTemperature3 = 0,
                        //  AvgCurrent = 0,
                        // LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            AvgSpindleTemperature1 = 0,
                            AvgSpindleTemperature4 = 0,
                            AvgSpindleTemperature2 = 0,
                            AvgSpindleTemperature3 = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };

                case "SCC":
                    return new
                    {
                        // AvgInstantWeight = 0,
                        // AccumulativeWeight = 0,
                        // AvgCurrent = 0,
                        // LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            // Output = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };

                case "PUL":
                    return new
                    {
                        // AvgVibrate1 = 0,
                        // AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 = 0,
                        //  AvgCurrent = 0,
                        //  LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            WearValue2 = 0,
                            WearValue1 = 0,
                            AvgVibrate1 = 0,
                            AvgVibrate2 = 0,
                            Current = 0,
                            RunningTime = 0,
                            UnixTime = 0
                        }
                    };

                case "IC":
                    return new
                    {
                        //AvgMotor2Current_B = 0,
                        //AvgMotor1Current_B = 0,
                        //AvgSpindleTemperature1 = 0,
                        //AvgSpindleTemperature2 = 0,
                        //AvgVibrate1 = 0,
                        //AvgVibrate2 = 0,
                        WearValue1 = 0,
                        WearValue2 = 0,
                        //LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            WearValue2 = 0,
                            WearValue1 = 0,
                            AvgVibrate1 = 0,
                            AvgVibrate2 = 0,
                            //AvgMotor2Current_B = 0,
                            AvgCurrent_B = 0,
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
                        //AvgSpindleTemperature4 = 0,
                        //AvgSpindleTemperature2 = 0,
                        //AvgSpindleTemperature3 = 0,
                        //AvgSpindleTemperature1 = 0,
                        //AvgOilReturnStress = 0,
                        //AvgOilFeedStress = 0,
                        //AvgCurrent = 0,
                        //LoadStall = 0,
                        RunningTime = 0,
                        SeriesData = new
                        {
                            AvgSpindleTemperature4 = 0,
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
                        //AvgInstantWeight = 0,
                        // AccumulativeWeight = 0,
                        // AvgCurrent = 0,
                        //LoadStall = 0,
                        RunningTime = 0,
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
                        //AccumulativeWeight = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
                case "MF":
                    return new
                    {
                        AvgCurrent = 0,
                        AvgFrequency = 0,
                        LoadStall = 0,
                    };
                case "JC":
                    return new
                    {
                        AvgMotiveSpindleTemperature1 = 0,
                        AvgMotiveSpindleTemperature2 = 0,
                        AvgRackSpindleTemperature1 = 0,
                        AvgRackSpindleTemperature2 = 0,
                        AvgVibrate1 = 0,
                        //WearValue1 = 0,
                        AvgVibrate2 = 0,
                        // WearValue2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
                case "CC":
                    return new
                    {
                        AvgMovaStress = 0,
                        AvgOilReturnTempreatur = 0,
                        AvgOilFeedTempreature = 0,
                        AvgTankTemperature = 0,
                        AvgSpindleTravel = 0,
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        //WearValue1 = 0,
                        //WearValue2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };

                case "VC":
                    return new
                    {
                        //WearValue1 = 0,
                        //WearValue2 = 0,
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
                case "VB":
                    return new
                    {
                        AvgSpindleTemperature4 = 0,
                        AvgSpindleTemperature2 = 0,
                        AvgSpindleTemperature1 = 0,
                        AvgSpindleTemperature3 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };

                case "SCC":
                    return new
                    {
                        // AvgInstantWeight = 0,
                        //AccumulativeWeight = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };

                case "PUL":
                    return new
                    {
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        //WearValue1 = 0,
                        //WearValue2 = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };

                case "IC":
                    return new
                    {
                        //AvgMotor2Current_B = 0,
                        AvgCurrent_B = 0,
                        AvgSpindleTemperature1 = 0,
                        AvgSpindleTemperature2 = 0,
                        AvgVibrate1 = 0,
                        AvgVibrate2 = 0,
                        //WearValue1 = 0,
                        // WearValue2 = 0,
                        LoadStall = 0,
                    };
                case "HVB":
                    return new
                    {
                        AvgSpindleTemperature4 = 0,
                        AvgSpindleTemperature2 = 0,
                        AvgSpindleTemperature3 = 0,
                        AvgSpindleTemperature1 = 0,
                        AvgOilReturnStress = 0,
                        AvgOilFeedStress = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
                    };
                default:
                    return new
                    {
                        //AvgInstantWeight = 0,
                        //AccumulativeWeight = 0,
                        AvgCurrent = 0,
                        LoadStall = 0,
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
                            // AvgInstantWeight = MathF.Round(datas.Average(e => (float)e.AvgInstantWeight), 2),
                            Output = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        outputSeries = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };
                case "MF":
                    return new
                    {
                        outline = new
                        {
                            Frequency = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
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
                            Load = 0,
                            RunningTime = 0,
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
                            Current = 0,
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
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        vibSeries = new { },
                        vib2Series = new { },
                        runningtimeSeries = new { },
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
                            ST4 = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        stSeries = new { },
                        st2Series = new { },
                        st3Series = new { },
                        st4Series = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };

                case "SCC":
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
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
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
                        xAxis = new { },
                        vibSeries = new { },
                        vib2Series = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },
                    };

                case "IC":
                    return new
                    {
                        outline = new
                        {
                            ST = 0,
                            ST2 = 0,
                            Current2 = 0,
                            VIB = 0,
                            VIB2 = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        stSeries = new { },
                        st2Series = new { },
                        current2Series = new { },
                        vibSeries = new { },
                        vib2Series = new { },
                        runningtimeSeries = new { },
                        currentSeries = new { },

                    };
                case "HVB":
                    return new
                    {
                        outline = new
                        {
                            ST = 0,
                            ST2 = 0,
                            ST3 = 0,
                            ST4 = 0,
                            OFS = 0,
                            ORS = 0,
                            Current = 0,
                            Load = 0,
                            RunningTime = 0,
                        },
                        xAxis = new { },
                        stSeries = new { },
                        st2Series = new { },
                        st3Series = new { },
                        st4Series = new { },
                        runningtimeSeries = new { },
                        ofsSeries = new { },
                        orsSeries = new { },
                        currentSeries = new { },
                    };
                default:
                    return new
                    {
                    };
            }
        }
        #endregion

        #region test
        public virtual dynamic test()
        {
            return GetEntities();
        }
        #endregion

        #region shift
        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情(其中皮带机为班次)
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="shiftStart">班次起始小时时间</param>
        /// <returns></returns>
        public virtual dynamic MotorShiftDetails(Motor motor,int shiftStart)
        {
            // var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motor.motorId)).SingleOrDefault();
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetShiftRealData(motor,shiftStart);
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
        /// 根据电机设备ID获取当日电机设备详情(其中皮带机为班次)
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="shiftStart">班次起始小时时间</param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorShiftHours(Motor motor,int shiftStart)
        {
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    return _cyByHourRep.GetShiftRealDatas(motor,shiftStart);
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
        /// 根据电机设备ID获取历史某一天电机设备详情(其中皮带机为班次)
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorShiftHours(Motor motor, long date, int shiftStart)
        {
            long startUnix = date, endUnix = date.Time().AddDays(1).TimeSpan();
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    var end = date.Time().Date.AddDays(1).AddHours(shiftStart).TimeSpan();
                    return _cyByHourRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time < end).ToList();
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
        /// 根据电机设备ID和时间节点获取电机设备详情(不包括今天)(其中皮带机为班次)
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="shiftStart">班次起始小时时间</param>
        /// <param name="shiftEnd">班次结束小时时间（不包含）</param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> MotorShifts(Motor motor, long start, long end, int shiftStart, int shiftEnd)
        {
            if (motor == null) return new List<dynamic>();
            dynamic list = new List<dynamic>();
            switch (motor.MotorTypeId)
            {
                case "CY":
                    list = _cyByHourRep.GetHistoryShiftSomeData(motor, start, end, shiftStart, shiftEnd)?.ToList(); //_cyByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&e.Time <= end).ToList();
                    return list;
                case "MF":
                    list = _mfByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();                   

                    return list;

                case "JC":
                    list = _jcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "CC":
                    list = _ccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "VC":
                    list = _vcByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                      e.Time <= end).ToList();

                    return list;

                case "VB":
                    list = _vbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "SCC":
                    list = _sccByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "PUL":
                    list = _pulByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "IC":
                    list = _icByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                case "HVB":
                    list = _hvbByDayRep.GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start &&
                     e.Time <= end).ToList();

                    return list;

                default:
                    return list;
            }
        }

        #endregion
    }
}
