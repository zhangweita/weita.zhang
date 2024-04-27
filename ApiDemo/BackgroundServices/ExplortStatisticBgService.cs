using ApiDemo.Models;
using System.Text;

namespace ApiDemo.BackgroundServices;

public class ExplortStatisticBgService : BackgroundService
{
    private readonly IServiceScope serviceScope;
    private readonly IdDbContext dbContext;
    private readonly ILogger<ExplortStatisticBgService> logger;

    public ExplortStatisticBgService(IServiceScopeFactory scopeFactory)
    {
        serviceScope = scopeFactory.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        dbContext = serviceProvider.GetRequiredService<IdDbContext>();
        logger = serviceProvider.GetRequiredService<ILogger<ExplortStatisticBgService>>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DoExecuteAsync();
                await Task.Delay(5000);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "获取用户统计数据失败");
                await Task.Delay(1000);
            }
        }
    }

    private async Task DoExecuteAsync()
    {
        var items = dbContext.Users.GroupBy(u => u.CreationTime.Date).Select(e => new { Date = e.Key, Count = e.Count() });
        StringBuilder sb = new();
        sb.AppendLine($"Date:{DateTime.Now}");
        foreach (var item in items)
        {
            sb.Append(item.Date).AppendLine($":{item.Count}");
        }
        await File.WriteAllTextAsync("d:/1.txt", sb.ToString());
        logger.LogInformation($"导出完成");
    }

    public override void Dispose()
    {
        base.Dispose();
        serviceScope.Dispose();
    }
}
