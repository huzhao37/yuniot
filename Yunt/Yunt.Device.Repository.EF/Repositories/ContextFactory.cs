﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Yunt.Common;

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
        public static  ConcurrentDictionary<int, DeviceContext> ContextDic;
        public static  IServiceProvider ServiceProvider;

        public static DeviceContext Get(int threadId)
        {
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return ContextDic[threadId];
                ContextDic[threadId] = (DeviceContext)ServiceProvider.GetService(typeof(DeviceContext));//第一次缓存的时候速度会慢很多，之后速度就上去了
#if DEBUG
                Logger.Info($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, DeviceContext>();
        }

        
    }
    
}
