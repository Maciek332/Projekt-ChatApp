using ChatApp.Commands;
using ChatApp.Services;
using ChatApp.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class NavigationService : INavigationService
    {
        private Frame _contentFrame;

        public NavigationService(Frame contentFrame)
        {
            _contentFrame = contentFrame;
        }

        public void Navigate(Type sourcePageType)
        {
            _contentFrame.Navigate(sourcePageType);
        }

        public void GoBack()
        {
            if (_contentFrame.CanGoBack)
            {
                _contentFrame.GoBack();
            }
        }
    }

    public class ShellPageViewModel : BaseViewModel
    {

        private NavigationService _navigationService;
        private Frame _contentFrame;
        private bool _privateMessageNavigation;
        private bool _groupMessageNavigation;
        private string _currentLoggedUserField;
        private NavigationViewItem _selectedMenuItem;
        private NavigationView _navigationMenu;
        private string _logoutStatusMessage;
        private bool _logoutStatusOpen;
        public Frame ContentFrame
        {
            get { return _contentFrame; }
            set
            {
                _contentFrame = value;
                OnPropertyChanged(nameof(ContentFrame));
            }
        }
        public bool PrivateMessageNavigation
        {
            get { return _privateMessageNavigation; }
            set
            {
                _privateMessageNavigation = value;
                OnPropertyChanged(nameof(PrivateMessageNavigation));
            }
        }

        public bool GroupMessageNavigation
        {
            get { return _groupMessageNavigation; }
            set
            {
                _groupMessageNavigation = value;
                OnPropertyChanged(nameof(GroupMessageNavigation));
            }
        }

        public NavigationViewItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedMenuItem));
            }
        }
        public string CurrentLoggedUserField
        {
            get { return _currentLoggedUserField; }
            set
            {
                _currentLoggedUserField = value;
                OnPropertyChanged(nameof(CurrentLoggedUserField));
            }
        }
        public NavigationView NavigationMenu
        {
            get { return _navigationMenu; }
            set
            {
                _navigationMenu = value;
                OnPropertyChanged(nameof(NavigationMenu));
            }
        }
        public string LogoutStatusMessage
        { 
            get { return _logoutStatusMessage; }
            set
            {
                _logoutStatusMessage = value;
                OnPropertyChanged(nameof(LogoutStatusMessage));
            }
        }

        public bool LogoutStatusOpen
        {
            get { return _logoutStatusOpen; }
            set
            {
                _logoutStatusOpen = value;
                OnPropertyChanged(nameof(LogoutStatusOpen));
            }
        }

        public RelayCommand<string> LogoutCommand { get; private set; }
        public ShellPageViewModel(Frame contentFrame)
        {
            _navigationService = new NavigationService(contentFrame);
            NavigationMenu = new NavigationView();
            NavigationMenu.ItemInvoked += NavigationMenu_ItemInvoked;
            NavigationMenu.BackRequested += NavigationMenu_BackRequested;
            PrivateMessageNavigation = true;
            GroupMessageNavigation = true;
            ContentFrame = contentFrame;
        }
        public void NavigationMenu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                _navigationService.Navigate(typeof(Views.SettingsPage));
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
                    _navigationService.Navigate(newPage);
                }

            }
        }

        public void NavigationMenu_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        public void OnNavigated(object sender, NavigationEventArgs e)
        {
            NavigationMenu.IsBackEnabled = ContentFrame.CanGoBack;
            
            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                SelectedMenuItem = NavigationMenu.SettingsItem as NavigationViewItem;
            }
            else if (ContentFrame.SourcePageType != null)
            {
                SelectedMenuItem = NavigationMenu.MenuItems
                    .OfType<NavigationViewItem>()
                    .FirstOrDefault(n => n.Tag?.ToString() == ContentFrame.SourcePageType.FullName);
            }

            NavigationMenu.Header = SelectedMenuItem?.Content?.ToString();
        }

        public bool IsLoggedIn()
        {
            return true;
        }
        public void LogoutFunc()
        {
            PrivateMessageNavigation = false;
            GroupMessageNavigation = false;
            LogoutStatusMessage = "Pomyślnie wylogowano";
            LogoutStatusOpen = true;
        }
    }
}
