using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           // services.AddDbContext<AppDbContext>();

            services.AddCap(x =>
            {
                // 如果你的 SqlServer 使用的 EF 进行数据操作，你需要添加如下配置：
                // 注意: 你不需要再次配置 x.UseSqlServer(""")
                x.UseEntityFramework<AppDbContext>();

                // 如果你使用的Dapper，你需要添加如下配置：
               // x.UseSqlServer("Server = 10.1.5.25, 1433; Database = test; Persist Security Info = True; User ID = sa; password = Password01!;");

            
                // 如果你使用的 RabbitMQ 作为MQ，你需要添加如下配置：
                x.UseRabbitMQ("localhost");

                //如果你使用的 Kafka 作为MQ，你需要添加如下配置：
               // x.UseKafka("localhost：5672");
            });
            var contextOptions = new DbContextOptionsBuilder().UseSqlServer("Server = 10.1.5.25, 1433; Database = test; Persist Security Info = True; User ID = sa; password = Password01!;").Options;
            services.AddSingleton(contextOptions) .AddTransient<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // 添加 CAP
            app.UseCap();
        }
    }
}
