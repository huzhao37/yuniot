using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class WareHousesRepository : InventoryRepositoryBase<WareHouses, Models.WareHouses>, IWareHousesRepository
    {
     
        public WareHousesRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }
    

    }
}
