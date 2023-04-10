// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatApp.DBModels;
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
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateGroupMessagePage : Page
    {
        public ObservableCollection<DBModels.User> Users { get; set; } = new ObservableCollection<DBModels.User>();
        public CreateGroupMessagePage()
        {
            this.InitializeComponent();
            LoadPeople();
        }
        private void LoadPeople()
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

        private void SaveGroupClick(object sender, RoutedEventArgs e)
        {
            string GroupNameTextBlock = ConversationName.Text;
            var context = new ChatDbContext();
            var group = new Group
            {
                GroupName = GroupNameTextBlock
            };
            context.Groups.Add(group);
            context.SaveChanges();

            var groupName = context.Groups
                .Where(x=> x.GroupName == GroupNameTextBlock)
                .FirstOrDefault();

            ListView listView = FindName("UsersListListview") as ListView;
            List<DBModels.User> usersToAdd = new List<DBModels.User>();
            //var selectedItems = listView.SelectedItems;
            //foreach (var item in selectedItems)
            //{
            //    var selectedUser = item as Models.User;
            //    var userGroupAdd = new UserGroup
            //    {
            //        UserId = selectedUser.UserId,
            //        GroupId = groupName.GroupId
            //    };
            //    context.Set<UserGroup>().Add(userGroupAdd);
            //}
            //var selectedUsers = listView.SelectedItems.Cast<DBModels.User>();
            //foreach (var item in selectedUsers)
            //{
            //    var userGroupAdd = new Usergroup
            //    {
            //        UserId = item.UserId,
            //        GroupId = groupName.GroupId
            //    };
            //    context.Set<Usergroup>().Add(userGroupAdd);
            //}

            context.SaveChanges();
        }
    }
}
