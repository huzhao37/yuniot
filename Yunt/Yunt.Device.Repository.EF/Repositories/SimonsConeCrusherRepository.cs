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
            long dayUnix = t.Time.Time().Date.TimeSpan();
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Add(Mapper.Map<Models.SimonsConeCrusher>(t));
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
        public override async Task InsertAsync(SimonsConeCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddAsync(Mapper.Map<Models.SimonsConeCrusher>(t));
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
        public override int Insert(IEnumerable<SimonsConeCrusher> ts)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddRange(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<SimonsConeCrusher> ts)
        {

            try
            {

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.SimonsConeCrusher>>(ts));
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

            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Find(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().FindAsync(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(SimonsConeCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(SimonsConeCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Remove(Mapper.Map<Models.SimonsConeCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<SimonsConeCrusher> ts)
        {
            int results;

            try
            {
                RedisProvider.DB = 15; var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);


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
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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

                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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
            Commit();
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

            var result = Commit();
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
        /// 仅可在某一天内查找数据，需要精确具体时间点的，在where中添加过滤，跨越一天的数据，请自行累加
        /// </summary>
        /// <param name="motorId"></param>
        ///  <param name="time"></param>
        /// <param name="isExceed">是否超出3 months数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<SimonsConeCrusher> GetEntities(string motorId, DateTime time, bool isExceed = false, Expression<Func<SimonsConeCrusher, bool>> where = null, Expression<Func<SimonsConeCrusher, object>> order = null)
        {

            var span = time.Date.TimeSpan();
            Expression<Func<Models.SimonsConeCrusher, bool>> wheres;
            Expression<Func<Models.SimonsConeCrusher, object>> orderby;
            IQueryable<SimonsConeCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<SimonsConeCrusher>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, bool>>, Expression<Func<Models.SimonsConeCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<SimonsConeCrusher, object>>, Expression<Func<Models.SimonsConeCrusher, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().Where(wheres).OrderBy(orderby).ProjectTo<SimonsConeCrusher>(Mapper);
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
                    return RedisProvider.ListRange<SimonsConeCrusher>(span + "_" + motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
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
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<SimonsConeCrusher>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
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
                RedisProvider.DB = 15;
                return RedisProvider.ListRange<SimonsConeCrusher>(span + "_" + motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.SimonsConeCrusher>().ProjectTo<SimonsConeCrusher>(Mapper);
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
        public SimonsConeCrusher GetLatestRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            RedisProvider.DB = 15;
            return RedisProvider.GetListItem<SimonsConeCrusher>(now + "_" + motorId, 0, DataType.Protobuf);
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
            if (lastData == null || now-lastData.Time> 10 * 60) return status;
            if (lastData.Current == -1)
                return status;
            status = lastData.Current > 0 ? MotorStatus.Run : MotorStatus.Stop;
            return status;
        }
        #endregion


        #region cache
        /// <summary>
        /// 缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motor">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <returns></returns>
        public int PreCache(Motor motor, DateTime dt)
        {
            var result = 0;
            var end = dt.Date.AddDays(1).TimeSpan();
            var start = dt.Date.TimeSpan();
            long dayUnix = start;
            var list = GetFromSqlDb(e => e.MotorId.Equals(motor.MotorId) && e.Time >= start && e.Time < end)?.OrderBy(e => e.Time)?.ToList();
            if (list != null && list.Any())
                result = Cache(motor.MotorId, dayUnix, list);
            return result;
        }
        #endregion
    }
}
