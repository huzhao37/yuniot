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
    public class JawCrusherRepository : DeviceRepositoryBase<JawCrusher, Models.JawCrusher>, IJawCrusherRepository
    {

        public JawCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(JawCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Add(Mapper.Map<Models.JawCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(JawCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Sadd(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddAsync(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<JawCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<JawCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(JawCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(JawCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Srem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<JawCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<JawCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<JawCrusher, bool>> where = null)
        {
            IQueryable<Models.JawCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<JawCrusher, bool>>, Expression<Func<Models.JawCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.JawCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<JawCrusher, bool>> where = null)
        {
            IQueryable<Models.JawCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<JawCrusher, bool>>, Expression<Func<Models.JawCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Srem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.JawCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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

        public override int UpdateEntity(JawCrusher t)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Update(Mapper.Map<Models.JawCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<JawCrusher> ts)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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

        public override void InsertOrUpdate(JawCrusher t)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.JawCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.JawCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(JawCrusher t)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Update(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<JawCrusher> ts)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(JawCrusher t)
        {
            Logger.Warn("[JawCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<JawCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.JawCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.JawCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<JawCrusher> GetEntities(Expression<Func<JawCrusher, bool>> where = null, Expression<Func<JawCrusher, object>> order = null)
        {
            Logger.Error("[JawCrusher]:forbidden use!");
            return new List<JawCrusher>().AsQueryable();
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
        public virtual IQueryable<JawCrusher> GetEntities(bool isExceed = false,Expression < Func<JawCrusher, bool>> where = null, Expression<Func<JawCrusher, object>> order = null)
        {


            Expression<Func<Models.JawCrusher, bool>> wheres;
            Expression<Func<Models.JawCrusher, object>> orderby;
            IQueryable<JawCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.HashGetAllValues<JawCrusher>("JawCrusher", DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<JawCrusher, bool>>, Expression<Func<Models.JawCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<JawCrusher, object>>, Expression<Func<Models.JawCrusher, object>>>(order);
                
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<JawCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<JawCrusher>("JawCrusher", DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<JawCrusher, object>>, Expression<Func<Models.JawCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().OrderBy(orderby).ProjectTo<JawCrusher>(Mapper);
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
                    return RedisProvider.HashGetAllValues<JawCrusher>("JawCrusher", DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<JawCrusher, bool>>, Expression<Func<Models.JawCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Where(wheres).ProjectTo<JawCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.HashGetAllValues<JawCrusher>("JawCrusher", DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().ProjectTo<JawCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion
    }
}
