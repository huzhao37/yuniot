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
using Yunt.Common.Shift;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ConveyorByHourRepository : DeviceRepositoryBase<ConveyorByHour, Models.ConveyorByHour>, IConveyorByHourRepository
    {

        private  readonly IConveyorRepository _cyRep;//{ get; set; }
        private  readonly IMotorRepository _motorRep;
        private  readonly IMotorTypeRepository _motorTypeRep;
        public ConveyorByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            if(_cyRep==null)
                _cyRep = ServiceProviderServiceExtensions.GetService<IConveyorRepository>(BootStrap.ServiceProvider);
            if (_motorRep == null)
                _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
            if (_motorTypeRep == null)
                _motorTypeRep = ServiceProviderServiceExtensions.GetService<IMotorTypeRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的皮带机数据;
        /// </summary>
        /// <param name="motor">设备idparam>
        /// <param name="isExceed">是否超过7 days的数据范围</param>
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
           // var dt3 = start.AddHours(-1);

            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();//, dt3Unix=dt3.TimeSpan();
            //上一个小时的最后一条记录;
            //var x = _cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= dt3Unix &&
            //  e.Time < startUnix, e => e.Time);
#if DEBUG
            //var lastRecord = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= dt3Unix &&
            // e.Time < startUnix, e => e.Time)?.LastOrDefault();

            //var lastRecord2 = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId)&&e.ActivePower>0f && e.Time >= dt3Unix &&
            //e.Time < startUnix, e => e.Time)?.LastOrDefault();
#else
            //var lastRecord = _cyRep.GetEntities(motor.MotorId, dt3, isExceed, e => e.Time >= dt3Unix &&
            //e.Time < startUnix, e => e.Time)?.LastOrDefault();

            // var lastRecord2 = _cyRep.GetEntities(motor.MotorId, dt3, isExceed, e => e.ActivePower>0f &&e.Time >= dt3Unix &&
            //e.Time < startUnix, e => e.Time)?.LastOrDefault();
#endif

#if DEBUG
            var originalDatas = motor.IsBeltWeight?_cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) &&
                                                                       e.AccumulativeWeight > 0f && e.Time >= startUnix &&
                                e.Time <=endUnix, e => e.Time)?.ToList():
                                _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId)  && e.Time >= startUnix &&
                                e.Time <=endUnix, e => e.Time)?.ToList();

            var originalDatas2 = _cyRep.GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) &&
                                                                      e.ActivePower > 0f && e.Time >= startUnix &&
                               e.Time <=endUnix, e => e.Time)?.ToList();
#else
            var originalDatas = motor.IsBeltWeight?_cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= startUnix &&
                                                                       e.Time < endUnix &&
                                                                       e.AccumulativeWeight > 0f, e => e.Time)?.ToList()
                                :_cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= startUnix &&
                                                                       e.Time < endUnix, e => e.Time)?.ToList();
            var originalDatas2 = _cyRep.GetEntities(motor.MotorId, dt, isExceed, e => e.Time >= startUnix &&
                                                                       e.Time < endUnix &&
                                                                       e.ActivePower > 0f, e => e.Time)?.ToList(); 
