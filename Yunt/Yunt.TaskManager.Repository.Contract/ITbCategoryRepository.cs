using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yunt.TaskManager.Model;

namespace Yunt.TaskManager.Repository.Contract
{
    public interface ITbCategoryRepository
    {
        IQueryable<TbCategory> Get();

        Task<PaginatedList<TbCategory>> GetByPage(DateTime start, DateTime end, int pageIndex,
            int pageSize);

        #region add

        int Add(IEnumerable<TbCategory> dbs);
        Task AddAsync(IEnumerable<TbCategory> dbs);
        int Add(TbCategory db);
        Task AddAsync(TbCategory db);

        #endregion

        #region update

        int Update(TbCategory db);

         int Update(IEnumerable<TbCategory> dbs);

         void InsertOrUpd(TbCategory db);

        Task UpdateAsync(TbCategory db);
        Task UpdateAsync(IEnumerable<TbCategory> dbs);

         Task InsertOrUpdAsync(TbCategory db);

        #endregion

        #region delete

        int Delete(int id);
         Task DeleteAsync(int id);
        int Delete(TbCategory t);

         Task DeleteAsync(TbCategory t);
        int Delete(IEnumerable<TbCategory> ts);
        Task DeleteAsync(IEnumerable<TbCategory> ts);

        #endregion

    }
}
