using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class InventoryAlarmInfoRepository : InventoryRepositoryBase<InventoryAlarmInfo, Models.InventoryAlarmInfo>, IInventoryAlarmInfoRepository
    {
     
        public InventoryAlarmInfoRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