#endif

            var length = originalDatas?.Count() ?? 0;
            var length2 = originalDatas2?.Count() ?? 0;

            float lastWeight = 0;
            float weightSum = 0;

            if (!(originalDatas?.Any()??false) && !(originalDatas2?.Any() ?? false)) return new ConveyorByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            };

            var first = !(originalDatas?.Any() ?? false) ? new Conveyor()
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            } : originalDatas[0];

            var first2 = !(originalDatas2?.Any() ?? false) ? new Conveyor()
            {
                Time = startUnix,
                MotorId = motor.MotorId,
            } : originalDatas2[0];
            //获取上一个有效累计称重的值      
            //if (lastRecord != null && lastRecord.AccumulativeWeight != -1 &&
            //    first.AccumulativeWeight - lastRecord.AccumulativeWeight <=10*600 &&
            //    first.AccumulativeWeight - lastRecord.AccumulativeWeight >= 0)
            //{
            //    lastWeight = lastRecord.AccumulativeWeight;
            //}

            #region 计算产量
            double lastPower = 0;
            double powerSum = 0;
            //获取上一个有效电能的值      
            //if (lastRecord2 != null && lastRecord2.ActivePower >0 &&
            //   first2.ActivePower - lastRecord2.ActivePower >= 0)
            //{
            //    lastPower = lastRecord2.ActivePower;
            //}

            #endregion
            if (motor.IsBeltWeight)
                for (var i = 0; i < length; i++)
                {
                    var cy = originalDatas[i];//cy.AccumulativeWeight == -1 &&               
                    if (cy.AccumulativeWeight < lastWeight || Math.Abs(cy.AccumulativeWeight - lastWeight) > 100|| i == 0) //比上次小，认作清零,或者比上次多出100t以上
                    {
                        lastWeight = cy.AccumulativeWeight;
                        continue;
                    }
                    //瞬时重量为负数时，统计按照零计算;
                    //if (cy.InstantWeight < 0)
                    //    cy.InstantWeight = 0;
                    float sub = cy.AccumulativeWeight - lastWeight;
                    lastWeight = cy.AccumulativeWeight;
                    weightSum += sub;
                }

            for (var i = 0; i < length2; i++)
            {
                var cy = originalDatas2[i]; //cy.ActivePower == -1      
                //电能
                if (Math.Abs(cy.ActivePower - lastPower) > 100 || cy.ActivePower < lastPower|| i == 0)//第一条开始做统计
                {
                    lastPower = cy.ActivePower;
                    continue;
                }
                //电能
                var subPower = cy.ActivePower - lastPower;
                lastPower = cy.ActivePower;
                powerSum += subPower;
            }
            //电能
            float k = motor.Slope, b = motor.OffSet, calcWeight = 0;
            calcWeight = (float)Math.Round((k * powerSum + b), 2);

            var instantWeight = originalDatas?.
                Where(c => c.InstantWeight >=0f);
            var active = originalDatas?. Where(c => c.Current_B > 0f);

            var count = motor.IsBeltWeight ? (active?.Count()??0) : GetDiRunningTimes(motor.MotorId, start, end);//非皮带秤使用开关机标志位

            var load = motor.UseCalc ? Math.Round(((calcWeight * 60) / count) / standValue, 2) :
                  Math.Round(((weightSum * 60) / count) / standValue, 2);

            if(originalDatas!=null&&originalDatas.Any())
                return new ConveyorByHour
                {
                    Time = startUnix,
                    MotorId = motor.MotorId,
                    AvgInstantWeight = MathF.Round(instantWeight?.Average(e => e.InstantWeight) ?? 0, 2),
                    AvgCurrent_B = MathF.Round(originalDatas?.Average(o => o.Current_B) ?? 0, 2),
                    AvgVoltage_B = MathF.Round(originalDatas?.Average(o => o.Voltage_B) ?? 0, 2),
                    AvgPowerFactor = MathF.Round(originalDatas?.Average(o => o.PowerFactor) ?? 0, 2),
                    AccumulativeWeight = motor.UseCalc ? calcWeight : MathF.Round(weightSum, 2), //TODO：累计称重计算;               
                    RunningTime = count,
                    ActivePower = (float)Math.Round(powerSum, 2),
                    AvgPulsesSecond = MathF.Round(originalDatas?.Average(o => o.PulsesSecond) ?? 0, 2),

                    //负荷 = 该小时内累计重量/额定产量 (单位: 吨/小时);
                    LoadStall = count * standValue == 0 ? 0 : (float)load,
                };
            else
                return new ConveyorByHour
                {
                    Time = startUnix,
                    MotorId = motor.MotorId,
                    AvgInstantWeight = 0,
                    AvgCurrent_B = 0,
                    AvgVoltage_B =0,
                    AvgPowerFactor = 0,
                    AccumulativeWeight = motor.UseCalc ? calcWeight : MathF.Round(weightSum, 2), //TODO：累计称重计算;               
                    RunningTime = count,
                    ActivePower = (float)Math.Round(powerSum, 2),
                    AvgPulsesSecond =0,

                    //负荷 = 该小时内累计重量/额定产量 (单位: 吨/小时);
                    LoadStall = count * standValue == 0 ? 0 : (float)load,
                };

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
        /// 获取当日实时数据(所有当日的负荷和瞬时负荷，历史为平均负荷)
        /// </summary>
        /// <param name="motor"></param>
        public ConveyorByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time<= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var weightSum = MathF.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);
            //var hours = hourData?.Count ?? 0;         
            var data = new ConveyorByDay
            {
                MotorId = motor.MotorId,
                AccumulativeWeight = weightSum,
                AvgInstantWeight = MathF.Round(_cyRep.GetLatestRecord(motor.MotorId)?.InstantWeight ?? 0, 2),//实时的瞬时称重
                RunningTime = MathF.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = GetInstantLoadStall(motor)//hours * standValue == 0 ? 0 : MathF.Round(weightSum / hours / standValue, 2)
            };           
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
                     DeleteEntity(exsit);
                var t = GetByMotor(motor, false, dt);
                if (t != null)
                    ts.Add(t);
            }
            await InsertAsync(ts);
        }
        /// <summary>
        ///恢复该小时内所有的电量数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task UpdatePowers(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConveyorByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId));
            foreach (var motor in query)
            {
                var exsit = GetEntities(o => o.Time == hour && o.MotorId == motor.MotorId)?.FirstOrDefault();
                if (exsit!=null)
                {
                    var t = GetByMotor(motor, false, dt);
                    if (t != null)
                    {
                        exsit.ActivePower = t.ActivePower;
                        ts.Add(exsit);
                    }
                       
                }
               
            }
            await UpdateEntityAsync(ts);
        }

        /// <summary>
        ///恢复该小时内所有的开机时间和负荷数据（非皮带秤）;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        public async Task UpdateRunLoads(DateTime dt, string motorTypeId)
        {
            var ts = new List<ConveyorByHour>();
            var hour = dt.Date.AddHours(dt.Hour).TimeSpan();
            var query = _motorRep.GetEntities(e => e.MotorTypeId.Equals(motorTypeId)&&!e.IsBeltWeight);
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
                        ts.Add(exsit);
                    }

                }

            }
            await UpdateEntityAsync(ts);
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

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        public float GetInstantLoadStall(Motor motor)
        {
            var data=_cyRep.GetLatestRecord(motor.MotorId);
            if (data != null)
                return data.Current_B * motor.StandValue == 0 ? 0 :MathF.Round(data.Current_B / motor.StandValue,3);
            return 0;
        }

        /// <summary>
        /// 获取瞬时称重
        /// </summary>
        /// <param name="motor"></param>
        public float GetInstantWeight(Motor motor)
        {
            var data = _cyRep.GetLatestRecord(motor.MotorId);
            if (data != null)
                return data.InstantWeight;
            return 0;
        }
        #endregion

        #region shift
        /// <summary>
        /// 获取当班次实时数据(所有当日的负荷和瞬时负荷，历史为平均负荷)
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="shiftStartHour">班次起始小时时间</param>
        public ConveyorByDay GetShiftRealData(Motor motor,int shiftStartHour)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date.AddHours(shiftStartHour);
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();            
            var minuteData = GetByMotor(motor, false, minuteStart);
            if (minuteData != null)
                hourData?.Add(minuteData);
            //最近一个小时是否已经统计
            if (minuteEnd.Minute <= 3)
            {
                var hourUnix = hourEnd.AddHours(-1).TimeSpan();
                var exist = hourData.Any(e => e.Time == hourUnix);
                if(!exist)
                {
                    var lastedHour = GetByMotor(motor, false, hourEnd.AddHours(-1));
                    if (lastedHour != null)
                        hourData?.Add(lastedHour);
                }
            }
            if (hourData == null || !hourData.Any()) return null;
            var weightSum = MathF.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);        
            var data = new ConveyorByDay
            {
                MotorId = motor.MotorId,
                AccumulativeWeight = weightSum,
                AvgInstantWeight = MathF.Round(_cyRep.GetLatestRecord(motor.MotorId)?.InstantWeight ?? 0, 2),//实时的瞬时称重
                RunningTime = MathF.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = GetInstantLoadStall(motor)
            };
            return data;
        }

        /// <summary>
        /// 获取历史某班次的数据
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="start">班次起始小时时间</param>
        ///   <param name="end">班次结束小时时间</param>
        public ConveyorByDay GetHistoryShiftOneData(Motor motor, long start,long end)
        {
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time <= end)?.ToList();     
            if (hourData == null || !hourData.Any()) return null;
            var weightSum = MathF.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);
            var source = hourData.Where(e => e.RunningTime > 0);
            if(source==null||!source.Any())
                return new ConveyorByDay
                {
                    MotorId = motor.MotorId,
                    AccumulativeWeight = weightSum,
                    AvgInstantWeight = 0f,//历史平均瞬时称重
                    RunningTime = 0f,
                    //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                    LoadStall = 0f
                };
            var data = new ConveyorByDay
            {
                MotorId = motor.MotorId,
                AccumulativeWeight = weightSum,
                AvgInstantWeight = MathF.Round(source?.Average(e => e.AvgInstantWeight) ?? 0, 2),//历史平均瞬时称重
                RunningTime = MathF.Round(source?.Sum(e => e.RunningTime) ?? 0, 2),
                //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                LoadStall = MathF.Round(source?.Average(e =>e.LoadStall) ?? 0, 3)
            };
            return data;
        }

        /// <summary>
        /// 获取历史某些班次的数据
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="start">起始时间</param>
        ///   <param name="end">结束时间</param>
        ///  <param name="shiftStart">班次起始小时时间</param>
        ///   <param name="shiftEnd">班次结束小时时间</param>
        public IEnumerable<ConveyorByDay> GetHistoryShiftSomeData(Motor motor, long start, long end,int shiftStart,int shiftEnd)
        {
            var datas= new List<ConveyorByDay>();
            DateTime startTime = start.Time(), endTime = end.Time();
            var times= ShiftCalc.GetTimesByShift(startTime, endTime, shiftStart, shiftEnd);
            if (times == null || !times.Any())
                return null;            
            times.ForEach(t=> {
                var st = t.Item1.Time().Date;var et = t.Item2.Time().Date;
               // var time = t.Item1.Time().Date.TimeSpan();
                var time = t.Item1.Time().TimeSpan();
                var specHour = t.Item2.Time().AddHours(1).TimeSpan();
                if (st.CompareTo(et) == 0&& t.Item1.Time().Hour<shiftStart)//同一自然日的情况
                    time = st.AddDays(-1).TimeSpan();

                if (datas.Count == 0)
                {
                    var retime = time.Time();
                    retime = retime.Date;
                    time = retime.AddHours(+8).TimeSpan();
                }
                //var endTime2 = t.Item2.Time();              
                //if (t.Item2 != shiftEnd)
                //    endTime2 = endTime2.AddHours(1);
                //var endUnix = endTime2.TimeSpan();
                var hourData =t.Item1==t.Item2? GetEntities(e => e.MotorId.Equals(motor.MotorId) && e.Time >= t.Item1 && e.Time < specHour)?.ToList():
                            GetEntities( e => e.MotorId.Equals(motor.MotorId) && e.Time >= t.Item1 && e.Time < specHour)?.ToList();
                if (hourData == null || !hourData.Any())
                {
                    datas.Add(new ConveyorByDay
                    {
                        MotorId = motor.MotorId,
                        AccumulativeWeight = 0,
                        AvgInstantWeight = 0,//历史平均瞬时称重
                        RunningTime =0,
                        //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                        LoadStall =0,
                        Time = time,
                       

                    });
                    return;
                }
                var weightSum = MathF.Round(hourData?.Sum(e => e.AccumulativeWeight) ?? 0, 2);
                var source = hourData?.Where(e => e.RunningTime > 0);
                if (source == null || !source.Any())
                {
                    datas.Add(new ConveyorByDay
                    {
                        MotorId = motor.MotorId,
                        AccumulativeWeight = 0,
                        AvgInstantWeight = 0,//历史平均瞬时称重
                        RunningTime = 0,
                        //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                        LoadStall = 0,
                        Time = time,
                        ActivePower = 0
                    });
                    return;
                }

                datas.Add(new ConveyorByDay
                {
                    MotorId = motor.MotorId,
                    AccumulativeWeight = weightSum,
                    AvgInstantWeight = MathF.Round(source?.Average(e => e.AvgInstantWeight) ?? 0, 2),//历史平均瞬时称重
                    RunningTime = MathF.Round(source?.Sum(e => e.RunningTime) ?? 0, 2),
                    //负荷 = 累计重量/额定产量 (单位: 吨/小时);
                    LoadStall = MathF.Round(source?.Average(e => e.LoadStall) ?? 0, 3),
                    Time = time,
                    ActivePower = MathF.Round(source?.Sum(e => e.ActivePower) ?? 0, 3)
                });
            });
          
            return datas;
        }

        /// <summary>
        /// 获取当前班次实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="shiftStartHour">班次起始小时时间</param>
        public IEnumerable<ConveyorByHour> GetShiftRealDatas(Motor motor,int shiftStartHour)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date.AddHours(shiftStartHour);
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();
            var minuteData = GetByMotor(motor, false, minuteStart);
            if (minuteData != null)
                hourData?.Add(minuteData);
            //最近一个小时是否已经统计
            if (minuteEnd.Minute <= 3)
            {
                var hourUnix = hourEnd.AddHours(-1).TimeSpan();
                var exist = hourData.Any(e => e.Time == hourUnix);
                if (!exist)
                {
                    var lastedHour = GetByMotor(motor, false, hourEnd.AddHours(-1));
                    if (lastedHour != null)
                        hourData?.Add(lastedHour);
                }
            }
            if (hourData == null || !hourData.Any()) return null;
            return hourData;
        }
        #endregion
    }
}
