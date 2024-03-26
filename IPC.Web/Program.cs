using IPC.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(logger => logger.AddConsole());

var connectionString = builder.Configuration.GetConnectionString("SqliteDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SqliteDbContextConnection' not found.");
builder.Services.AddDbContext<SqliteDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SqliteDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
