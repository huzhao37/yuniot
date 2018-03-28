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
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Auth.Domain.Model.IdModel;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Auth.Repository.EF.Repositories
{
    public class UserRepository : AuthRepositoryBase<User, Models.User>, IUserRepository
    {
        private readonly IUserIdFactoriesRepository _idRep;

        public UserRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
            _idRep =
                ServiceProviderServiceExtensions.GetService<IUserIdFactoriesRepository>(BootStrap.ServiceProvider);
        }

        #region Insert
        public override int Insert( User t)
        {
            var idfac=_idRep.GetEntities().FirstOrDefault();
            var newIndex = idfac?. UserIndex + 1 ?? 1;
            if (idfac != null)
            {
                idfac. UserIndex = newIndex;              
            }
            else
            {
                idfac=new UserIdFactories(){ UserIndex = newIndex, Time = DateTime.Now.TimeSpan()};

            }
            _idRep.InsertOrUpdate(idfac);
            t. UserId = newIndex.NewUser(newIndex);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Add(Mapper.Map<Models. User>(t));
            return Commit();
        }
        public override async Task InsertAsync( User t)
        {
            var idfac = _idRep.GetEntities().FirstOrDefault();
            var newIndex = idfac?. UserIndex ?? 0 + 1;
            if (idfac != null)
            {
                idfac. UserIndex = newIndex;
            }
            else
            {
                idfac = new UserIdFactories() {  UserIndex = newIndex,  Time = DateTime.Now.TimeSpan() };
            }
            _idRep.InsertOrUpdate(idfac);
            t. UserId = newIndex.NewUser(  newIndex);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().AddAsync(Mapper.Map<Models. User>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable< User> ts)
        {
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities().FirstOrDefault();
                    var newIndex = idfac?. UserIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac. UserIndex = newIndex;
                    }
                    else
                    {
                        idfac = new UserIdFactories() {  UserIndex = newIndex, Time = DateTime.Now.TimeSpan() };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t. UserId = newIndex.NewUser( newIndex);
                }
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().AddRange(Mapper.Map<IEnumerable<Models. User>>(ts));
                return Commit();
                
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public override async Task InsertAsync(IEnumerable< User> ts)
        {
           
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities().FirstOrDefault();
                    var newIndex = idfac?. UserIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac. UserIndex = newIndex;
                    }
                    else
                    {
                        idfac = new UserIdFactories() {  UserIndex = newIndex,  Time = DateTime.Now.TimeSpan() };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t. UserId = newIndex.NewUser(newIndex);
                }
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().AddRangeAsync(Mapper.Map<IEnumerable<Models. User>>(ts));
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
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Find(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().FindAsync(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Remove(Mapper.Map<Models. User>(t));
            await CommitAsync();
        }
        public override int DeleteEntity( User t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Remove(Mapper.Map<Models. User>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync( User t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Remove(Mapper.Map<Models. User>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable< User> ts)
        {
            int results;
            
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().RemoveRange(Mapper.Map<IEnumerable<Models. User>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable< User> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().RemoveRange(Mapper.Map<IEnumerable<Models. User>>(ts));
                    await CommitAsync();
                  
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func< User, bool>> where = null)
        {
            IQueryable<Models. User> ts;
            Expression<Func<Models. User, bool>> wheres;
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func< User, bool>>, Expression<Func<Models. User, bool>>>(where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models. User>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models. User>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func< User, bool>> where = null)
        {
            IQueryable<Models. User> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func< User, bool>>, Expression<Func<Models. User, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>();
            }
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models. User>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models. User>>(ts));
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

        public override int UpdateEntity( User t)
        {

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Update(Mapper.Map<Models. User>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable< User> ts)
        {
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().UpdateRange(Mapper.Map<IEnumerable<Models. User>>(ts));
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

        public override void InsertOrUpdate( User t)
        {
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models. User>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models. User>(t));
            }

           var result = Commit();
        }

        public override async Task UpdateEntityAsync( User t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Update(Mapper.Map<Models. User>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable< User> ts)
        {
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().UpdateRange(Mapper.Map<IEnumerable<Models. User>>(ts));
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

        public override async Task InsertOrUpdateAsync( User t)
        {
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set< User>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models. User>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models. User>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        public override IQueryable<  User> GetEntities(Expression<Func<  User, bool>> where = null, Expression<Func< User, object>> order = null)
        {
          

            Expression<Func<Models. User, bool>> wheres;
            Expression<Func<Models. User, object>> orderby;
            IQueryable< User> sql = null;
            if (where != null && order != null)
            {              
                wheres = Mapper.MapExpression<Expression<Func< User, bool>>, Expression<Func<Models. User, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func< User, object>>, Expression<Func<Models. User, object>>>(order);
                // RedisProvider.DB = 1;
                //var list = RedisProvider.ListRange< User>(" User", DataType.Protobuf).Where(wheres);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().Where(wheres).OrderBy(orderby).ProjectTo< User>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }

            if (order != null)
            {
                orderby = Mapper.MapExpression<Expression<Func< User, object>>, Expression<Func<Models. User, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().OrderBy(orderby).ProjectTo< User>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func< User, bool>>, Expression<Func<Models. User, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.User>().Where(wheres).ProjectTo< User>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().ProjectTo< User>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        public override IQueryable<IGrouping<object,  User>> GetEntities(object paramter)
        {
            var sql = new RawSqlString($"select {paramter} from dbo.{typeof(Models. User).Name} group by {paramter}");
            var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().FromSql(sql).ProjectTo< User>(Mapper);
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
        public override IQueryable< User> GetEntities(RawSqlString sql, params object[] paramterss)
        {
            try
            {
                var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models. User>().FromSql(sql, paramterss).ProjectTo< User>(Mapper);
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
        public override  User GetEntityById(int id)
        {
            //return _context.Set<T>().SingleOrDefault(e => e.Id == id);
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Find<Models. User>(id).MapTo< User>();
        }


        #endregion

    }
}
