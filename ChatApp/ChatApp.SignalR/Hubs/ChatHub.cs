using ChatApp.DBModels.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendObjectUpdate(Message[] messageUpdate)
        {
            await Clients.All.SendAsync("ReceiveObjUpdate", messageUpdate);
        }
    }

    public class Message
    {
        public int MessageId { get; set; }

        public DateTime SentDate { get; set; }

        public int MessageAuthor { get; set; }

        public int MessageDestination { get; set; }

        public string MessageContent { get; set; }
    }
}
