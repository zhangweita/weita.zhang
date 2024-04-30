using ApiDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ApiDemo.SignalR;

[Authorize]
public class ChatRoomHub(UserManager<User> userManager) : Hub
{
    private readonly UserManager<User> userManager = userManager;

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

    public async Task<string> SendPrivateMessage(string destUserName, string message)
    {
        User? destUser = await userManager.FindByNameAsync(destUserName);
        if (destUser == null) return "DestUserNotFound";
        string destUserId = destUser.Id.ToString();
        string srcUserName = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
        string time = DateTime.Now.ToShortTimeString();
        await this.Clients.User(destUserId).SendAsync("ReceivePrivateMessage", srcUserName, time, message);
        return "Ok";
    }
}
