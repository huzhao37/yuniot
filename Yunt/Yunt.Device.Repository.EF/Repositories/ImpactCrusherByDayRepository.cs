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
    public class ImpactCrusherByDayRepository : DeviceRepositoryBase<ImpactCrusherByDay, Models.ImpactCrusherByDay>, IImpactCrusherByDayRepository
    {

        private readonly IImpactCrusherByHourRepository _icRep;
        private readonly IMotorRepository _motorRep;

        public ImpactCrusherByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _icRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public ImpactCrusherByDay GetByMotor(Motor motor, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _icRep.GetEntities(e => e.Time >= startUnix &&
                                    e.Time< endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any() ?? false)) return new ImpactCrusherByDay
            {
                Time = start.TimeSpan(),
                MotorId = motor.MotorId,
            };

            var average = MathF.Round(originalDatas.Average(o => o.AvgMotor1Current_B), 2);
            var entity = new ImpactCrusherByDay
            {
                Time = start.TimeSpan(),
                MotorId = motor.MotorId,
                AvgMotor1Current_B = average,
                AvgMotor2Current_B = MathF.Round(originalDatas.Average(o => o.AvgMotor2Current_B), 2),
                AvgSpindleTemperature1 = MathF.Round(originalDatas.Average(o => o.AvgSpindleTemperature1), 2),
                AvgSpindleTemperature2 = MathF.Round(originalDatas.Average(o => o.AvgSpindleTemperature2), 2),
                AvgMotor1Voltage_B = MathF.Round(originalDatas.Average(o => o.AvgMotor1Voltage_B), 2),
                AvgMotor2Voltage_B= MathF.Round(originalDatas.Average(o => o.AvgMotor2Voltage_B), 2),
                AvgVibrate1 = MathF.Round(originalDatas.Average(o => o.AvgVibrate1), 2),
                AvgVibrate2 = MathF.Round(originalDatas.Average(o => o.AvgVibrate2), 2),
                WearValue1 = MathF.Round(originalDatas.Average(o => o.WearValue1), 2),
                WearValue2 = MathF.Round(originalDatas.Average(o => o.WearValue2), 2),
                //OnOffCounts = offCounts,
                RunningTime = originalDatas.Sum(e=>e.RunningTime),
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
            var ts = new List<ImpactCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time== day && o.MotorId == motor.MotorId).Any();
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
            var ts = new List<ImpactCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == day && o.MotorId == motor.MotorId)?.ToList();
                if (exsit?.Any()??false)
                     DeleteEntity(exsit);
                var t = GetByMotor(motor, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }
        #endregion
    }
}
