using ChatApp.Models;
using Microsoft.UI.Xaml;

namespace SignalRSever
{
    public interface IRecieveHubSync
    {
        void Recieve_AddMessage(string messageContent, DateTime dateTime, HorizontalAlignment horizontalAlignment);

        void Recieve_Heartbeat();

        void Recieve_SendHelloObject(PrivateMessage hello);

    }
}
