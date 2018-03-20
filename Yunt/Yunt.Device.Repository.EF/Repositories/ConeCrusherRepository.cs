using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Common;
using Yunt.Redis;
using AutoMapper.XpressionMapper.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ConeCrusherRepository : DeviceRepositoryBase<ConeCrusher, Models.ConeCrusher>, IConeCrusherRepository
    {

        public ConeCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(ConeCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Add(Mapper.Map<Models.ConeCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(ConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().AddAsync(Mapper.Map<Models.ConeCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<ConeCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().AddRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
                var result=Commit();

                 RedisProvider.DB = 16;
                 RedisProvider.Sadd(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
           
                return result;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public override async Task InsertAsync(IEnumerable<ConeCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
                await CommitAsync();

                RedisProvider.DB = 16;
                RedisProvider.Sadd(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Remove(Mapper.Map<Models.ConeCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(ConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Remove(Mapper.Map<Models.ConeCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(ConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Remove(Mapper.Map<Models.ConeCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<ConeCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<ConeCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<ConeCrusher, bool>> where = null)
        {
            IQueryable<Models.ConeCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ConeCrusher, bool>>, Expression<Func<Models.ConeCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ConeCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<ConeCrusher, bool>> where = null)
        {
            IQueryable<Models.ConeCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ConeCrusher, bool>>, Expression<Func<Models.ConeCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ConeCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
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

        public override int UpdateEntity(ConeCrusher t)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Update(Mapper.Map<Models.ConeCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<ConeCrusher> ts)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
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

        public override void InsertOrUpdate(ConeCrusher t)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.ConeCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ConeCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(ConeCrusher t)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Update(Mapper.Map<Models.ConeCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<ConeCrusher> ts)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ConeCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(ConeCrusher t)
        {
            Logger.Warn("[ConeCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ConeCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.ConeCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ConeCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<ConeCrusher> GetEntities(Expression<Func<ConeCrusher, bool>> where = null, Expression<Func<ConeCrusher, object>> order = null)
        {
            Logger.Error("[ConeCrusher]:forbidden use!");
            return new List<ConeCrusher>().AsQueryable();
        }


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isExceed">是否超出2个月数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<ConeCrusher> GetEntities(bool isExceed = false,Expression < Func<ConeCrusher, bool>> where = null, Expression<Func<ConeCrusher, object>> order = null)
        {


            Expression<Func<Models.ConeCrusher, bool>> wheres;
            Expression<Func<Models.ConeCrusher, object>> orderby;
            IQueryable<ConeCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<ConeCrusher>("ConeCrusher", DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ConeCrusher, bool>>, Expression<Func<Models.ConeCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<ConeCrusher, object>>, Expression<Func<Models.ConeCrusher, object>>>(order);
                
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<ConeCrusher>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }

            if (order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<ConeCrusher>("ConeCrusher", DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<ConeCrusher, object>>, Expression<Func<Models.ConeCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().OrderBy(orderby).ProjectTo<ConeCrusher>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (where != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<ConeCrusher>("ConeCrusher", DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ConeCrusher, bool>>, Expression<Func<Models.ConeCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().Where(wheres).ProjectTo<ConeCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.HashGetAllValues<ConeCrusher>("ConeCrusher", DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ConeCrusher>().ProjectTo<ConeCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion
    }
}
