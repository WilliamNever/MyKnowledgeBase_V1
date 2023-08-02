1 - 
Areas basic config.
Adding an area by 'new Scaffolded item'

2 - 
DB migrations - 

. create migration sql script file
script-migration -Idempotent

. add migration step to Migrations folder
Add-Migration Migration_Name -OutputDir Migrations

. create ef entities and context from a database
Scaffold-DbContext "connetion string" -Force -DataAnnotations Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities

. remove the latest migration
Remove-Migration

3 -
Enable Session in asp.net core

    ASP.net教程之netcore的Session使用小记

之前说过，core需要什么功能就添加并使用什么中间件

照例，在Startup.cs的ConfigureServices方法中添加services.AddSession();再在Configure方法中添加app.UseSession();(注意要在UseMvc之前)

 

再引用Microsoft.AspNetCore.Http就可以使用session啦

HttpContext.Session.Set(name, buffer);//使用上下文点出session赋值Set

HttpContext.Session.Get(name);//使用上下文点出session拿到对应值，用TryGetValue会保险一点

Set只是设置字节数组的值，如果要针对特定类型，可以引入相应的扩展程序集或者自行扩展

 

注意：

1. 在services.AddSession();之前最好有services.AddDistributedMemoryCache();表示进程内保持session。也可以使用redis等保存session，如services.AddDistributedRedisCache();

2. 在Startup.cs的ConfigureServices方法中默认配置了用户协议

services.Configure<CookiePolicyOptions>(options =>
{
　　options.CheckConsentNeeded = context => true;
　　options.MinimumSameSitePolicy = SameSiteMode.None;
});

其中options.CheckConsentNeeded = context => true;表示是否经过用户cookie协议同意。默认true

当为true时而又无弹窗提示用户是否同意时，将导致session无法回传，故设置为false才可正常使用session

至于原理，此处引用网上大神的说法：

　　首先session在写入时会返回给用户sessionid，这个id一般是存储在用户cookice或者url里在取session值的时候使用id查询的，
但是上面这个一旦开启，代表着用户必须同意你才可以使用cookie技术，导致你的sessionid无法回传，后端就会认为这是一个新的会话，所以产生了新的sessionid，就取不到值了。




 4 - 
 Enable log4net logger appending config file