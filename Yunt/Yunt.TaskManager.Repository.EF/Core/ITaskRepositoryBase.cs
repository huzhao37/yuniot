using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Yunt.TaskManager.Repository.EF
{
    public interface ITaskRepositoryBase<T> where T : class
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null);
        T GetEntityById(int id);

        int CreateEntity(T t);
        int CreateEntities(IEnumerable<T> ts);
        int DeleteEntity(T t);
        int DeleteEntity(int id);
        int DeleteEntities(IEnumerable<T> ts);


        int UpdateEntity(T t);
        int UpdateEntities(IEnumerable<T> ts);



    }
}
