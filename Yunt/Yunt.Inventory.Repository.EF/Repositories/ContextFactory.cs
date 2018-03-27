using System;
using System.Collections.Concurrent;

namespace Yunt.Inventory.Repository.EF.Repositories
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

        public static InventoryContext Get(int threadId)
        {
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return (InventoryContext) ContextDic[threadId];
                ContextDic[threadId] = ServiceProvider.GetService(typeof(InventoryContext));//第一次缓存的时候速度会慢很多，之后速度就上去了
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return (InventoryContext)ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, object>();
        }

    }
    
}
