using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface  IImpactCrusherByDayRepository : IDeviceRepositoryBase<ImpactCrusherByDay>
    {

        /// <summary>
        /// 统计该当日内所有反击破的数据;
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
        ///恢复该小时内所有的其他参数数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
         Task UpdateOthers(DateTime dt, string motorTypeId);
        #endregion
    }
}
