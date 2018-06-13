using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Inventory.Domain.Model;
using Yunt.Common;

namespace Yunt.Inventory.Domain.IRepository
{
    public interface IInHouseRepository : IInventoryRepositoryBase<InHouse>
    {
        PaginatedList<InHouse> GetPage(int pageIndex, int pageSize, Expression<Func<InHouse, bool>> where);
    }
}
