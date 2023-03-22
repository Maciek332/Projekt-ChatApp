namespace ChatApp.Core.Models;

// Model for the SampleDataService. Replace with your own model.
public class GroupMessage
{
    public int GroupMessageID
    {
        get; set;
    }

    public DateTime SentDate
    {
        get; set;
    }

    public int MessageAuthor
    {
        get; set;
    }

    public int MessageGroup
    {
        get; set;
    }

    public string MessageContent
    {
        get; set;
    }
}
