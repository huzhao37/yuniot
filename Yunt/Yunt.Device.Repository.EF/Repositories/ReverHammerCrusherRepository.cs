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
    public class ReverHammerCrusherRepository : DeviceRepositoryBase<ReverHammerCrusher, Models.ReverHammerCrusher>, IReverHammerCrusherRepository
    {

        public ReverHammerCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(ReverHammerCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Add(Mapper.Map<Models.ReverHammerCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(ReverHammerCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().AddAsync(Mapper.Map<Models.ReverHammerCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<ReverHammerCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().AddRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<ReverHammerCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Remove(Mapper.Map<Models.ReverHammerCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(ReverHammerCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Remove(Mapper.Map<Models.ReverHammerCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(ReverHammerCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Remove(Mapper.Map<Models.ReverHammerCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<ReverHammerCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<ReverHammerCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<ReverHammerCrusher, bool>> where = null)
        {
            IQueryable<Models.ReverHammerCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, bool>>, Expression<Func<Models.ReverHammerCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ReverHammerCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<ReverHammerCrusher, bool>> where = null)
        {
            IQueryable<Models.ReverHammerCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, bool>>, Expression<Func<Models.ReverHammerCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ReverHammerCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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

        public override int UpdateEntity(ReverHammerCrusher t)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Update(Mapper.Map<Models.ReverHammerCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<ReverHammerCrusher> ts)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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

        public override void InsertOrUpdate(ReverHammerCrusher t)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.ReverHammerCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ReverHammerCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(ReverHammerCrusher t)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Update(Mapper.Map<Models.ReverHammerCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<ReverHammerCrusher> ts)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ReverHammerCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(ReverHammerCrusher t)
        {
            Logger.Warn("[ReverHammerCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ReverHammerCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.ReverHammerCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ReverHammerCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<ReverHammerCrusher> GetEntities(Expression<Func<ReverHammerCrusher, bool>> where = null, Expression<Func<ReverHammerCrusher, object>> order = null)
        {
            Logger.Error("[ReverHammerCrusher]:forbidden use!");
            return new List<ReverHammerCrusher>().AsQueryable();
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
        public virtual IQueryable<ReverHammerCrusher> GetEntities(bool isExceed = false,Expression < Func<ReverHammerCrusher, bool>> where = null, Expression<Func<ReverHammerCrusher, object>> order = null)
        {


            Expression<Func<Models.ReverHammerCrusher, bool>> wheres;
            Expression<Func<Models.ReverHammerCrusher, object>> orderby;
            IQueryable<ReverHammerCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<ReverHammerCrusher>("ReverHammerCrusher", DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, bool>>, Expression<Func<Models.ReverHammerCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, object>>, Expression<Func<Models.ReverHammerCrusher, object>>>(order);
                
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<ReverHammerCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<ReverHammerCrusher>("ReverHammerCrusher", DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, object>>, Expression<Func<Models.ReverHammerCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().OrderBy(orderby).ProjectTo<ReverHammerCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<ReverHammerCrusher>("ReverHammerCrusher", DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ReverHammerCrusher, bool>>, Expression<Func<Models.ReverHammerCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().Where(wheres).ProjectTo<ReverHammerCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.HashGetAllValues<ReverHammerCrusher>("ReverHammerCrusher", DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ReverHammerCrusher>().ProjectTo<ReverHammerCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion
    }
}
