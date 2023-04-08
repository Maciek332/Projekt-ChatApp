// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatApp.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupMessagePage : Page
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public GroupMessagePage()
        {
            this.InitializeComponent();
            LoadGroups();
        }
        private void LoadGroups()
        {
            var user1 = new User { UserId = 1, UserName = "Ania", EMail = "blabla@gmail.com", Password = "1234", IsLogedIn = false };
            var user2 = new User { UserId = 1, UserName = "Szymek", EMail = "blabla@gmail.com", Password = "1234", IsLogedIn = false };
            var user3 = new User { UserId = 1, UserName = "Krzysiek", EMail = "blabla@gmail.com", Password = "1234", IsLogedIn = false };
            
            var groupList = new List<Group>()
            {
                new Group() { GroupId = 1, GroupName = "Anonimowi Alkoholicy", CreationDate = new DateTime(2020,10,5)},
                new Group() { GroupId = 2, GroupName = "Cebulaki w Akcji", CreationDate = new DateTime(2020,10,5) },
                new Group() { GroupId = 3, GroupName = "Broku³y", CreationDate = new DateTime(2020,10,5) },
            };
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
        private void GroupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // pobierz wybrany element z listy
            var selectedGroup = e.AddedItems.FirstOrDefault() as Group;
            // ustaw zawartoœæ kontrolki Frame na now¹ stronê
            GroupMessagesDetailsFrame.Navigate(typeof(GroupMessagesDetail), selectedGroup);
        }
    }
}
