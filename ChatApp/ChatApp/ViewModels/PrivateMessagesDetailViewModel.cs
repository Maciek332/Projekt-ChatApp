using ChatApp.Commands;
using ChatApp.Models;
using ChatApp.Views;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class PrivateMessagesDetailViewModel : BaseViewModel
    {
        public ObservableCollection<PrivateMessage> MessagesList { get; } = new ObservableCollection<PrivateMessage>();
        private string _userName;
        private string _messageContent;
        private string _messagePaleholder;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if(_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string MessagePlaceholder
        {
            get { return _messagePaleholder; }
            set
            {
                if (_messagePaleholder != value)
                {
                    _messagePaleholder = value;
                    OnPropertyChanged(nameof(MessagePlaceholder));
                }
            }
        }

        public string MessageContent
        {
            get { return _messageContent; }
            set
            {
                if (_messageContent != value)
                {
                    _messageContent = value;
                    OnPropertyChanged(nameof(MessageContent));
                    SendMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand<string> SendMessageCommand { get; set; }
        public RelayCommand<string> ReplyMessageCommand { get; set; }
        public PrivateMessagesDetailViewModel(DBModels.User user)
        {
            UserName = user.UserName;
            MessagePlaceholder = $"Napisz do {user.UserName}";
            SendMessageCommand = new RelayCommand<string>(x => CreateMessageAndSend(), x => MessageIsValid);
            ReplyMessageCommand = new RelayCommand<string>(x => ReplyMessageAndSend(), x => true);

        }

        private void CreateMessageAndSend()
        {
            MessagesList.Add(new PrivateMessage(MessageContent, DateTime.Now, HorizontalAlignment.Right));
            MessageContent = string.Empty;
        }

        private void ReplyMessageAndSend()
        {
            MessageContent = "Odpowiadam na twoją wiadomość";
            MessagesList.Add(new PrivateMessage(MessageContent, DateTime.Now, HorizontalAlignment.Left));
            MessageContent = string.Empty;
        }

        public bool MessageIsValid
        {
            get => !string.IsNullOrEmpty(MessageContent);
        }
    }
}
