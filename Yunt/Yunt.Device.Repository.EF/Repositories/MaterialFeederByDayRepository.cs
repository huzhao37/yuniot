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
    public class MaterialFeederByDayRepository : DeviceRepositoryBase<MaterialFeederByDay, Models.MaterialFeederByDay>, IMaterialFeederByDayRepository
    {

        private readonly IMaterialFeederByHourRepository _mfRep;
        private readonly IMotorRepository _motorRep;

        public MaterialFeederByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _mfRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        public MaterialFeederByDay GetByMotorId(string motorId, DateTime dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date;
            var end = start.AddDays(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _mfRep.GetEntities(e => e.Time >= startUnix &&
                                    e.Time< endUnix, e => e.Time);

            if (!(originalDatas?.Any() ?? false)) return null;

            var avgFre = (float) Math.Round(originalDatas.Average(o => o.AvgFrequency), 2);
            var entity = new MaterialFeederByDay
            {
                Time = start.TimeSpan(),
                MotorId = motorId,
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.AvgCurrent_B), 2),
                AvgFrequency = avgFre,

                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.AvgVoltage_B), 2),

                RunningTime = originalDatas.Sum(c => c.AvgVoltage_B),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(avgFre / standValue, 2)
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
            var ts = new List<MaterialFeederByDay>();
            var day = dt.Date.TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time == day && o.MotorId == motor.MotorId).Any();
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
