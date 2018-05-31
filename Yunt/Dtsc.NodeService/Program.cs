using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using XCode.DataAccessLayer;

namespace Dtsc.NodeService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                XCode.Setting.Current.Migration = Migration.Off;
                //var myService = new MainService();
                //var serviceHost = new Win32ServiceHost(myService);
                //serviceHost.Run();//容易碰到权限问题，请使用管理员运行
                MainService.StartService();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
       
            
        }

    
    }
}
