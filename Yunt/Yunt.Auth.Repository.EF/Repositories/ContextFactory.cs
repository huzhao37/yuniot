using System;
using System.Collections.Concurrent;

namespace Yunt.Auth.Repository.EF.Repositories
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

    }
    
}
