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
    public class ConveyorByDayRepository : DeviceRepositoryBase<ConveyorByDay, Models.ConveyorByDay>, IConveyorByDayRepository
    {

        private readonly IConveyorByHourRepository _cyRep;
        private readonly IMotorRepository _motorRep;

        public ConveyorByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _cyRep = ServiceProviderServiceExtensions.GetService<IConveyorByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method

        /// <summary>
        /// 统计该当日的皮带机数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public ConveyorByDay GetByMotorId(string motorId, DateTime dt)
        {
            var motor = _motorRep.GetEntities(e => e.MotorId.EqualIgnoreCase(motorId))?.FirstOrDefault() ?? null;
            if (motor == null)
                return null;
            var cap = motor?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1);

            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas =
                _cyRep.GetEntities(
                    o =>
                        o.Time >= startUnix && o.Time <= endUnix && o.MotorId.Equals(motorId) &&
                        o.AccumulativeWeight > -1, o => o.Time);

            if (originalDatas == null || !originalDatas.Any()) return null;

            var count = originalDatas.Sum(e => e.RunningTime);
            var weightSum = (float) Math.Round(originalDatas.Sum(o => o.AccumulativeWeight), 2);
            var powerSum = (float)Math.Round(originalDatas.Sum(e => e.ActivePower), 2);
          
            var load = motor.UseCalc
             ? Math.Round(((powerSum * 60) / count) / cap, 2)
             : Math.Round(((weightSum * 60) / count) / cap, 2);
            var entity = new ConveyorByDay()
            {
                Time = start.TimeSpan(),
                MotorId = motorId,
                AvgInstantWeight = (float)Math.Round(originalDatas.Average(e=>e.AvgInstantWeight), 2),
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.AvgCurrent_B), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.AvgVoltage_B), 2),
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.AvgPowerFactor), 2),
                AccumulativeWeight = weightSum, //TODO：累计称重计算;
                ActivePower =powerSum,
                RunningTime = count,
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = count*cap == 0?0:(float)load
            };
            return entity;



        }
        /// <summary>
        /// 统计该当日内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertDayStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConveyorByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time==day && o.MotorId == motor.MotorId).Any();
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
