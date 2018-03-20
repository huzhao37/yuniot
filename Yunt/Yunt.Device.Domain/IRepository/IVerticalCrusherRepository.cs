using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IVerticalCrusherRepository : IDeviceRepositoryBase<VerticalCrusher>
    {

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        new IQueryable<VerticalCrusher> GetEntities(Expression<Func<VerticalCrusher, bool>> where = null,
            Expression<Func<VerticalCrusher, object>> order = null);


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
        IQueryable<VerticalCrusher> GetEntities(bool isExceed = false,
            Expression<Func<VerticalCrusher, bool>> where = null, Expression<Func<VerticalCrusher, object>> order = null);

        #endregion
    }
}
