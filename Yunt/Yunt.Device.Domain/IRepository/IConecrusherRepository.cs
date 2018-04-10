using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Common;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IConeCrusherRepository : IDeviceRepositoryBase<ConeCrusher>
    {
        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        new IQueryable<ConeCrusher> GetEntities(Expression<Func<ConeCrusher, bool>> where = null,
            Expression<Func<ConeCrusher, object>> order = null);


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 仅可在某一天内查找数据，需要精确具体时间点的，在where中添加过滤，跨越一天的数据，请自行累加
        /// </summary>
        /// <param name="motorId"></param>
        ///  <param name="time"></param>
        /// <param name="isExceed">是否超出3 months数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IQueryable<ConeCrusher> GetEntities(string motorId, DateTime time, bool isExceed = false,
            Expression<Func<ConeCrusher, bool>> where = null, Expression<Func<ConeCrusher, object>> order = null);

        #endregion

        #region extend method

        /// <summary>
        /// 获取10min内的最新数据，没有的话，认作设备失联，通讯状态中断
        /// </summary>
        /// <param name="motorId">设备电机编号</param>
        /// <returns></returns>
        ConeCrusher GetLatestRecord(string motorId);

        /// <summary>
        /// 获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        int GetTodayRunningTime(string motorId);

        /// <summary>
        /// 获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
        MotorStatus GetCurrentStatus(string motorId);

        #endregion
    }
}
