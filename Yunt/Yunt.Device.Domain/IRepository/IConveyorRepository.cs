using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IConveyorRepository : IDeviceRepositoryBase<Conveyor>
    {

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        new IQueryable<Conveyor> GetEntities(Expression<Func<Conveyor, bool>> where = null,
            Expression<Func<Conveyor, object>> order = null);


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="isExceed">是否超出2 hours数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IQueryable<Conveyor> GetEntities(string motorId, bool isExceed = false,
            Expression<Func<Conveyor, bool>> where = null, Expression<Func<Conveyor, object>> order = null);

        #endregion

        #region extend method

        /// <summary>
        /// 获取10min内的最新数据，没有的话，认作设备失联，通讯状态中断
        /// </summary>
        /// <param name="motorId">设备电机编号</param>
        /// <returns></returns>
        Conveyor GetLatestRecord(string motorId);

        /// <summary>
        /// 根据电流获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        int GetTodayRunningTimeByCurrent(string motorId);

        /// <summary>
        /// 根据瞬时称重获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        int GetTodayRunningTimeByInstant(string motorId);

        /// <summary>
        /// 获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
         bool GetCurrentStatus(string motorId);

        #endregion
    }
}
