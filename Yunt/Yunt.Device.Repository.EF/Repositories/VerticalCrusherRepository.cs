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
    public class VerticalCrusherRepository : DeviceRepositoryBase<VerticalCrusher, Models.VerticalCrusher>, IVerticalCrusherRepository
    {

        public VerticalCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(VerticalCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Add(Mapper.Map<Models.VerticalCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(VerticalCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().AddAsync(Mapper.Map<Models.VerticalCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<VerticalCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().AddRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
                var result=Commit();

                 RedisProvider.DB = 16;
                 RedisProvider.LPUSH(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
           
                return result;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public override async Task InsertAsync(IEnumerable<VerticalCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
                await CommitAsync();

                RedisProvider.DB = 16;
                RedisProvider.LPUSH(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Remove(Mapper.Map<Models.VerticalCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(VerticalCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Remove(Mapper.Map<Models.VerticalCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(VerticalCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Remove(Mapper.Map<Models.VerticalCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<VerticalCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<VerticalCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<VerticalCrusher, bool>> where = null)
        {
            IQueryable<Models.VerticalCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<VerticalCrusher, bool>>, Expression<Func<Models.VerticalCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.VerticalCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<VerticalCrusher, bool>> where = null)
        {
            IQueryable<Models.VerticalCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<VerticalCrusher, bool>>, Expression<Func<Models.VerticalCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.VerticalCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
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

        public override int UpdateEntity(VerticalCrusher t)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Update(Mapper.Map<Models.VerticalCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<VerticalCrusher> ts)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
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

        public override void InsertOrUpdate(VerticalCrusher t)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.VerticalCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.VerticalCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(VerticalCrusher t)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Update(Mapper.Map<Models.VerticalCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<VerticalCrusher> ts)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.VerticalCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(VerticalCrusher t)
        {
            Logger.Warn("[VerticalCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<VerticalCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.VerticalCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.VerticalCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<VerticalCrusher> GetEntities(Expression<Func<VerticalCrusher, bool>> where = null, Expression<Func<VerticalCrusher, object>> order = null)
        {
            Logger.Error("[VerticalCrusher]:forbidden use!");
            return new List<VerticalCrusher>().AsQueryable();
        }


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="isExceed">是否超出2 hours数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<VerticalCrusher> GetEntities(string motorId, bool isExceed = false, Expression<Func<VerticalCrusher, bool>> where = null, Expression<Func<VerticalCrusher, object>> order = null)
        {


            Expression<Func<Models.VerticalCrusher, bool>> wheres;
            Expression<Func<Models.VerticalCrusher, object>> orderby;
            IQueryable<VerticalCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.ListRange<VerticalCrusher>(motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<VerticalCrusher, bool>>, Expression<Func<Models.VerticalCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<VerticalCrusher, object>>, Expression<Func<Models.VerticalCrusher, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<VerticalCrusher>(Mapper);
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
                    return RedisProvider.ListRange<VerticalCrusher>(motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<VerticalCrusher, object>>, Expression<Func<Models.VerticalCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().OrderBy(orderby).ProjectTo<VerticalCrusher>(Mapper);
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
                    return RedisProvider.ListRange<VerticalCrusher>(motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<VerticalCrusher, bool>>, Expression<Func<Models.VerticalCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().Where(wheres).ProjectTo<VerticalCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.ListRange<VerticalCrusher>(motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.VerticalCrusher>().ProjectTo<VerticalCrusher>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        #endregion

        #region extend method
        /// <summary>
        /// 获取10min内的最新数据，没有的话，认作设备失联，通讯状态中断
        /// </summary>
        /// <param name="motorId">设备电机编号</param>
        /// <returns></returns>
        public VerticalCrusher GetLatestRecord(string motorId)
        {
            RedisProvider.DB = 16;
            return RedisProvider.LPop<VerticalCrusher>(motorId, DataType.Protobuf);
        }
        /// <summary>
        /// 获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
        public bool GetCurrentStatus(string motorId)
        {
            var status = false;
            var lastData = GetLatestRecord(motorId);
            if (lastData != null && DateTimeOffset.UtcNow.CompareTo(lastData.Time) <= 10)
            {
                status = lastData.Current_B > 0;
            }
            return status;
        }
        #endregion
    }
}
