using System;
using System.Threading.Tasks;
using Yunt.TaskManager.Model;
using Yunt.TaskManager.Repository.Contract;
using Yunt.TaskManager.Service.Contract;

namespace Yunt.TaskManager.Service
{
    public class TbCategoryService:ITbCategoryService
    {
        private readonly ITbCategoryRepository _tbCategoryRepository;

        public TbCategoryService(ITbCategoryRepository tbCategoryRepository)
        {
            _tbCategoryRepository = tbCategoryRepository;
        }

        public async Task<PaginatedList<TbCategory>> GetTbCategories(DateTime start, DateTime end, int pageIndex, int pageSize)
        {
            var pagedList = await _tbCategoryRepository.GetTbCategories(start, end, pageIndex, pageSize);

            if (pageSize * (pageIndex - 1) >= pagedList.Count)
            {
                pageIndex = (int)Math.Ceiling(((double)pagedList.Count) / pageSize);
                pagedList = await _tbCategoryRepository.GetTbCategories(start, end, pageIndex, pageSize);
            }

            return pagedList;
        }
    }
}
