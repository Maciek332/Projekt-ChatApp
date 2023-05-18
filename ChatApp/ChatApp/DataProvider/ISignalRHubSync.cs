using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DataProvider
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
