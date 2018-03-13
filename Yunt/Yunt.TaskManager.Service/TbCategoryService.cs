using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        #region query
        public IQueryable<TbCategory> Get(Expression<Func<TbCategory, bool>> where = null, Expression<Func<TbCategory, object>> order = null)
        {
            return  _tbCategoryRepository.Get(where,order);
        }
        public IQueryable<IGrouping<object, TbCategory>> Get(object paramter)
        {
           return _tbCategoryRepository.Get(paramter);
        }
        public async Task<PaginatedList<TbCategory>> GetByPage(DateTime start, DateTime end, int pageIndex,
            int pageSize)
        {
            return await _tbCategoryRepository.GetByPage(start, end, pageIndex, pageSize);
        }
        #endregion

        #region add


        public void Add(IEnumerable<TbCategory> dbs)
        {
            _tbCategoryRepository.Add(dbs);
        }
        public async Task AddAsyn(IEnumerable<TbCategory> dbs)
        {
            await _tbCategoryRepository.AddAsync(dbs);
        }

        public int Add(TbCategory db)
        {
            return _tbCategoryRepository.Add(db);
        }
        public async Task AddAsync(TbCategory db)
        {
            await _tbCategoryRepository.AddAsync(db);
        }

        #endregion

        #region update

        public int Update(TbCategory db)
        {
            return _tbCategoryRepository.Update(db);
        }

        public int Update(IEnumerable<TbCategory> dbs)
        {
            return _tbCategoryRepository.Update(dbs);

        }

        public void InsertOrUpd(TbCategory db)
        {
            _tbCategoryRepository.InsertOrUpd(db);
        }

        public async Task UpdateAsync(TbCategory db)
        {
            await   _tbCategoryRepository.UpdateAsync(db);
        }

        public async Task UpdateAsync(IEnumerable<TbCategory> dbs)
        {
            await _tbCategoryRepository.UpdateAsync(dbs);

        }

        public async Task InsertOrUpdAsync(TbCategory db)
        {
           await  _tbCategoryRepository.InsertOrUpdAsync(db);
        }

        #endregion

        #region delete
        public int Delete(int id)
        {
            return _tbCategoryRepository.Delete(id);
        }
        public async Task DeleteAsync(int id)
        {
            await  _tbCategoryRepository.DeleteAsync(id);
        }
        public int Delete(TbCategory t)
        {
            return _tbCategoryRepository.Delete(t);
        }

        public async Task DeleteAsync(TbCategory t)
        {
            await  _tbCategoryRepository.DeleteAsync(t); ;
        }

        public int Delete(IEnumerable<TbCategory> ts)
        {
            return _tbCategoryRepository.Delete(ts);
        }
        public async Task DeleteAsync(IEnumerable<TbCategory> ts)
        {
            await   _tbCategoryRepository.DeleteAsync(ts);
        }

        #endregion
    }
}
