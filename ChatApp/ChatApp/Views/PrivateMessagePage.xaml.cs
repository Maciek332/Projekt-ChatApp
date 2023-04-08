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
    public sealed partial class PrivateMessagePage : Page
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();


        public PrivateMessagePage()
        {
            this.InitializeComponent();
            LoadPeople();

        }

        private void LoadPeople()
        {
            using var context = new ChatDbContext();
            var userList = context.Users
                .Where(x => x.UserId >0)
                .ToList(); ;
            // Przyk³adowa lista osób
        //    var userList = new List<User>()
        //{
        //    new User() { Id=1, Name = "Adam"},
        //    new User() { Id=2, Name = "Alicja" },
        //    new User() { Id=3, Name = "Bartek" },
        //    new User() { Id=4, Name = "Celina" },
        //    new User() { Id=5, Name = "Dominik" },
        //    new User() { Id=6, Name = "Emilia" },
        //    new User() { Id=7, Name = "Filip" },
        //    new User() { Id=8, Name = "Gabriela" },
        //    new User() { Id=9, Name = "Henryk" },
        //    new User() { Id=10, Name = "Igor" },
        //    new User() { Id=11, Name = "Julia" },
        //    new User() { Id=12, Name = "Karol" },
        //    new User() { Id=13, Name = "Lena" },
        //    new User() { Id=14, Name = "Maciej" },
        //    new User() { Id=15, Name = "Natalia" },
        //    new User() { Id=16, Name = "Oskar" },
        //    new User() { Id=17, Name = "Patrycja" },
        //    new User() { Id=18, Name = "Rafa³" },
        //    new User() { Id=19, Name = "Sylwia" },
        //    new User() { Id=20, Name = "Tomasz" },
        //    new User() { Id=21, Name = "Ula" },
        //    new User() { Id=22, Name = "Wojtek" },
        //    new User() { Id=23, Name = "Zuzanna" }
        //    };
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
        private void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // pobierz wybrany element z listy
            var selectedPerson = e.AddedItems.FirstOrDefault() as User;
            // ustaw zawartoœæ kontrolki Frame na now¹ stronê
            PrivateMessageDetailsFrame.Navigate(typeof(PrivateMessagesDetail), selectedPerson);
        }

    }

}
