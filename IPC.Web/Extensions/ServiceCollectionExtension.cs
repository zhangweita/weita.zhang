﻿using IPC.Common.Utils.Encryption;
using IPC.DataAccess.Oracle;
using IPC.DataAccess.Sqlite.ConfigModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IPC.Web.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 加载多个数据库上下文配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection LoadConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        // 添加配置数据库
        string connStr = configuration.GetSection("IPCInfomation").Value!;
        services.AddDbContext<DataAccess.Sqlite.SqliteEFDbContext>(options =>
        {
            options.UseSqlite(connStr);
        });

        configuration.AddDbConfiguration(new DBConfigOptions
        {
            CreateDbConnection = () => new SqliteConnection(connStr),
            TableName = "IPC_Configuration",
            ReloadOnChange = true,
            ReloadInterval = TimeSpan.FromSeconds(5)
        });

        // 加载配置类
        services.Configure<DbStringOptions>(configuration.GetSection("ConnectionString"));


        // 注册正式测试环境数据库上下文
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var dbStringOptions = serviceProvider.GetService<IOptionsSnapshot<DbStringOptions>>().Value;
        services.AddDbContext<FormalReadDbContext>(options =>
        {
            string connectionString = DesUtil.Decrypt(dbStringOptions.OracleRead);
            options.UseOracle(connectionString);
        });
        services.AddDbContext<FormalWriteDbContext>(options =>
        {
            string connectionString = DesUtil.Decrypt(dbStringOptions.OracleWrite);
            options.UseOracle(connectionString);
        });
        services.AddDbContext<TestRWDbContext>(options =>
        {
            string connectionString = DesUtil.Decrypt(dbStringOptions.OracleTest);
            options.UseOracle(connectionString);
        });

        return services;
    }
}
