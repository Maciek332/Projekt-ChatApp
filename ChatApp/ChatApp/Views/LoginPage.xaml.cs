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
using ChatApp.Views;
using System.Security.Cryptography.X509Certificates;
using Windows.Storage;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Windows.UI.Core;
using ChatApp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using ChatApp.Models;

namespace ChatApp.Views
{

    public partial class LoginPage : Page
    {
        public LoginPageViewModel _viewModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            var model = new LoginPageModel();
            _viewModel = new LoginPageViewModel(model);
            DataContext = _viewModel;
        }
    }
}
