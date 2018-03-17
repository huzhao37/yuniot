using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace Yunt.Common
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

        public static DbContext Get<T>(int threadId)
        {
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return (DbContext) ContextDic[threadId];
                ContextDic[threadId] = (DbContext)ServiceProvider.GetService(typeof(T));
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return (DbContext)ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, object>();
        }

    }
    
}
