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


    }
}
