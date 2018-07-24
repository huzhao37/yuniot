using System;
using System.Collections.Generic;
using System.Text;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.MapModel;
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
        /// 获取设备瞬时详情项
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        dynamic GetMotorInstantDetails( Motor motor );
        /// <summary>
        /// 根据动态数据获取设备历史详情项
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        dynamic GetMotorHistoryDetails(IEnumerable<dynamic> datas, Motor motor);
        /// <summary>
        /// 根据动态数据获取设备详情图表
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="motor"></param>
        /// <returns></returns>
        dynamic GetMotorSeries(IEnumerable<dynamic> datas, Motor motor);
        /// <summary>
        /// 根据电机设备ID和时间节点获取电机设备详情
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="motorId"></param>
        /// <returns></returns>
        //IEnumerable<dynamic> MotorDetails(DateTime start, DateTime end, string motorId);

        /// <summary>
        /// 根据电机设备ID获取当日电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        IEnumerable<dynamic> MotorHours( Motor motor);
        /// <summary>
        /// 根据电机设备ID获取历史某一天电机设备详情
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        IEnumerable<dynamic> MotorHours(Motor motor, long date);
        /// <summary>
        /// 缓存中是否存在该产线下的设备原始数据库
        /// </summary>
        /// <param name="productionLineId"></param>
        /// <returns></returns>
        bool  GetInstanceFromRedis(string productionLineId);
        /// <summary>
        /// 删除缓存（慎用！）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motor">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        [Obsolete]
         int DelCache(Motor motor, DateTime dt);
        /// <summary>
        /// 缓存预热（慎用！）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motor">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        [Obsolete]
        int PreCache(Motor motor, DateTime dt);
        /// <summary>
        ///  缓存预热（慎用！）同一天内的某段时间数据
        /// </summary>
        /// <param name="motor"></param>
        /// <param name="dayUnix"></param>
        /// <param name="start"></param>
        /// <param name="end">不包括</param>
        /// <returns></returns>
        [Obsolete]
        int PreCache2(Motor motor, long dayUnix, long start, long end);
        /// <summary>
        /// 获取设备原始数据
        /// </summary>
        /// <param name="motor">设备</param>
        /// <param name="date">日期</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        IEnumerable<dynamic> GetMotorHistoryByDate(Motor motor, DateTime date, bool cache);


        /// <summary>
        /// 计算当日产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        List<PowerCal> CalcMotorPowers(List<Motor> motors);
        /// <summary>
        /// 计算历史区间内产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        List<PowerCal> CalcMotorPowers(List<Motor> motors, long start, long end);

        /// <summary>
        /// 计算历史某一天产线耗电量明细
        /// </summary>
        /// <param name="motors"></param>
        /// <returns></returns>
        List<PowerCal> CalcMotorPowers(List<Motor> motors, long date);

        /// <summary>
        /// 根据动态数据获取移动端设备详情
        /// </summary>
        /// <param name="datas">需要先排序</param>
        /// <param name="motor"></param>
        ///  <param name="isInstant"></param>
        /// <returns></returns>
         dynamic GetMobileMotorDetails(IEnumerable<dynamic> datas, Motor motor, bool isInstant);
        #endregion

        #region version 18.07.17
        /// <summary>
        /// 根据电机设备获取电机设备瞬时负荷
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        float MotorIntantLoadStall(Motor motor);
        #endregion
    }
}
