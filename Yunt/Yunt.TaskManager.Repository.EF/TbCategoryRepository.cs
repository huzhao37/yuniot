using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yunt.TaskManager.Model;
using Yunt.TaskManager.Repository.Contract;
using Yunt.TaskManager.Repository.EF.Core;

namespace Yunt.TaskManager.Repository.EF
{
    public class TbCategoryRepository<T>:TaskRepositoryBase<T>,ITbCategoryRepository where T: TbCategory
    {
        //private readonly TaskManagerContext _context;

        //public TbCategoryRepository(TaskManagerContext context)
        //{
        //    _context = context;
        //}


        public TbCategoryRepository(TaskManagerContext context) : base(context)
        {

        }

        public async Task<PaginatedList<TbCategory>> GetTbCategories(DateTime start, DateTime end, int pageIndex,
            int pageSize)
        {
            throw new NotImplementedException();     
        }

        public IQueryable<TbCategory> Get()
        {
            return GetEntities();
        }
        //public async Task<PaginatedList<TbCategory>> GetTbCategories(DateTime start, DateTime end, int pageIndex, int pageSize)
        //{
        //    var source = _context.TbCategory.Where(x => x.Categorycreatetime >= start && x.Categorycreatetime < new DateTime(end.Year, end.Month, end.Day).AddDays(1));
        //    int count = await source.CountAsync();
        //    List<TbCategory> dailys = null;
        //    if (count > 0)
        //    {
        //        dailys = await source.OrderBy(x => x.Categorycreatetime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        //    }

        //    return new PaginatedList<TbCategory>(pageIndex, pageSize, count, dailys ?? new List<TbCategory>());
        //}
    }
}
