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
    public class JawCrusherByDayRepository : DeviceRepositoryBase<JawCrusherByDay, Models.JawCrusherByDay>, IJawCrusherByDayRepository
    {

        private readonly IJawCrusherByDayRepository _ccRep;
        private readonly IMotorRepository _motorRep;

        public JawCrusherByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _ccRep = ServiceProviderServiceExtensions.GetService<IJawCrusherByDayRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public JawCrusherByDay GetByMotorId(string motorId, DateTime dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1);

            var originalDatas = _ccRep.GetEntities(e => e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (!(originalDatas?.Any() ?? false)) return null;

            var average = (float)Math.Round(originalDatas.Average(o => o.AvgCurrent_B), 2);
            var entity = new JawCrusherByDay
            {
                Time = start.TimeSpan(),
                MotorId = motorId,
                AvgCurrent_B = average,
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.AvgVoltage_B), 2),
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.AvgPowerFactor), 2),

                AvgRackSpindleTemperature1 = (float)Math.Round(originalDatas.Average(o => o.AvgRackSpindleTemperature1), 2),
                AvgMotiveSpindleTemperature1 = (float)Math.Round(originalDatas.Average(o => o.AvgMotiveSpindleTemperature1), 2),
                AvgMotiveSpindleTemperature2 = (float)Math.Round(originalDatas.Average(o => o.AvgMotiveSpindleTemperature2), 2),
                AvgRackSpindleTemperature2 = (float)Math.Round(originalDatas.Average(o => o.AvgRackSpindleTemperature2), 2),

                RunningTime = originalDatas.Sum(c => c.RunningTime),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
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
            var ts = new List<JawCrusherByDay>();
            var day = dt.Date;
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time.CompareTo(day) == 0 && o.MotorId == motor.MotorId).Any();
                if (exsit)
                    continue;
                var t = GetByMotorId(motor.MotorId, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }
        #endregion

    }
}
