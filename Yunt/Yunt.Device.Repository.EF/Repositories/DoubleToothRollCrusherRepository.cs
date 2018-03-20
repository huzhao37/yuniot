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
    public class DoubleToothRollCrusherRepository : DeviceRepositoryBase<DoubleToothRollCrusher, Models.DoubleToothRollCrusher>, IDoubleToothRollCrusherRepository
    {

        public DoubleToothRollCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(DoubleToothRollCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Add(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(DoubleToothRollCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().AddAsync(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<DoubleToothRollCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().AddRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<DoubleToothRollCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Remove(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(DoubleToothRollCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Remove(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(DoubleToothRollCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Remove(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<DoubleToothRollCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<DoubleToothRollCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<DoubleToothRollCrusher, bool>> where = null)
        {
            IQueryable<Models.DoubleToothRollCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, bool>>, Expression<Func<Models.DoubleToothRollCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.DoubleToothRollCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<DoubleToothRollCrusher, bool>> where = null)
        {
            IQueryable<Models.DoubleToothRollCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, bool>>, Expression<Func<Models.DoubleToothRollCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.DoubleToothRollCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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

        public override int UpdateEntity(DoubleToothRollCrusher t)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Update(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<DoubleToothRollCrusher> ts)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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

        public override void InsertOrUpdate(DoubleToothRollCrusher t)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(DoubleToothRollCrusher t)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Update(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<DoubleToothRollCrusher> ts)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.DoubleToothRollCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(DoubleToothRollCrusher t)
        {
            Logger.Warn("[DoubleToothRollCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<DoubleToothRollCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.DoubleToothRollCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<DoubleToothRollCrusher> GetEntities(Expression<Func<DoubleToothRollCrusher, bool>> where = null, Expression<Func<DoubleToothRollCrusher, object>> order = null)
        {
            Logger.Error("[DoubleToothRollCrusher]:forbidden use!");
            return new List<DoubleToothRollCrusher>().AsQueryable();
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
        public virtual IQueryable<DoubleToothRollCrusher> GetEntities(bool isExceed = false,Expression < Func<DoubleToothRollCrusher, bool>> where = null, Expression<Func<DoubleToothRollCrusher, object>> order = null)
        {


            Expression<Func<Models.DoubleToothRollCrusher, bool>> wheres;
            Expression<Func<Models.DoubleToothRollCrusher, object>> orderby;
            IQueryable<DoubleToothRollCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<DoubleToothRollCrusher>("DoubleToothRollCrusher", DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, bool>>, Expression<Func<Models.DoubleToothRollCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, object>>, Expression<Func<Models.DoubleToothRollCrusher, object>>>(order);
                
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<DoubleToothRollCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<DoubleToothRollCrusher>("DoubleToothRollCrusher", DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, object>>, Expression<Func<Models.DoubleToothRollCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().OrderBy(orderby).ProjectTo<DoubleToothRollCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<DoubleToothRollCrusher>("DoubleToothRollCrusher", DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<DoubleToothRollCrusher, bool>>, Expression<Func<Models.DoubleToothRollCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().Where(wheres).ProjectTo<DoubleToothRollCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.HashGetAllValues<DoubleToothRollCrusher>("DoubleToothRollCrusher", DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.DoubleToothRollCrusher>().ProjectTo<DoubleToothRollCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion
    }
}
