## NetCoreStudy
这个仓库主要用于.NetCore学习，把学习中遇到的问题解决后留存到此处方便参考。

### IdentityStudy
用于.NET Core认证授权的学习。使用自己写的简单认证服务代替了自带的Identity组件。Cookie管理上使用自带的Cookie中间件，可以使用AuthorizeAttribute管理权限。认证功能十分简单，遇到需求了可以在这个基础上扩展。程序运行会检查数据库，没有需要的数据库表会自动创建。数据库用的Sqlite，为了方便试用了Dapper。

### LoggingStudy
主要学习.Net Core中的Logging组建，包括组件的设计，以NLog为例说明了如何集成第三方日志工具，最后继承了自己写的日志工具。

### MiddlewareStudy
主要学习了.NetCore中中间件是如何构建成管道的，以及用中间件类的方式添加中间件需要遵循的约定，最后自己实现了一个记录请求完成时间的中间件TimeMiddleware，用于学习目的的Map和MapWhen中间件。
