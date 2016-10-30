## NetCoreStudy
这个仓库主要用于.NetCore学习，把学习中遇到的问题解决后留存到此处方便参考。

### IdentityStudy
用于.NET Core认证授权的学习。使用自己写的简单认证服务代替了自带的Identity组件。Cookie管理上使用自带的Cookie中间件，可以使用AuthorizeAttribute管理权限。认证功能十分简单，遇到需求了可以在这个基础上扩展。程序运行会检查数据库，没有需要的数据库表会自动创建。数据库用的Sqlite，为了方便试用了Dapper。相关文章：

[.NET Core中的认证管理解析](http://www.cnblogs.com/durow/p/5783089.html)

### LoggingStudy
主要学习.Net Core中的Logging组建，包括组件的设计，以NLog为例说明了如何集成第三方日志工具，最后继承了自己写的日志工具。相关文章：

[.NetCore中的日志（1）日志组件解析](http://www.cnblogs.com/durow/p/5850301.html)  
[.NetCore中的日志（2）集成第三方日志工具](http://www.cnblogs.com/durow/p/5856538.html)

### MiddlewareStudy
主要学习了.NetCore中中间件是如何构建成管道的，以及用中间件类的方式添加中间件需要遵循的约定，最后自己实现了一个记录请求完成时间的中间件TimeMiddleware，用于学习目的的Map和MapWhen中间件。相关文章：

[.NET Core中间件的注册和管道的构建（1）---- 注册和构建原理](http://www.cnblogs.com/durow/p/5736385.html)  
[ .NET Core中间件的注册和管道的构建（2）---- 用UseMiddleware扩展方法注册中间件类](http://www.cnblogs.com/durow/p/5748124.html)  
[.NET Core中间件的注册和管道的构建（3） ---- 使用Map/MapWhen扩展方法](http://www.cnblogs.com/durow/p/5752055.html)

### RouteStudy
主要学习.NetCore MVC中的路由。相关文章：

[.NetCore MVC中的路由（1）路由配置基础](http://www.cnblogs.com/durow/p/5992926.html)  
[.NetCore MVC中的路由（2）在路由中使用约束](http://www.cnblogs.com/durow/p/6012788.html)
