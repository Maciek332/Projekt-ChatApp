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
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.UI.Dispatching;
using ChatApp.DBModels;

namespace ChatApp.ViewModels
{
    public class PrivateMessagesDetailViewModel : BaseViewModel
    {
        public ObservableCollection<PrivateMessage> MessagesList { get; } = new ObservableCollection<PrivateMessage>();
        private string _userName;
        private string _messageContent;
        private string _messagePlaceholder;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string MessagePlaceholder
        {
            get { return _messagePlaceholder; }
            set
            {
                if (_messagePlaceholder != value)
                {
                    _messagePlaceholder = value;
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
        HubConnection connection;
        public RelayCommand<string> SendMessageCommand { get; set; }
        private User SelectedUser;

        public PrivateMessagesDetailViewModel(DBModels.User user)
        {
            SelectedUser= user;
            UserName = user.UserName;
            MessagePlaceholder = $"Napisz do {user.UserName}";
            SendMessageCommand = new RelayCommand<string>(x => CreateMessageAndSend(), x => MessageIsValid);

            var context = new ChatDbContext();
            var messagesHistory = context.Messages
                .Where(u => u.MessageAuthor == LoginPageViewModel.LoggedUser.UserId || u.MessageDestination == LoginPageViewModel.LoggedUser.UserId)
                .ToList();
            foreach (Message message in messagesHistory)
            {
                if (message.MessageDestination == SelectedUser.UserId)
                {
                    if (message.MessageAuthor == LoginPageViewModel.LoggedUser.UserId)
                    {
                        MessagesList.Add(new PrivateMessage(message.MessageContent, message.SentDate, HorizontalAlignment.Right));
                    }
                }
                if (message.MessageAuthor == SelectedUser.UserId)
                {
                    MessagesList.Add(new PrivateMessage(message.MessageContent, message.SentDate, HorizontalAlignment.Left));
                }
            }

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7026/ChatHub")
                .WithAutomaticReconnect()
                .Build();
            connection.StartAsync();

            connection.On<int,int,string>("ReceiveMessage", (MessageAuthor, MessageDestination, MessageContent) =>
            {
                if (MessageAuthor == SelectedUser.UserId && MessageDestination == LoginPageViewModel.LoggedUser.UserId)
                {
                    PrivateMessagesDetail.PrivateMessageP.DispatcherQueue.TryEnqueue(() =>
                    {
                        MessagesList.Add(new PrivateMessage(MessageContent, DateTime.Now, HorizontalAlignment.Left));
                    });
                }
            });
        }

        private async void CreateMessageAndSend()
        {
            try
            {
                await connection.InvokeAsync("SendMessage", LoginPageViewModel.LoggedUser.UserId, SelectedUser.UserId, MessageContent);
                MessagesList.Add(new PrivateMessage(MessageContent, DateTime.Now, HorizontalAlignment.Right));

                var context = new ChatDbContext();
                var privateMessage = new Message
                {
                    SentDate = DateTime.Now,
                    MessageAuthor = LoginPageViewModel.LoggedUser.UserId,
                    MessageDestination = SelectedUser.UserId,
                    MessageContent = MessageContent,
                };
                context.Add(privateMessage);
                context.SaveChanges();

                MessageContent = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool MessageIsValid
        {
            get => !string.IsNullOrEmpty(MessageContent);
        }
    }
}
