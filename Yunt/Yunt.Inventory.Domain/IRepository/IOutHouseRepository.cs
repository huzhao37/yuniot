using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Inventory.Domain.Model;
using Yunt.Common;

namespace Yunt.Inventory.Domain.IRepository
{
    public interface IOutHouseRepository : IInventoryRepositoryBase<OutHouse>
    {

        /// <summary>
        /// 获取所有设备相关的备件消耗明细
        /// </summary>
        /// <param name="motorIds"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
       IEnumerable<OutHouse> GetUseless(IEnumerable<string> motorIds, long start, long end);
        /// <summary>
        /// 计算备件成本
        /// </summary>
        /// <param name="spares"></param>
        /// <returns></returns>
        float CalcCost(IEnumerable<OutHouse> spares);
    }
}
