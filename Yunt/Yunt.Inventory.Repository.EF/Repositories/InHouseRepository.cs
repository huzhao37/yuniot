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
    public class InHouseRepository : InventoryRepositoryBase<InHouse, Models.InHouse>, IInHouseRepository
    {
      //  private readonly ISparePartsIdFactoriesRepository _idRep;

        public InHouseRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
            //_idRep =
            //    ServiceProviderServiceExtensions.GetService<ISparePartsIdFactoriesRepository>(BootStrap.ServiceProvider);
        }

        public virtual PaginatedList<InHouse> GetPage(int pageIndex, int pageSize, Expression<Func<InHouse, bool>> where)
        {
            var wheres = Mapper.MapExpression<Expression<Func<InHouse, bool>>, Expression<Func<Models.InHouse, bool>>>(where);
            var source = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.InHouse>().Where(wheres).ProjectTo<InHouse>(Mapper);
            var count = source.Count();
            List<InHouse> dailys = null;
            if (count > 0)
            {
                dailys = source.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            return new PaginatedList<InHouse>(pageIndex, pageSize, count, dailys ?? new List<InHouse>());
        }

    }
}
