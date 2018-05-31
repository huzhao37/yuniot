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
    public class VibrosieveByHourRepository : DeviceRepositoryBase<VibrosieveByHour, Models.VibrosieveByHour>, IVibrosieveByHourRepository
    {

        private readonly IVibrosieveRepository _ccRep;
        private readonly IMotorRepository _motorRep;

        public VibrosieveByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _ccRep = ServiceProviderServiceExtensions.GetService<IVibrosieveRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的振动筛数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public VibrosieveByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _ccRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Current_B > -1 && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any() ?? false)) return null;

            var Average = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2);
            var entity = new VibrosieveByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.PowerFactor), 2),

                RunningTime = originalDatas.Count(c => c.Current_B > 0),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(Average / standValue, 2)
            };
            return entity;

        }
        /// <summary>
        /// 统计该小时内所有振动筛的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<VibrosieveByHour>();
            var hour =dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time == hour && o.MotorId == motor.MotorId).Any();
                if (exsit)
                    continue;
                var t = GetByMotor(motor, false, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }

        /// <summary>
        /// 获取当日实时数据
        /// </summary>
        /// <param name="motorId"></param>
        public VibrosieveByDay GetRealData(string motorId)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            var standValue = motor?.StandValue ?? 0;
            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var average = (float)Math.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new VibrosieveByDay
            {
                MotorId = motorId,
                AvgCurrent_B = average,
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),

                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
            };

            return data;
        }
        #endregion


    }
}
