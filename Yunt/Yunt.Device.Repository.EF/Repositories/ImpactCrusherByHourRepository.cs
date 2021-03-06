﻿using System;
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
using Yunt.Common.Shift;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ImpactCrusherByHourRepository : DeviceRepositoryBase<ImpactCrusherByHour, Models.ImpactCrusherByHour>, IImpactCrusherByHourRepository
    {

        private readonly IImpactCrusherRepository _icRep;
        private readonly IMotorRepository _motorRep;

        public ImpactCrusherByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _icRep = ServiceProviderServiceExtensions.GetService<IImpactCrusherRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }
        #region extend method


        /// <summary>
        /// 统计该小时的反击破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超过7 days的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ImpactCrusherByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {
            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
#if DEBUG
            var originalDatas = _icRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Current_B > -1f && e.Time >= startUnix &&
                                e.Time < endUnix, e => e.Time)?.ToList();
#else
        
            var originalDatas = _icRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Current_B > -1f && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();
#endif
            if (!originalDatas?.Any()??false) return new ImpactCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            };
    
            var average = MathF.Round(originalDatas.Average(o => o.Current_B), 2);
            var entity = new ImpactCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgMotor1Current_B = MathF.Round(originalDatas.Average(o => o.Motor1Current_B), 2),
                AvgMotor2Current_B = MathF.Round(originalDatas.Average(o => o.Motor2Current_B), 2),
                AvgSpindleTemperature1 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature1), 2),
                AvgSpindleTemperature2 = MathF.Round(originalDatas.Average(o => o.SpindleTemperature2), 2),
                AvgMotor1Voltage_B = MathF.Round(originalDatas.Average(o => o.Motor1Voltage_B), 2),
                AvgMotor2Voltage_B = MathF.Round(originalDatas.Average(o => o.Motor2Voltage_B), 2),
                AvgVibrate1 = MathF.Round(originalDatas.Average(o => o.Vibrate1), 2),
                AvgVibrate2 = MathF.Round(originalDatas.Average(o => o.Vibrate2), 2),
                AvgVoltage_B = MathF.Round(originalDatas.Average(o => o.Voltage_B), 2),
                //WearValue1 = MathF.Round(originalDatas.Average(o => o.WearValue1), 2),
                //WearValue2 = MathF.Round(originalDatas.Average(o => o.WearValue2), 2),
                AvgCurrent_B = average,
                RunningTime = originalDatas.Count(c => c.Current_B > 0f),
                LoadStall = (standValue == 0) ? 0 : MathF.Round(average / standValue, 2)
            };
            return entity;

        }
        /// <summary>
        /// 统计该小时内所有反击破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ImpactCrusherByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time==hour && o.MotorId == motor.MotorId).Any();
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
        public ImpactCrusherByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;
            long startUnix = hourStart.TimeSpan(),endUnix=hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time<= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var average = MathF.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new ImpactCrusherByDay
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
        public IEnumerable<ImpactCrusherByHour> GetRealDatas(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;           

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
            var ts = new List<ImpactCrusherByHour>();
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

        #region private method


        /// <summary>
        /// 获取反击破关机次数;
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private static int GetOnoffSets(IEnumerable<float> datas)
        {
            var isOn = false;
            int onOffs = 0, tempCount = 0;
            float temp = 0;
            foreach (var data in datas)
            {
                if (data > 0)
                {
                    //开机
                    isOn = true;
                    temp = data;
                    tempCount = 0;
                }
                else
                {
                    //关机
                    isOn = false;
                    tempCount++;
                    temp = 0;
                }

                //判断是否连续超过10分钟关机
                if (tempCount == 10 && temp == 0 && !isOn)
                    onOffs++;
            }
            return onOffs;
        }

        #endregion

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        public float GetInstantLoadStall(Motor motor)
        {
            var data = _icRep.GetLatestRecord(motor.MotorId);
            if (data != null)
                return data.Current_B * motor.StandValue == 0 ? 0 : MathF.Round(data.Current_B / motor.StandValue, 3);
            return 0;
        }

        /// <summary>
        ///恢复该小时内所有的相关参数;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task UpdateOthers(DateTime dt, string motorTypeId)
        {
            var ts = new List<ImpactCrusherByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == hour && o.MotorId == motor.MotorId)?.FirstOrDefault();
                if (exsit != null)
                {
                    var t = GetByMotor(motor, false, dt);
                    if (t != null)
                    {
                        exsit.RunningTime = t.RunningTime;
                        exsit.LoadStall = t.LoadStall;
                        exsit.AvgCurrent_B = t.AvgCurrent_B;
                        exsit.AvgVoltage_B = t.AvgVoltage_B;
                        ts.Add(exsit);
                    }

                }

            }
            await UpdateEntityAsync(ts);
        }
        #endregion

        #region 2018.9.29 powers
        /// <summary>
        /// 获取历史某些班次的数据
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="start">起始时间</param>
        ///   <param name="end">结束时间</param>
        ///  <param name="shiftStart">班次起始小时时间</param>
        ///   <param name="shiftEnd">班次结束小时时间</param>
        public IEnumerable<ImpactCrusherByDay> GetHistoryShiftSomeData(Motor motor, long start, long end, int shiftStart, int shiftEnd)
        {
            var datas = new List<ImpactCrusherByDay>();
            DateTime startTime = start.Time(), endTime = end.Time();
            var times = ShiftCalc.GetTimesByShift(startTime, endTime, shiftStart, shiftEnd);
            if (times == null || !times.Any())
                return null;
            times.ForEach(t => {
                var st = t.Item1.Time().Date; var et = t.Item2.Time().Date;
                var time = t.Item1.Time().TimeSpan();
                var specHour = t.Item2.Time().AddHours(1).TimeSpan();
                if (st.CompareTo(et) == 0 && t.Item1.Time().Hour < shiftStart)//同一自然日的情况
                    time = st.AddDays(-1).TimeSpan();

                if (datas.Count == 0)
                {
                    var retime = time.Time();
                    retime = retime.Date;
                    time = retime.AddHours(+8).TimeSpan();
                }
                var hourData = t.Item1 == t.Item2 ? GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= t.Item1 && e.Time < specHour)?.ToList() :
                            GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= t.Item1 && e.Time < specHour)?.ToList();
                if (hourData == null || !hourData.Any())
                {
                    datas.Add(new ImpactCrusherByDay
                    {
                        MotorId = motor.MotorId,
                        Motor1ActivePower = 0,
                        Motor2ActivePower = 0,
                        Time = time,


                    });
                    return;
                }
                var source = hourData?.Where(e => e.RunningTime > 0);
                if (source == null || !source.Any())
                {
                    datas.Add(new ImpactCrusherByDay
                    {
                        MotorId = motor.MotorId,
                        Time = time,
                        Motor1ActivePower = 0,
                        Motor2ActivePower = 0,
                    });
                    return;
                }

                datas.Add(new ImpactCrusherByDay
                {
                    MotorId = motor.MotorId,
                    Time = time,
                    Motor1ActivePower = MathF.Round(source?.Sum(e => e.Motor1ActivePower) ?? 0, 3),
                    Motor2ActivePower = MathF.Round(source?.Sum(e => e.Motor2ActivePower) ?? 0, 3)
                });
            });

            return datas;
        }
        #endregion

    }
}
