using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using Windows.System;
using ChatApp.Models;
using ChatApp.ViewModels;
using ChatApp.DBModels;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivateMessagesDetail : Page
    {
        public PrivateMessagesDetailViewModel _viewModel { get; set; }
        HubConnection hubConnection;
        public PrivateMessagesDetail()
        {
            InitializeComponent();
            
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/ChatHub")
                .Build();
            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };
        }

        async private void DoRealTimeSuff()
        {
            SignalRDashSyncSetup();
            await SignalRConnect();
        }

        private void SignalRDashSyncSetup()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"/chatHub").Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newmessage = $"{user}: {message}";
                    _viewModel.MessagesList.Add(newmessage);
                });
                //var receivedDashUpdate = dashupdate;
                //((ArrowGaugeIndicator)this.RadialGauge.Indicators[0]).Value = Int32.Parse(dashupdate);
                //((LinearBarGaugeIndicator)this.LinearGauge.Indicators[0]).Value = Int32.Parse(dashupdate);
            });

            hubConnection.On<ObservableCollection<PrivateMessage>>("ReceiveObjUpdate", (gridUpdate) =>
            {
                var receivedChartUpdate = gridUpdate;
                _viewModel.MessagesList = gridUpdate;
                //this.DataGrid.ItemsSource = gridUpdate;
            });

        }

        private async Task SignalRConnect()
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                // Connection failed.
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is DBModels.User user)
            {
                _viewModel = new PrivateMessagesDetailViewModel(user);
                DataContext = _viewModel;
            }
        }
    }
}