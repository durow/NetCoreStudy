using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterStudy.Filters
{
    public class MyActionAttribute : Attribute, IActionFilter
    {
        DateTime start;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ResponseHelper.Write("MyAction.OnActionExecuted Start!",context.HttpContext.Response);

            var ts = DateTime.Now - start;
            ResponseHelper.Write($"MyAction.OnActionExecuted.Total time:{ts.TotalMilliseconds}",context.HttpContext.Response);            

            ResponseHelper.Write("MyAction.OnActionExecuted End!",context.HttpContext.Response);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ResponseHelper.Write("MyAction.OnActionExecuting Start!",context.HttpContext.Response);
            
            start = DateTime.Now;

            ResponseHelper.Write("MyAction.OnActionExecuting End!",context.HttpContext.Response);
        }
    }
}
