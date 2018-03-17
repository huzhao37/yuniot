﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.IRepository
{
    public interface IDeviceRepositoryBase<T> where T : AggregateRoot
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null);

        IQueryable<IGrouping<object, T>> GetEntities(object paramter);

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
        Task DeleteEntityAsync(Expression<Func<T, bool>> where = null);
        int DeleteEntity(Expression<Func<T, bool>> where = null);
        int UpdateEntity(T t);
        int UpdateEntity(IEnumerable<T> ts);
        void InsertOrUpdate(T t);
        Task UpdateEntityAsync(T t);
        Task UpdateEntityAsync(IEnumerable<T> ts);

        Task InsertOrUpdateAsync(T t);
    }
}
