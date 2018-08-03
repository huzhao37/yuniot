using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Yunt.Common
{
   public class ServiceEx
    {    /// <summary>
         /// 启动所有的服务插件
         /// </summary>
         /// <param name="services"></param>
        public static Dictionary<string, IServiceProvider> StartServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            var providers = new Dictionary<string, IServiceProvider>();
            try
            {
                //var path = AppDomain.CurrentDomain.BaseDirectory;
                dynamic type = (new ServiceEx()).GetType();
                string path = Path.GetDirectoryName(type.Assembly.Location);

                //var dllPath= configuration.GetSection("AppSettings").GetValue<string>("DllPath");
                //FileEx.CopyFolderTo(dllPath, path);

                //FileEx.TryLoadAssembly();
                var files = new DirectoryInfo(path).GetFiles();
                foreach (var f in files)
                {
                    if (!f.Name.Contains(".dll") || !f.Name.Contains("Repository")) continue;
                    var dll = AssemblyLoadContext.Default.LoadFromAssemblyPath(f.FullName);
                 
                    //AssemblyEx.GetReferencingAssemblies(f.FullName);
                   // AssemblyLoadContext.Default.LoadFromAssemblyPath(path+"\\Yunt.Redis.dll");
                    if(dll==null) continue;
                    //Logger.Info(dll?.FullName??"null");
                    //Logger.Info(dll?.Location??"null");
                    var types = dll.GetTypes()?.Where(a => a.IsClass && a.Name.Equals("BootStrap"));
                    if(!types?.Any()??false)
                        continue;
                    types.ToList().ForEach(d =>
                    {                          
                        var method = d.GetMethod("Start", BindingFlags.Instance | BindingFlags.Public);

                        var method2 = d.GetMethod("ContextInit", BindingFlags.Instance | BindingFlags.Public);
                        var obj = Activator.CreateInstance(d);

                           
                        method.Invoke(obj, new object[] { services, configuration });
                        var serviceProvider = services.BuildServiceProvider();
                        method2.Invoke(obj, new object[] { serviceProvider });

                        var attribute = d.GetCustomAttributes(typeof(ServiceAttribute), false).FirstOrDefault();

                        if (attribute != null)
                        {
                            providers.Add(((ServiceAttribute)attribute).Name.ToString(), serviceProvider); ;
                        }

                    });
                }

            }
            catch (Exception ex)
            {
                Logger.Info($"ServiceEx报错:{ex.StackTrace}");
                Logger.Info($"ServiceEx报错:{ex.InnerException}");
                Logger.Info($"ServiceEx报错:{ex.InnerException.Message+"_"+ex.InnerException.StackTrace}");
                Logger.Info($"ServiceEx报错:{ex.InnerException.InnerException}");
                Logger.Info($"ServiceEx报错:{ex.InnerException.InnerException.Message + "_" + ex.InnerException.InnerException.StackTrace}");
                Logger.Exception(ex);
            }
            return providers;


        }
    }
}
