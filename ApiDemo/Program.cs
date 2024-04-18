using BooksEFCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
{
    string connStr = builder.Configuration.GetConnectionString("Default")!;
    options.UseSqlServer(connStr);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置跨域处理，允许所有来源
string[] urls = ["http://localhost:5173"];
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(urls)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});

//解决Multipart body length limit 134217728 exceeded
builder.Services.Configure<FormOptions>(x =>
{
    x.BufferBody = true;
    x.BufferBodyLengthLimit = long.MaxValue;
    x.MemoryBufferThreshold = int.MaxValue;
    x.MultipartBoundaryLengthLimit = int.MaxValue;
    x.ValueLengthLimit = int.MaxValue;
    x.KeyLengthLimit = int.MaxValue;
    x.ValueCountLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersCountLimit = int.MaxValue;
});


builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "ipc_";
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
        options.EnableTryItOutByDefault();
    });
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// 允许所有跨域，cors是在ConfigureServices方法中配置的跨域策略名称
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.Run();
