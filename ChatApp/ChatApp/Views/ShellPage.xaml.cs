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
using ChatApp.DBModels;
using ChatApp.ViewModels;
using System.ComponentModel;
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
        public ShellPageViewModel _viewModel { get; set; }
        public Frame COntentFrame;
        public ShellPage()
        {
            InitializeComponent();
            CurrentLoggedUserLabel = CurrentLoggedUser;
            ContentFramePublic = ContentFrame;
            _viewModel = new ShellPageViewModel(ContentFrame);
            _viewModel.SelectedMenuItem = NavigationMenu.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.LoginPage),
                       null
                       //new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                       );
            DataContext = _viewModel;
        }

        public static Frame ContentFramePublic;

        public Grid CustomAppTitleBar
        {
            get { return AppTitleBar; }
        }
    }
}