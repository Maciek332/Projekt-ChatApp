using ChatApp.DBModels;
using ChatApp.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class PrivateMessagesPageViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        private Frame _privateMessageDetailsFrame;
        public Frame PrivateMessageDetailFrame
        {
            get { return _privateMessageDetailsFrame; }
            set
            {
                _privateMessageDetailsFrame = value;
                OnPropertyChanged(nameof(PrivateMessageDetailFrame));
            }
        }

        public PrivateMessagesPageViewModel(Frame privateMessageDetailsFrame)
        {
            PrivateMessageDetailFrame = privateMessageDetailsFrame;

            LoadPeople();
        }

        public void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = e.AddedItems.FirstOrDefault() as User;
            PrivateMessageDetailFrame.Navigate(typeof(PrivateMessagesDetail), selectedPerson);
        }
        private void LoadPeople()
        {
            using var context = new ChatDbContext();
            var userList = context.Users
                .Where(x => x.UserId != LoginPageViewModel.LoggedUser.UserId)
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
