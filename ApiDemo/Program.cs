using ApiDemo.Filters;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);  // ��½ʧ������ʱ��
    options.Lockout.MaxFailedAccessAttempts = 5;    // ��¼����ʧ�ܴ���
});
IdentityBuilder identityBuilder = new(typeof(User), typeof(Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<IdDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<UserManager<User>>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���ÿ���������������Դ
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

//���Multipart body length limit 134217728 exceeded
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
    options.Filters.Add<MyExceptionFilter>();   // ����ȫ���Զ����쳣������
    options.Filters.Add<MyActionFilter1>();   // ����ȫ�ֲ���������
    options.Filters.Add<MyActionFilter2>();   // ����ȫ�ֲ���������
    options.Filters.Add<TransactionScopeFilter>();   // ������������Ĳ���������
    options.Filters.Add<RateLimitFilter>();   // ����ip������������������
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
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
        options.EnableTryItOutByDefault();
    });
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// �������п���cors����ConfigureServices���������õĿ����������
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.Run();
