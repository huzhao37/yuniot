using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.;
using System.Threading;

namespace Yunt.TaskManager.Core
{

    /// <summary>
    /// 数据库建立工厂
    /// Modify By:
    /// Modify Date:
    /// Modify Reason:
    /// </summary>
    [Serializable]
    public sealed class DbFactory
    {
        public static DbContext GetCurrentDbContext(string connectstring,string dbNamestring,string threadId)
        {
            lock (threadId)
            {
                AsyncLocal
                //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）  
                //传递Context进去获取实例的信息，在这里进行强制转换。  
                var Context = CallContext.LogicalGetData(dbNamestring) as IDbContext;

                if (Context == null)  //线程在内存中没有此上下文  
                {
                    var Scope = UnitoonIotContainer.Container.BeginLifetimeScope();
                    //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
                    Context = Scope.Resolve<IDbContext>(new NamedParameter("connectionString", connectstring));
                    CallContext.LogicalSetData(dbNamestring, Context);
                }
                else
                {

                    if (!Context.ConnectionString.Equals(connectstring))
                    {
                        var Scope = UnitoonIotContainer.Container.BeginLifetimeScope();
                        //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
                        Context = Scope.Resolve<IDbContext>(new NamedParameter("connectionString", connectstring));
                        CallContext.LogicalSetData(dbNamestring, Context);
                    }
                }
                //Context.Configuration.ProxyCreationEnabled = false;//解决跨域远程调用dll不能被序列化的问题
               // Context.Configuration.LazyLoadingEnabled = false;
                return Context;
            }
        }

    }

    public static class HttpContext
    {
        public static IServiceProvider ServiceProvider;
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}
