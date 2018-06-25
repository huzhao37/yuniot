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
        /// <param name="motor">设备idparam>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConveyorByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {
            
            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault();
            if (motor == null)
                return null;
            var standValue = motor?.StandValue??0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            var dt3 = start.AddHours(-1);

            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan(), dt3Unix=dt3.TimeSpan();
            //上一个小时的最后一条记录;
            //var x = _cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= dt3Unix &&
            //  e.Time < startUnix, e => e.Time);
#if DEBUG
            var lastRecord = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= dt3Unix &&
             e.Time < startUnix, e => e.Time)?.LastOrDefault();
#else
            var lastRecord = _cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= dt3Unix &&
            e.Time < startUnix, e => e.Time)?.LastOrDefault();
#endif

#if DEBUG
            var originalDatas = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) &&
                                                                       e.AccumulativeWeight > 0f && e.Time >= startUnix &&
                                e.Time < endUnix, e => e.Time)?.ToList();
#else
            var originalDatas = _cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= startUnix &&
                                                                       e.Time < endUnix &&
                                                                       e.AccumulativeWeight > 0f, e => e.Time)?.ToList();  
#endif

            var length = originalDatas?.Count() ?? 0;
            float lastWeight = 0;
            float weightSum = 0;

            if (!(originalDatas?.Any()??false)) return new ConveyorByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            };

            var first = originalDatas[0];
            //获取上一个有效累计称重的值      
            if (lastRecord != null && lastRecord.AccumulativeWeight != -1 &&
                first.AccumulativeWeight - lastRecord.AccumulativeWeight <=10*600 &&
                first.AccumulativeWeight - lastRecord.AccumulativeWeight >= 0)
            {
                lastWeight = lastRecord.AccumulativeWeight;
            }

            #region 计算产量
            double lastPower = 0;
            double powerSum = 0;
            //获取上一个有效电能的值      
            if (lastRecord != null && lastRecord.ActivePower != -1 &&
               first.ActivePower - lastRecord.ActivePower >= 0)
            {
                lastPower = lastRecord.ActivePower;
            }

            #endregion

            for (var i = 0; i < length; i++)
            {
                var cy = originalDatas[i];
                if (cy.AccumulativeWeight == -1 && cy.AccumulativeWeight < lastWeight ||Math.Abs(cy.AccumulativeWeight - lastWeight) > 100) //比上次小，认作清零,或者比上次多出100t以上
                {
                    lastWeight = cy.AccumulativeWeight;
                    continue;
                }
                //电能
                if (cy.ActivePower == -1 || cy.ActivePower < lastPower)
                {
                    lastPower = cy.ActivePower;
                    continue;
                }
                //瞬时重量为负数时，统计按照零计算;
                if (cy.InstantWeight < 0)
                    cy.InstantWeight = 0;         
                float sub = cy.AccumulativeWeight - lastWeight;
                lastWeight = cy.AccumulativeWeight;
                weightSum += sub;
                //电能
                var subPower = cy.ActivePower - lastPower;
                lastPower = cy.ActivePower;
                powerSum += subPower;
            }
            //电能
            float k = motor.Slope, b = motor.OffSet, calcWeight = 0;
            calcWeight = (float)Math.Round((k * powerSum + b), 2);

            var instantWeight = originalDatas.
                Where(c => c.InstantWeight >= 0);
            var count = instantWeight.Count();


            var load = motor.UseCalc ? Math.Round(((calcWeight * 60) / count) / standValue, 2) :
                  Math.Round(((weightSum * 60) / count) / standValue, 2);
            var entity = new ConveyorByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgInstantWeight = (float)Math.Round(instantWeight.Average(e => e.InstantWeight), 2),
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),           
                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.PowerFactor), 2),             
                AccumulativeWeight =motor.UseCalc? calcWeight:(float)Math.Round(weightSum, 2), //TODO：累计称重计算;               
                RunningTime = count,
                ActivePower = (float)Math.Round(powerSum, 2),
                AvgPulsesSecond = (float)Math.Round(originalDatas.Average(o => o.PulsesSecond), 2),
               
                //负荷 = 该小时内累计重量/额定产量 (单位: 吨/小时);
                LoadStall = count* standValue == 0 ? 0 : (float)load,
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
        public ConveyorByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).FirstOrDefault();
            var standValue = motor?.StandValue ?? 0;

            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time<= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var weightSum = (float)Math.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);
            var hours = hourData?.Count ?? 0;
            var data = new ConveyorByDay
            {
                MotorId = motor.MotorId,
                AccumulativeWeight = weightSum,
                AvgInstantWeight = (float)Math.Round(_cyRep.GetLatestRecord(motor.MotorId)?.InstantWeight ?? 0, 2),//实时的瞬时称重
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = hours * standValue == 0 ? 0 : (float)Math.Round(weightSum / hours / standValue, 2)
             };
                    
            //data.LoadStall = data.RunningTime * standValue == 0
            //    ? 0 : (float)Math.Round(data.AccumulativeWeight / standValue, 2);
            return data;
        }

        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        public IEnumerable<ConveyorByHour> GetRealDatas(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motor.MotorId)).FirstOrDefault();

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
            var ts = new List<ConveyorByHour>();
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

        /// <summary>
        /// 统计该小时内皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorId">设备类型</param>
        public ConveyorByHour GetHour(DateTime dt, string motorId)
        {
            var ts = new List<ConveyorByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).FirstOrDefault();
   
            return GetByMotor(motor, false, dt);  
        }
        #endregion
    }
}
