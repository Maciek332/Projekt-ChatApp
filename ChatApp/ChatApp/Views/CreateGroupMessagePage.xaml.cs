// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatApp.DBModels;
using ChatApp.ViewModels;
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
        public CreateGroupMessageViewModel _viewModel { get; set; }
        //public ObservableCollection<DBModels.User> Users { get; set; } = new ObservableCollection<DBModels.User>();
        public CreateGroupMessagePage()
        {
            InitializeComponent();
            //LoadPeople();
            _viewModel = new CreateGroupMessageViewModel();
            DataContext = _viewModel;
        }
        //private void LoadPeople()
        //{
        //    using var context = new ChatDbContext();
        //    var userList = context.Users
        //        .Where(x => x.UserId > 0)
        //        .ToList(); ;
        //    var sortedPeople = from user in userList
        //                       orderby user.UserName
        //                       group user by user.UserName.Substring(0, 1).ToUpper() into groups
        //                       select new { Key = groups.Key, People = groups };

        //    foreach (var group in sortedPeople)
        //    {
        //        foreach (var person in group.People)
        //        {
        //            Users.Add(person);
        //        }
        //    }

        //}

        //private void SaveGroupClick(object sender, RoutedEventArgs e)
        //{
        //    ListView listView = FindName("UsersListListview") as ListView;

        //    var selectedItems = listView.SelectedItems;
        //    List<DBModels.User> usersList = selectedItems.Cast<DBModels.User>().ToList();
        //    string GroupNameTextBlock = ConversationName.Text;
        //    var context = new ChatDbContext();

        //    var group = new Group
        //    {
        //        GroupName = GroupNameTextBlock,
        //        Users = new List<DBModels.User>()
        //    };

        //    foreach (var user in usersList)
        //    {
        //        var userToAdd = context.Users.Find(user.UserId);
        //        group.Users.Add(userToAdd);
        //    }

        //    context.Groups.Add(group);
        //    context.SaveChanges();
        //}
    }
}
