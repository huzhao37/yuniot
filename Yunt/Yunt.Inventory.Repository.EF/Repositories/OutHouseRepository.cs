using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class OutHouseRepository : InventoryRepositoryBase<OutHouse, Models.OutHouse>, IOutHouseRepository
    {

        public OutHouseRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
            
        }
        /// <summary>
        /// 获取所有设备相关的备件消耗明细
        /// </summary>
        /// <param name="motorIds"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<OutHouse> GetUseless(IEnumerable<string> motorIds,long start,long end)
        {
            return GetEntities(e => motorIds.Contains(e.MotorId) &&
                                e.SparePartsStatus.Equals(SparePartsTypeStatus.Useless) && 
                                e.UselessTime>=start&&e.UselessTime<=end)?.OrderBy(x=>x.UselessTime)?.ToList();
        }
        /// <summary>
        /// 计算备件成本
        /// </summary>
        /// <param name="spares"></param>
        /// <returns></returns>
        public float CalcCost(IEnumerable<OutHouse> spares)
        {
           return (float)Math.Round(spares?.Sum(e => e.UnitPrice)??0,2);
        }
    }
}
