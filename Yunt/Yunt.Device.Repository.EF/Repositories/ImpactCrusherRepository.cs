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
    public class ImpactCrusherRepository : DeviceRepositoryBase<ImpactCrusher, Models.ImpactCrusher>, IImpactCrusherRepository
    {

        public ImpactCrusherRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(ImpactCrusher t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Add(Mapper.Map<Models.ImpactCrusher>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(ImpactCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().AddAsync(Mapper.Map<Models.ImpactCrusher>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<ImpactCrusher> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().AddRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<ImpactCrusher> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Remove(Mapper.Map<Models.ImpactCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(ImpactCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Remove(Mapper.Map<Models.ImpactCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(ImpactCrusher t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Remove(Mapper.Map<Models.ImpactCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<ImpactCrusher> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<ImpactCrusher> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().RemoveRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<ImpactCrusher, bool>> where = null)
        {
            IQueryable<Models.ImpactCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ImpactCrusher, bool>>, Expression<Func<Models.ImpactCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ImpactCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<ImpactCrusher, bool>> where = null)
        {
            IQueryable<Models.ImpactCrusher> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<ImpactCrusher, bool>>, Expression<Func<Models.ImpactCrusher, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.ImpactCrusher>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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

        public override int UpdateEntity(ImpactCrusher t)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Update(Mapper.Map<Models.ImpactCrusher>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<ImpactCrusher> ts)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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

        public override void InsertOrUpdate(ImpactCrusher t)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.ImpactCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ImpactCrusher>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(ImpactCrusher t)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Update(Mapper.Map<Models.ImpactCrusher>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<ImpactCrusher> ts)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().UpdateRange(Mapper.Map<IEnumerable<Models.ImpactCrusher>>(ts));
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

        public override async Task InsertOrUpdateAsync(ImpactCrusher t)
        {
            Logger.Warn("[ImpactCrusher]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ImpactCrusher>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.ImpactCrusher>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.ImpactCrusher>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<ImpactCrusher> GetEntities(Expression<Func<ImpactCrusher, bool>> where = null, Expression<Func<ImpactCrusher, object>> order = null)
        {
            Logger.Error("[ImpactCrusher]:forbidden use!");
            return new List<ImpactCrusher>().AsQueryable();
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
        public virtual IQueryable<ImpactCrusher> GetEntities(string motorId, bool isExceed = false, Expression<Func<ImpactCrusher, bool>> where = null, Expression<Func<ImpactCrusher, object>> order = null)
        {


            Expression<Func<Models.ImpactCrusher, bool>> wheres;
            Expression<Func<Models.ImpactCrusher, object>> orderby;
            IQueryable<ImpactCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.ListRange<ImpactCrusher>(motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ImpactCrusher, bool>>, Expression<Func<Models.ImpactCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<ImpactCrusher, object>>, Expression<Func<Models.ImpactCrusher, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().OrderBy(orderby).Where(wheres).ProjectTo<ImpactCrusher>(Mapper);
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
                    return RedisProvider.ListRange<ImpactCrusher>(motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<ImpactCrusher, object>>, Expression<Func<Models.ImpactCrusher, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().OrderBy(orderby).ProjectTo<ImpactCrusher>(Mapper);
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
                    return RedisProvider.ListRange<ImpactCrusher>(motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<ImpactCrusher, bool>>, Expression<Func<Models.ImpactCrusher, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().Where(wheres).ProjectTo<ImpactCrusher>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.ListRange<ImpactCrusher>(motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.ImpactCrusher>().ProjectTo<ImpactCrusher>(Mapper);
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
        public ImpactCrusher GetLatestRecord(string motorId)
        {
            RedisProvider.DB = 16;
            return RedisProvider.LPop<ImpactCrusher>(motorId, DataType.Protobuf);
        }

        /// <summary>
        /// 根据电流获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public int GetTodayRunningTimeByCurrent(string motorId)
        {
            var time = DateTimeOffset.UtcNow.Date;
            return GetEntities(motorId, false, c => c.Motor1Current_B > 0 && c.Time.CompareTo(time) >= 0).Count();
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
                status = lastData.Motor1Current_B > 0;
            }
            return status;
        }
        #endregion
    }
}
