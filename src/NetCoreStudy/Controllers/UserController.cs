using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreStudy.Entity;
using Microsoft.AspNetCore.Mvc.Routing;
using NetCoreStudy.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreStudy.Controllers
{
    public class UserController : Controller
    {
        private StudyDbContext dbContext { get; set; }

        public UserController(StudyDbContext context)
        {
            dbContext = context;
        }

        [HttpGet()]
        public IEnumerable<User> Get()
        {
            var result = dbContext.User.ToList();
            if (result.Any())
                return result;
            else
                return new List<User>
                {
                    new Entity.User
                    {
                        Name = "fail",
                    }
                };
        }
    }
}
