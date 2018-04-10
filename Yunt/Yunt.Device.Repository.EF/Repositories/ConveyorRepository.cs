using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ConveyorRepository : DeviceRepositoryBase<Conveyor, Models.Conveyor>, IConveyorRepository
    {
        public ConveyorRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }

        #region Insert
        public override int Insert(Conveyor t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Add(Mapper.Map<Models.Conveyor>(t));
            var result = Commit();
            //redis缓存
            RedisProvider.DB = 15;         
            RedisProvider.LPUSH(t.Time+"_"+t.MotorId, t, DataType.Protobuf);
            if (RedisProvider.Exists(t.Time + "_" + t.MotorId)<=0)
            {
                RedisProvider.Expire(t.Time + "_" + t.MotorId, t.Time.Expire());
            }
           
            return result;
        }
        public override async Task InsertAsync(Conveyor t)
        { 
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().AddAsync(Mapper.Map<Models.Conveyor>(t));
            await CommitAsync();

            RedisProvider.DB = 15;
            //RedisProvider.HashSetFieldValue(t.Time.ToString(), t.MotorId, t, DataType.Protobuf);
            await RedisProvider.LpushAsync(t.Time + "_" + t.MotorId, t, DataType.Protobuf);
            if (RedisProvider.Exists(t.Time + "_" + t.MotorId) <= 0)
            {
                RedisProvider.Expire(t.Time + "_" + t.MotorId, t.Time.Expire());
            }
        }
        /// <summary>
        /// 新增motorId相同的数据
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public override int Insert(IEnumerable<Conveyor> ts)
        {
            try
            {
                       

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().AddRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
                 var result=Commit();
                var single = ts.ElementAt(0);
                 RedisProvider.DB = 15;
                 RedisProvider.LPUSH(single.Time + "_"+ single.MotorId, ts, DataType.Protobuf);
                if (RedisProvider.Exists(single.Time + "_" + single.MotorId) <= 0)
                {
                    RedisProvider.Expire(single.Time + "_" + single.MotorId, single.Time.Expire());
                }

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
        public override async Task InsertAsync(IEnumerable<Conveyor> ts)
        {

            try
            {             

                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().AddRangeAsync(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
                await CommitAsync();var single = ts.ElementAt(0);

                RedisProvider.DB = 15;
                await RedisProvider.LpushAsync(single.Time + "_"+ single.MotorId, ts, DataType.Protobuf);
                if (RedisProvider.Exists(single.Time + "_" + single.MotorId) <= 0)
                {
                    RedisProvider.Expire(single.Time + "_" + single.MotorId, single.Time.Expire());
                }

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
          
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Find(id);

            RedisProvider.DB = 15;
            RedisProvider.Lrem(t.Time + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().FindAsync(id);

            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(t.Time + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Remove(Mapper.Map<Models.Conveyor>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(Conveyor t)
        {
            RedisProvider.DB = 15;
            RedisProvider.Lrem(t.Time + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Remove(Mapper.Map<Models.Conveyor>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(Conveyor t)
        {
            RedisProvider.DB = 15;
            await RedisProvider.LremAsync(t.Time + "_" + t.MotorId, t, DataType.Protobuf);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Remove(Mapper.Map<Models.Conveyor>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<Conveyor> ts)
        {
            int results;

            try
            {                         
                RedisProvider.DB = 15; var single = ts.ElementAt(0);
                RedisProvider.Lrem(single.Time + "_" + single.MotorId, ts, DataType.Protobuf);
        

                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().RemoveRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<Conveyor> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    await RedisProvider.LremAsync(single.Time + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().RemoveRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
                    await CommitAsync();

                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<Conveyor, bool>> where = null)
        {
            IQueryable<Models.Conveyor> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Conveyor, bool>>, Expression<Func<Models.Conveyor, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {

                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    RedisProvider.Lrem(single.Time + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Conveyor>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<Conveyor, bool>> where = null)
        {
            IQueryable<Models.Conveyor> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Conveyor, bool>>, Expression<Func<Models.Conveyor, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>();
            }
            {
                try
                {
                    RedisProvider.DB = 15; var single = ts.ElementAt(0);
                    await RedisProvider.LremAsync(single.Time + "_" + single.MotorId, ts, DataType.Protobuf);

                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Conveyor>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
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

        public override int UpdateEntity(Conveyor t)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return 0;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Update(Mapper.Map<Models.Conveyor>(t));
            Commit();
        }

        public override int UpdateEntity(IEnumerable<Conveyor> ts)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return 0;
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().UpdateRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
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

        public override void InsertOrUpdate(Conveyor t)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return;
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.Conveyor>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Conveyor>(t));
            }

           var result = Commit();
        }

        public override async Task UpdateEntityAsync(Conveyor t)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return;
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Update(Mapper.Map<Models.Conveyor>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<Conveyor> ts)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return;
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().UpdateRange(Mapper.Map<IEnumerable<Models.Conveyor>>(ts));
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

        public override async Task InsertOrUpdateAsync(Conveyor t)
        {
            Logger.Warn("[Conveyor]:forbiden update!");
            return;
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Conveyor>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.Conveyor>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Conveyor>(t));
            }

            await CommitAsync();
        }

        #endregion

        #region query
        //注意闭包效率，参数应设置成作用域变量，可重复利用sql查询计划
        [Obsolete]
        public new IQueryable<Conveyor> GetEntities(Expression<Func<Conveyor, bool>> where = null, Expression<Func<Conveyor, object>> order = null)
        {
            Logger.Error("[Conveyor]:forbidden use!");
            return new List<Conveyor>().AsQueryable();
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
        public virtual IQueryable<Conveyor> GetEntities(string motorId,DateTime time, bool isExceed = false, Expression<Func<Conveyor, bool>> where = null, Expression<Func<Conveyor, object>> order = null)
        {

            var span = time.Date.TimeSpan();
            Expression<Func<Models.Conveyor, bool>> wheres;
            Expression<Func<Models.Conveyor, object>> orderby;
            IQueryable<Conveyor> sql = null;
            if (where != null && order != null)
            {
                if (!isExceed)
                {
                    RedisProvider.DB = 15;
                    return RedisProvider.ListRange<Conveyor>(span+"_"+motorId, DataType.Protobuf).Where(where.Compile())
                        .OrderBy(order.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Conveyor, bool>>, Expression<Func<Models.Conveyor, bool>>>(where);
                orderby = Mapper.MapExpression<Expression<Func<Conveyor, object>>, Expression<Func<Models.Conveyor, object>>>(order);

                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Where(wheres).OrderBy(orderby).ProjectTo<Conveyor>(Mapper);
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
                    return RedisProvider.ListRange<Conveyor>(span+"_"+motorId, DataType.Protobuf).OrderBy(order.Compile()).AsQueryable();
                }
                orderby = Mapper.MapExpression<Expression<Func<Conveyor, object>>, Expression<Func<Models.Conveyor, object>>>(order);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().OrderBy(orderby).ProjectTo<Conveyor>(Mapper);
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
                    return RedisProvider.ListRange<Conveyor>(span+"_"+motorId, DataType.Protobuf).Where(where.Compile()).AsQueryable();
                }

                wheres = Mapper.MapExpression<Expression<Func<Conveyor, bool>>, Expression<Func<Models.Conveyor, bool>>>(where);
                sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().Where(wheres).ProjectTo<Conveyor>(Mapper);
#if DEBUG
                //Logger.Info($"translate sql:{sql.ToSql()} \n untranslate sql:");
                // Logger.Info(string.Join(Environment.NewLine, sql.ToUnevaluated()));
#endif
                return sql;
            }
            if (!isExceed)
            {
                RedisProvider.DB = 15;
                return RedisProvider.ListRange<Conveyor>(span+"_"+motorId, DataType.Protobuf).AsQueryable();
            }

            sql = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Conveyor>().ProjectTo<Conveyor>(Mapper);
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
        public Conveyor GetLatestRecord(string motorId)
        {
            var now = DateTime.Now.Date.TimeSpan();
            //var time = now;// 1521876278;

            //RedisProvider.DB = 15;
            //var list=new List<Conveyor>();
            //var sw=new Stopwatch();
            //sw.Start();
            //for (var i = 0; i < 30; i++)
            //{
            //    time= now.AddDays(1);
            //        //if (RedisProvider.Exists(time.ToString()) <= 0) continue;
            //        //if (RedisProvider.HashFieldExists(time.ToString(), motorId, DataType.Protobuf) <= 0) continue;
            //        var item = RedisProvider.ListRange<Conveyor>(time.TimeSpan() + "_" + motorId, DataType.Protobuf);
            //        if (item != null)
            //            list.AddRange(item);
            //}
            ////var x = RedisProvider.ListRange<Conveyor>(span+"_"+motorId, DataType.Protobuf);//.Where(e=>e.Time== 1521882489);
            //sw.Stop();
            //Logger.Warn($"cost {sw.ElapsedMilliseconds}ms");
            //return list.FirstOrDefault();
            RedisProvider.DB = 15;
            return RedisProvider.LPop<Conveyor>(now+"_"+motorId, DataType.Protobuf);
        }


        /// <summary>
        /// 根据电流获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public int GetTodayRunningTimeByCurrent(string motorId)
        {
            var time = DateTime.Now.Date;
            return GetEntities(motorId, time, false, c => c.Current_B > 0).Count();
        }
        /// <summary>
        /// 根据瞬时称重获取当日开机时间
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public int GetTodayRunningTimeByInstant(string motorId)
        {
            var time = DateTime.Now.Date;
            return GetEntities(motorId, time, false, c => c.InstantWeight > 0).Count();
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
            if (lastData == null || now.CompareTo(lastData.Time) > 10 * 60) return status;
            if (lastData.Current_B == -1)
                return status;
            status = lastData.Current_B>0?MotorStatus.Run : MotorStatus.Stop;
            return status;
        }
       
        #endregion
    }
}
