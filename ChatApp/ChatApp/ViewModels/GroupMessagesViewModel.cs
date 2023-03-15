using System.Collections.ObjectModel;

using ChatApp.Contracts.ViewModels;
using ChatApp.Core.Contracts.Services;
using ChatApp.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace ChatApp.ViewModels;

public class GroupMessagesViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private SampleUser? _selected;

    public SampleUser? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<SampleUser> SampleItems { get; private set; } = new ObservableCollection<SampleUser>();

    public GroupMessagesViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        SampleItems.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetListDetailsDataAsync();

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
