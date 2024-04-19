using MiddlewareDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseMiddleware<MarkDownViewerMiddleware>();
app.UseStaticFiles();

app.Run();
