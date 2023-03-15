namespace ChatApp.Core.Models;

// Model for the SampleDataService. Replace with your own model.
public class SampleGroup
{
    public string GroupID
    {
        get; set;
    }

    public ICollection<SampleUser> Users
    {
        get; set;
    }
}
