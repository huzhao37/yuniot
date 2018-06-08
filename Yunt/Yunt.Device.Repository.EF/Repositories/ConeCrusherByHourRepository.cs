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
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超过3个月的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConeCrusherByHour GetByMotor(Motor motor, bool isExceed, DateTime dt)
        {

            var standValue = motor?.StandValue ?? 0;//_motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);
            long startUnix = start.TimeSpan(), endUnix = end.TimeSpan();
            var originalDatas = _ccRep.GetEntities(motor.MotorId, dt,isExceed, e => e.Current_B > -1 && e.Time >= startUnix &&
                                    e.Time < endUnix, e => e.Time)?.ToList();

            if (!(originalDatas?.Any() ?? false)) return null;

            var average = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2);
            var entity = new ConeCrusherByHour
            {
                Time = startUnix,
                MotorId = motor.MotorId,
                AvgCurrent_B = average,
                AvgMovaStress = (float)Math.Round(originalDatas.Average(o => o.MovaStress), 2),
                AvgOilFeedTempreature = (float)Math.Round(originalDatas.Average(o => o.OilFeedTempreature), 2),
                AvgSpindleTravel = (float)Math.Round(originalDatas.Average(o => o.SpindleTravel), 2),
                AvgOilReturnTempreatur = (float)Math.Round(originalDatas.Average(o => o.OilReturnTempreatur), 2),
                AvgTankTemperature = (float)Math.Round(originalDatas.Average(o => o.TankTemperature), 2),

                AvgPowerFactor = (float)Math.Round(originalDatas.Average(o => o.PowerFactor), 2),
                AvgVibrate1 = (float)Math.Round(originalDatas.Average(o => o.Vibrate1), 2),
                AvgVibrate2 = (float)Math.Round(originalDatas.Average(o => o.Vibrate2), 2),
                //ActivePower = ,
                AvgVoltage_A = (float)Math.Round(originalDatas.Average(o => o.Voltage_A), 2),
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),
                AvgVoltage_C = (float)Math.Round(originalDatas.Average(o => o.Voltage_C), 2),
                // WearValue1 = ,
                //WearValue2 = ,
                AvgAbsSpindleTravel = (float)Math.Round(originalDatas.Average(o => o.AbsSpindleTravel), 2),
                RunningTime = originalDatas.Count(c => c.Current_B > 0),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
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
        public ConeCrusherByDay GetRealData(Motor motor)
        {
            var minuteEnd = DateTime.Now;
            var hourStart = minuteEnd.Date;
            var hourEnd = minuteEnd.Date.AddHours(minuteEnd.Hour);
            var minuteStart = hourEnd;

            //var motor = _motorRep.GetEntities(e => e.MotorId.Equals(motor.MotorId)).SingleOrDefault();
            var standValue = motor?.StandValue ?? 0;
            long startUnix = hourStart.TimeSpan(), endUnix = hourEnd.TimeSpan();
            var hourData =
                GetEntities(
                    e => e.MotorId.Equals(motor.MotorId) && e.Time >= startUnix && e.Time <= endUnix)?.ToList();

            var minuteData = GetByMotor(motor, false, minuteStart);

            if (minuteData != null)
                hourData?.Add(minuteData);
            if (hourData == null || !hourData.Any()) return null;
            var average = (float)Math.Round(hourData.Average(o => o.AvgCurrent_B), 2);
            var data = new ConeCrusherByDay
            {
                MotorId = motor.MotorId,
                AvgCurrent_B = average,
                RunningTime = (float)Math.Round(hourData?.Sum(e => e.RunningTime) ?? 0, 2),
          
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(average / standValue, 2)
            };
            
            return data;
        }

        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        public IEnumerable<ConeCrusherByHour> GetRealDatas(Motor motor)
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
    }
}
