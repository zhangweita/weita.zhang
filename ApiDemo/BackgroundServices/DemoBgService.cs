
namespace ApiDemo.BackgroundServices;

public class DemoBgService(ILogger<DemoBgService> logger) : BackgroundService
{
    private readonly ILogger<DemoBgService> logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000);
        string s = await File.ReadAllTextAsync("d:/1.txt");
        await Task.Delay(20000);
        logger.LogInformation(s);
    }
}
