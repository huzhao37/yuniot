using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IJawCrusherByDayRepository : IDeviceRepositoryBase<JawCrusherByDay>
    {
        /// <summary>
        /// 统计该当日内所有鄂破的数据;
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
        /// <summary>
        ///恢复该小时内所有的电量数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task UpdatePowers(DateTime dt, string motorTypeId);
        #endregion
    }
}
