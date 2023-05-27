using ChatApp.Commands;
using ChatApp.DBModels;
using ChatApp.Views;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class CreateGroupMessageViewModel : BaseViewModel
    {
        public ObservableCollection<DBModels.User> Users { get; set; } = new ObservableCollection<DBModels.User>();

        private string _conversationName;
        public string ConversationName
        {
            get { return _conversationName; }
            set
            {
                _conversationName = value;
                OnPropertyChanged(nameof(ConversationName));
                CreateGroupCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<ListView> CreateGroupCommand { get; private set; }
        public CreateGroupMessageViewModel() 
        {
            LoadPeople();
            CreateGroupCommand = new RelayCommand<ListView>(SaveGroup, x=>CantCreate);
            //new RelayCommand<string>(x => LeaveGroup(), x => true);
        }
        public bool CantCreate 
        {
            get => !string.IsNullOrEmpty(ConversationName);
        }
        public void SaveGroup(ListView listView)
        {
            var selectedItems = listView.SelectedItems;
            List<DBModels.User> usersList = selectedItems.Cast<DBModels.User>().ToList();
            string GroupNameTextBlock = ConversationName;
            var context = new ChatDbContext();

            var group = new Group
            {
                GroupName = GroupNameTextBlock,
                CreationDate = DateTime.Now,
                Users = new List<DBModels.User>()
            };

            foreach (var user in usersList)
            {
                var userToAdd = context.Users.Find(user.UserId);
                group.Users.Add(userToAdd);
            }

            context.Groups.Add(group);
            context.SaveChanges();

            ShellPage.ContentFramePublic.Navigate(typeof(GroupMessagePage));
        }

        public void LoadPeople()
        {
            using var context = new ChatDbContext();
            var userList = context.Users
                .Where(x => x.UserId > 0)
                .ToList(); ;
            var sortedPeople = from user in userList
                               orderby user.UserName
                               group user by user.UserName.Substring(0, 1).ToUpper() into groups
                               select new { Key = groups.Key, People = groups };

            foreach (var group in sortedPeople)
            {
                foreach (var person in group.People)
                {
                    Users.Add(person);
                }
            }

        }
    }
}
