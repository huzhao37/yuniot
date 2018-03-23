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
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public ConeCrusherByHour GetByMotorId(string motorId, bool isExceed, DateTimeOffset dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);

            var originalDatas = _ccRep.GetEntities(motorId, isExceed, e => e.Current_B > -1 && e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (!(originalDatas?.Any()??false)) return new ConeCrusherByHour();

            var Average = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2);
            var entity = new ConeCrusherByHour
            {
                Time = start,
                MotorId = motorId,
                AvgCurrent_B = Average,
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
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(Average / standValue, 2)
            };
            return entity;




        }
        /// <summary>
        /// 统计该小时内所有圆锥破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTimeOffset dt, string MotorTypeId)
        {
            var ts = new List<ConeCrusherByHour>();
            var hour = new DateTimeOffset(new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0));
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
        #endregion
    }
}
