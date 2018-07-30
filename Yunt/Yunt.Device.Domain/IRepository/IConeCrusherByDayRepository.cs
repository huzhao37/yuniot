using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IConeCrusherByDayRepository : IDeviceRepositoryBase<ConeCrusherByDay>
    {

        /// <summary>
        /// 统计该当日内所有圆锥破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task InsertDayStatistics(DateTime dt, string motorTypeId);
        /// <summary>
        /// 统计该当日的圆锥破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
        ConeCrusherByDay GetByMotor(Motor motor, DateTime dt);
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
