using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RouteStudy.Constraint
{
    public class EvenRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            int id;
            if (!int.TryParse(values[routeKey].ToString(), out id))
                return false;

            return id % 2 == 0;
        }
    }
}
