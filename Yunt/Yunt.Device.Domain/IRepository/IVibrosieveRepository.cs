using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IVibrosieveRepository : IDeviceRepositoryBase<Vibrosieve>
    {

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        new IQueryable<Vibrosieve> GetEntities(Expression<Func<Vibrosieve, bool>> where = null,
            Expression<Func<Vibrosieve, object>> order = null);


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
        IQueryable<Vibrosieve> GetEntities(string motorId, bool isExceed = false,
            Expression<Func<Vibrosieve, bool>> where = null, Expression<Func<Vibrosieve, object>> order = null);

        #endregion


        #region extend method

        /// <summary>
        /// 获取10min内的最新数据，没有的话，认作设备失联，通讯状态中断
        /// </summary>
        /// <param name="motorId">设备电机编号</param>
        /// <returns></returns>
        Vibrosieve GetLatestRecord(string motorId);
        /// <summary>
        /// 获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
        bool GetCurrentStatus(string motorId);


        #endregion
    }
}
