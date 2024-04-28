using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiDemo.SignalR;

[Authorize]
public class ChatRoomHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public Task SendPublicMessage(string message)
    {
        //string connectionId = this.Context.ConnectionId;
        //string msg = $"{connectionId} {DateTime.Now}: {message}";
        string name = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
        string msg = $"{name} {DateTime.Now}: {message}";

        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
}
