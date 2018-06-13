using System;
using System.Collections.Generic;
using System.Text;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.Services
{
    public interface IProductionLineRepository : IDeviceRepositoryBase<ProductionLine>
    {
        #region extend method

        /// <summary>
        /// 根据产线ID获取所有失联、停止、运行电机数目
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
         Tuple<int, int, int> GetMotors(string lineId);

        /// <summary>
        /// 获取产线状态
        /// </summary>
        /// <param name="lineId">产线Id</param>
        /// <returns></returns>
         bool GetStatus(string lineId);

        /// <summary>
        /// 根据电机设备ID获取设备状态
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        MotorStatus GetMotorStatusByMotorId(string motorId);

        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        dynamic MotorDetails(Motor motor);

        /// <summary>
        /// 根据电机设备ID和时间节点获取电机设备详情(不包括今天)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        IEnumerable<dynamic> MotorDays(long start, long end, Motor motor);

        /// <summary>
        /// 根据动态数据获取设备详情
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        dynamic GetMotorDetails(IEnumerable<dynamic> datas, Motor motor);
        /// <summary>
        /// 根据电机设备ID和时间节点获取电机设备详情
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="motorId"></param>
        /// <returns></returns>
        IEnumerable<dynamic> MotorDetails(DateTime start, DateTime end, string motorId);

        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        IEnumerable<dynamic> MotorHours( Motor motor);
        /// <summary>
        /// 缓存中是否存在该产线下的设备原始数据库
        /// </summary>
        /// <param name="productionLineId"></param>
        /// <returns></returns>
        bool  GetInstanceFromRedis(string productionLineId);

        /// <summary>
        /// 读取缓存预热原始数据(慎用！！！)
        /// </summary>
        /// <param name="motorId"></param>
        ///  <param name="dt">日期</param>
        /// <returns></returns>
        [Obsolete]
        IEnumerable<dynamic> PreCache(string motorId, DateTime dt);

        /// <summary>
        /// 获取设备原始数据
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="date">日期</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        IEnumerable<dynamic> GetMotorHistoryByDate(Motor motor, DateTime date, bool cache);

        #endregion
    }
}
