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

        #endregion
    }
}
