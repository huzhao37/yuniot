using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Yunt.TaskManager.Core;
using Yunt.TaskManager.Model;

namespace Yunt.TaskManager.Repository.EF.Core
{
    public class TaskRepositoryBase<T> : ITaskRepositoryBase<T> where T:BaseModel 
    {
        private readonly TaskManagerContext _context;

        public TaskRepositoryBase(TaskManagerContext context)
        {
            _context = context;
        }

        #region add
        public int CreateEntity(T t)
        {
            _context.Set<T>().Add(t);
            return _context.SaveChanges();
        }
        public int CreateEntities(IEnumerable<T> ts)
        {
            var results = 0;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().AddRange(ts);
                    results = _context.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
            }
            return results;
        }

        #endregion

        #region delete
        public int DeleteEntity(int id)
        {
            var t = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(t);
            return _context.SaveChanges();
        }
        public int DeleteEntity(T t)
        {
            _context.Set<T>().Remove(t);
            return _context.SaveChanges();
        }

        public int DeleteEntities(IEnumerable<T> ts)
        {
            var results = 0;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().RemoveRange(ts);
                    results = _context.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
            }
            return results;
        }


        #endregion

        #region query
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> where = null)
        {
            return where == null ? _context.Set<T>() : _context.Set<T>().Where(where);
        }


        public T GetEntityById(int id)
        {
            //return _context.Set<T>().SingleOrDefault(e => e.Id == id);
            return _context.Find<T>(id);
        }


        #endregion

        #region update

        public int UpdateEntity(T t)
        {
            _context.Set<T>().Update(t);
            return _context.SaveChanges();
        }

        public int UpdateEntities(IEnumerable<T> ts)
        {
            var results = 0;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().UpdateRange(ts);
                    results = _context.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                    results = 0;
                }
            }
            return results;
          
        }

        #endregion

    }
}
