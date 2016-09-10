using Microsoft.AspNetCore.Http;
using MiddlewareStudy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareStudy.Middlewares
{
    //中间件的结构是基于约定的，强调约定大于配置
    //因此在写中间件时需要遵循约定
    public class TimeMiddleware
    {
        private RequestDelegate _next;
        private StopWatch _watch;

        //构造函数第一个参数最好为RequestDelegate
        //后面接着是需要注入的参数
        //除了依赖注入外，也可以在使用UseMiddleware方法添加中间件时添加参数中所需的依赖
        public TimeMiddleware(RequestDelegate next, StopWatch watch)
        {
            _next = next;
            _watch = watch;
        }

        //中间件入口只能叫Invoke，且返回Task
        //该方法参数也可以注入，不过建议全都在构造函数中注入，Invoke方法只保留一个HttpContext参数
        public async Task Invoke(HttpContext context)
        {
            _watch?.Start();
            await _next.Invoke(context); //调用后面中间件
            //后面中间件都调用完成后记录花费时间
            await context.Response.WriteAsync($"<div class=\"alert alert-info\" rol=\"alert\">共耗时：{_watch.GetMillionSeconds()} 毫秒</div>");
        }
    }
}
