using ChatApp.Commands;
using ChatApp.DBModels;
using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class PrivateMessagesDetailViewModel : BaseViewModel
    {
        public ObservableCollection<PrivateMessage> MessagesList { get; set; } = new ObservableCollection<PrivateMessage>();
        private string _userName;
        private string _messageContent;
        private string _messagePaleholder;
        private SignalRChatService _chatServices;
        private readonly ChatServices _chatService1;
        private readonly ChatServices _chatService2;
        private HubConnection _hubConnection1;
        private HubConnection _hubConnection2; 
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

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isConnected;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        public ObservableCollection<PrivateMessagesPageViewModel> Messages { get; }
        public ICommand SendMessageChatCommand { get; }

        public PrivateMessagesDetailViewModel()
        {
            _chatService1 = new ChatServices();
            _chatService2 = new ChatServices();

            // połączenie z serwerem SignalR dla pierwszej instancji
            _hubConnection1 = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub")
                .Build();

            // obsługa odebrania wiadomości dla pierwszej instancji
            _hubConnection1.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // dodanie odebranej wiadomości do listy wiadomości
                MessagesList.Add(new PrivateMessage(message, DateTime.Now, HorizontalAlignment.Left));
            });

            // połączenie z serwerem SignalR dla drugiej instancji
            _hubConnection2 = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub")
                .Build();

            // obsługa odebrania wiadomości dla drugiej instancji
            _hubConnection2.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // dodanie odebranej wiadomości do listy wiadomości
                MessagesList.Add(new PrivateMessage(message, DateTime.Now, HorizontalAlignment.Left));
            });

            // rozpoczęcie połączenia z serwerem SignalR dla obu instancji
            Task.WhenAll(
                _hubConnection1.StartAsync(),
                _hubConnection2.StartAsync());
        }

        public async Task SendMessage1(string user, string message)
        {
            await _hubConnection1.InvokeAsync("SendMessage", user, message);
        }

        // metoda do wysyłania wiadomości dla drugiej instancji
        public async Task SendMessage2(string user, string message)
        {
            await _hubConnection2.InvokeAsync("SendMessage", user, message);
        }

        private void ChatService_MessageReceived(DBModels.Models.Message message)
        {
            MessagesList.Add(new PrivateMessage(message.MessageContent, message.SentDate, HorizontalAlignment.Left));
        }

        public void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {

            using var context = new ChatDbContext();

         

            var message = new ChatApp.DBModels.Models.Message
            {
                MessageContent = MessageContent
                
            };
            _chatServices.SendMessage(message);
        }

        public PrivateMessagesDetailViewModel(SignalRChatService chatService)
        {
            // ...

            //chatService.MessageReceived += ChatService_MessageReceived;
        }

        private void OnMessageReceived(Message message)
        {
            // Przetworzenie odebranej wiadomości i dodanie jej do listy wiadomości.
            // ...

            // Powiadomienie widoku o zmianie listy wiadomości.
            OnPropertyChanged(nameof(MessagesList));
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

            var context = new ChatDbContext();

            var message = new Message
            {
                MessageContent = MessageContent,
                MessageAuthor = 1,
                MessageDestination = 1

            };

            context.Add(message);
            context.SaveChanges();
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
