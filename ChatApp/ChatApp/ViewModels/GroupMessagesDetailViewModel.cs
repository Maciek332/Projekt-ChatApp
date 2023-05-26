﻿using ChatApp.Commands;
using ChatApp.Models;
using ChatApp.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatApp.ViewModels
{
    public class GroupMessagesDetailViewModel: BaseViewModel
    {
        public ObservableCollection<GroupMessage> GroupMessagesList { get; } = new ObservableCollection<GroupMessage>();
        private string _GroupName;
        private string _GroupMessageContent;
        private string _GroupMessagePlaceholder;
        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                if (_GroupName != value)
                {
                    _GroupName = value;
                    OnPropertyChanged(nameof(GroupName));
                }
            }
        }

        public string GroupMessagePlaceholder
        {
            get { return _GroupMessagePlaceholder; }
            set
            {
                if (_GroupMessagePlaceholder != value)
                {
                    _GroupMessagePlaceholder = value;
                    OnPropertyChanged(nameof(GroupMessagePlaceholder));
                }
            }
        }

        public string GroupMessageContent
        {
            get { return _GroupMessageContent; }
            set
            {
                if (_GroupMessageContent != value)
                {
                    _GroupMessageContent = value;
                    OnPropertyChanged(nameof(GroupMessageContent));
                    SendGroupMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }
        HubConnection connection;
        public RelayCommand<string> SendGroupMessageCommand { get; set; }
        public GroupMessagesDetailViewModel(DBModels.Group group)
        {
            GroupName = group.GroupName;
            GroupMessagePlaceholder = $"Napisz na {group.GroupName}";
            SendGroupMessageCommand = new RelayCommand<string>(x => CreateMessageAndSend(), x => MessageIsValid);

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7026/ChatHub")
                .WithAutomaticReconnect()
                .Build();
            connection.StartAsync();

            connection.On<string, string>("ReceiveMessage", (GroupName, MessageContent) =>
            {
                GroupMessagesDetail.GroupMessageP.DispatcherQueue.TryEnqueue(() =>
                {
                    GroupMessagesList.Add(new GroupMessage(MessageContent, DateTime.Now, HorizontalAlignment.Left));
                });
            });
        }

        private async void CreateMessageAndSend()
        {
            try
            {
                await connection.InvokeAsync("SendMessage", GroupName, GroupMessageContent);
                GroupMessagesList.Add(new GroupMessage(GroupMessageContent, DateTime.Now, HorizontalAlignment.Right));
                GroupMessageContent = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool MessageIsValid
        {
            get => !string.IsNullOrEmpty(GroupMessageContent);
        }
    }
}