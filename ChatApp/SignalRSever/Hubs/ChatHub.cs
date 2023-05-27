using Microsoft.AspNetCore.SignalR;
namespace SignalRSever.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(int author, int destination, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", author, destination, message);
    }
}
