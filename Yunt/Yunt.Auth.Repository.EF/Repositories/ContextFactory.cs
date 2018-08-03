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
        internal static  ConcurrentDictionary<int, AuthContext> ContextDic;
       // public static  IServiceProvider ServiceProvider;

        internal static AuthContext Get(int threadId)
        {
            #region test
#if DEBUG

            //var test1= BootStrap.ServiceProvider;
            //var x= test1.GetType();
#endif
            #endregion
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                ContextDic[threadId] = BootStrap.ServiceProvider.GetService<AuthContext>();
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
           // ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, AuthContext>();
        }

    }
    
}
