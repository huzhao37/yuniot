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
    public class ConveyorByHourRepository : DeviceRepositoryBase<ConveyorByHour, Models.ConveyorByHour>, IConveyorByHourRepository
    {

        private readonly IConveyorRepository _cyRep;
        private readonly IMotorRepository _motorRep;
        private readonly IMotorTypeRepository _motorTypeRep;
        public ConveyorByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _cyRep = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
            _motorTypeRep = ServiceProviderServiceExtensions.GetService<IMotorTypeRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的皮带机数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConveyorByHour GetByMotorId(string motorId, bool isExceed, DateTime dt)
        {
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();         
            var standValue = motor?.StandValue??0;

            var start = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            var end = start.AddHours(1);
            var dt3 = start.AddHours(-1);

            //上一个小时的最后一条记录;
            var lastRecord = _cyRep.GetEntities(motorId, dt, isExceed, e=>e.Time.CompareTo(dt3)>=0&&
            e.Time.CompareTo(end)<0,e=>e.Time).LastOrDefault();

            var originalDatas = _cyRep.GetEntities(motorId, dt, isExceed, e => e.Time.CompareTo(start) >= 0 &&
                                                                       e.Time.CompareTo(end) < 0 &&
                                                                       e.AccumulativeWeight > -1, e => e.Time).ToList();         
            var length = originalDatas?.Count() ?? 0;
            float lastWeight = 0;
            float weightSum = 0;

            if (!(originalDatas?.Any()??false)) return null;

            //获取上一个有效累计称重的值      
            if (lastRecord != null && lastRecord.AccumulativeWeight != -1 &&
                originalDatas[0].AccumulativeWeight - lastRecord.AccumulativeWeight <=10*600 &&
                originalDatas[0].AccumulativeWeight - lastRecord.AccumulativeWeight >= 0)
            {
                lastWeight = lastRecord.AccumulativeWeight;
            }

            for (var i = 0; i < length; i++)
            {
                //瞬时重量为负数时，统计按照零计算;
                if (originalDatas[i].InstantWeight < 0)
                    originalDatas[i].InstantWeight = 0;

                var cy = originalDatas[i];
                if (cy.AccumulativeWeight == -1)
                    continue;

                if (cy.AccumulativeWeight < lastWeight || cy.AccumulativeWeight - lastWeight > 100) //比上次小，认作清零,或者比上次多出100t以上
                {
                    lastWeight = cy.AccumulativeWeight;
                    continue;
                }
                var sub = cy.AccumulativeWeight - lastWeight;
                lastWeight = cy.AccumulativeWeight;
                weightSum += sub;

            }
          
            var instantWeight = originalDatas.
                Where(c => c.InstantWeight >= 0);
            var count = instantWeight.Count();
            var entity = new ConveyorByHour
            {
                Time = start.TimeSpan(),
                MotorId = motorId,
                AvgInstantWeight = (float)Math.Round(instantWeight.Average(e => e.InstantWeight), 2),
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),           
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.PowerFactor), 2),             
                AccumulativeWeight = (float)Math.Round(weightSum, 2), //TODO：累计称重计算;               
                RunningTime = count,

                //负荷 = 该小时内累计重量/额定产量 (单位: 吨/小时);
                LoadStall = count* standValue == 0 ? 0 : (float)Math.Round(weightSum / standValue, 2),
            };
            return entity;

        }
        /// <summary>
        /// 统计该小时内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string MotorTypeId)
        {
            var ts = new List<ConveyorByHour>();
            var hour = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(MotorTypeId));
            foreach (var motor in query)
            {
                var exsit = false;
                exsit = GetEntities(o => o.Time.CompareTo(hour) == 0 && o.MotorId == motor.MotorId).Any();
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
        public ConveyorByDay GetRealData(string motorId)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            var standValue = motor?.StandValue ?? 0;

            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motorId) && e.Time.CompareTo(hourStart) >= 0 && e.Time.CompareTo(hourEnd) <= 0)?.ToList();

            var minuteData = GetByMotorId(motorId, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            var weightSum = (float)Math.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);
            var hours = hourData?.Count ?? 0;
            var data = new ConveyorByDay
            {
                MotorId = motorId,
                AccumulativeWeight = weightSum,
                AvgInstantWeight = (float)Math.Round(_cyRep.GetLatestRecord(motorId)?.InstantWeight ?? 0, 2),//实时的瞬时称重
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = hours * standValue == 0 ? 0 : (float)Math.Round(weightSum / hours / standValue, 2)
             };
                    
            //data.LoadStall = data.RunningTime * standValue == 0
            //    ? 0 : (float)Math.Round(data.AccumulativeWeight / standValue, 2);
            return data;
        }
        #endregion

    }
}
