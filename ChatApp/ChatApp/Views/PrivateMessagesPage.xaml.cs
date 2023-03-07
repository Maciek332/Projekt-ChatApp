using ChatApp.ViewModels;

using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

namespace ChatApp.Views;

public sealed partial class PrivateMessagesPage : Page
{
    public PrivateMessagesViewModel ViewModel
    {
        get;
    }

    public PrivateMessagesPage()
    {
        ViewModel = App.GetService<PrivateMessagesViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
