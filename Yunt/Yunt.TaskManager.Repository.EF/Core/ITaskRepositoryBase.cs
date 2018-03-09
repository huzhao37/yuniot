using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.TaskManager.Repository.EF
{
    public interface ITaskRepositoryBase<T> where T : class
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null);
        T GetEntityById(int id);

        int Insert(T t);
        Task InsertAsync(T t);
        int Insert(IEnumerable<T> ts);
        Task InsertAsync(IEnumerable<T> ts);
        int DeleteEntity(T t);
        Task DeleteEntityAsync(T t);
        int DeleteEntity(int id);
        Task DeleteEntityAsync(int id);
        int DeleteEntity(IEnumerable<T> ts);
        Task DeleteEntityAsync(IEnumerable<T> ts);

        int UpdateEntity(T t);
        int UpdateEntity(IEnumerable<T> ts);
        void InsertOrUpdate(T t);
        Task UpdateEntityAsync(T t);
        Task UpdateEntityAsync(IEnumerable<T> ts);

        Task InsertOrUpdateAsync(T t);
        // Task CommitAsync();
        //int Commit();
    }
}
