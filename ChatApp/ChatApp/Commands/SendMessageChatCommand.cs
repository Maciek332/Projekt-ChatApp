using ChatApp.Services;
using ChatApp.DBModels.Models;
using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.Commands
{
    public class SendMessageChatCommand : ICommand
    {
        private readonly PrivateMessagesDetailViewModel _viewModel;
        private readonly SignalRChatService _chatService;

        public SendMessageChatCommand(PrivateMessagesDetailViewModel viewModel, SignalRChatService chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await _chatService.SendMessage(new Message()
                {
                    MessageContent = _viewModel.MessageContent,
                });
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Unable to send color message.";
            }
        }
    }
}
