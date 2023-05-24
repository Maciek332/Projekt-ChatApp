using ChatApp.Models;

namespace PortableSignalR.Base
{
    public interface IContext
    {
        void SendConnectionEvent(MainViewModel mainViewModel, bool connected);
        void RecieveMessageEvent(MainViewModel mainViewModel, PrivateMessage myMessage);
    }
}