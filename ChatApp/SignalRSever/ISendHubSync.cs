using ChatApp.Models;
using Microsoft.UI.Xaml;

namespace SignalRSever
{
    public interface ISendHubSync
    {
        void AddMessage(string messageContent, DateTime dateTime, HorizontalAlignment horizontalAlignment);

        void Heartbeat();

        void SendHelloObject(PrivateMessage hello);

    }
}
