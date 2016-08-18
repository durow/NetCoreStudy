using IdentityStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityStudy.Repositories
{
    public interface IIdentityRepository
    {
        void AddUserAsync(MyUser user);
        Task<MyUser> GetUserAsync(string username, string password);
        Task<IEnumerable<MyRole>> GetUserRolesAsync(string username);
        Task<bool> CheckUserNameAsync(string username);
    }
}
