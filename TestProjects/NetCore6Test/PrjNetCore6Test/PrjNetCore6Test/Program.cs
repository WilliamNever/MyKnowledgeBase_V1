

using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(cfg => {
    cfg.AddLog4Net();

    //默认的配置文件路径是在根目录，且文件名为log4net.config
    //如果文件路径或名称有变化，需要重新设置其路径或名称
    //比如在项目根目录下创建一个名为cfg的文件夹，将log4net.config文件移入其中，并改名为log.config
    //则需要使用下面的代码来进行配置
    //cfg.AddLog4Net(new Log4NetProviderOptions()
    //{
    //    Log4NetConfigFileName = "cfg/log.config",
    //    Watch = true
    //});

    //查看支持日志写入哪些地方。本例中用的是-
    //log4net.Appender.FileAppender

    //var mateher = new log4net.Filter.LoggerMatchFilter();
    //mateher.AcceptOnMatch = false;
    //mateher.LoggerToMatch

});
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.UseMemberCasing();
    })
    ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    /*
     * 表示是否经过用户cookie协议同意。默认true
     * 当为true时而又无弹窗提示用户是否同意时，将导致session无法回传，故设置为false才可正常使用session
     */
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

#region Authorization part
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();
#endregion


var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
logger?.LogError("Ex Error - ");
logger?.LogInformation("Ex Information - ");
logger?.LogDebug("Ex Debug - ");

app.UseSession();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
    }
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "hp/{controller=Home}/{action=Index}/{id?}",//{area:exists}
      defaults: new { area = "AHelpArea" }
    );
});

app.MapControllerRoute(
    name: "Apis",
    pattern: "api/{controller}/{action}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "defaultAreas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
