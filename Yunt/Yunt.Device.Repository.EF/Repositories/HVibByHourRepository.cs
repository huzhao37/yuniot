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
    public class HVibByHourRepository : DeviceRepositoryBase<HVibByHour, Models.HVibByHour>, IHVibByHourRepository
    {

        private readonly IHVibRepository _hvibRep;
        private readonly IMotorRepository _motorRep;

        public HVibByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _hvibRep = ServiceProviderServiceExtensions.GetService<IHVibRepository>(BootStrap.ServiceProvider);
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
        public HVibByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            var dt3 = start.AddHours(-1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan(), dt3Unix = dt3.TimeSpan();
        
#if DEBUG
            var originalDatas = _hvibRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Current_B > -1f && e.Time >= startUnix &&
                                e.Time < endUnix, e => e.Time)?.ToList();

            var originalDatas2 = _hvibRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.ActivePower >0f && e.Time >= startUnix &&
                                e.Time < endUnix, e => e.Time)?.ToList();
#else
               var originalDatas = _hvibRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Current_B > -1f && e.Time >= startUnix &&
                                    e.Time< endUnix, e => e.Time)?.ToList();
            var originalDatas2 = _hvibRep.GetEntities(motor.MotorId, dt, isExceed, e => e.ActivePower > 0f && e.Time >= startUnix &&
                                    e.Time< endUnix, e => e.Time)?.ToList();
#endif
            if (!(originalDatas?.Any() ?? false) && !(originalDatas2?.Any() ?? false)) return new HVibByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            } ;
            #region 电能计算
            var first = !(originalDatas2?.Any() ?? false) ? new HVib()
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            } : originalDatas2[0];
            //上一个小时的最后一条记录;
#if DEBUG
            var lastRecord = _hvibRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.ActivePower > 0f&& e.Time >= dt3Unix &&
             e.Time < startUnix, e => e.Time)?.LastOrDefault();
#else           
            var lastRecord = _hvibRep.GetEntities(motor.MotorId, dt3, isExceed, e =>  e.ActivePower > 0f&&e.Time >= dt3Unix &&
            e.Time < startUnix, e => e.Time)?.LastOrDefault();
#endif
            var length = originalDatas2?.Count() ?? 0;
            double lastPower = 0;
            double powerSum = 0;
            //获取上一个有效电能的值      
            if (lastRecord != null && lastRecord.ActivePower >0 &&
               first.ActivePower - lastRecord.ActivePower >= 0)
            {
                lastPower = lastRecord.ActivePower;
            }
            for (var i = 0; i < length; i++)
            {
                var item = originalDatas2[i];
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
            var average = MathF.Round(originalDatas.Average(o => o.Current_B), 2);

            var load = (standValue == 0) ? 0 : MathF.Round(average / standValue, 2);
            load = double.IsNaN(load) ? 0 : load;

            var entity = new HVibByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgCurrent_B = MathF.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgVoltage_B = MathF.Round(originalDatas.Average(o => o.Voltage_B), 2),
                AvgPowerFactor = MathF.Round(originalDatas.Average(o => o.PowerFactor), 2),
                AvgOilFeedStress = MathF.Round(originalDatas.Average(o => o.OilFeedStress), 2),
                AvgOilReturnStress = MathF.Round(originalDatas.Average(o => o.OilReturnStress), 2),
                AvgSpindleTemperature1 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature1), 2),
                AvgSpindleTemperature2 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature2), 2),
                AvgSpindleTemperature3 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature3), 2),
                AvgSpindleTemperature4 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature4), 2),
                ActivePower = (float)Math.Round(powerSum, 2),
                RunningTime = originalDatas.Count(c => c.Current_B > 0f),
                LoadStall = load
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
            var ts = new List<HVibByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
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
        public HVibByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

           // var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            //var standValue = motor?.StandValue ?? 0;
            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            var average = MathF.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new HVibByDay
            {
                MotorId = motor.MotorId,
                AvgCurrent_B = average,
                RunningTime = MathF.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),

                LoadStall = GetInstantLoadStall(motor)//(standValue == 0) ? 0 : MathF.Round(average / standValue, 2)
            };

            return data;
        }

        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        public IEnumerable<HVibByHour> GetRealDatas(Motor motor)
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
            var ts = new List<HVibByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == hour && o.MotorId == motor.MotorId)?.ToList();
                if (exsit?.Any()??false)
                     DeleteEntity(exsit);
                var t = GetByMotor(motor, false, dt);
                if (t != null)
                    ts.Add(t);
            }
            await InsertAsync(ts);
        }
        #endregion

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        public float GetInstantLoadStall(Motor motor)
        {
            var data = _hvibRep.GetLatestRecord(motor.MotorId);
            if (data != null)
                return data.Current_B * motor.StandValue == 0 ? 0 : MathF.Round(data.Current_B / motor.StandValue, 3);
            return 0;
        }
        #endregion
    }
}
