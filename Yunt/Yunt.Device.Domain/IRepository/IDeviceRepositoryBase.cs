using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yunt.Common;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.IRepository
{
    public interface IDeviceRepositoryBase<T> where T : AggregateRoot
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null);

        IQueryable<IGrouping<object, T>> GetEntities(object paramter);

        T GetEntityById(int id);
        Task<PaginatedList<T>> GetPage(int pageIndex, int pageSize);

        /// <summary>
        /// （跳过缓存从数据库中获取数据）慎用！！！
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IQueryable<T> GetFromSqlDb(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null);
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

        /// <summary>
        /// 批量提交
        /// </summary>
       void Batch();

        #region redis_cache
        /// <summary>
        /// 缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motorId">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        int Cache(string motorId, long dayUnix, List<T> ts);
        #endregion

        #region version 18.7.20
        [Obsolete("forbiden:only use for recovery")]
        Task UpdateAsync(T t);
        /// <summary>
        /// （跳过缓存从数据库中获取数据）慎用！！！
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        List<T> GetFromDb(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null);
        #endregion
    }
}
