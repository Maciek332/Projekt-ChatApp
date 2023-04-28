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
    private List<Users> _allUsers;
    //private List<Messages> _allMessages;
    //private List<GroupMessages> _allGroupMessages;

    public SampleDataService()
    {
    }

    private static IEnumerable<Users> AllUsers()
    {
        // The following is order summary data
        var users = AddUsers();
        return users.ToList();
    }

    //private static IEnumerable<Messages> AllMessages()
    //{
    //    // The following is order summary data
    //    return (IEnumerable<Messages>)messages.SelectMany(c => c.MessageContent);
    //}

    //private static IEnumerable<GroupMessages> AllGroupMessages()
    //{
    //    // The following is order summary data
    //    var companies = AddGroupMessages();
    //    return (IEnumerable<GroupMessages>)companies.SelectMany(c => c.MessageContent);
    //}

    private static IEnumerable<Users> AddUsers()
    {
        return new List<Users>()
        {
            new Users()
            {
                UserId = 1,
                EMail = "bleble@gmail.com",
                UserName = "Ola",
                Password = "1234",
                IsLogedIn = true,
                RegisterDate = new DateTime(2023, 3, 1),
            },
            new Users()
            {
                UserId = 2,
                EMail = "aleale@gmail.com",
                UserName = "Adam",
                Password = "qwer",
                IsLogedIn= false,
                RegisterDate = new DateTime(2023, 3, 5),
            },
            new Users()
            {
                UserId = 3,
                EMail = "qwer@gmail.com",
                UserName = "Jan",
                Password = "asdf",
                IsLogedIn= false,
                RegisterDate = new DateTime(2023, 3, 4),
            },
            new Users()
            {
                UserId = 4,
                EMail = "piupiu@gmail.com",
                UserName = "Leon",
                Password = "zxcv",
                IsLogedIn= false,
                RegisterDate = new DateTime(2023, 3, 8),
            },
        };
    }

    //private static IEnumerable<GroupMessages> AddGroupMessages()
    //{
    //    return new List<GroupMessages>()
    //    {
    //        new GroupMessages()
    //        {
    //            SentDate = new DateTime(2023, 3, 11, 10, 0, 0),
    //            MessageAuthor = 1,
    //            MessageGroup = 1,
    //            MessageContent = "Hej",
    //        },
    //        new GroupMessages()
    //        {
    //            SentDate = new DateTime(2023, 3, 11, 10, 10, 0),
    //            MessageAuthor = 2,
    //            MessageGroup = 1,
    //            MessageContent = "Hej2",
    //        },
    //        new GroupMessages()
    //        {
    //            SentDate = new DateTime(2023, 3, 11, 10, 10, 5),
    //            MessageAuthor = 3,
    //            MessageGroup = 2,
    //            MessageContent = "Hej3",
    //        },
    //        new GroupMessages()
    //        {
    //            SentDate = new DateTime(2023, 3, 11, 10, 20, 5),
    //            MessageAuthor = 2,
    //            MessageGroup = 3,
    //            MessageContent = "Hej4",
    //        },
    //    };
    //}

    public async Task<IEnumerable<Users>> GetListUsersDataAsync()
    {
        if (_allUsers == null)
        {
            _allUsers = new List<Users>(AllUsers());
        }

        await Task.CompletedTask;
        return _allUsers;
    }

    //public async Task<IEnumerable<Messages>> GetListMessagesDataAsync()
    //{
    //    if (_allMessages == null)
    //    {
    //        _allMessages = new List<Messages>(AllMessages());
    //    }

    //    await Task.CompletedTask;
    //    return _allMessages;
    //}

    //public async Task<IEnumerable<GroupMessages>> GetListGroupMessagesDataAsync()
    //{
    //    if (_allGroupMessages == null)
    //    {
    //        _allGroupMessages = new List<GroupMessages>(AllGroupMessages());
    //    }

    //    await Task.CompletedTask;
    //    return _allGroupMessages;
    //}
}
