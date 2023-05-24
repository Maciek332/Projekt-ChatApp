using SignalRSever.Hubs;
using ChatApp.Models;
using SignalRSever.Logging;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRSever.DataProvider
{
    public class SignalRHubSync : ISignalRHubSync
    {
        private readonly MyHubClient _myHubClient;

        //public event Action<bool> ConnectionEvent;

        public SignalRHubSync()
        {
            _myHubClient = new MyHubClient();
            _myHubClient.ConnectionEvent += _myHubClient_ConnectionEvent;
            _myHubClient.RecievedMessageEvent += _myHubClient_RecievedMessageEvent;
        }

        void _myHubClient_RecievedMessageEvent(PrivateMessage obj)
        {
            if (RecieveMessageEvent != null) RecieveMessageEvent.Invoke(obj);
        }

        void _myHubClient_ConnectionEvent(bool obj)
        {
            if (ConnectionEvent != null) ConnectionEvent.Invoke(obj);
        }

        public event Action<bool> ConnectionEvent;
        public event Action<PrivateMessage> RecieveMessageEvent;


        public void Connect()
        {
            _myHubClient.StartHub();
            HubClientEvents.Log.Informational("Started the Hub");
        }

        public void Disconnect()
        {
            _myHubClient.CloseHub();
            HubClientEvents.Log.Informational("Closed Hub");
        }

        public void AddMessage(PrivateMessage message)
        {
            if (_myHubClient.State == ConnectionState.Connected)
            {
                _myHubClient.AddMessage(message.MessageText, message.MessageDateTime);
            }
            else
            {
                HubClientEvents.Log.Warning("Can't send message, connectionState= " + _myHubClient.State);
            }
        }

        public void Heartbeat()
        {
            _myHubClient.Heartbeat();
        }

        public void SendHelloObject(PrivateMessage message)
        {
            if (_myHubClient.State == ConnectionState.Connected)
            {
                _myHubClient.SendHelloObject(new PrivateMessage { Age = int.Parse(message.Name), Molly = message.Message });
            }
            else
            {
                HubClientEvents.Log.Warning("Can't send message, connectionState= " + _myHubClient.State);
            }
        }
    }
}
