// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Fleck;
using ChatApp.Views;
using WebSocketSharp;
using ChatApp.Services;
using Microsoft.UI;
using Microsoft.AspNetCore.SignalR.Client;
using ChatApp.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5281/messagehub")
                .Build();

            //var server = new WebSocketServer("ws://localhost:8080");

            //server.Start(socket =>
            //{
            //    socket.OnOpen = () =>
            //    {
            //        Console.WriteLine("Połączenie nawiązane.");
            //    };

            //    socket.OnClose = () =>
            //    {
            //        Console.WriteLine("Połączenie zamknięte.");
            //    };

            //    socket.OnMessage = message =>
            //    {
            //        Console.WriteLine("Otrzymano wiadomość: " + message);
            //        // Tu przesyłaj wiadomość do drugiej aplikacji
            //    };
            //});

            //Console.ReadLine();

            //PrivateMessagesDetailViewModel privateMessagesDetailViewModel = PrivateMessagesDetailViewModel.CreatedConnectedViewModel(new ISignalRChatService(connection));

            Window window = new MainWindow();
            window.Title = "Koci kącik rozmów :3";
            ShellPage shellPage = new ShellPage();

            // Ustaw zawartość ramki na ShellPage
            window.Content = shellPage;
            //shellPage.MainContentFrame.Navigate(typeof(LoginPage));
            // Wyświetl okno
            window.ExtendsContentIntoTitleBar = true;
            window.SetTitleBar(shellPage.CustomAppTitleBar);
            window.Activate();
        }
    }
}
