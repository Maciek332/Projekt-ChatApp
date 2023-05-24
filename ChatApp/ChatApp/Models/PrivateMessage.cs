using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class PrivateMessage
    {
        public string MessageText { get; }
        public DateTime MessageDateTime { get; }
        public HorizontalAlignment MessageAligment { get; }

        public PrivateMessage(string messageText, DateTime messageDateTime, HorizontalAlignment messageAligment)
        {
            MessageText = messageText;
            MessageDateTime = messageDateTime;
            MessageAligment = messageAligment;
        }

        public override string ToString()
        {
            return MessageDateTime.ToString() + " " + MessageText;
        }
    }
}
