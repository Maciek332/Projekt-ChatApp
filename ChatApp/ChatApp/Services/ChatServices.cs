using ChatApp.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class ChatServices
    {
        public event EventHandler<Message> MessageSent;

        public void SendMessage(Message message)
        {
            MessageSent?.Invoke(this, message);
        }
    }
}
