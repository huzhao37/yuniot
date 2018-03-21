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
    public class ConeCrusherByHourRepository : DeviceRepositoryBase<ConeCrusherByHour, Models.ConeCrusherByHour>, IConeCrusherByHourRepository
    {
        private readonly IConeCrusherRepository _ccRep;
        private readonly IMotorRepository _motorRep;

        public ConeCrusherByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _ccRep = ServiceProviderServiceExtensions.GetService<IConeCrusherRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的圆锥破数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConeCrusherByHour GetByMotorIdAndHour(string motorId, bool isExceed, DateTimeOffset dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);

            var originalDatas = _ccRep.GetEntities(motorId, isExceed, e => e.Current > -1 && e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (!originalDatas.Any()) return new ConeCrusherByHour();

            var average = Math.Round(originalDatas.Average(o => o.Current), 2);
            var entity = new ConeCrusherByHour
            {
                Time = start,
                MotorId = motorId,
                AverageCurrent = average,
                AverageMovaStress = Math.Round(originalDatas.Average(o => o.MovaStress), 2),
                AverageOilFeedTempreature = Math.Round(originalDatas.Average(o => o.OilFeedTempreature), 2),
                AverageSpindleTravel = Math.Round(originalDatas.Average(o => o.SpindleTravel), 2),
                AverageOilReturnTempreature = Math.Round(originalDatas.Average(o => o.OilReturnTempreature), 2),
                AverageTankTemperature = Math.Round(originalDatas.Average(o => o.TankTemperature), 2),
                RunningTime = originalDatas.Count(c => c.Current > 0),
                LoadStall = (standValue == 0) ? 0 : Math.Round(average / standValue, 2)
            };
            return entity;




        }
        /// <summary>
        /// 统计该小时内所有圆锥破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConeCrusherByHour>();
            var hour = new DateTimeOffset(new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0));
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time.CompareTo(hour) == 0 && o.MotorId == motor.MotorId).Any();
                if (exsit)
                    continue;
                var t = GetByMotorIdAndHour(motor.MotorId, false, dt);
                if (t != null)
                    ts.Add(t);
            }

            await InsertAsync(ts);
        }
        #endregion
    }
}
