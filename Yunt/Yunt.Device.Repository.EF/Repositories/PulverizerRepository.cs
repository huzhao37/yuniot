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
    public class PulverizerRepository : DeviceRepositoryBase<Pulverizer, Models.Pulverizer>, IPulverizerRepository
    {

        public PulverizerRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(Pulverizer t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Add(Mapper.Map<Models.Pulverizer>(t));
            var result = Commit();
            //redis缓存
            RedisProvider.DB = 15;
            RedisProvider.LPUSH(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);
            //if (RedisProvider.Exists(dayUnix + "_" + t.MotorId) > 0)
            //{
            RedisProvider.Expire(dayUnix + "_" + t.MotorId, dayUnix.Expire());
            //}

            return result;
        }
        public override async Task InsertAsync(Pulverizer t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().AddAsync(Mapper.Map<Models.Pulverizer>(t));
            await CommitAsync();

            RedisProvider.DB = 15;
            RedisProvider.LPUSH(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            //if (RedisProvider.Exists(dayUnix + "_" + t.MotorId)>0)
            //{
            RedisProvider.Expire(dayUnix + "_" + t.MotorId, dayUnix.Expire());
            //}

        }
        /// <summary>
        /// 新增motorId相同的数据
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public override int Insert(IEnumerable<Pulverizer> ts)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().AddRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
                var result = Commit();
                var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.DB = 15;
                RedisProvider.LPUSH(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);
                //if (RedisProvider.Exists(dayUnix + "_" + single.MotorId) > 0)
                //{
                RedisProvider.Expire(dayUnix + "_" + single.MotorId, dayUnix.Expire());
                //}

                return result;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        /// <summary>
        /// 新增motorId相同的数据
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public override async Task InsertAsync(IEnumerable<Pulverizer> ts)
        {

            try
            {

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().AddRangeAsync(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
                await CommitAsync(); var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.DB = 15;
                RedisProvider.LPUSH(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);
                //if (RedisProvider.Exists(dayUnix + "_" + single.MotorId) > 0)
                //{
                RedisProvider.Expire(dayUnix + "_" + single.MotorId, dayUnix.Expire());
                //}

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

            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Find(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().FindAsync(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Remove(Mapper.Map<Models.Pulverizer>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(Pulverizer t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Remove(Mapper.Map<Models.Pulverizer>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(Pulverizer t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Remove(Mapper.Map<Models.Pulverizer>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<Pulverizer> ts)
        {
            int results;

            try
            {
                RedisProvider.DB = 15; var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);


                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().RemoveRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<Pulverizer> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().RemoveRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<Pulverizer, bool>> where = null)
        {
            IQueryable<Models.Pulverizer> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Pulverizer, bool>>, Expression<Func<Models.Pulverizer, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Pulverizer>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<Pulverizer, bool>> where = null)
        {
            IQueryable<Models.Pulverizer> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Pulverizer, bool>>, Expression<Func<Models.Pulverizer, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>();
            }
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Pulverizer>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
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

        public override int UpdateEntity(Pulverizer t)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Update(Mapper.Map<Models.Pulverizer>(t));
            Commit();
        }

        public override int UpdateEntity(IEnumerable<Pulverizer> ts)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().UpdateRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
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

        public override void InsertOrUpdate(Pulverizer t)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.Pulverizer>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Pulverizer>(t));
            }

            var result = Commit();
        }

        public override async Task UpdateEntityAsync(Pulverizer t)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Update(Mapper.Map<Models.Pulverizer>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<Pulverizer> ts)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().UpdateRange(Mapper.Map<IEnumerable<Models.Pulverizer>>(ts));
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

        public override async Task InsertOrUpdateAsync(Pulverizer t)
        {
            Logger.Warn("[Pulverizer]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Pulverizer>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.Pulverizer>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Pulverizer>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<Pulverizer> GetEntities(Expression<Func<Pulverizer, bool>> where = null, Expression<Func<Pulverizer, object>> order = null)
        {
            Logger.Error("[Pulverizer]:forbidden use!");
            return new List<Pulverizer>().AsQueryable();
        }


        #endregion

        #region queryNew
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        /// <summary>
        /// 仅可在某一天内查找数据，需要精确具体时间点的，在where中添加过滤，跨越一天的数据，请自行累加
        /// </summary>
        /// <param name="motorId"></param>
        ///  <param name="time"></param>
        /// <param name="isExceed">是否超出3 months数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<Pulverizer> GetEntities(string motorId, DateTime time, bool isExceed = false, Expression<Func<Pulverizer, bool>> where = null, Expression<Func<Pulverizer, object>> order = null)
        {

            var span = time.Date.TimeSpan();
            Expression<Func<Models.Pulverizer, bool>> wheres;
            Expression<Func<Models.Pulverizer, object>> orderby;
            IQueryable<Pulverizer> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<Pulverizer>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Pulverizer, bool>>, Expression<Func<Models.Pulverizer, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<Pulverizer, object>>, Expression<Func<Models.Pulverizer, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Where(wheres).OrderBy(orderby).ProjectTo<Pulverizer>(Mapper);
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
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<Pulverizer>(span + "_" + motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<Pulverizer, object>>, Expression<Func<Models.Pulverizer, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().OrderBy(orderby).ProjectTo<Pulverizer>(Mapper);
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
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<Pulverizer>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Pulverizer, bool>>, Expression<Func<Models.Pulverizer, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().Where(wheres).ProjectTo<Pulverizer>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 15;
                return RedisProvider.ListRange<Pulverizer>(span + "_" + motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Pulverizer>().ProjectTo<Pulverizer>(Mapper);
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
        public Pulverizer GetLatestRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            RedisProvider.DB = 15;
            return RedisProvider.LPop<Pulverizer>(now + "_" + motorId, DataType.Protobuf);
        }
        /// <summary>
        /// 获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
        public MotorStatus GetCurrentStatus(string motorId)
        {
            var now = DateTime.Now.TimeSpan();
            var status = MotorStatus.Lose;
            var lastData = GetLatestRecord(motorId);
            if (lastData == null || now-lastData.Time > 10 * 60) return status;
            if (lastData.Current_B == -1)
                return status;
            status = lastData.Current_B > 0 ? MotorStatus.Run : MotorStatus.Stop;
            return status;
        }
        #endregion
    }
}
