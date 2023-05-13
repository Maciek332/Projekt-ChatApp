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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivateMessagesDetail : Page
    {
        private PrivateMessagesDetailViewModel _viewModel { get; set; }
        HubConnection hubConnection;
        public PrivateMessagesDetail()
        {
            InitializeComponent();
            
        }

        async private void DoRealTimeSuff()
        {
            SignalRDashSyncSetup();
            await SignalRConnect();
        }

        private void SignalRDashSyncSetup()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"/chatHub").Build();

            hubConnection.On<string>("ReceiveDashUpdate", (dashupdate) =>
            {
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