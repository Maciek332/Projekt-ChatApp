using ChatApp.Core.Models;

namespace ChatApp.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface IDataService
{
    Task<IEnumerable<User>> GetListUsersDataAsync();
    Task<IEnumerable<Messages>> GetListMessagesDataAsync();
    Task<IEnumerable<GroupMessage>> GetListGroupMessagesDataAsync();
}
