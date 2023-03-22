using ChatApp.Core.Contracts.Services;
using ChatApp.Core.Models;

namespace ChatApp.Core.Services;

// This class holds sample data used by some generated pages to show how they can be used.
// TODO: The following classes have been created to display sample data. Delete these files once your app is using real data.
// 1. Contracts/Services/ISampleDataService.cs
// 2. Services/SampleDataService.cs
// 3. Models/SampleCompany.cs
// 4. Models/SampleOrder.cs
// 5. Models/SampleOrderDetail.cs
public class SampleDataService : IDataService
{
    private List<User> _allUsers;
    private List<Messages> _allMessages;
    private List<GroupMessage> _allGroupMessages;

    public SampleDataService()
    {
    }

    private static IEnumerable<User> AllUsers()
    {
        // The following is order summary data
        var users = AddUsers();
        return users.ToList();
    }

    private static IEnumerable<Messages> AllMessages()
    {
        // The following is order summary data
        var messages = AddMessages();
        return (IEnumerable<Messages>)messages.SelectMany(c => c.MessageContent);
    }

    private static IEnumerable<GroupMessage> AllGroupMessages()
    {
        // The following is order summary data
        var companies = AddGroupMessages();
        return (IEnumerable<GroupMessage>)companies.SelectMany(c => c.MessageContent);
    }

    private static IEnumerable<User> AddUsers()
    {
        return new List<User>()
        {
            new User()
            {
                UserID = 1,
                E_Mail = "bleble@gmail.com",
                UserName = "Ola",
                Password = "1234",
                RegisterDate = new DateTime(2023, 3, 1),
            },
            new User()
            {
                UserID = 2,
                E_Mail = "aleale@gmail.com",
                UserName = "Adam",
                Password = "qwer",
                RegisterDate = new DateTime(2023, 3, 5),
            },
            new User()
            {
                UserID = 3,
                E_Mail = "qwer@gmail.com",
                UserName = "Jan",
                Password = "asdf",
                RegisterDate = new DateTime(2023, 3, 4),
            },
            new User()
            {
                UserID = 4,
                E_Mail = "piupiu@gmail.com",
                UserName = "Leon",
                Password = "zxcv",
                RegisterDate = new DateTime(2023, 3, 8),
            },
        };
    }

    private static IEnumerable<Messages> AddMessages()
    {
        return new List<Messages>()
        {
            new Messages()
            {
                MessageID = 1,
                SentDate = new DateTime(2023, 3, 10, 20, 0, 0),
                MessageAuthor = 1,
                MessageDestination = 2,
                MessageContent = "Hej",
            },
            new Messages()
            {
                MessageID = 2,
                SentDate = new DateTime(2023, 3, 10, 20, 0, 5),
                MessageAuthor = 2,
                MessageDestination = 1,
                MessageContent = "Hej2",
            },
            new Messages()
            {
                MessageID = 3,
                SentDate = new DateTime(2023, 3, 10, 20, 5, 0),
                MessageAuthor = 1,
                MessageDestination = 2,
                MessageContent = "Hej3",
            },
            new Messages()
            {
                MessageID = 4,
                SentDate = new DateTime(2023, 3, 10, 20, 10, 5),
                MessageAuthor = 2,
                MessageDestination = 1,
                MessageContent = "Hej4",
            },
        };
    }

    private static IEnumerable<GroupMessage> AddGroupMessages()
    {
        return new List<GroupMessage>()
        {
            new GroupMessage()
            {
                GroupMessageID = 1,
                SentDate = new DateTime(2023, 3, 11, 10, 0, 0),
                MessageAuthor = 1,
                MessageGroup = 1,
                MessageContent = "Hej",
            },
            new GroupMessage()
            {
                GroupMessageID = 2,
                SentDate = new DateTime(2023, 3, 11, 10, 10, 0),
                MessageAuthor = 2,
                MessageGroup = 1,
                MessageContent = "Hej2",
            },
            new GroupMessage()
            {
                GroupMessageID = 3,
                SentDate = new DateTime(2023, 3, 11, 10, 10, 5),
                MessageAuthor = 3,
                MessageGroup = 2,
                MessageContent = "Hej3",
            },
            new GroupMessage()
            {
                GroupMessageID = 4,
                SentDate = new DateTime(2023, 3, 11, 10, 20, 5),
                MessageAuthor = 2,
                MessageGroup = 3,
                MessageContent = "Hej4",
            },
        };
    }

    public async Task<IEnumerable<User>> GetListUsersDataAsync()
    {
        if (_allUsers == null)
        {
            _allUsers = new List<User>(AllUsers());
        }

        await Task.CompletedTask;
        return _allUsers;
    }

    public async Task<IEnumerable<Messages>> GetListMessagesDataAsync()
    {
        if (_allMessages == null)
        {
            _allMessages = new List<Messages>(AllMessages());
        }

        await Task.CompletedTask;
        return _allMessages;
    }

    public async Task<IEnumerable<GroupMessage>> GetListGroupMessagesDataAsync()
    {
        if (_allGroupMessages == null)
        {
            _allGroupMessages = new List<GroupMessage>(AllGroupMessages());
        }

        await Task.CompletedTask;
        return _allGroupMessages;
    }
}
