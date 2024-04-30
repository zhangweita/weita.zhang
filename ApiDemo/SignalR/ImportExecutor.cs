using ApiDemo.Common;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace ApiDemo.SignalR
{
    public class ImportExecutor(ILogger<ImportExecutor> logger,IHubContext<ImportDictHub> hubContext, IOptionsSnapshot<ConnStringOptions> connOptions)
    {
        private readonly ILogger<ImportExecutor> logger = logger;
        private readonly IHubContext<ImportDictHub> hubContext = hubContext;
        private readonly IOptionsSnapshot<ConnStringOptions> connOptions = connOptions;

        internal void ExecuteAsync(string connectionId)
        {
            throw new NotImplementedException();
        }
    }
}
