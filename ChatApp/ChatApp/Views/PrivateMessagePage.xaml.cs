// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
    public class User
    {
        public string Name { get; set; }
    }

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
            // Przyk³adowa lista osób
            var userList = new List<User>()
        {
            new User() { Name = "Adam"},
            new User() { Name = "Alicja" },
            new User() { Name = "Bartek" },
            new User() { Name = "Celina" },
            new User() { Name = "Dominik" },
            new User() { Name = "Emilia" },
            new User() { Name = "Filip" },
            new User() { Name = "Gabriela" },
            new User() { Name = "Henryk" },
            new User() { Name = "Igor" },
            new User() { Name = "Julia" },
            new User() { Name = "Karol" },
            new User() { Name = "Lena" },
            new User() { Name = "Maciej" },
            new User() { Name = "Natalia" },
            new User() { Name = "Oskar" },
            new User() { Name = "Patrycja" },
            new User() { Name = "Rafa³" },
            new User() { Name = "Sylwia" },
            new User() { Name = "Tomasz" },
            new User() { Name = "Ula" },
            new User() { Name = "Wojtek" },
            new User() { Name = "Zuzanna" }
            };
            var sortedPeople = from user in userList
                               orderby user.Name
                               group user by user.Name.Substring(0, 1).ToUpper() into groups
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
            PrivateMessageDetailsFrame.Navigate(typeof(PrivateMessagesDetail), selectedPerson.Name);
        }

    }

}
