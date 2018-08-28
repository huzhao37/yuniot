using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Yunt.Common;
using System.Collections.Generic;
namespace Yunt.Device.Repository.EF.Repositories
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
        public static ConcurrentDictionary<int, DeviceContext> ContextDic = new ConcurrentDictionary<int, DeviceContext>();
        public static IServiceScope ServiceScope = null;
        static ConcurrentDictionary<Thread, IServiceScope> ThreadPool = new ConcurrentDictionary<Thread, IServiceScope>();
        public static DeviceContext Get(int threadId)
        {
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                //ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<DeviceContext>(BootStrap.ServiceProvider); //第一次缓存的时候速度会慢很多，之后速度就上去了
                //test
                ServiceScope = ServiceProviderServiceExtensions.GetService<IServiceScopeFactory>(BootStrap.ServiceProvider).CreateScope();
                ContextDic[threadId] = ServiceProviderServiceExtensions.GetService<DeviceContext>(ServiceScope.ServiceProvider);
                ThreadPool[Thread.CurrentThread] = ServiceScope;
#if DEBUG
                Logger.Info($"current threadid is :{threadId}");
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
            //ContextDic=new ConcurrentDictionary<int, DeviceContext>();
            ////TEST
            //ThreadPool = new ConcurrentDictionary<Thread, IServiceScope>();
            //启动线程状态监测
            System.Threading.Tasks.Task.Factory.StartNew(() => Dispose());
        }

  
        void DoSthAboutRoute()
        {
            using (IServiceScope serviceScope = ServiceProviderServiceExtensions.GetService<IServiceScopeFactory>(BootStrap.ServiceProvider).CreateScope())
            {
                var routeService = ServiceProviderServiceExtensions.GetService<DeviceContext>(serviceScope.ServiceProvider);
                //....
            }
        }
    }
    
}
