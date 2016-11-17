using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        //添加和配置IOC
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddMvc(); //添加MVC相关服务
        }

        //添加和配置中间件
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(); //添加日志服务中间件
            if (env.IsDevelopment()) //开发环境中使用详细异常页面
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages(); //发送友好状态码页面
            app.UseStaticFiles(); //添加静态文件中间件，静态文件路由到wwwroot目录，不进入MVC路由
            app.UseMvcWithDefaultRoute(); //最后添加MVC中间件，并使用默认路由配置
        }
    }
}
