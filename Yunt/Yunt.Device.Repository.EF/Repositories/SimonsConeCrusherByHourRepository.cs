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

namespace Yunt.Device.Repository.EF.Repositories
{
    public class SimonsConeCrusherByHourRepository : DeviceRepositoryBase<SimonsConeCrusherByHour, Models.SimonsConeCrusherByHour>, ISimonsConeCrusherByHourRepository
    {

        private readonly ISimonsConeCrusherRepository _ccRep;
        private readonly IMotorRepository _motorRep;

        public SimonsConeCrusherByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _ccRep = ServiceProviderServiceExtensions.GetService<ISimonsConeCrusherRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的西蒙斯数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超过7 days的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public SimonsConeCrusherByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _ccRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Current > -1f && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any()??false)) return new SimonsConeCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            };

            var average = MathF.Round(originalDatas.Average(o => o.Current), 2);
            var entity = new SimonsConeCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AverageCurrent = MathF.Round(originalDatas.Average(o => o.Current), 2),
                AverageOilFeedTempreature = MathF.Round(originalDatas.Average(o => o.OilFeedTempreature), 2),
                AverageOilReturnTempreature = MathF.Round(originalDatas.Average(o => o.OilReturnTempreature), 2),
                AverageTankTemperature = MathF.Round(originalDatas.Average(o => o.TankTemperature), 2),

                RunningTime = originalDatas.Count(c => c.Current > 0f),
                LoadStall = (standValue == 0) ? 0 : MathF.Round(average / standValue, 2)
            };
            return entity;




        }
        /// <summary>
        /// 统计该小时内所有西蒙斯的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<SimonsConeCrusherByHour>();
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
        public SimonsConeCrusherByDay GetRealData(Motor motor)
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
            if (hourData == null || !hourData.Any()) return null;
            var average = MathF.Round(hourData.Average(o => o.AverageCurrent), 2);
            var data = new SimonsConeCrusherByDay
            {
                MotorId = motor.MotorId,
                AverageCurrent = average,
                RunningTime = MathF.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),

                LoadStall = GetInstantLoadStall(motor)//(standValue == 0) ? 0 : MathF.Round(average / standValue, 2)
            };

            return data;
        }

        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        public IEnumerable<SimonsConeCrusherByHour> GetRealDatas(Motor motor)
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
            var ts = new List<SimonsConeCrusherByHour>();
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

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        public float GetInstantLoadStall(Motor motor)
        {
            var data = _ccRep.GetLatestRecord(motor.MotorId);
            if (data != null)
                return data.Current* motor.StandValue == 0 ? 0 : MathF.Round(data.Current / motor.StandValue, 3);
            return 0;
        }
        #endregion
    }
}
