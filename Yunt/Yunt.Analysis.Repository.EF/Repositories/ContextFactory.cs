using System;
using System.Collections.Concurrent;

namespace Yunt.Analysis.Repository.EF.Repositories
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
        public static  ConcurrentDictionary<int, AnalysisContext> ContextDic;
        //public static  IServiceProvider ServiceProvider;

        public static AnalysisContext Get(int threadId)
        {
            #region test
#if DEBUG
            //return BootStrap.ServiceProvider.GetService<AnalysisContext>();
#endif
            #endregion
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                ContextDic[threadId] = BootStrap.ServiceProvider.GetService<AnalysisContext>();
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            //ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, AnalysisContext>();
        }

    }
    
}
