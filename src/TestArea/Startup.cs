using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace TestArea
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MyStopWatch>();
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //记录耗时的中间件
            app.UseMiddleware<TimeMiddleware>();

            //强制等待的中间件
            app.Use(next =>
            {
                return async context =>
                {
                    Thread.Sleep(100);  //为了使测试更明显我们强制Sleep 100毫秒
                    await next(context);
                };
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public class TimeMiddleware
        {
            private RequestDelegate _next;
            private MyStopWatch _watch;
            private string _name;

            public TimeMiddleware(RequestDelegate next, MyStopWatch watch)
            {
                _name = "2";
                _next = next;
                _watch = watch;
            }

            public TimeMiddleware(MyStopWatch watch, RequestDelegate next)
            {
                _name = "1";
                _next = next;
                _watch = watch;
            }

            

            public async Task Invoke(HttpContext context)
            {
                _watch?.Start();
               await _next.Invoke(context);   //调用后面中间件
                await context.Response.WriteAsync($"<div class=\"alert alert-info\" rol=\"alert\">共耗时:{_watch?.GetMillionSeconds()} 毫秒!  {_name}</div>");
            }
        }

        public class MyStopWatch
        {
            public DateTime StartTime { get; private set; } = DateTime.Now;

            public void Start()
            {
                StartTime = DateTime.Now;
            }

            public double GetMillionSeconds()
            {
                var ts = DateTime.Now - StartTime;
                return ts.TotalMilliseconds;
            }
        }
    }
}
