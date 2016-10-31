using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterStudy.Filters
{
    public class MyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ResponseHelper.Write("MyAuth.OnAuthorization Start!", context.HttpContext.Response);

            var key = context.HttpContext.Request.Query["key"];
            if (key != "durow")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                ResponseHelper.Write("MyAuth.OnAuthorization Authorization failed!", context.HttpContext.Response);
            }
            else
            {
                ResponseHelper.Write("MyAuth.OnAuthorization Success!", context.HttpContext.Response);
            }
        }
    }
}
