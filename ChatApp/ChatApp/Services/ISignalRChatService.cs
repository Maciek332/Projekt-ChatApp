using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatApp.DBModels.Models;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public interface ISignalRChatService
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task SendMessageAsync(Message message);

        event EventHandler<Message> MessageReceived;
        event EventHandler<Exception> ConnectionError;
    }
}
