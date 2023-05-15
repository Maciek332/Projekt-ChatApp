// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatApp.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.UI.Xaml;

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
