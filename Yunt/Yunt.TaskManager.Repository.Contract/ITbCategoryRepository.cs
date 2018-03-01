using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yunt.TaskManager.Model;

namespace Yunt.TaskManager.Repository.Contract
{
    public interface ITbCategoryRepository
    {
        Task<PaginatedList<TbCategory>> GetTbCategories(DateTime start, DateTime end, int pageIndex, int pageSize);
        IQueryable<TbCategory> Get();
    }
}
