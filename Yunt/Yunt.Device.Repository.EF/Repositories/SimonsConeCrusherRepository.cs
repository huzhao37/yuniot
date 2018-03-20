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
    public class SimonsConeCrusherRepository : DeviceRepositoryBase<SimonsConeCrusher, Models.SimonsConeCrusher>, ISimonsConeCrusherRepository
    {

        public SimonsConeCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(SimonsConeCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Add(Mapper.Map<Models.SimonsConeCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(SimonsConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddAsync(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<SimonsConeCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<SimonsConeCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(SimonsConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(SimonsConeCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<SimonsConeCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<SimonsConeCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<SimonsConeCrusher, bool>> where = null)
        {
            IQueryable<Models.SimonsConeCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, bool>>, Expression<Func<Models.SimonsConeCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.SimonsConeCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<SimonsConeCrusher, bool>> where = null)
        {
            IQueryable<Models.SimonsConeCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, bool>>, Expression<Func<Models.SimonsConeCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.SimonsConeCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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

        public override int UpdateEntity(SimonsConeCrusher t)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Update(Mapper.Map<Models.SimonsConeCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<SimonsConeCrusher> ts)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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

        public override void InsertOrUpdate(SimonsConeCrusher t)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.SimonsConeCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.SimonsConeCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(SimonsConeCrusher t)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Update(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<SimonsConeCrusher> ts)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(SimonsConeCrusher t)
        {
            Logger.Warn("[SimonsConeCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<SimonsConeCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.SimonsConeCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.SimonsConeCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<SimonsConeCrusher> GetEntities(Expression<Func<SimonsConeCrusher, bool>> where = null, Expression<Func<SimonsConeCrusher, object>> order = null)
        {
            Logger.Error("[SimonsConeCrusher]:forbidden use!");
            return new List<SimonsConeCrusher>().AsQueryable();
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
        public virtual IQueryable<SimonsConeCrusher> GetEntities(bool isExceed = false,Expression < Func<SimonsConeCrusher, bool>> where = null, Expression<Func<SimonsConeCrusher, object>> order = null)
        {


            Expression<Func<Models.SimonsConeCrusher, bool>> wheres;
            Expression<Func<Models.SimonsConeCrusher, object>> orderby;
            IQueryable<SimonsConeCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<SimonsConeCrusher>("SimonsConeCrusher", DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, bool>>, Expression<Func<Models.SimonsConeCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, object>>, Expression<Func<Models.SimonsConeCrusher, object>>>(order);
                
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<SimonsConeCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<SimonsConeCrusher>("SimonsConeCrusher", DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, object>>, Expression<Func<Models.SimonsConeCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().OrderBy(orderby).ProjectTo<SimonsConeCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<SimonsConeCrusher>("SimonsConeCrusher", DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, bool>>, Expression<Func<Models.SimonsConeCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Where(wheres).ProjectTo<SimonsConeCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.HashGetAllValues<SimonsConeCrusher>("SimonsConeCrusher", DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().ProjectTo<SimonsConeCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion
    }
}
