using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IHVibByDayRepository : IDeviceRepositoryBase<HVibByDay>
    {
        /// <summary>
        /// 统计该当日内所有高频筛的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task InsertDayStatistics(DateTime dt, string motorTypeId);
    }
}
