using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Yunt.WebApiV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>();
        new WebHostBuilder()
         .UseKestrel()
        // .UseUrls("http://*:5200")
         .UseContentRoot(Directory.GetCurrentDirectory())
         .ConfigureLogging((hostingContext, logging) =>
         {
             logging.AddConsole();
         })
         .UseStartup<Startup>();
    }
}
