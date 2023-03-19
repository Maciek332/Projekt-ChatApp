using System.Collections.ObjectModel;

using ChatApp.Contracts.ViewModels;
using ChatApp.Core.Contracts.Services;
using ChatApp.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.ViewModels;

public class PrivateMessagesViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private User? _selected;

    public User? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<User> SampleItems { get; private set; } = new ObservableCollection<User>();

    public PrivateMessagesViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        SampleItems.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetListUsersDataAsync();

        foreach (var item in data)
        {
            SampleItems.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        if (Selected == null)
        {
            Selected = SampleItems.First();
        }
    }
}
