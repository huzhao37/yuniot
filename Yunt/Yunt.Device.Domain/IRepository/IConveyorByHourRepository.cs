using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IConveyorByHourRepository : IDeviceRepositoryBase<ConveyorByHour>
    {
        #region extend method

        /// <summary>
        /// 统计该小时的皮带机数据;
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="isExceed">是否超出3 months数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        ConveyorByHour GetByMotor(Motor motor, bool isExceed, DateTime dt);

        /// <summary>
        /// 统计该小时内所有皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        Task InsertHourStatistics(DateTime dt, string MotorTypeId);

        /// <summary>
        /// 获取当日实时数据
        /// </summary>
        /// <param name="motor"></param>
        ConveyorByDay GetRealData(Motor motor);
        /// <summary>
        /// 获取当日实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        IEnumerable<ConveyorByHour> GetRealDatas(Motor motor);
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
        /// <summary>
        ///恢复该小时内所有的开机时间和负荷数据（非皮带秤）;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorTypeId">设备类型</param>
        Task UpdateRunLoads(DateTime dt, string motorTypeId);

        /// <summary>
        /// 统计该小时内皮带机的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="motorId">设备类型</param>
        ConveyorByHour GetHour(DateTime dt, string motorId);
        #endregion

        #region version 18.07.17
        /// <summary>
        /// 获取瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        float GetInstantLoadStall(Motor motor);
        /// <summary>
        /// 获取瞬时称重
        /// </summary>
        /// <param name="motor"></param>
        float GetInstantWeight(Motor motor);
        #endregion

        #region shift
        /// <summary>
        /// 获取当班次实时数据(所有当日的负荷和瞬时负荷，历史为平均负荷)
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="shiftStartHour">班次起始小时时间</param>
        ConveyorByDay GetShiftRealData(Motor motor, int shiftStartHour);
        /// <summary>
        /// 获取历史某班次的数据
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="start">班次起始小时时间</param>
        ///   <param name="end">班次结束小时时间</param>
        ConveyorByDay GetHistoryShiftOneData(Motor motor, long start, long end);
        /// <summary>
        /// 获取历史某些班次的数据
        /// </summary>
        /// <param name="motor"></param>
        ///  <param name="start">起始时间</param>
        ///   <param name="end">结束时间（不包含）</param>
        ///  <param name="shiftStart">班次起始小时时间</param>
        ///   <param name="shiftEnd">班次结束小时时间（不包含）</param>
        IEnumerable<ConveyorByDay> GetHistoryShiftSomeData(Motor motor, long start, long end, int shiftStart, int shiftEnd);
        /// <summary>
        /// 获取当前班次实时数据统计
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="shiftStartHour">班次起始小时时间</param>
        IEnumerable<ConveyorByHour> GetShiftRealDatas(Motor motor, int shiftStartHour);
        #endregion
    }
}
