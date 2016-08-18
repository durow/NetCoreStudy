using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityStudy.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;

namespace IdentityStudy.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        const string DbFile = "identity.db";
        const string DbConnection = "Data Source=" + DbFile;
        public SqliteConnection Con
        {
            get { return new SqliteConnection(DbConnection); }
        }

        //添加用户
        public async void AddUserAsync(MyUser user)
        {
            const string sql = "INSERT INTO Users(UserName,Password) VALUES(@UserName,@Password)";
            await Con.ExecuteAsync(sql, user);
        }

        //检查用户名是否已经存在
        public async Task<bool> CheckUserNameAsync(string username)
        {
            const string sql = "SELECT UserName FROM Users WHERE UserName=@UserName";
            var result = await Con.QueryAsync(sql, new { UserName = username });
            return result.Count() > 0;
        }

        //使用用户名和密码获取用户信息
        public async Task<MyUser> GetUserAsync(string username, string password)
        {
            const string sql = "SELECT * FROM Users WHERE UserName=@UserName and Password=@Password";
            return await Con.QueryFirstOrDefaultAsync<MyUser>(sql, new { UserName = username, Password = password });
        }

        //获取用户角色信息
        public async Task<IEnumerable<MyRole>> GetUserRolesAsync(string username)
        {
            const string sql = @"SELECT Roles.Id, RoleName FROM Roles 
                                            INNER JOIN UserRoles ON UserRoles.RoleId=Roles.Id 
                                          WHERE UserRoles.UserName=@UserName";
            return await Con.QueryAsync<MyRole>(sql, new { UserName = username });
        }

        //创建空数据库
        public static async void CreateDbAsync()
        {
            const string sql = @"CREATE TABLE IF NOT EXISTS [Roles](
                                            [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, 
                                            [RoleName] TEXT);

                                        CREATE TABLE IF NOT EXISTS [UserRoles](
                                            [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, 
                                            [UserName] TEXT, 
                                            [RoleId] INTEGER);

                                        CREATE TABLE IF NOT EXISTS [Users](
                                            [UserName] TEXT PRIMARY KEY NOT NULL UNIQUE, 
                                            [Email] TEXT, 
                                            [Password] TEXT NOT NULL);";

            await new SqliteConnection(DbConnection).ExecuteAsync(sql);
        }
    }
}
