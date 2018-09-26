using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IMaterialFeederByHourRepository : IDeviceRepositoryBase<MaterialFeederByHour>
    {
        #region extend method

        /// <summary>
        /// 统计该小时的给料机破数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超出7 days数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        MaterialFeederByHour GetByMotor(Motor motor, bool isExceed, DateTime dt);

        /// <summary>
        /// 统计该小时内所有给料机破的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        Task InsertHourStatistics(DateTime dt, string MotorTypeId);

        /// <summary>
        /// 获取当日实时数据
        /// </summary>
        /// <param name="motor"></param>
        MaterialFeederByDay GetRealData(Motor motor);
        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        IEnumerable<MaterialFeederByHour> GetRealDatas(Motor motor);
        #endregion

        #region assitant method
        /// <summary>
        ///恢复该小时内所有的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task RecoveryHourStatistics(DateTime dt, string motorTypeId);
        /// <summary>
        ///恢复该小时内所有的电量数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task UpdatePowers(DateTime dt, string motorTypeId);
        #endregion

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        float GetInstantLoadStall(Motor motor);
        #endregion
    }
}
