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
    public interface IVibrosieveByDayRepository : IDeviceRepositoryBase<VibrosieveByDay>
    {
        /// <summary>
        /// 统计该当日内所有振动筛的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task InsertDayStatistics(DateTime dt, string motorTypeId);

        #region assitant method
        /// <summary>
        ///恢复该小时内所有的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task RecoveryDayStatistics(DateTime dt, string motorTypeId);
        #endregion
    }
}
