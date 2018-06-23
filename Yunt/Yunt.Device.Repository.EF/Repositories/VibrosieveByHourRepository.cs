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
            var dt3 = start.AddHours(-1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan(), dt3Unix = dt3.TimeSpan();
            var originalDatas = _ccRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Current_B > -1 && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any() ?? false)) return new VibrosieveByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            };
            #region 电能计算
            var first = originalDatas[0];
            //上一个小时的最后一条记录;
            var lastRecord = _ccRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= dt3Unix &&
            e.Time < endUnix, e => e.Time)?.LastOrDefault();
            var length = originalDatas?.Count() ?? 0;
            double lastPower = 0;
            double powerSum = 0;
            //获取上一个有效电能的值      
            if (lastRecord != null && lastRecord.ActivePower != -1 &&
               first.ActivePower - lastRecord.ActivePower >= 0)
            {
                lastPower = lastRecord.ActivePower;
            }
            for (var i = 0; i < length; i++)
            {
                var item = originalDatas[i];
                //电能
                if (item.ActivePower == -1 || item.ActivePower < lastPower)
                {
                    lastPower = item.ActivePower;
                    continue;
                }
                //电能
                var subPower = item.ActivePower - lastPower;
                lastPower = item.ActivePower;
                powerSum += subPower;
            }
            #endregion
            var Average = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2);
            var entity = new VibrosieveByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.PowerFactor), 2),
                AvgSpindleTemperature1 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature1), 2),
                AvgSpindleTemperature2 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature2), 2),
                AvgSpindleTemperature3 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature3), 2),
                AvgSpindleTemperature4 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature4), 2),
                RunningTime = originalDatas.Count(c => c.Current_B > 0),
                ActivePower = (float)Math.Round(powerSum, 2),
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
        /// <param name="motor"></param>
        public VibrosieveByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            var standValue = motor?.StandValue ?? 0;
            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var average = (float)Math.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new VibrosieveByDay
            {
                MotorId = motor.MotorId,
                AvgCurrent_B = average,
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),

                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
            };

            return data;
        }

        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        public IEnumerable<VibrosieveByHour> GetRealDatas(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).FirstOrDefault();

            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            return hourData;
        }
        #endregion

        #region assitant method
        /// <summary>
        ///恢复该小时内所有的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task RecoveryHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<VibrosieveByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == hour && o.MotorId == motor.MotorId)?.ToList();
                if (exsit?.Any()??false)
                    await DeleteEntityAsync(exsit);
                var t = GetByMotor(motor, false, dt);
                if (t != null)
                    ts.Add(t);
            }
            await InsertAsync(ts);
        }
        #endregion
    }
}
