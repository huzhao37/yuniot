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
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConveyorByHour GetByMotorIdAndHour(string motorId, bool isExceed, DateTimeOffset dt)
        {
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();         
            var capicity = motor?.Capicity??0;

            var start = new DateTimeOffset(new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0));
            var end = start.AddHours(1);
            var dt3 = start.AddHours(-1);

            //上一个小时的最后一条记录;
            var lastRecord = _cyRep.GetEntities(motorId, isExceed, e=>e.Time.CompareTo(dt3)>=0&&
            e.Time.CompareTo(end)<0,e=>e.Time).LastOrDefault();

            var originalDatas = _cyRep.GetEntities(motorId, isExceed, e => e.Time.CompareTo(start) >= 0 &&
                                                                       e.Time.CompareTo(end) < 0 &&
                                                                       e.AccumulativeWeight > -1, e => e.Time).ToList();         
            var length = originalDatas?.Count() ?? 0;
            double lastWeight = 0;
            double weightSum = 0;

            if (!(originalDatas?.Any()??false)) return new ConveyorByHour();

            //获取上一个有效累计称重的值      
            if (lastRecord != null && lastRecord.AccumulativeWeight != -1 &&
                originalDatas[0].AccumulativeWeight - lastRecord.AccumulativeWeight <= 100 &&
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
            //一直为确定，到底是以bootflag为主，还是instantWeight为主。
            var bootOnArrays = (originalDatas.Any(c => c.InstantWeight > 0))
                ? originalDatas.Count(c => c.InstantWeight > 0) : 0;

            var averageInstantWeight = bootOnArrays == 0 ? 0 : originalDatas.
                Where(c => c.InstantWeight >= 0).Average(e => e.InstantWeight);
            var entity = new ConveyorByHour
            {
                Time = start,
                MotorId = motorId,
                AverageInstantWeight = Math.Round(averageInstantWeight / 60, 2),
                AverageCurrent = Math.Round(originalDatas.Average(o => o.Current), 2),
                AverageVoltage = Math.Round(originalDatas.Average(o => o.Voltage), 2),
                AverageVelocity = Math.Round(originalDatas.Average(o => o.Velocity), 2),
                AverageFrequency = Math.Round(originalDatas.Average(o => o.Frequency), 2),
                AveragePowerFactor = Math.Round(originalDatas.Average(o => o.PowerFactor), 2),
                AverageReactivePower = Math.Round(originalDatas.Average(o => o.ReactivePower), 2),
                AverageTotalPower = Math.Round(originalDatas.Average(o => o.TotalPower), 2),

                AccumulativeWeight = Math.Round(weightSum, 2), //TODO：累计称重计算;

                RunningTime = bootOnArrays,

                //负荷 = 该小时内累计重量/额定产量 (单位: 吨/小时);
                LoadStall = bootOnArrays == 0 ? 0 : Math.Round(((weightSum * 60) / bootOnArrays) / capicity, 2),
            };
            return entity;

        }
        /// <summary>
        /// 统计该小时内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConveyorByHour>();
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
