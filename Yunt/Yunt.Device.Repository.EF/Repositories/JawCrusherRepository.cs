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
            long dayUnix = t.Time.Time().Date.TimeSpan();
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Add(Mapper.Map<Models.JawCrusher>(t));
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
        public override async Task InsertAsync(JawCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddAsync(Mapper.Map<Models.JawCrusher>(t));
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
        public override int Insert(IEnumerable<JawCrusher> ts)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddRange(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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
        public override async Task InsertAsync(IEnumerable<JawCrusher> ts)
        {

            try
            {

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().AddRangeAsync(Mapper.Map<IEnumerable<Models.JawCrusher>>(ts));
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

            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Find(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().FindAsync(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(JawCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(JawCrusher t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Remove(Mapper.Map<Models.JawCrusher>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<JawCrusher> ts)
        {
            int results;

            try
            {
                RedisProvider.DB = 15; var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);


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
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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

                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

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
            Commit();
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

            var result = Commit();
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
        /// 仅可在某一天内查找数据，需要精确具体时间点的，在where中添加过滤，跨越一天的数据，请自行累加
        /// </summary>
        /// <param name="motorId"></param>
        ///  <param name="time"></param>
        /// <param name="isExceed">是否超出3 months数据</param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<JawCrusher> GetEntities(string motorId, DateTime time, bool isExceed = false, Expression<Func<JawCrusher, bool>> where = null, Expression<Func<JawCrusher, object>> order = null)
        {

            var span = time.Date.TimeSpan();
            Expression<Func<Models.JawCrusher, bool>> wheres;
            Expression<Func<Models.JawCrusher, object>> orderby;
            IQueryable<JawCrusher> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<JawCrusher>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<JawCrusher, bool>>, Expression<Func<Models.JawCrusher, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<JawCrusher, object>>, Expression<Func<Models.JawCrusher, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().Where(wheres).OrderBy(orderby).ProjectTo<JawCrusher>(Mapper);
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
                    return RedisProvider.ListRange<JawCrusher>(span + "_" + motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
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
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<JawCrusher>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
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
                RedisProvider.DB = 15;
                return RedisProvider.ListRange<JawCrusher>(span + "_" + motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.JawCrusher>().ProjectTo<JawCrusher>(Mapper);
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
        public JawCrusher GetLatestRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            RedisProvider.DB = 15;
            return RedisProvider.GetListItem<JawCrusher>(now + "_" + motorId, 0, DataType.Protobuf);
        }

        /// <summary>
        /// 根据电流获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public int GetTodayRunningTimeByCurrent(string motorId)
        {
            var time = DateTime.Now.Date;
            return GetEntities(motorId,time, false, c => c.Current_B > 0f).Count();
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
            status = lastData.Current_B > 0f ? MotorStatus.Run : MotorStatus.Stop;
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
