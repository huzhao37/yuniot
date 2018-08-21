﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading;

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
        public static  ConcurrentDictionary<int, InventoryContext> ContextDic;
        //public static  IServiceProvider ServiceProvider;
        static IServiceScope ServiceScope;
        static ConcurrentDictionary<Thread, IServiceScope> ThreadPool;
        public static InventoryContext Get(int threadId)
        {
        
            lock (Objlock)
            {
                //if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                //ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<InventoryContext>(BootStrap.ServiceProvider);//第一次缓存的时候速度会慢很多，之后速度就上去了
                #region test
                ServiceScope = ServiceProviderServiceExtensions.GetService<IServiceScopeFactory>(BootStrap.ServiceProvider).CreateScope();
                {
                    ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<InventoryContext>(ServiceScope.ServiceProvider);
                    ThreadPool[Thread.CurrentThread] = ServiceScope;
                }
                #endregion
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        static void Dispose()
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
                Thread.Sleep(10000);//10s
            }

        }
        public static void Init(IServiceProvider serviceProvider)
        {
            //ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, InventoryContext>();
            //TEST
            ThreadPool = new ConcurrentDictionary<Thread, IServiceScope>();
            //启动线程状态监测
            System.Threading.Tasks.Task.Factory.StartNew(() => Dispose());
        }

    }
    
}
