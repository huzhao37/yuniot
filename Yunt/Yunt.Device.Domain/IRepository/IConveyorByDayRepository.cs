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
        /// <param name="motorId">设备id</param>
        /// <param name="dt">查询时间,精确到当日</param>
        /// <returns></returns>
         ConveyorByDay GetByMotorId(string motorId, DateTime dt);

        /// <summary>
        /// 统计该当日内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task InsertDayStatistics(DateTime dt, string motorTypeId);

        #endregion
    }
}
