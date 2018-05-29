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
    public class MaterialFeederByHourRepository : DeviceRepositoryBase<MaterialFeederByHour, Models.MaterialFeederByHour>, IMaterialFeederByHourRepository
    {

        private readonly IMaterialFeederRepository _mfRep;
        private readonly IMotorRepository _motorRep;

        public MaterialFeederByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _mfRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的给料机破数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public MaterialFeederByHour GetByMotorId(string motorId, bool isExceed, DateTime dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _mfRep.GetEntities(motorId, dt, isExceed, e => e.Frequency > 0 && e.Time>= startUnix &&
                                    e.Time < endUnix, e => e.Time);

            if (!(originalDatas?.Any() ?? false))return null;
            
            var average = (float)Math.Round(originalDatas.Average(o => o.Frequency), 2);
            var entity = new MaterialFeederByHour
            {
                Time = startUnix,
                MotorId = motorId,
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgFrequency = (float)Math.Round(originalDatas.Where(c => c.Frequency > 0).Average(o => o.Frequency), 2),
               
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),

                RunningTime = originalDatas.Count(c => c.Frequency > 0),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
            };
            return entity;




        }
        /// <summary>
        /// 统计该小时内所有给料机破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string MotorTypeId)
        {
            var ts = new List<MaterialFeederByHour>();
            var hour = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(MotorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time== hour && o.MotorId == motor.MotorId).Any();
                if (exsit)
                    continue;
                var t = GetByMotorId(motor.MotorId, false, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }

        /// <summary>
        /// 获取当日实时数据
        /// </summary>
        /// <param name="motorId"></param>
        public MaterialFeederByDay GetRealData(string motorId)
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

            var minuteData = GetByMotorId(motorId, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            var average = (float)Math.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new MaterialFeederByDay
            {
                MotorId = motorId,
                AvgCurrent_B = average,
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
                AvgFrequency = (float)Math.Round(hourData?.Average(e => e.AvgFrequency) ?? 0, 2),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
            };

            return data;
        }
        #endregion



    }
}
