using IPC.Common.AutoMapper;
using IPC.Common.Configuration;
using IPC.Service.ApiLog;
using IPC.Web.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadConfiguration(builder.Configuration);

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
//builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
//{
//    string connStr = builder.Configuration.GetValue<string>("Redis:ConnStr");
//    return ConnectionMultiplexer.Connect(connStr);
//});


builder.Services.AddLogging(logger => logger.AddConsole());

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(IPCMapperProfile));

builder.Services.AddScoped<ApiLogService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseCors();
//app.UseResponseCaching();   // 启用响应缓存中间件

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints?.MapRazorPages();
    endpoints?.MapControllerRoute(
        name: "default",
        pattern: "{controller=ApiLog}/{action=Index}/{id?}");
});


app.Run();
