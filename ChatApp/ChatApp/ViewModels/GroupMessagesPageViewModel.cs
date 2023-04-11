using ChatApp.Commands;
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
    public class GroupMessagesPageViewModel : BaseViewModel
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        Frame _groupMessageDetailsFrame;
        public Frame GroupMessageDetailsFrame
        {
            get { return _groupMessageDetailsFrame; }
            set
            {
                _groupMessageDetailsFrame = value;
                OnPropertyChanged(nameof(GroupMessageDetailsFrame));
            }
        }
        public GroupMessagesPageViewModel(Frame contentFrame)
        {
            GroupMessageDetailsFrame = contentFrame;
            CreateNewGroupCommand = new RelayCommand<string>(x => NavigateToCreationPage(), x => true);
            LoadGroups();
        }
        public RelayCommand<string> CreateNewGroupCommand { get; private set; }
        public void GroupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedGroup = e.AddedItems.FirstOrDefault() as Group;
            

            GroupMessageDetailsFrame.Navigate(typeof(GroupMessagesDetail), selectedGroup);
        }

        private void NavigateToCreationPage()
        {
            GroupMessageDetailsFrame.Navigate(typeof(CreateGroupMessagePage));
        }

        private void LoadGroups()
        {
            using var context = new ChatDbContext();
            var groupList = context.Groups
                .Where(x => x.GroupId > 0)
                .ToList();
            var sortedGroups = from groupConf in groupList
                               orderby groupConf.GroupName
                               group groupConf by groupConf.GroupName.Substring(0, 1).ToUpper() into groups
                               select new { Key = groups.Key, People = groups };

            foreach (var group in sortedGroups)
            {
                foreach (var groupConf in group.People)
                {
                    Groups.Add(groupConf);
                }
            }

        }
    }
}
