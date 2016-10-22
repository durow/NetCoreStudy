using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiddlewareStudy.Extensions;

namespace MiddlewareStudy
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
            //添加时间记录中间件所需的服务(其实就只有一个StopWatch)
            services.AddTimeMiddleware();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region 中间件测试1

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("<h1>I'm Middleware1</1>");
            //        await next(context); //注释掉后将不再调用后面的中间件
            //    };
            //});
            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("<h1>I'm Middleware2</1>");
            //        await next(context); //注释掉后将不再调用后面的中间件
            //    };
            //});
            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("<h1>I'm Middleware3</1>");
            //        await next(context); //注释掉后将不再调用后面的中间件
            //    };
            //});

            #endregion

            #region Map 测试

            app.Map("/Home/About", a =>
            {
                a.Use(next =>
                {
                    return async context =>
                    {
                        await context.Response.WriteAsync("<h1>From a Map pipeline!</h1>");
                    };
                });
            });

            #endregion

            #region MapWhen 测试

            //有大概20%概率进入这条管道
            app.MapWhen(context =>
            {
                var lucky = new Random(DateTime.Now.Second).Next();
                return lucky % 5 == 0;
            },
            a =>
            {
                a.Use(next =>
                {
                    return async context =>
                    {
                        await context.Response.WriteAsync("<h1>GoodLuck! From a MapWhen pipeline</h1>");
                    };
                });
            });


            #endregion

            app.UseStaticFiles();
            //添加时间记录中间件
            app.UseTimeMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
