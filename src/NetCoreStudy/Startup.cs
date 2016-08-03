using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetCoreStudy.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace NetCoreStudy
{
    public class Startup
    {
        //用来保存配置信息
        public IConfigurationRoot Configuration { get; }

        /*
         * 这里添加构造函数主要就是为了读取配置文件
         * 然后将配置信息保存在IConfigurationRoot中
         * 这样后面需要的时候可以直接读取配置
         */
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath) //设置根路径
                .AddJsonFile("appsettings.json"); //添加appsettings.json这个配置文件
            //如果添加了多个配置文件，会合并到一起。如果key重复了后面的会覆盖前面的

            Configuration = builder.Build();
        }

        /*
         * 这个方法主要用于配置服务，就是添加应用中依赖的各项服务，通过AddXXX的方式添加
         * 也就是往依赖注入容器中配置依赖
         * 这个方法必须叫这个名字，因为在Program.cs中我们注册了从Startup这个类启动
         * 程序在启动时会通过反射构造Startup的一个实例，同时通过反射调用ConfigureServices这个方法。
         * 如果不叫这个名字的话程序就执行不了这个方法。
         */
        public void ConfigureServices(IServiceCollection services)
        {
            //把配置文件对象注入到容器中，这样需要配置信息的时候可以随时从容器中获取。
            //因为配置是唯一的，所以AddSingleton，添加单例，每次返回的为同一个对象。
            services.AddSingleton<IConfiguration>(Configuration);

            //添加EntityFramework.Sqlite
            services.AddDbContext<StudyDbContext>(options =>
                options.UseSqlite(Configuration["DataConnection"]));
            //添加MVC依赖
            services.AddMvc();
        }

        /*
         * 这个方法主要用于配置中间件，通过UserXXX的方式添加和配置中间件。
         * 当一个HTTP请求到达时，会依次经过注册的每一个中间件，
         * 每个中间件都会对HTTP请求进行响应的处理，最终返回HTTP响应。
         * 同样这个方法由运行时通过反射调用，所以方法名字不要修改。
         * 方法的参数可以根据需要任意添加，因为该方法是通过反射调用，所以在调用时会根据该方法需要（依赖）的参数，
         * 从依赖注入容器中构造响应的实例。因此有一点需要注意的就是参数列表中的依赖必须是系统本身已经注入了的，
         * 或是在上面ConfigureServices中注入过的，不然无法构造实例。
         */
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(next =>
            {
                return async context =>
                {
                    var start = DateTime.Now;
                    await next.Invoke(context);
                    var ts = DateTime.Now - start;
                    await context.Response.WriteAsync(ts.TotalMilliseconds.ToString());
                    //return Task.FromResult(0);
                };
            });

            app.Use(next =>
            {
                return async context =>
                {
                    Thread.Sleep(1000);
                    //await context.Response.WriteAsync("<h1>MiddleWare B</h1>");
                    await next.Invoke(context);
                };
            });

            //添加并配置MVC中间件
            app.UseMvc(route =>
            route.MapRoute("default", "{controller=Home}/{action=Index}"));
        }
    }

    public class MidC
    {
        RequestDelegate _next;
        public MidC(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //await context.Response.WriteAsync("<h1>MiddleWare C</h1>");
            await _next.Invoke(context);
        }
    }
}
