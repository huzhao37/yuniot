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
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ImpactCrusherByHour GetByMotorIdAndHour(string motorId, bool isExceed, DateTimeOffset dt)
        {
            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);

            var originalDatas = _icRep.GetEntities(motorId, isExceed, e => e.Current > -1 && e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (!originalDatas.Any()) return new ImpactCrusherByHour();

            var offCounts = 0;
    


            var totalDatas = _icRep.GetEntities(motorId, isExceed, e => e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (totalDatas.Any())
            {
                var currents = totalDatas.Select(c => c.Current).ToList();
                offCounts = GetOnoffSets(currents);
            }
            var average = Math.Round(originalDatas.Average(o => o.Current), 2);
            var entity = new ImpactCrusherByHour
            {
                Time = start,
                MotorId = motorId,
                AverageCurrent = average,
                AverageCurrent2 = Math.Round(originalDatas.Average(o => o.Current2), 2),
                AverageSpindleTemperature1 = Math.Round(originalDatas.Average(o => o.SpindleTemperature1), 2),
                AverageSpindleTemperature2 = Math.Round(originalDatas.Average(o => o.SpindleTemperature2), 2),
                OnOffCounts = offCounts,

                RunningTime = originalDatas.Count(c => c.Current > 0),
                LoadStall = (standValue == 0) ? 0 : Math.Round(average / standValue, 2)
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
            double temp = 0;
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
    }
}
