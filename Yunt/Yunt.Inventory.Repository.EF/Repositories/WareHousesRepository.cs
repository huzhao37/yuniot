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
using Yunt.Inventory.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class WareHousesRepository : InventoryRepositoryBase<WareHouses, Models.WareHouses>, IWareHousesRepository
    {
     
        public WareHousesRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }
        public virtual PaginatedList<WareHouses> GetPage(int pageIndex, int pageSize, string wareHouseId)
        {
            var source = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.WareHouses>().Where(e => e.WareHousesId.Equals(wareHouseId)).ProjectTo<WareHouses>(Mapper);
            var count = source.Count();
            List<WareHouses> dailys = null;
            if (count > 0)
            {
                dailys = source.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            return new PaginatedList<WareHouses>(pageIndex, pageSize, count, dailys ?? new List<WareHouses>());
        }

    }
}
