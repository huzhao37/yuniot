using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IConveyorByDayRepository : IDeviceRepositoryBase<ConveyorByDay>
    {
        #region extend method

        /// <summary>
        /// 统计该当日的皮带机数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
         ConveyorByDay GetByMotor(Motor motor, DateTime dt);

        /// <summary>
        /// 统计该当日内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task InsertDayStatistics(DateTime dt, string motorTypeId);

        #endregion

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
