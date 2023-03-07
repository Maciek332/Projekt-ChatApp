using ChatApp.ViewModels;

using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

namespace ChatApp.Views;

public sealed partial class GroupMessagesPage : Page
{
    public GroupMessagesViewModel ViewModel
    {
        get;
    }

    public GroupMessagesPage()
    {
        ViewModel = App.GetService<GroupMessagesViewModel>();
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
