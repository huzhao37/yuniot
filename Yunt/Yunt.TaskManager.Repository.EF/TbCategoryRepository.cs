using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yunt.TaskManager.Model;
using Yunt.TaskManager.Repository.Contract;
using Yunt.TaskManager.Repository.EF.Core;

namespace Yunt.TaskManager.Repository.EF
{
    public class TbCategoryRepository :TaskRepositoryBase<TbCategory>,ITbCategoryRepository
    {
        //private readonly TaskManagerContext _context;

        //public TbCategoryRepository(TaskManagerContext context)
        //{
        //    _context = context;
        //}


        public TbCategoryRepository(TaskManagerContext context) : base(context)
        {

        }

        #region query
        public async Task<PaginatedList<TbCategory>> GetByPage(DateTime start, DateTime end, int pageIndex,
            int pageSize)
        {
            return await GetPage(start, end, pageIndex, pageSize);
        }

        public IQueryable<TbCategory> Get(Expression<Func<TbCategory, bool>> where = null)
        {
            return GetEntities(where);
        }
        #endregion
        
        #region add

        public int Add(IEnumerable<TbCategory> dbs)
        {
            return Insert(dbs);
        }
        public async Task AddAsync(IEnumerable<TbCategory> dbs)
        {
            await InsertAsync(dbs);
        }

        public int Add(TbCategory db)
        {
            return Insert(db);
        }
        public async Task AddAsync(TbCategory db)
        {
            await InsertAsync(db);
        }

        #endregion

        #region update

        public int Update(TbCategory db)
        {          
            return UpdateEntity(db);
        }

        public int Update(IEnumerable<TbCategory> dbs)
        {
            return UpdateEntity(dbs);

        }

        public void InsertOrUpd(TbCategory db)
        {
             InsertOrUpdate(db);
        }

        public async Task UpdateAsync(TbCategory db)
        {
            await   UpdateEntityAsync(db);
        }

        public async Task UpdateAsync(IEnumerable<TbCategory> dbs)
        {
            await  UpdateEntityAsync(dbs);

        }

        public async Task InsertOrUpdAsync(TbCategory db)
        {
            await  InsertOrUpdateAsync(db);
        }

        #endregion
  
        #region delete
        public int Delete(int id)
        {
            return DeleteEntity(id);
        }
        public async Task DeleteAsync(int id)
        {
            await DeleteEntityAsync(id);
        }
        public int Delete(TbCategory t)
        {
            return DeleteEntity(t);
        }

        public async Task DeleteAsync(TbCategory t)
        {
            await  DeleteEntityAsync(t);
        }

        public int Delete(IEnumerable<TbCategory> ts)
        {
            return DeleteEntity(ts);
        }
        public async Task DeleteAsync(IEnumerable<TbCategory> ts)
        {
            await DeleteEntityAsync(ts);
        }

        #endregion

        
    }
}
