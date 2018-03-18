using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class MotorRepository : DeviceRepositoryBase<Motor, Models.Motor>, IMotorRepository
    {
        private readonly IMotorIdFactoriesRepository _idRep;

        public MotorRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
            _idRep =
                ServiceProviderServiceExtensions.GetService<IMotorIdFactoriesRepository>(BootStrap.ServiceProvider);
        }

        #region Insert
        public override int Insert(Motor t)
        {
            var idfac=_idRep.GetEntities(e => e.ProductionLineId.Equals(t.ProductionLineId) && e.MotorTypeId.Equals(t.MotorTypeId)).FirstOrDefault();
            var newIndex = idfac?.MotorIndex + 1 ?? 1;
            if (idfac != null)
            {
                idfac.MotorIndex = newIndex;              
            }
            else
            {
                idfac=new MotorIdFactories(){MotorIndex = newIndex,MotorTypeId = t.MotorTypeId,ProductionLineId = t.ProductionLineId,Time = DateTimeOffset.Now};

            }
            _idRep.InsertOrUpdate(idfac);
            t.MotorId = newIndex.NewMotor(t.ProductionLineId, t.MotorTypeId, newIndex);

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Add(Mapper.Map<Models.Motor>(t));
            return Commit();
        }
        public override async Task InsertAsync(Motor t)
        {
            var idfac = _idRep.GetEntities(e => e.ProductionLineId.Equals(t.ProductionLineId) && e.MotorTypeId.Equals(t.MotorTypeId)).FirstOrDefault();
            var newIndex = idfac?.MotorIndex ?? 0 + 1;
            if (idfac != null)
            {
                idfac.MotorIndex = newIndex;
            }
            else
            {
                idfac = new MotorIdFactories() { MotorIndex = newIndex, MotorTypeId = t.MotorTypeId, ProductionLineId = t.ProductionLineId, Time = DateTimeOffset.Now };
            }
            _idRep.InsertOrUpdate(idfac);
            t.MotorId = newIndex.NewMotor(t.ProductionLineId, t.MotorTypeId, newIndex);

            await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().AddAsync(Mapper.Map<Models.Motor>(t));
            await CommitAsync();
        }
        public override int Insert(IEnumerable<Motor> ts)
        {
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities(e => e.ProductionLineId.Equals(t.ProductionLineId) && e.MotorTypeId.Equals(t.MotorTypeId)).FirstOrDefault();
                    var newIndex = idfac?.MotorIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac.MotorIndex = newIndex;
                    }
                    else
                    {
                        idfac = new MotorIdFactories() { MotorIndex = newIndex, MotorTypeId = t.MotorTypeId, ProductionLineId = t.ProductionLineId, Time = DateTimeOffset.Now };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t.MotorId = newIndex.NewMotor(t.ProductionLineId, t.MotorTypeId, newIndex);
                }
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().AddRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
                return Commit();
                
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"提交事务错误");
                return 0;
            }
        }
        public override async Task InsertAsync(IEnumerable<Motor> ts)
        {
           
            try
            {
                foreach (var t in ts)
                {
                    var idfac = _idRep.GetEntities(e => e.ProductionLineId.Equals(t.ProductionLineId) && e.MotorTypeId.Equals(t.MotorTypeId)).FirstOrDefault();
                    var newIndex = idfac?.MotorIndex ?? 0 + 1;
                    if (idfac != null)
                    {
                        idfac.MotorIndex = newIndex;
                    }
                    else
                    {
                        idfac = new MotorIdFactories() { MotorIndex = newIndex, MotorTypeId = t.MotorTypeId, ProductionLineId = t.ProductionLineId, Time = DateTimeOffset.Now };
                    }
                    _idRep.InsertOrUpdate(idfac);
                    t.MotorId = newIndex.NewMotor(t.ProductionLineId, t.MotorTypeId, newIndex);
                }
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().AddRangeAsync(Mapper.Map<IEnumerable<Models.Motor>>(ts));
                await CommitAsync();
               
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
            var t = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Find(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Remove(t);
            return Commit();
        }
        public override async Task DeleteEntityAsync(int id)
        {
            var t = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().FindAsync(id);
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Remove(Mapper.Map<Models.Motor>(t));
            await CommitAsync();
        }
        public override int DeleteEntity(Motor t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Remove(Mapper.Map<Models.Motor>(t));
            return ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).SaveChanges();
        }

        public override async Task DeleteEntityAsync(Motor t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Remove(Mapper.Map<Models.Motor>(t));
            await CommitAsync();
        }

        public override int DeleteEntity(IEnumerable<Motor> ts)
        {
            int results;
            
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().RemoveRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
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
        public override async Task DeleteEntityAsync(IEnumerable<Motor> ts)
        {
            //using (var transaction = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().RemoveRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
                    await CommitAsync();
                  
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex, $"提交事务错误");
                }
            }
        }

        public override int DeleteEntity(Expression<Func<Motor, bool>> where = null)
        {
            IQueryable<Models.Motor> ts;
            Expression<Func<Models.Motor, bool>> wheres;
            if (where != null)
            {
                wheres = Mapper.MapExpression<Expression<Func<Motor, bool>>, Expression<Func<Models.Motor, bool>>>(where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>();
            }
            int results;
            // using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Motor>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
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

        public override async Task DeleteEntityAsync(Expression<Func<Motor, bool>> where = null)
        {
            IQueryable<Models.Motor> ts;
            if (where != null)
            {
                var wheres = Mapper.MapExpression<Expression<Func<Motor, bool>>, Expression<Func<Models.Motor, bool>>>(@where);
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Where(wheres);
            }
            else
            {
                ts = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>();
            }
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId)
                        .Set<Models.Motor>()
                        .RemoveRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
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

        public override int UpdateEntity(Motor t)
        {

            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Update(Mapper.Map<Models.Motor>(t));
            return Commit();
        }

        public override int UpdateEntity(IEnumerable<Motor> ts)
        {
            var results = 0;
            //using (var transaction = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransaction())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().UpdateRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
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

        public override void InsertOrUpdate(Motor t)
        {
            var existing = ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Find(t.Id);
            if (existing == null)
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Add(Mapper.Map<Models.Motor>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Motor>(t));
            }

            Commit();
        }

        public override async Task UpdateEntityAsync(Motor t)
        {
            ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().Update(Mapper.Map<Models.Motor>(t));
            await CommitAsync();
        }

        public override async Task UpdateEntityAsync(IEnumerable<Motor> ts)
        {
            //using (var transaction =await  ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Database.BeginTransactionAsync())
            {
                try
                {
                    ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Models.Motor>().UpdateRange(Mapper.Map<IEnumerable<Models.Motor>>(ts));
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

        public override async Task InsertOrUpdateAsync(Motor t)
        {
            var existing = await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Set<Motor>().FindAsync(t.Id);
            if (existing == null)
            {
                await ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).AddAsync(Mapper.Map<Models.Motor>(t));
            }
            else
            {
                ContextFactory.Get(Thread.CurrentThread.ManagedThreadId).Entry(existing).CurrentValues.SetValues(Mapper.Map<Models.Motor>(t));
            }

            await CommitAsync();
        }

        #endregion



    }
}
