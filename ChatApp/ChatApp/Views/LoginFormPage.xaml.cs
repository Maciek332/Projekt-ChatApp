using ChatApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ChatApp.Views;

public sealed partial class LoginFormPage : Page
{
    public LoginFormViewModel ViewModel
    {
        get;
    }

    public LoginFormPage()
    {
        ViewModel = App.GetService<LoginFormViewModel>();
        InitializeComponent();
    }
}
