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
    public class SimonsConeCrusherByDayRepository : DeviceRepositoryBase<SimonsConeCrusherByDay, Models.SimonsConeCrusherByDay>, ISimonsConeCrusherByDayRepository
    {

        private readonly ISimonsConeCrusherByHourRepository _sccRep;
        private readonly IMotorRepository _motorRep;

        public SimonsConeCrusherByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _sccRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public SimonsConeCrusherByDay GetByMotor(Motor motor, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _sccRep.GetEntities(e => e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any() ?? false)) return new SimonsConeCrusherByDay
            {
                Time = start.TimeSpan(),
                MotorId = motor.MotorId,
            };

            var average = (float)Math.Round(originalDatas.Average(o => o.AverageCurrent), 2);
            var entity = new SimonsConeCrusherByDay
            {
                Time = start.TimeSpan(),
                MotorId = motor.MotorId,
                AverageCurrent = average,
                AverageOilFeedTempreature = (float)Math.Round(originalDatas.Average(o => o.AverageOilFeedTempreature), 2),
                AverageOilReturnTempreature = (float)Math.Round(originalDatas.Average(o => o.AverageOilReturnTempreature), 2),
                AverageTankTemperature = (float)Math.Round(originalDatas.Average(o => o.AverageTankTemperature), 2),

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
            var ts = new List<SimonsConeCrusherByDay>();
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
            var ts = new List<SimonsConeCrusherByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == day && o.MotorId == motor.MotorId)?.ToList();
                if (exsit?.Any()??false)
                    await DeleteEntityAsync(exsit);
                var t = GetByMotor(motor, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }
        #endregion
    }
}
