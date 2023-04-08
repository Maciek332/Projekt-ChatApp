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
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupMember> GroupMembers  { get; set;}
    }
    public class GroupMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
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
            var user1 = new GroupMember { Id= 1, Name = "Ania"};
            var user2 = new GroupMember { Id = 1, Name = "Szymek" };
            var user3 = new GroupMember { Id = 1, Name = "Krzysiek" };
            
            var groupList = new List<Group>()
            {
                new Group() { Id = 1, Name = "Anonimowi Alkoholicy", GroupMembers= new List<GroupMember> { user1, user2, user3 } },
                new Group() { Id = 2, Name = "Cebulaki w Akcji", GroupMembers = new List<GroupMember> {user1, user2 } },
                new Group() { Id = 3, Name = "Broku³y", GroupMembers = new List<GroupMember> { user2, user3 } },
            };
            var sortedGroups = from groupConf in groupList
                               orderby groupConf.Name
                               group groupConf by groupConf.Name.Substring(0, 1).ToUpper() into groups
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
