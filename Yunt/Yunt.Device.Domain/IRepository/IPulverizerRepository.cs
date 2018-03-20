using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IPulverizerRepository : IDeviceRepositoryBase<Pulverizer>
    {

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        new IQueryable<Pulverizer> GetEntities(Expression<Func<Pulverizer, bool>> where = null,
            Expression<Func<Pulverizer, object>> order = null);


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExceed">是否超出2个月数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IQueryable<Pulverizer> GetEntities(bool isExceed = false,
            Expression<Func<Pulverizer, bool>> where = null, Expression<Func<Pulverizer, object>> order = null);

        #endregion
    }
}
