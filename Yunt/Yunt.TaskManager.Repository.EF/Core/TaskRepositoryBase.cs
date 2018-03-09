using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yunt.TaskManager.Core;
using Yunt.TaskManager.Model;

namespace Yunt.TaskManager.Repository.EF.Core
{
    public class TaskRepositoryBase<T> : ITaskRepositoryBase<T> where T : BaseModel
    {
        private readonly TaskManagerContext _context;
        public TaskRepositoryBase(TaskManagerContext context)
        {
            _context = context;
        }

        #region Insert
        public int Insert(T t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Add(t);
            return Commit();
        }
        public async Task InsertAsync(T t)
        {
            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().AddAsync(t);
            await CommitAsync();
        }
        public int Insert(IEnumerable<T> ts)
        {
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            try
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().AddRange(ts);
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
        public async Task InsertAsync(IEnumerable<T> ts)
        {
            //using (var transaction = _context.Database.BeginTransaction())
            //{
            try
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().AddRangeAsync(ts);
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
        public int DeleteEntity(int id)
        {
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Find(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Remove(t);
            return Commit();
        }
        public async Task DeleteEntityAsync(int id)
        {
            var t =await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().FindAsync(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Remove(t);
            await CommitAsync();
        }
        public int DeleteEntity(T t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Remove(t);
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public async Task DeleteEntityAsync(T t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Remove(t);
            await CommitAsync();
        }

        public int DeleteEntity(IEnumerable<T> ts)
        {
            int results;
           // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().RemoveRange(ts);
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
        public async Task DeleteEntityAsync(IEnumerable<T> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().RemoveRange(ts);
                    await  CommitAsync();
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

        #endregion

        #region query
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null)
        {
            return where == null ? ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>() : 
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Where(where);
        }


        public T GetEntityById(int id)
        {
            //return _context.Set<T>().SingleOrDefault(e => e.Id == id);
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Find<T>(id);
        }
        public async Task<PaginatedList<TbCategory>> GetPage(DateTime start, DateTime end, int pageIndex,
          int pageSize)
        {
            var source = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<TbCategory >().Where(x => x.Categorycreatetime >= start && x.Categorycreatetime < new DateTime(end.Year, end.Month, end.Day).AddDays(1));
            var count = await source.CountAsync();
            List<TbCategory> dailys = null;
            if (count > 0)
            {
                dailys = await source.OrderBy(x => x.Categorycreatetime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return new PaginatedList<TbCategory>(pageIndex, pageSize, count, dailys ?? new List<TbCategory>());
        }

        #endregion

        #region update

        public int UpdateEntity(T t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Update(t);
            return Commit();
        }

        public int UpdateEntity(IEnumerable<T> ts)
        {
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().UpdateRange(ts);
                    results =Commit();

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

        public void InsertOrUpdate(T t)
        {
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(t);
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(t);
            }

            Commit();
        }

        public async Task UpdateEntityAsync(T t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().Update(t);
            await  CommitAsync();
        }

        public async Task UpdateEntityAsync(IEnumerable<T> ts)
        {
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().UpdateRange(ts);
                    await  CommitAsync();

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

        public async Task InsertOrUpdateAsync(T t)
        {
            var existing =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<T>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(t);
            }
            else
            {
                 ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(t);
            }

            await CommitAsync();
        }

        #endregion

        #region private commit 
        private static async Task CommitAsync()
        {
            try
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Exception(e,"可接受范围内的异常");
            }
        }
        private static int Commit()
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


        #endregion

    }
}
