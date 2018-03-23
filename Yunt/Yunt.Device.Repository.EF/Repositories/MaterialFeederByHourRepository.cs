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
    public class MaterialFeederByHourRepository : DeviceRepositoryBase<MaterialFeederByHour, Models.MaterialFeederByHour>, IMaterialFeederByHourRepository
    {

        private readonly IMaterialFeederRepository _mfRep;
        private readonly IMotorRepository _motorRep;

        public MaterialFeederByHourRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _mfRep = ServiceProviderServiceExtensions.GetService<IMaterialFeederRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }

        #region extend method
        /// <summary>
        /// 统计该小时的给料机破数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        public MaterialFeederByHour GetByMotorId(string motorId, bool isExceed, DateTimeOffset dt)
        {

            var standValue = _motorRep.GetEntities(e => e.MotorId.Equals(motorId)).SingleOrDefault()?.StandValue ?? 0;

            var start = dt.Date.AddHours(dt.Hour);
            var end = start.AddHours(1);

            var originalDatas = _mfRep.GetEntities(motorId, isExceed, e => e.Frequency > 0 && e.Time.CompareTo(start) >= 0 &&
                                    e.Time.CompareTo(end) < 0, e => e.Time);

            if (!(originalDatas?.Any() ?? false))return new MaterialFeederByHour();
            
            var Average = (float)Math.Round(originalDatas.Average(o => o.Frequency), 2);
            var entity = new MaterialFeederByHour
            {
                Time = start,
                MotorId = motorId,
                AvgCurrent_B = (float)Math.Round(originalDatas.Average(o => o.Current_B), 2),
                AvgFrequency = (float)Math.Round(originalDatas.Where(c => c.Frequency > 0).Average(o => o.Frequency), 2),
               
                AvgVoltage_B = (float)Math.Round(originalDatas.Average(o => o.Voltage_B), 2),

                RunningTime = originalDatas.Count(c => c.Frequency > 0),
                LoadStall = (standValue == 0) ? 0 : (float)Math.Round(Average / standValue, 2)
            };
            return entity;




        }
        /// <summary>
        /// 统计该小时内所有给料机破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        public async Task InsertHourStatistics(DateTimeOffset dt, string MotorTypeId)
        {
            var ts = new List<MaterialFeederByHour>();
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
