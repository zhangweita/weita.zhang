using Microsoft.AspNetCore.SignalR;

namespace ApiDemo.SignalR;

public class ImportDictHub(ImportExecutor importExecutor) : Hub
{
    private readonly ImportExecutor importExecutor = importExecutor;

    public Task Import()
    {
        importExecutor.ExecuteAsync(this.Context.ConnectionId);
        return Task.CompletedTask;
    }
}
