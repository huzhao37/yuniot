using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Inventory.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class SparePartsIdFactoriesRepository : InventoryRepositoryBase<SparePartsIdFactories, Models.IdModel.SparePartsIdFactories>, ISparePartsIdFactoriesRepository
    {
     
        public SparePartsIdFactoriesRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
