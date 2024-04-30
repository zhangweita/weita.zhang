using ApiDemo.BackgroundServices;
using ApiDemo.Common;
using ApiDemo.Controllers;
using ApiDemo.Filters;
using ApiDemo.Models;
using ApiDemo.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR(options =>
{
}).AddStackExchangeRedis("127.0.0.1", options =>
{
    options.Configuration.ChannelPrefix = RedisChannel.Literal("SignalR_");
});

// 后台服务
builder.Services.AddHostedService<DemoBgService>();
builder.Services.AddHostedService<ExplortStatisticBgService>();
//builder.Services.Configure<HostOptions>(hostOptions =>
//{
//    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
//});

// 验证服务
//builder.Services.AddScoped<IValidator<Login2Request>, Login2RequestValidator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddDbContext<IdDbContext>(options =>
{
    string connStr = builder.Configuration.GetConnectionString("Default")!;
    options.UseSqlServer(connStr);
});
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);  // 登陆失败锁定时间
    options.Lockout.MaxFailedAccessAttempts = 5;    // 登录允许失败次数
});
IdentityBuilder identityBuilder = new(typeof(User), typeof(ApiDemo.Models.Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<IdDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<ApiDemo.Models.Role>>()
    .AddUserManager<UserManager<User>>();

builder.Services.Configure<ConnStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
        byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt!.SigningKey!);

        SymmetricSecurityKey secKey = new(keyBytes);

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hub/ChatRoomHub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    OpenApiSecurityScheme scheme = new()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };

    options.AddSecurityDefinition("Authorization", scheme);
    OpenApiSecurityRequirement requirement = new();
    requirement[scheme] = new List<string>();
    options.AddSecurityRequirement(requirement);
});

// 配置跨域处理，允许所有来源
string[] urls = ["http://localhost:5173"];
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
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

builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add<MyExceptionFilter>();   // 配置全局自定义异常拦截器
    options.Filters.Add<MyActionFilter1>();   // 配置全局操作拦截器
    options.Filters.Add<MyActionFilter2>();   // 配置全局操作拦截器
    options.Filters.Add<TransactionScopeFilter>();   // 配置启用事务的操作拦截器
    options.Filters.Add<RateLimitFilter>();   // 配置ip访问限流操作拦截器
    options.Filters.Add<JWTValidationFilter>();
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "ipc_";
});
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
        //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
        options.EnableTryItOutByDefault();
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatRoomHub>("/Hub/ChatRoomHub");

app.MapControllers();

// 允许所有跨域，cors是在ConfigureServices方法中配置的跨域策略名称
app.UseCors();

app.UseHttpsRedirection();

app.Run();
