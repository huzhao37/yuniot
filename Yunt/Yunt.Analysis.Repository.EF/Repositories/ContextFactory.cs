﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading;

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
        public static ConcurrentDictionary<int, AnalysisContext> ContextDic = new ConcurrentDictionary<int, AnalysisContext>();
        public static IServiceScope ServiceScope = null;
        static ConcurrentDictionary<Thread, IServiceScope> ThreadPool = new ConcurrentDictionary<Thread, IServiceScope>();
        public static AnalysisContext Get(int threadId)
        {        
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                //ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<AnalysisContext>(BootStrap.ServiceProvider);
                #region test
                ServiceScope = ServiceProviderServiceExtensions.GetService<IServiceScopeFactory>(BootStrap.ServiceProvider).CreateScope();
                ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<AnalysisContext>(ServiceScope.ServiceProvider);
                ThreadPool[Thread.CurrentThread] = ServiceScope;
            
                #endregion
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        private static void Dispose()
        {
            while (true)
            {
                var threads = ThreadPool;
                if (threads != null && threads.Count > 0)
                {
                    foreach (var item in threads)
                    {
                        //if (item.Key.ThreadState != System.Threading.ThreadState.Running)
                        if (!item.Key.IsAlive)
                        {
                            if (ContextDic.ContainsKey(item.Key.ManagedThreadId))
                            {
                                ContextDic[item.Key.ManagedThreadId]?.Dispose();
                                ContextDic.Remove(item.Key.ManagedThreadId);
                            }
                            ThreadPool[item.Key]?.Dispose();
                            ThreadPool.Remove(item.Key);
                            GC.Collect();
                        }
                    }

                }
                Thread.Sleep(50 * 1000);//10s
            }

        }
        public static void Init()
        {
            //ContextDic=new ConcurrentDictionary<int, AnalysisContext>();
            ////TEST
            //ThreadPool = new ConcurrentDictionary<Thread, IServiceScope>();
            //启动线程状态监测
            System.Threading.Tasks.Task.Factory.StartNew(() => Dispose());
        }

    }
    
}
