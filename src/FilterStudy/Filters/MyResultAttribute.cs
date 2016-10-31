using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterStudy.Filters
{
    public class MyResultAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            ResponseHelper.Write("MyResult.OnResultExecuted Start!",context.HttpContext.Response);
            
            ResponseHelper.Write("MyResult.OnResultExecuted End!",context.HttpContext.Response);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            ResponseHelper.Write("MyResult.OnResultExecuting Start!",context.HttpContext.Response);
            
            ResponseHelper.Write("MyResult.OnResultExecuting End!",context.HttpContext.Response);
        }
    }
}
