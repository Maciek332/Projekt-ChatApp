using ChatApp.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.UI.Xaml;
using SignalRSever.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace SignalRSever.Hubs
{
    public class MyHubClient : BaseHubClient, ISendHubSync, IRecieveHubSync
    {
        public event Action<PrivateMessage> RecievedMessageEvent;
        public ObservableCollection<PrivateMessage> MessagesList { get; } = new ObservableCollection<PrivateMessage>();


        public MyHubClient()
        {
            Init();
        }

        public new void Init()
        {
            HubConnectionUrl = "http://localhost:8089/";
            HubProxyName = "Hubsync";
            HubTraceLevel = TraceLevels.None;
            HubTraceWriter = Console.Out;

            base.Init();

            _myHubProxy.On<string, DateTime, HorizontalAlignment>("addMessage", Recieve_AddMessage);
            _myHubProxy.On("heartbeat", Recieve_Heartbeat);
            _myHubProxy.On<PrivateMessage>("sendHelloObject", Recieve_SendHelloObject);

            StartHubInternal();
        }

        public override void StartHub()
        {
            _hubConnection.Dispose();
            Init();
        }

        public void Recieve_AddMessage(string messageContent, DateTime dateTime, HorizontalAlignment horizontalAlignment)
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new PrivateMessage { messageText = messageContent, Message = message });
            HubClientEvents.Log.Informational("Recieved addMessage: " + messageContent + ": " + dateTime);
        }

        public void Recieve_Heartbeat()
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new PrivateMessage { Name = "Heartbeat", Message = "recieved" });
            HubClientEvents.Log.Informational("Recieved heartbeat ");
        }

        public void Recieve_SendHelloObject(PrivateMessage hello)
        {
            if (RecievedMessageEvent != null) RecievedMessageEvent.Invoke(new PrivateMessage { Name = hello.Age.ToString(), Message = hello.Molly });
            HubClientEvents.Log.Informational("Recieved sendHelloObject " + hello.MessageText + ", " + hello.MessageDateTime);
        }

        public void AddMessage(string messageContent, DateTime dateTime, HorizontalAlignment horizontalAlignment)
        {
            _myHubProxy.Invoke("addMessage", messageContent, dateTime, horizontalAlignment).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client Sending addMessage to server");
        }

        public void Heartbeat()
        {
            _myHubProxy.Invoke("Heartbeat").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client heartbeat sent to server");
        }

        public void SendHelloObject(PrivateMessage hello)
        {
            _myHubProxy.Invoke<PrivateMessage>("SendHelloObject", hello).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    HubClientEvents.Log.Error("There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            HubClientEvents.Log.Informational("Client sendHelloObject sent to server");
        }


    }
}
