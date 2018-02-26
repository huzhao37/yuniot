using System;
using System.Threading.Tasks;
using Yunt.TaskManager.Model;

namespace Yunt.TaskManager.Service.Contract
{
    public interface ITbCategoryService
    {
        Task<PaginatedList<TbCategory>> GetTbCategories(DateTime start, DateTime end, int pageIndex, int pageSize);
    }
}
