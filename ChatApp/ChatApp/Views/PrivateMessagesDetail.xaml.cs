using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using Windows.System;
using ChatApp.Models;
using ChatApp.ViewModels;
using ChatApp.DBModels;
using ChatApp.Services;

namespace ChatApp.Views
{
    public sealed partial class PrivateMessagesDetail : Page
    {
        private readonly SignalRChatService _chatService;
        private PrivateMessagesDetailViewModel _viewModel { get; set; }

        public PrivateMessagesDetail()
        {
            InitializeComponent();
            //_chatService = new SignalRChatService();
            _viewModel = new PrivateMessagesDetailViewModel(_chatService);

            //_chatService.MessageReceived += ChatService_MessageReceived;
            //_chatService.ConnectionError += ChatService_ConnectionError;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is DBModels.User user)
            {
                _viewModel = new PrivateMessagesDetailViewModel(user);
                DataContext = _viewModel;
            }
        }

        private async void ChatService_MessageReceived(Message message)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //_viewModel.Messages.Add(message);
            });
        }


        private async void ChatService_ConnectionError(object sender, Exception exception)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                _viewModel.ErrorMessage = exception.Message;
            });
        }
    }
}
