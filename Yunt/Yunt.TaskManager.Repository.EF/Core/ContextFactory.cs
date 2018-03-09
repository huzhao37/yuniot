using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Yunt.TaskManager.Repository.EF.Core
{

    /// <summary>
    /// 上下文工厂
    /// Modify By:
    /// Modify Date:
    /// Modify Reason:
    /// </summary>
    [Serializable]
    public sealed class ContextFactory
    {
        private static readonly object Objlock = new object();
        public static  ConcurrentDictionary<int, object> ContextDic;
        public static  IServiceProvider ServiceProvider;

        public static TaskManagerContext Get(int threadId)
        {
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return (TaskManagerContext) ContextDic[threadId];
                ContextDic[threadId] = (TaskManagerContext)ServiceProvider.GetService(typeof(TaskManagerContext));
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return (TaskManagerContext)ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, object>();
        }
        //public static DbContext GetCurrentDbContext(string connectstring,string dbNamestring,string threadId)
        //{
        //    lock (threadId)
        //    {
                
        //       // //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）  
        //       // //传递Context进去获取实例的信息，在这里进行强制转换。  
        //       // var Context = CallContext.LogicalGetData(dbNamestring) as IDbContext;

        //       // if (Context == null)  //线程在内存中没有此上下文  
        //       // {
        //       //     var Scope = UnitoonIotContainer.Container.BeginLifetimeScope();
        //       //     //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
        //       //     Context = Scope.Resolve<IDbContext>(new NamedParameter("connectionString", connectstring));
        //       //     CallContext.LogicalSetData(dbNamestring, Context);
        //       // }
        //       // else
        //       // {

        //       //     if (!Context.ConnectionString.Equals(connectstring))
        //       //     {
        //       //         var Scope = UnitoonIotContainer.Container.BeginLifetimeScope();
        //       //         //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
        //       //         Context = Scope.Resolve<IDbContext>(new NamedParameter("connectionString", connectstring));
        //       //         CallContext.LogicalSetData(dbNamestring, Context);
        //       //     }
        //       // }
        //       // //Context.Configuration.ProxyCreationEnabled = false;//解决跨域远程调用dll不能被序列化的问题
        //       //// Context.Configuration.LazyLoadingEnabled = false;
        //       // return Context;
        //    }
        //}

    }
    
}
