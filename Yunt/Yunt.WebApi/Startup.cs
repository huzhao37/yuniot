using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Metrics;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using NewLife.Log;
using Swashbuckle.AspNetCore.Swagger;
using Yunt.Common;
using Yunt.Redis;
using Yunt.WebApi.Data;
namespace Yunt.WebApi
{
   
    public class Startup
    {
        //internal static Dictionary<string, IServiceProvider> Providers;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            XTrace.UseConsole(true, true);
#endif
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
                  .SetBasePath(currentDirectory)
                  .AddJsonFile("appsettings.json", true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            //Logger.Create(configuration, new LoggerFactory(), "Yunt.WebApi");


            ServiceEx.StartServices(services, configuration);//Providers = 
            services.AddAutoMapper(typeof(Startup).Assembly);
            BufferPool.DEFAULT_BUFFERLENGTH = 1024 * 1024;//1M缓冲区
            services.AddMvc();
            #region Metrics监控配置
            string IsOpen = configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
            if (IsOpen == "true")
            {
                string database = configuration.GetSection("InfluxDB")["DataBaseName"];
                string InfluxDBConStr = configuration.GetSection("InfluxDB")["ConnectionString"];
                string app = configuration.GetSection("InfluxDB")["app"];
                string env = configuration.GetSection("InfluxDB")["env"];
                string username = configuration.GetSection("InfluxDB")["username"];
                string password = configuration.GetSection("InfluxDB")["password"];

                var uri = new Uri(InfluxDBConStr);

                var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.Configure(
                options =>
                {
                    options.AddAppTag(app);
                    options.AddEnvTag(env);
                })
                .Report.ToInfluxDb(
                options =>
                {
                    options.InfluxDb.BaseUri = uri;
                    options.InfluxDb.Database = database;
                    options.InfluxDb.UserName = username;
                    options.InfluxDb.Password = password;
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    options.FlushInterval = TimeSpan.FromSeconds(5);
                })
                .Build();

                services.AddMetrics(metrics);
                services.AddMetricsReportScheduler();
                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();

            }
            #endregion
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder2 =>
                {
                    builder2.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            //services.AddSingleton<UserStore>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
               // o.Authority = "https://wddapi.unitoon.cn/";
                o.Authority = "https://oidc.faasx.com/";
                o.Audience = "api";

                o.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,

                    // 用于适配本地模拟Token
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Consts.Secret))
                };
            });

            //swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "Yunt Api"
            //    });
            //    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    var xmlPath = Path.Combine(basePath, "Yunt.WebApi.xml");
            //    c.IncludeXmlComments(xmlPath);
            //     }
            //);

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseMvc();

            app.UseStaticFiles();

            app.UseAuthentication();
            #region 注入Metrics
            string IsOpen = "true";//Configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
            if (IsOpen == "true")
            {
                app.UseMetricsAllMiddleware();
                // Or to cherry-pick the tracking of interest
                app.UseMetricsActiveRequestMiddleware();
                app.UseMetricsErrorTrackingMiddleware();
                app.UseMetricsPostAndPutSizeTrackingMiddleware();
                app.UseMetricsRequestTrackingMiddleware();
                app.UseMetricsOAuth2TrackingMiddleware();
                app.UseMetricsApdexTrackingMiddleware();

                app.UseMetricsAllEndpoints();
                // Or to cherry-pick endpoint of interest
                app.UseMetricsEndpoint();
                app.UseMetricsTextEndpoint();
                app.UseEnvInfoEndpoint();
            }
            #endregion
            //app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=SampleData}/{action=WeatherForecasts}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "SampleData", action = "WeatherForecasts" });
            });

            //swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(c => {
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YuntApi");
            //});

            //跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                //builder.WithOrigins("http://10.1.5.102:8087");//生产环境-前端地址
                builder.AllowAnyOrigin();
            });
        }
    }
}
