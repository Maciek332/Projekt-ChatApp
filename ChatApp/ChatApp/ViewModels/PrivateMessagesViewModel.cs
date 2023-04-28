using System.Collections.ObjectModel;

using ChatApp.Contracts.ViewModels;
using ChatApp.Core.Contracts.Services;
using ChatApp.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.ViewModels;

public class PrivateMessagesViewModel : ObservableRecipient, INavigationAware
{
    private readonly IDataService _sampleDataService;
    private Users? _selected;

    public Users? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<Users> SampleItems { get; private set; } = new ObservableCollection<Users>();

    public PrivateMessagesViewModel(IDataService sampleDataService)
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
