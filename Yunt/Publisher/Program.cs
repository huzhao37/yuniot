using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Publisher.Models;

namespace Publisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            //var app = new AppDbContext();
            //app.Person.Add(new Person() {Name = "test", Age = 1});
            //var rel = app.SaveChanges();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

    }
}
