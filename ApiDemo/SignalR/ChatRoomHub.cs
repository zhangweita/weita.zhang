using Microsoft.AspNetCore.SignalR;

namespace ApiDemo.SignalR;

public class ChatRoomHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public Task SendPublicMessage(string message)
    {
        string connectionId = this.Context.ConnectionId;
        string msg = $"{connectionId} {DateTime.Now}: {message}";

        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
}
