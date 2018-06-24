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
    public class MaterialFeederRepository : DeviceRepositoryBase<MaterialFeeder, Models.MaterialFeeder>, IMaterialFeederRepository
    {

        public MaterialFeederRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }


        #region Insert
        public override int Insert(MaterialFeeder t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Add(Mapper.Map<Models.MaterialFeeder>(t));
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
        public override async Task InsertAsync(MaterialFeeder t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().AddAsync(Mapper.Map<Models.MaterialFeeder>(t));
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
        public override int Insert(IEnumerable<MaterialFeeder> ts)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().AddRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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
        public override async Task InsertAsync(IEnumerable<MaterialFeeder> ts)
        {

            try
            {

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().AddRangeAsync(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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

            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Find(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().FindAsync(id);
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Remove(Mapper.Map<Models.MaterialFeeder>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(MaterialFeeder t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            RedisProvider.Lrem(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Remove(Mapper.Map<Models.MaterialFeeder>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(MaterialFeeder t)
        {
            long dayUnix = t.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(dayUnix + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Remove(Mapper.Map<Models.MaterialFeeder>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<MaterialFeeder> ts)
        {
            int results;

            try
            {
                RedisProvider.DB = 15; var single = ts.ElementAt(0);
                long dayUnix = single.Time.Time().Date.TimeSpan();
                RedisProvider.Lrem(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);


                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().RemoveRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<MaterialFeeder> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().RemoveRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<MaterialFeeder, bool>> where = null)
        {
            IQueryable<Models.MaterialFeeder> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<MaterialFeeder, bool>>, Expression<Func<Models.MaterialFeeder, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>();
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
                        .Set<Models.MaterialFeeder>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<MaterialFeeder, bool>> where = null)
        {
            IQueryable<Models.MaterialFeeder> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<MaterialFeeder, bool>>, Expression<Func<Models.MaterialFeeder, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>();
            }
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    long dayUnix = single.Time.Time().Date.TimeSpan();
                    await RedisProvider.LremAsync(dayUnix + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.MaterialFeeder>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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

        public override int UpdateEntity(MaterialFeeder t)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Update(Mapper.Map<Models.MaterialFeeder>(t));
            Commit();
        }

        public override int UpdateEntity(IEnumerable<MaterialFeeder> ts)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().UpdateRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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

        public override void InsertOrUpdate(MaterialFeeder t)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.MaterialFeeder>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.MaterialFeeder>(t));
            }

            var result = Commit();
        }

        public override async Task UpdateEntityAsync(MaterialFeeder t)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Update(Mapper.Map<Models.MaterialFeeder>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<MaterialFeeder> ts)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().UpdateRange(Mapper.Map<IEnumerable<Models.MaterialFeeder>>(ts));
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

        public override async Task InsertOrUpdateAsync(MaterialFeeder t)
        {
            Logger.Warn("[MaterialFeeder]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<MaterialFeeder>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.MaterialFeeder>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.MaterialFeeder>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<MaterialFeeder> GetEntities(Expression<Func<MaterialFeeder, bool>> where = null, Expression<Func<MaterialFeeder, object>> order = null)
        {
            Logger.Error("[MaterialFeeder]:forbidden use!");
            return new List<MaterialFeeder>().AsQueryable();
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
        public virtual IQueryable<MaterialFeeder> GetEntities(string motorId, DateTime time, bool isExceed = false, Expression<Func<MaterialFeeder, bool>> where = null, Expression<Func<MaterialFeeder, object>> order = null)
        {

            var span = time.Date.TimeSpan();
            Expression<Func<Models.MaterialFeeder, bool>> wheres;
            Expression<Func<Models.MaterialFeeder, object>> orderby;
            IQueryable<MaterialFeeder> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<MaterialFeeder>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<MaterialFeeder, bool>>, Expression<Func<Models.MaterialFeeder, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<MaterialFeeder, object>>, Expression<Func<Models.MaterialFeeder, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Where(wheres).OrderBy(orderby).ProjectTo<MaterialFeeder>(Mapper);
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
                    return RedisProvider.ListRange<MaterialFeeder>(span + "_" + motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<MaterialFeeder, object>>, Expression<Func<Models.MaterialFeeder, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().OrderBy(orderby).ProjectTo<MaterialFeeder>(Mapper);
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
                    return RedisProvider.ListRange<MaterialFeeder>(span + "_" + motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<MaterialFeeder, bool>>, Expression<Func<Models.MaterialFeeder, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().Where(wheres).ProjectTo<MaterialFeeder>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 15;
                return RedisProvider.ListRange<MaterialFeeder>(span + "_" + motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.MaterialFeeder>().ProjectTo<MaterialFeeder>(Mapper);
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
        public MaterialFeeder GetLatestRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            RedisProvider.DB = 15;
            return RedisProvider.LPop<MaterialFeeder>(now + "_" + motorId, DataType.Protobuf);
        }

        /// <summary>
        /// 根据频率获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public int GetTodayRunningTimeByFrequence(string motorId)
        {
            var time = DateTime.Now.Date;
            return GetEntities(motorId,time, false, c => c.Frequency > 0f).Count();
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
            if (lastData.Frequency == -1)
                return status;
            status = lastData.Frequency > 0f ? MotorStatus.Run : MotorStatus.Stop;
            return status;
        }
        #endregion
    }
}
