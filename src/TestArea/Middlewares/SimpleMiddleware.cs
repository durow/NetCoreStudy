using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestArea.Middlewares
{
    public class SimpleMiddleware
    {
        //保存下一个中间件
        private RequestDelegate _next;
        //构造函数中传入下一个中间件
        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //中间件方法，接收HttpContext参数返回Task
        public Task Invoke(HttpContext context)
        {
            //什么都不干直接调用下一个中间件
            return _next(context);
        }
    }
}
