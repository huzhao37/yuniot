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
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ImpactCrusherByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {
            var standValue = motor?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _icRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Motor1Current_B > -1 && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!originalDatas?.Any()??false) return null;

      
            var average = (float)Math.Round(originalDatas.Average(o => o.Motor1Current_B), 2);
            var entity = new ImpactCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgMotor1Current_B = average,
                AvgMotor2Current_B = (float)Math.Round(originalDatas.Average(o => o.Motor2Current_B), 2),
                AvgSpindleTemperature1 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature1), 2),
                AvgSpindleTemperature2 = (float)Math.Round(originalDatas.Average(o => o.SpindleTemperature2), 2),
                //OnOffCounts = offCounts,
                RunningTime = originalDatas.Count(c => c.Motor1Current_B > 0),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
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

           // var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            var standValue = motor?.StandValue ?? 0;
            long startUnix = hourStart.TimeSpan(),endUnix=hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time<= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var average = (float)Math.Round(hourData.Average(o => o.AvgMotor1Current_B), 2);
            var data = new ImpactCrusherByDay
            {
                MotorId = motor.MotorId,
                AvgMotor1Current_B = average,
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),

                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
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
    }
}
