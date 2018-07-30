using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Common;
using Yunt.Redis;
using AutoMapper.XpressionMapper.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ConeCrusherByDayRepository : DeviceRepositoryBase<ConeCrusherByDay, Models.ConeCrusherByDay>, IConeCrusherByDayRepository
    {

        private readonly IConeCrusherByHourRepository _ccRep;
        private readonly IMotorRepository _motorRep;

        public ConeCrusherByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _ccRep = ServiceProviderServiceExtensions.GetService<IConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public ConeCrusherByDay GetByMotor(Motor motor, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1).TimeSpan();
            var startUnix = start.TimeSpan();
            var originalDatas = _ccRep.GetEntities(e =>e.MotorId.Equals(motor.MotorId)&&e.Time >= startUnix &&
                                    e.Time < end, e => e.Time)?.ToList();
       
            if (!(originalDatas?.Any() ?? false))
                return new ConeCrusherByDay
                {
                    Time = startUnix,
                    MotorId = motor.MotorId
                };

            var average = MathF.Round(originalDatas.Average(o => o.AvgCurrent_B), 2);
            var entity = new ConeCrusherByDay
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgCurrent_B = average,
                AvgMovaStress = MathF.Round(originalDatas.Average(o => o.AvgMovaStress), 2),
                AvgOilFeedTempreature = MathF.Round(originalDatas.Average(o => o.AvgOilFeedTempreature), 2),
                AvgSpindleTravel = MathF.Round(originalDatas.Average(o => o.AvgSpindleTravel), 2),
                AvgAbsSpindleTravel = MathF.Round(originalDatas.Average(o => o.AvgAbsSpindleTravel), 2),
                AvgTankTemperature = MathF.Round(originalDatas.Average(o => o.AvgTankTemperature), 2),
                AvgCurrent_A = MathF.Round(originalDatas.Average(o => o.AvgCurrent_A), 2),
                AvgCurrent_C = MathF.Round(originalDatas.Average(o => o.AvgCurrent_C), 2),
                AvgOilReturnTempreatur = MathF.Round(originalDatas.Average(o => o.AvgOilReturnTempreatur), 2),
                AvgPowerFactor = MathF.Round(originalDatas.Average(o => o.AvgPowerFactor), 2),
                AvgVibrate1 = MathF.Round(originalDatas.Average(o => o.AvgVibrate1), 2),
                AvgVibrate2 = MathF.Round(originalDatas.Average(o => o.AvgVibrate2), 2),
                ActivePower = MathF.Round(originalDatas.Sum(o => o.ActivePower), 2),
                AvgVoltage_A = MathF.Round(originalDatas.Average(o => o.AvgVoltage_A), 2),
                AvgVoltage_B = MathF.Round(originalDatas.Average(o => o.AvgVoltage_B), 2),
                AvgVoltage_C = MathF.Round(originalDatas.Average(o => o.AvgVoltage_C), 2),
                WearValue1 = MathF.Round(originalDatas.Sum(o => o.WearValue1), 2),
                WearValue2=MathF.Round(originalDatas.Sum(o => o.WearValue2), 2),
                RunningTime = MathF.Round(originalDatas.Sum(o => o.RunningTime), 2),
                LoadStall = (standValue == 0) ? 0 : MathF.Round(average / standValue, 2)
            };
            return entity;

        }
        /// <summary>
        /// 统计该当日内所有圆锥破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertDayStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConeCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time == day && o.MotorId == motor.MotorId).Any();
                if (exsit)
                    continue;
                var t = GetByMotor(motor, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }
        #endregion

        #region assitant method
        /// <summary>
        ///恢复该小时内所有的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task RecoveryDayStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConeCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit=GetEntities(o => o.Time == day && o.MotorId == motor.MotorId)?.ToList();
                if (exsit?.Any()??false)
                     DeleteEntity(exsit);
                var t = GetByMotor(motor, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }  /// <summary>
           ///恢复该小时内所有的电量数据;
           /// </summary>
           /// <param name="dt">时间</param>
           /// <param name="motorTypeId">设备类型</param>
        public async Task UpdatePowers(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConeCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == day && o.MotorId == motor.MotorId)?.FirstOrDefault();
                if (exsit != null)
                {
                    var t = GetByMotor(motor, dt);
                    if (t != null)
                    {
                        exsit.ActivePower = t.ActivePower;
                        ts.Add(exsit);
                    }

                }

            }

            await UpdateEntityAsync(ts);
        }
        #endregion
    }
}
