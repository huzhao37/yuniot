using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;
using Yunt.Inventory.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Inventory.Repository.EF.Repositories
{
    public class SparePartsRepository : InventoryRepositoryBase< SpareParts, Models. SpareParts>, ISparePartsRepository
    {
        private readonly ISparePartsIdFactoriesRepository _idRep;

        public SparePartsRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
            _idRep =
                ServiceProviderServiceExtensions.GetService<ISparePartsIdFactoriesRepository>(BootStrap.ServiceProvider);
        }

        #region Insert
        public override int Insert( SpareParts t)
        {
            var idfac=_idRep.GetEntities(e => e.SparePartsTypeId.Equals(t.SparePartsTypeId)).FirstOrDefault();
            var newIndex = idfac?. SparePartsIndex + 1 ?? 1;
            if (idfac != null)
            {
                idfac. SparePartsIndex = newIndex;              
            }
            else
            {
                idfac=new SparePartsIdFactories(){ SparePartsIndex = newIndex, SparePartsTypeId = t. SparePartsTypeId,Time = DateTime.Now.TimeSpan()};

            }
            _idRep.InsertOrUpdate(idfac);
            t. SparePartsId = newIndex.NewSpareParts( t. SparePartsTypeId, newIndex);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Add(Mapper.Map<Models. SpareParts>(t));
            return Commit();
        }
        public override async Task InsertAsync( SpareParts t)
        {
            var idfac = _idRep.GetEntities(e =>  e. SparePartsTypeId.Equals(t. SparePartsTypeId)).FirstOrDefault();
            var newIndex = idfac?. SparePartsIndex ?? 0 + 1;
            if (idfac != null)
            {
                idfac. SparePartsIndex = newIndex;
            }
            else
            {
                idfac = new SparePartsIdFactories() {  SparePartsIndex = newIndex,  SparePartsTypeId = t. SparePartsTypeId,  Time = DateTime.Now.TimeSpan() };
            }
            _idRep.InsertOrUpdate(idfac);
            t. SparePartsId = newIndex.NewSpareParts( t. SparePartsTypeId, newIndex);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().AddAsync(Mapper.Map<Models. SpareParts>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable< SpareParts> ts)
        {
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities(e => e.SparePartsTypeId.Equals(t.SparePartsTypeId) && e. SparePartsTypeId.Equals(t. SparePartsTypeId)).FirstOrDefault();
                    var newIndex = idfac?. SparePartsIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac. SparePartsIndex = newIndex;
                    }
                    else
                    {
                        idfac = new SparePartsIdFactories() {  SparePartsIndex = newIndex,  SparePartsTypeId = t. SparePartsTypeId, Time = DateTime.Now.TimeSpan() };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t. SparePartsId = newIndex.NewSpareParts( t. SparePartsTypeId, newIndex);
                }
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().AddRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                return Commit();
                
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public override async Task InsertAsync(IEnumerable< SpareParts> ts)
        {
           
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities(e => e. SparePartsTypeId.Equals(t. SparePartsTypeId)).FirstOrDefault();
                    var newIndex = idfac?. SparePartsIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac. SparePartsIndex = newIndex;
                    }
                    else
                    {
                        idfac = new SparePartsIdFactories() {  SparePartsIndex = newIndex,  SparePartsTypeId = t. SparePartsTypeId,  Time = DateTime.Now.TimeSpan() };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t. SparePartsId = newIndex.NewSpareParts(t. SparePartsTypeId, newIndex);
                }
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().AddRangeAsync(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                await CommitAsync();
               
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");

            }
        }

        #endregion

        #region delete
        public override int DeleteEntity(int id)
        {
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Find(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().FindAsync(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Remove(Mapper.Map<Models. SpareParts>(t));
            await CommitAsync();
        }
        public override int DeleteEntity( SpareParts t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Remove(Mapper.Map<Models. SpareParts>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync( SpareParts t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Remove(Mapper.Map<Models. SpareParts>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable< SpareParts> ts)
        {
            int results;
            
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().RemoveRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    results = Commit();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
       
            return results;
        }
        public override async Task DeleteEntityAsync(IEnumerable< SpareParts> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().RemoveRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    await CommitAsync();
                  
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func< SpareParts, bool>> where = null)
        {
            IQueryable<Models. SpareParts> ts;
            Expression<Func<Models. SpareParts, bool>> wheres;
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func< SpareParts, bool>>, Expression<Func<Models. SpareParts, bool>>>(where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models. SpareParts>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    results = Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
            }
            return results;
        }

        public override async Task DeleteEntityAsync(Expression<Func< SpareParts, bool>> where = null)
        {
            IQueryable<Models. SpareParts> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func< SpareParts, bool>>, Expression<Func<Models. SpareParts, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>();
            }
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models. SpareParts>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    await CommitAsync();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }
        #endregion

        #region update

        public override int UpdateEntity( SpareParts t)
        {

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Update(Mapper.Map<Models. SpareParts>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable< SpareParts> ts)
        {
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().UpdateRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    results = Commit();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
            }
            return results;

        }

        public override void InsertOrUpdate( SpareParts t)
        {
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models. SpareParts>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models. SpareParts>(t));
            }

           var result = Commit();
        }

        public override async Task UpdateEntityAsync( SpareParts t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Update(Mapper.Map<Models. SpareParts>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable< SpareParts> ts)
        {
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().UpdateRange(Mapper.Map<IEnumerable<Models. SpareParts>>(ts));
                    await CommitAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    // transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }

        }

        public override async Task InsertOrUpdateAsync( SpareParts t)
        {
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set< SpareParts>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models. SpareParts>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models. SpareParts>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        public override IQueryable<  SpareParts> GetEntities(Expression<Func<  SpareParts, bool>> where = null, Expression<Func< SpareParts, object>> order = null)
        {
          

            Expression<Func<Models. SpareParts, bool>> wheres;
            Expression<Func<Models. SpareParts, object>> orderby;
            IQueryable< SpareParts> sql = null;
            if (where != null && order != null)
            {              
                wheres = Mapper.MapExpression<Expression<Func< SpareParts, bool>>, Expression<Func<Models. SpareParts, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func< SpareParts, object>>, Expression<Func<Models. SpareParts, object>>>(order);
                // RedisProvider.DB = 1;
                //var list = RedisProvider.ListRange< SpareParts>(" SpareParts", DataType.Protobuf).Where(wheres);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Where(wheres).OrderBy(orderby).ProjectTo< SpareParts>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }

            if (order != null)
            {
                orderby = Mapper.MapExpression<Expression<Func< SpareParts, object>>, Expression<Func<Models. SpareParts, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().OrderBy(orderby).ProjectTo< SpareParts>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func< SpareParts, bool>>, Expression<Func<Models. SpareParts, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().Where(wheres).ProjectTo< SpareParts>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().ProjectTo< SpareParts>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        public override IQueryable<IGrouping<object,  SpareParts>> GetEntities(object paramter)
        {
            var sql = new RawSqlString($"select {paramter} from dbo.{typeof(Models. SpareParts).Name} group by {paramter}");
            var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().FromSql(sql).ProjectTo< SpareParts>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{query.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, query.ToUnevaluated()));
#endif
            return null;
        }
        /// <summary>
        /// 复杂查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramterss"></param>
        /// <returns></returns>
        public override IQueryable< SpareParts> GetEntities(RawSqlString sql, params object[] paramterss)
        {
            try
            {
                var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. SpareParts>().FromSql(sql, paramterss).ProjectTo< SpareParts>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{query.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, query.ToUnevaluated()));
#endif
                return query;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                //throw;
                return null;
            }

        }
        public override  SpareParts GetEntityById(int id)
        {
            //return _context.Set<T>().SingleOrDefault(e => e.Id == id);
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Find<Models. SpareParts>(id).MapTo< SpareParts>();
        }

        public virtual PaginatedList<SpareParts> GetPage(int pageIndex, int pageSize, SparePartsStatus status)
        {
            var source = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SpareParts>().Where(e=>e.SparePartsStatus.Equals(status)).ProjectTo<SpareParts>(Mapper);
            var count = source.Count();
            List<SpareParts> dailys = null;
            if (count > 0)
            {
                dailys = source.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            return new PaginatedList<SpareParts>(pageIndex, pageSize, count, dailys ?? new List<SpareParts>());
        }
        #endregion

    }
}
