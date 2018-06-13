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
    public class SparePartsTypeRepository : InventoryRepositoryBase<SparePartsType, Models.SparePartsType>, ISparePartsTypeRepository
    {
     
        public SparePartsTypeRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
