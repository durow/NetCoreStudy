## Route的作用
检查收到请求的URL并找到相应的Controller和Action来处理请求。

## 路由规则的配置
### 模式匹配
路由规则中的变量，内建的特殊变量controller和action。  
获取变量的方法，可以在Action方法的参数中获取，也可以使用RouteDate.Values["v"]的方法获取。
在通过参数获取变量时MVC会尝试将变量转换为参数中指定的类型，这是种非常简洁的方式
可选变量，使用?，可选变量与C#中的可选参数十分相似，需要放在最后
可以用*把后面URL中所有的内容匹配上
匹配与Segments数量相同的URL，在需要匹配至Controller的位置一定要用controller作为变量名称，同样在需要匹配至Action的位置一定要用Action作为变量名称。  
其余参数可以自行设定。例如第三部分设置为id，那么当url匹配至这条规则（例如Home/Index/Test）后，变量id会赋值为Test。
同样的controller和action是两个名字比较特殊的变量，这两个变量会去匹配Controller和Action，但也同样是变量，也就是说可以和id一样在Action方法的参数中取出
可以给变量提供默认值

### 使用静态的路由配置
除了使用变量外，也可以直接指定静态的字符串进行路由匹配，例如可以使用Durow/{controller}/{action}，Path会静态匹配Durow，然后在从Path中匹配controller和action
此外也可以把静态和变量混合在同一个区段，例如My{controller}/My{action}。

路由规则是有顺序的，匹配到了某条规则后选取当前路由并停止匹配


如果路由找不到匹配的Controller和Action则返回空？
静态文件不经过路由。

## 路由规则中的约束
用:进行约束，例如{id:int?}，id需要为int?类型，只有当这部分可以parse为int?时路由才能匹配  
同样的，定义约束除了在路由模版外也可以在constraint参数中指定

### 自定义约束
实现IRouteConstraint接口

### 自定义约束
在Startup.cs中的ConfigureServices方法中配置RouteOptions，添加ConstraintMap。

## 使用Attribute定义路由规则
使用Attribute可以给Controller和Action单独指定路由
对于使用Attribute配置了路由的Controller或Action不能通过其它规则路由
## Tips
UseMvcWithDefaultRoute()方法可以使用{controller}/{action}/{id?}规则

## 使用Areas
一方面在路由模版中要指明area变量，以便在Area中路由，另一方面Area中的Controller必须使用Area特性以说明这个Controller是在哪个Area中
如果不指定Area Attirbute，Controller将会同普通Controller一样参与路由。

## 配置路由系统
