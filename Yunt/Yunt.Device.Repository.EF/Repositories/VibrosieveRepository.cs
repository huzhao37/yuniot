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
    public class VibrosieveRepository : DeviceRepositoryBase<Vibrosieve, Models.Vibrosieve>, IVibrosieveRepository
    {

        public VibrosieveRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(Vibrosieve t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Add(Mapper.Map<Models.Vibrosieve>(t));
            Commit();
            //redis缓存，暂定2h
            RedisProvider.DB = 16;
            return RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);
        }
        public override async Task InsertAsync(Vibrosieve t)
        {
            RedisProvider.DB = 16;
            RedisProvider.LPUSH(t.MotorId, t, DataType.Protobuf);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().AddAsync(Mapper.Map<Models.Vibrosieve>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<Vibrosieve> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().AddRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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
        public override async Task InsertAsync(IEnumerable<Vibrosieve> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().AddRangeAsync(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Find(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().FindAsync(id);

            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Remove(Mapper.Map<Models.Vibrosieve>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(Vibrosieve t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Remove(Mapper.Map<Models.Vibrosieve>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(Vibrosieve t)
        {
            RedisProvider.DB = 16;
            RedisProvider.Lrem(t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Remove(Mapper.Map<Models.Vibrosieve>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<Vibrosieve> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 16;
                RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().RemoveRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<Vibrosieve> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().RemoveRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<Vibrosieve, bool>> where = null)
        {
            IQueryable<Models.Vibrosieve> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Vibrosieve, bool>>, Expression<Func<Models.Vibrosieve, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Vibrosieve>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<Vibrosieve, bool>> where = null)
        {
            IQueryable<Models.Vibrosieve> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Vibrosieve, bool>>, Expression<Func<Models.Vibrosieve, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>();
            }
            {
                try
                {
                    RedisProvider.DB = 16;
                    RedisProvider.Lrem(ts.ElementAt(0).MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Vibrosieve>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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

        public override int UpdateEntity(Vibrosieve t)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Update(Mapper.Map<Models.Vibrosieve>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<Vibrosieve> ts)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().UpdateRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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

        public override void InsertOrUpdate(Vibrosieve t)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.Vibrosieve>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Vibrosieve>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(Vibrosieve t)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Update(Mapper.Map<Models.Vibrosieve>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<Vibrosieve> ts)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().UpdateRange(Mapper.Map<IEnumerable<Models.Vibrosieve>>(ts));
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

        public override async Task InsertOrUpdateAsync(Vibrosieve t)
        {
            Logger.Warn("[Vibrosieve]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Vibrosieve>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.Vibrosieve>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Vibrosieve>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<Vibrosieve> GetEntities(Expression<Func<Vibrosieve, bool>> where = null, Expression<Func<Vibrosieve, object>> order = null)
        {
            Logger.Error("[Vibrosieve]:forbidden use!");
            return new List<Vibrosieve>().AsQueryable();
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
        public virtual IQueryable<Vibrosieve> GetEntities(string motorId, bool isExceed = false, Expression<Func<Vibrosieve, bool>> where = null, Expression<Func<Vibrosieve, object>> order = null)
        {


            Expression<Func<Models.Vibrosieve, bool>> wheres;
            Expression<Func<Models.Vibrosieve, object>> orderby;
            IQueryable<Vibrosieve> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 16;
                    return RedisProvider.ListRange<Vibrosieve>(motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Vibrosieve, bool>>, Expression<Func<Models.Vibrosieve, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<Vibrosieve, object>>, Expression<Func<Models.Vibrosieve, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().OrderBy(orderby).Where(wheres).ProjectTo<Vibrosieve>(Mapper);
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
                    return RedisProvider.ListRange<Vibrosieve>(motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<Vibrosieve, object>>, Expression<Func<Models.Vibrosieve, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().OrderBy(orderby).ProjectTo<Vibrosieve>(Mapper);
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
                    return RedisProvider.ListRange<Vibrosieve>(motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Vibrosieve, bool>>, Expression<Func<Models.Vibrosieve, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().Where(wheres).ProjectTo<Vibrosieve>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 16;
                return RedisProvider.ListRange<Vibrosieve>(motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Vibrosieve>().ProjectTo<Vibrosieve>(Mapper);
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
        public Vibrosieve GetLatestRecord(string motorId)
        {
            RedisProvider.DB = 16;
            return RedisProvider.LPop<Vibrosieve>(motorId, DataType.Protobuf);
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
