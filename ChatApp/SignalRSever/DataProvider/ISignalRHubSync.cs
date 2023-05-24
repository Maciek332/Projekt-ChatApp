using ChatApp.Models;

namespace SignalRSever.DataProvider
{
    public interface ISignalRHubSync
    {
        event Action<bool> ConnectionEvent;

        event Action<PrivateMessage> RecieveMessageEvent;

        void Connect();

        void Disconnect();

        void AddMessage(PrivateMessage message);

        void Heartbeat();

        void SendHelloObject(PrivateMessage message);
    }
}
