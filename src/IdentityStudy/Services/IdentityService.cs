using IdentityStudy.Models;
using IdentityStudy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityStudy.Services
{
    public class IdentityService
    {
        public const string AuthenticationScheme = "MyAuthCookie";

        private IIdentityRepository _identityRepository;

        public MyUser User { get; private set; }

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        //使用用户名和密码获取用户
        public async Task<ClaimsPrincipal> CheckUserAsync(string username, string password)
        {
            var user = await _identityRepository.GetUser(username, password);
            if (user == null) return null;

            User = user;
            var ci = CreateClaimsIdentity(user);

            var roles = await _identityRepository.GetUserRolesAsync(user.UserName);
            AddRoleClaims(ci, roles.ToList());
            return new ClaimsPrincipal(ci);
        }
        
        //注册新用户
        public async Task<IdentityResult> Register(string username, string password)
        {
            if (await _identityRepository.CheckUserNameAsync(username))
                return new IdentityResult( "用户名已经存在!");

            var user = new MyUser { UserName = username, Password = password };
            _identityRepository.AddUserAsync(user);

            var ci = CreateClaimsIdentity(user);
            return new IdentityResult(new ClaimsPrincipal(ci));
        }

        #region 辅助方法

        private ClaimsIdentity CreateClaimsIdentity(MyUser user)
        {
            //用当前用户信息创建一个ClaimsIdentity
            //AuthenticationScheme需要和Cookie中间件中AuthenticationScheme一致
            //如果添加的角色时使用的类型不是ClaimTypes.Role，则需要在此处指定类型
            //var result = new ClaimsIdentity(AuthenticationScheme,NameType,RoleType);
            var result = new ClaimsIdentity(AuthenticationScheme);
            //NameType使用自带的ClaimTypes.Name
            result.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            return result;
        }

        private void AddRoleClaims(ClaimsIdentity claimsIdentity, IList<MyRole> roles)
        {
            foreach (var role in roles)
            {
                //添加角色时使用自带的ClaimTypes.Role就不需要在新建ClaimsIdentity时指定角色验证类型
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }
        }

        #endregion
    }

    public class IdentityResult
    {
        public bool IsSuccess { get; }
        public string ErrorString { get; }
        public ClaimsPrincipal User { get; }

        public IdentityResult(string error)
        {
            IsSuccess = false;
            ErrorString = error;
        }

        public IdentityResult(ClaimsPrincipal user)
        {
            IsSuccess = true;
            User = user;
        }
    }
}
