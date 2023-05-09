using ChatApp.DBModels.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<Message> MessageReceived;
        //public event EventHandler<Exception> ConnectionError;

        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<Message>("ReceiveColorMessage", (message) => MessageReceived?.Invoke(message));

            //_connection = new HubConnectionBuilder()
            //        .WithUrl("https://localhost:5001/hub")
            //        .Build();

            //_connection.On<Message>("ReceiveMessage", message =>
            //{
            //    MessageReceived?.DynamicInvoke(this, message);
            //});

            //_connection.Closed += async (error) =>
            //{
            //    ConnectionError?.Invoke(this, error.InnerException);
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await _connection.StartAsync();
            //};
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        //public async Task Disconnect()
        //{
        //    await _connection.StopAsync();
        //}

        public async Task SendMessage(Message message)
        {
            await _connection.SendAsync("SendMessage", message);
        }
    }
}
