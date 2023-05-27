using ChatApp.Commands;
using ChatApp.DBModels;
using ChatApp.Models;
using ChatApp.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatApp.ViewModels
{
    public class GroupMessagesDetailViewModel : BaseViewModel
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
        private string _groupMembesList;
        public string GroupMembesList
        {
            get { return _groupMembesList; }
            set
            {
                if (_groupMembesList != value)
                {
                    _groupMembesList = value;
                    OnPropertyChanged(nameof(GroupMembesList));
                }
            }
        }
        HubConnection connection;
        public RelayCommand<string> SendGroupMessageCommand { get; set; }
        public RelayCommand<string> LeaveGroupCommand { get; set; }
        private Group SelectedGroup;
        private List<User> GroupMembers;
        public GroupMessagesDetailViewModel(DBModels.Group group)
        {
            var context = new ChatDbContext();
            SelectedGroup = group;

            GroupMembers = context.Groups
                 .Where(g => g.GroupId == SelectedGroup.GroupId)
                 .SelectMany(g => g.Users)
                 .ToList();


            GroupMembesList = string.Join("\n", GroupMembers.Select(u => u.UserName));
            GroupName = group.GroupName;
            GroupMessagePlaceholder = $"Napisz na {group.GroupName}";
            SendGroupMessageCommand = new RelayCommand<string>(x => CreateMessageAndSend(), x => MessageIsValid);
            LeaveGroupCommand = new RelayCommand<string>(x => LeaveGroup(), x => true);


            var groupMessagesHistory = context.Groupmessages
                .Where(u => u.MessageAuthor == LoginPageViewModel.LoggedUser.UserId || u.MessageGroup == LoginPageViewModel.LoggedUser.UserId)
                .ToList();
            foreach (Groupmessage groupMessage in groupMessagesHistory)
            {
                if (groupMessage.MessageGroup == SelectedGroup.GroupId)
                {
                    if (groupMessage.MessageAuthor == LoginPageViewModel.LoggedUser.UserId)
                    {
                        GroupMessagesList.Add(new GroupMessage(groupMessage.MessageContent, groupMessage.SentDate, HorizontalAlignment.Right));
                    }
                }
                if (groupMessage.MessageAuthor == SelectedGroup.GroupId)
                {
                    GroupMessagesList.Add(new GroupMessage(groupMessage.MessageContent, groupMessage.SentDate, HorizontalAlignment.Right));
                }
            }

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7026/ChatHub")
                .WithAutomaticReconnect()
                .Build();
            connection.StartAsync();

            connection.On<int, int, string>("ReceiveMessage", (MessageAuthor, MessageGroup, MessageContent) =>
            {
                if (MessageAuthor == SelectedGroup.GroupId && MessageGroup == LoginPageViewModel.LoggedUser.UserId)
                {
                    GroupMessagesDetail.GroupMessageP.DispatcherQueue.TryEnqueue(() =>
                    {
                        GroupMessagesList.Add(new GroupMessage(MessageContent, DateTime.Now, HorizontalAlignment.Left));
                    });
                }
            });
        }

        private async void CreateMessageAndSend()
        {
            try
            {
                await connection.InvokeAsync("SendMessage", LoginPageViewModel.LoggedUser.UserId, SelectedGroup.GroupId, GroupMessageContent);
                GroupMessagesList.Add(new GroupMessage(GroupMessageContent, DateTime.Now, HorizontalAlignment.Right));

                var context = new ChatDbContext();
                var groupMessage = new Groupmessage
                {
                    SentDate = DateTime.Now,
                    MessageAuthor = LoginPageViewModel.LoggedUser.UserId,
                    MessageGroup = SelectedGroup.GroupId,
                    MessageContent = GroupMessageContent,
                };
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

        private async void LeaveGroup()
        {
            var LeaveGroupDialog = new ContentDialog
            {
                Title = "Opuszczanie grupy",
                Content = $"Czy napewno chcesz opuścić grupę: {GroupName}?",
                PrimaryButtonText = "Tak",
                SecondaryButtonText = "Anuluj",
                DefaultButton = ContentDialogButton.Primary
            };

            LeaveGroupDialog.XamlRoot = GroupMessagesDetail.GroupMessageP.Content.XamlRoot;
            var leaveResult = await LeaveGroupDialog.ShowAsync();

            if (leaveResult == ContentDialogResult.Primary)
            {
                var usersList = GroupMembers.Select(u => u.UserId);
                var contex = new ChatDbContext();
                var groupToLeave = contex.Groups
                    .Include(g => g.Users)
                    .FirstOrDefault(g => g.GroupId == SelectedGroup.GroupId);

                if (groupToLeave != null)
                {
                    foreach (var user in usersList)
                    {
                        var userGroup = groupToLeave.Users.FirstOrDefault(ug => ug.UserId == user);
                        if (userGroup.UserId == LoginPageViewModel.LoggedUser.UserId)
                        {
                            groupToLeave.Users.Remove(userGroup);
                            if (groupToLeave.Users.Count == 1)
                            {
                                groupToLeave.Users.Remove(userGroup);

                            }
                        }
                    }
                    if (groupToLeave.Users.Count == 1)
                    {
                        foreach (var user in usersList)
                        {
                            var userGroup = groupToLeave.Users.FirstOrDefault(ug => ug.UserId == user);

                            groupToLeave.Users.Remove(userGroup);
                            contex.Groups.Remove(groupToLeave);

                        }
                    }
                    contex.SaveChanges();
                    ShellPage.ContentFramePublic.Navigate(typeof(GroupMessagePage));
                }
            }
        }
    }
}
