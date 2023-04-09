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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using ChatApp.Views;
using ChatApp.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public static NavigationViewItem CurrentLoggedUserLabel;
        public static NavigationViewItem PrivateMessageNavigationLabel;
        public static NavigationViewItem GroupMessageNavigationLabel;
        public static Frame ContentFramePublic;
        public ShellPage()
        {
            this.InitializeComponent();
            CurrentLoggedUserLabel = CurrentLoggedUser;
            PrivateMessageNavigationLabel = PrivateMessageNavigation;
            GroupMessageNavigationLabel = GroupMessageNavigation;
            NavigationMenu.SelectedItem = NavigationMenu.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.LoginPage),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );
        }
        public Frame MainContentFrame
        {
            get { return ContentFrame; }
        }
        public Grid CustomAppTitleBar
        {
            get { return AppTitleBar; }
        }
        public NavigationViewItem PrivMessagePublic
        {
            get { return PrivateMessageNavigation; }
        }
        public NavigationViewItem GroupMessagePublic
        {
            get { return (NavigationViewItem)GroupMessageNavigation.Content; }
            set { GroupMessageNavigation.Content = value; }
        }
        public NavigationViewItem CurrentLoggedUserField
        {
            get { return CurrentLoggedUser; }
            set { CurrentLoggedUser.Content = value; }
        }
        private void NavigationMenu_ItemInvoked(NavigationView sender,
                      NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Views.SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                if (args.InvokedItemContainer.Tag.ToString() == "LogOut")
                {
                    LogoutFunc();
                }
                else
                {
                    Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                    ContentFrame.Navigate(
                           newPage,
                           null,
                           args.RecommendedNavigationTransitionInfo
                           );
                }
                
            }

        }
        private void LogoutFunc()
        {
            using var context = new ChatDbContext();
            
            var CurrentLogedIn = context.Users
                        .FirstOrDefault(x => x.IsLogedIn == true);
            var updateLoginStaus = context.Users
                .FirstOrDefault(x => x.IsLogedIn == CurrentLogedIn.IsLogedIn);
            updateLoginStaus.IsLogedIn = false;
            context.SaveChanges();
            LoginStatusMessage.Message = "Pomyœlnie wylogowano";
            LoginStatusMessage.IsOpen = true;
            PrivateMessageNavigation.IsEnabled = false;
            GroupMessageNavigation.IsEnabled = false;
            ContentFrame.Navigate(typeof(Views.LoginPage));
            CurrentLoggedUser.Content = "Nie jesteœ zalogowany";
        }
        private void NavigationMenu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationMenu.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavigationMenu.SelectedItem = (NavigationViewItem)NavigationMenu.SettingsItem;
            }
            else if (ContentFrame.SourcePageType != null)
            {
                NavigationMenu.SelectedItem = NavigationMenu.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
            }

            NavigationMenu.Header = ((NavigationViewItem)NavigationMenu.SelectedItem)?.Content?.ToString();

        }


    }
}
