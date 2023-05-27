using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class GroupMessage
    {
        public string MessageText { get; }
        public DateTime MessageDateTime { get; }
        public string MessageAuthor { get; }
        public HorizontalAlignment MessageAligment { get; }

        public GroupMessage(string messageAuthor, string messageText, DateTime messageDateTime, HorizontalAlignment messageAligment)
        {
            MessageAuthor = $"~{messageAuthor}";
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
