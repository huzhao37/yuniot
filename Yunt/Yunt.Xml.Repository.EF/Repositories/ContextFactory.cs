using System;
using System.Collections.Concurrent;

namespace Yunt.Xml.Repository.EF.Repositories
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
        public static  ConcurrentDictionary<int, XmlContext> ContextDic;
        //public static  IServiceProvider ServiceProvider;

        public static XmlContext Get(int threadId)
        {
            #region test
#if DEBUG
            //return BootStrap.ServiceProvider.GetService<XmlContext>();
#endif
            #endregion
            lock (Objlock)
            {
                if (ContextDic.ContainsKey(threadId)) return  ContextDic[threadId];
                ContextDic[threadId] = BootStrap.ServiceProvider.GetService<XmlContext>();//第一次缓存的时候速度会慢很多，之后速度就上去了
#if DEBUG
                Console.WriteLine($"current threadid is :{threadId}");
#endif
                return ContextDic[threadId];
            }

        }

        public static void Init(IServiceProvider serviceProvider)
        {
            //ServiceProvider = serviceProvider;
            ContextDic=new ConcurrentDictionary<int, XmlContext>();
        }

    }
    
}
