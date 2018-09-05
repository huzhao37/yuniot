using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Yunt.Device.Domain.BaseModel;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Repository.EF.Models;
using Yunt.Common;
using Yunt.Redis;
using Yunt.Share.Domain.Model;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class DeviceRepositoryBase<DT, ST> : IDeviceRepositoryBase<DT> where DT : AggregateRoot
        where ST : BaseModel
    {
        protected readonly IRedisCachingProvider RedisProvider;
        protected IMapper Mapper { get; set; }
        // private readonly DeviceContext _context;
        //  public IDeviceRepositoryBase<DT> Rep { get; }
        public DeviceRepositoryBase(IMapper mapper, IRedisCachingProvider redisProvider)//DeviceContext context, 
        {
            // _context = context;
            Mapper = mapper;
            RedisProvider = redisProvider;
        }

        #region Insert
        public virtual int Insert(DT t)
        {
            //_provider.DB = 0;
            //_provider.Set("t1", "t",DataType.Protobuf);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Add(Mapper.Map<ST>(t));
            return Commit();
        }
        public virtual async Task InsertAsync(DT t)
        {
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().AddAsync(Mapper.Map<ST>(t));
            await CommitAsync();
        }
        public virtual int Insert(IEnumerable<DT> ts)
        {
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().AddRange(Mapper.Map<IEnumerable<ST>>(ts));
                return Commit();

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public virtual async Task InsertAsync(IEnumerable<DT> ts)
        {
            if (!ts?.Any() ?? false) return;
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            try
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().AddRangeAsync(Mapper.Map<IEnumerable<ST>>(ts));
                await CommitAsync();
                //await _safecontext.SaveChangesAsync();
                //Commit();
                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");

            }
        }

        #endregion

        #region delete
        public virtual int DeleteEntity(int id)
        {
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Find(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Remove(t);
            return Commit();
        }
        public virtual async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().FindAsync(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Remove(Mapper.Map<ST>(t));
            await CommitAsync();
        }
        public virtual int DeleteEntity(DT t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Remove(Mapper.Map<ST>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public virtual async Task DeleteEntityAsync(DT t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Remove(Mapper.Map<ST>(t));
            await CommitAsync();
        }

        public virtual int DeleteEntity(IEnumerable<DT> ts)
        {
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().RemoveRange(Mapper.Map<IEnumerable<ST>>(ts));
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
        public virtual async Task DeleteEntityAsync(IEnumerable<DT> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().RemoveRange(Mapper.Map<IEnumerable<ST>>(ts));
                    await CommitAsync();
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public virtual int DeleteEntity(Expression<Func<DT, bool>> where = null)
        {
            IQueryable<ST> ts;
            Expression<Func<ST, bool>> wheres;
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<ST>()
                        .RemoveRange(Mapper.Map<IEnumerable<ST>>(ts));
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

        public virtual async Task DeleteEntityAsync(Expression<Func<DT, bool>> where = null)
        {
            IQueryable<ST> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>();
            }
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<ST>()
                        .RemoveRange(Mapper.Map<IEnumerable<ST>>(ts));
                    await CommitAsync();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }
        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        public virtual IQueryable<DT> GetEntities(Expression<Func<DT, bool>> where = null, Expression<Func<DT, object>> order = null)
        {

            Expression<Func<ST, bool>> wheres;
            Expression<Func<ST, object>> orderby;
            IQueryable<DT> sql = null;
            if (where != null && order != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Warn(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }

            if (order != null)
            {
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).ProjectTo<DT>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().ProjectTo<DT>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }

        public virtual IQueryable<IGrouping<object, DT>> GetEntities(object paramter)
        {
            var sql = new RawSqlString($"select {paramter} from dbo.{typeof(ST).Name} group by {paramter}");
            var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().FromSql(sql).ProjectTo<DT>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{query.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, query.ToUnevaluated()));
#endif
            return null;
        }
        /// <summary>
        /// 复杂查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramterss"></param>
        /// <returns></returns>
        public virtual IQueryable<DT> GetEntities(RawSqlString sql, params object[] paramterss)
        {
            try
            {
                var query = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().FromSql(sql, paramterss).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{query.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, query.ToUnevaluated()));
#endif
                return query;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                //throw;
                return null;
            }

        }
        public virtual DT GetEntityById(int id)
        {
            //return _context.Set<T>().SingleOrDefault(e => e.Id == id);
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Find<ST>(id).MapTo<DT>();
        }

        public virtual async Task<PaginatedList<DT>> GetPage(int pageIndex, int pageSize)
        {
            var source = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().ProjectTo<DT>(Mapper);
            var count = await source.CountAsync();
            List<DT> dailys = null;
            if (count > 0)
            {
                dailys = await source.OrderBy(x => x.Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedList<DT>(pageIndex, pageSize, count, dailys ?? new List<DT>());
        }

        /// <summary>
        /// （跳过缓存从数据库中获取数据）慎用！！！
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IQueryable<DT> GetFromSqlDb(Expression<Func<DT, bool>> where = null, Expression<Func<DT, object>> order = null)
        {

            Expression<Func<ST, bool>> wheres;
            Expression<Func<ST, object>> orderby;
            IQueryable<DT> sql = null;
            if (where != null && order != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Warn(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }

            if (order != null)
            {
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).ProjectTo<DT>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().ProjectTo<DT>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql;


        }
        #endregion

        #region update

        public virtual int UpdateEntity(DT t)
        {

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Update(Mapper.Map<ST>(t));
            return Commit();
        }

        public virtual int UpdateEntity(IEnumerable<DT> ts)
        {
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().UpdateRange(Mapper.Map<IEnumerable<ST>>(ts));
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

        public virtual void InsertOrUpdate(DT t)
        {
            try
            {
                var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Find(t.Id);
                if (existing == null)
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<ST>(t));
                }
                else
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<ST>(t));
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.Warn($"并发异常:{e.Message}");
#endif
            }


            var result = Commit();
        }

        public virtual async Task UpdateEntityAsync(DT t)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Update(Mapper.Map<ST>(t));
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.Warn($"并发异常:{e.Message}");
#endif
            }

            await CommitAsync();
        }

        public virtual async Task UpdateEntityAsync(IEnumerable<DT> ts)
        {
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().UpdateRange(Mapper.Map<IEnumerable<ST>>(ts));
                    await CommitAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    // transaction.Commit();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Logger.Warn($"并发异常:{ex.Message}");
#endif
                }
            }

        }

        public virtual async Task InsertOrUpdateAsync(DT t)
        {
            try
            {
                var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<DT>().FindAsync(t.Id);
                if (existing == null)
                {
                    await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<ST>(t));
                }
                else
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<ST>(t));
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.Warn($"并发异常:{e.Message}");
#endif
            }


            await CommitAsync();
        }

        #endregion

        #region private commit 
        protected static async Task CommitAsync()
        {
            //UnDO
            //try
            //{
            //    await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChangesAsync();

            //}
            //catch (DbUpdateConcurrencyException e)
            //{
            //    Logger.Exception(e, "可接受范围内的异常");
            //}
        }
        protected static int Commit()
        {
            try
            {
                return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Exception(e, "可接受范围内的异常");
                return 0;
            }
        }

        /// <summary>
        /// 批量提交
        /// </summary>
        public void Batch()
        {
            try
            {
                var contextObjs = ContextFactory.ContextDic.Values;
                foreach (var context in contextObjs)
                {
                    var con = context;
                    con.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e, "批量提交错误！");
            }

        }
        #endregion


        #region redis_cache
    

        /// <summary>
        /// 缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="motorId">设备</param>
        /// <param name="dayUnix">日期</param>
        /// <param name="ts">集合数据</param>
        /// <returns></returns>
        public virtual int Cache(string motorId, long dayUnix, List<DT> ts)
        {
            RedisProvider.DB = 15;
            var result = RedisProvider.Lpush(dayUnix + "_" + motorId, ts, DataType.Protobuf);
            RedisProvider.Expire(dayUnix + "_" + motorId, dayUnix.Expire());
            return result;
        }
        #endregion

        #region version 18.7.20
        [Obsolete("forbiden:only use for recovery")]
        public virtual async Task UpdateAsync(DT t)
        {
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Update(Mapper.Map<ST>(t));
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.Warn($"并发异常:{e.Message}");
#endif
            }

            await CommitAsync();
        }

        /// <summary>
        /// （跳过缓存从数据库中获取数据）慎用！！！
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual List<DT> GetFromDb(Expression<Func<DT, bool>> where = null, Expression<Func<DT, object>> order = null)
        {

            Expression<Func<ST, bool>> wheres;
            Expression<Func<ST, object>> orderby;
            IQueryable<DT> sql = null;
            if (where != null && order != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Warn(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql.ToList();
            }

            if (order != null)
            {
                orderby = Mapper.MapExpression<Expression<Func<DT, object>>, Expression<Func<ST, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().OrderBy(orderby).ProjectTo<DT>(Mapper);
#if DEBUG
                Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql.ToList();
            }
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<DT, bool>>, Expression<Func<ST, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().Where(wheres).ProjectTo<DT>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql.ToList();
            }
            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<ST>().ProjectTo<DT>(Mapper);
#if DEBUG
            Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
            Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
            return sql.ToList();


        }
        #endregion

        #region v 18.8.23
        /// <summary>
        /// 获取10min最新一条运行反馈记录
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public DiHistory GetLatestDiStatusRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            long lastTime = now - 10 * 60;
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(now + "|" + motorId, DataType.Protobuf)?.Where(e =>
                    e.Param.Contains("运行反馈") && e.Time>=lastTime&&e.DataPhysic.Equals("状态"))?.OrderByDescending(e => e.Time)?.FirstOrDefault();

        }

        /// <summary>
        /// 根据运行反馈获取设备实时状态
        /// </summary>
        /// <param name="motorId">电机Id</param>
        /// <returns></returns>
        public virtual MotorStatus GetCurrentStatus(string motorId)
        {
            var now = DateTime.Now.TimeSpan();
            var status = MotorStatus.Stop;
            var di = GetLatestDiStatusRecord(motorId);
            if (di == null) return status;
            status = di.Value > 0&&now-di.Time<=10*60 ? MotorStatus.Run : MotorStatus.Stop;
            return status;
        }

        /// <summary>
        /// 获取某日设备数字量记录
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IEnumerable<DiHistory> GetDis(string motorId, DateTime dt)
        {
            long dayUnix = dt.Date.TimeSpan();
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf);
        }
        /// <summary>
        /// 获取某日设备数字量运行反馈记录
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IEnumerable<DiHistory> GetDiStatus(string motorId, DateTime dt)
        {
            long dayUnix = dt.Date.TimeSpan();
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf)?.Where(e=>e.Param.Contains("运行反馈"));
        }
        /// <summary>
        /// 获取某日设备数字量运行反馈记录(不包含结束时间)
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DiHistory> GetDiStatus(string motorId, DateTime start,DateTime end)
        {
            long dayUnix = start.Date.TimeSpan(),startT=start.TimeSpan(),endT=end.TimeSpan();
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf)?.Where(e => e.Param.Contains("运行反馈")
                    &&e.Time>=startT&&e.Time < endT);
        }
        /// <summary>
        /// 获取某日设备开机情况下的时间数据集合(不包含结束时间)
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<long> GetDiStatusTimes(string motorId, DateTime start, DateTime end)
        {
            var times = new List<long>();
            long dayUnix = start.Date.TimeSpan(), startT = start.TimeSpan(), endT = end.TimeSpan();
            RedisProvider.DB = 3;
            var data= RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf)?.Where(e => e.Param.Contains("运行反馈")
                    && e.Value > 0 && e.Time >= startT && e.Time < endT)?.GroupBy(e => e.Time)?.ToList();
            if(data!=null&&data.Any())
                foreach (var d in data)
                {
                    times.Add(d.Key);
                }
            return times;
        }

        /// <summary>
        /// 获取某日设备的开机时间(不包含结束时间)
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long GetDiRunningTimes(string motorId, DateTime start, DateTime end)
        {
            long dayUnix = start.Date.TimeSpan(), startT = start.TimeSpan(), endT = end.TimeSpan();
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf)?.Where(e => e.Param.Contains("运行反馈")
                     &&e.Value>0&& e.Time >= startT && e.Time < endT)?.GroupBy(e=>e.Time)?.Count()??0;
        }
        #endregion


    }
}
