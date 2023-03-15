namespace ChatApp.Core.Models;

// Model for the SampleDataService. Replace with your own model.
public class SampleUser
{
    public long UserID
    {
        get; set;
    }

    public string Name
    {
        get; set;
    }

    public string Surname
    {
        get; set;
    }

    public string ShortDescription => $"{Name} {Surname}";

    public override string ToString() => $"{Name} {Surname}";
}
