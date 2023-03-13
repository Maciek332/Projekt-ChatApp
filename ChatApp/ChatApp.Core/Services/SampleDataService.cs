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
public class SampleDataService : ISampleDataService
{
    private List<SampleUser> _allUsers;

    public SampleDataService()
    {
    }

    private static IEnumerable<SampleUser> AllUsers()
    {
        // The following is order summary data
        var companies = AllGroups();
        return companies.SelectMany(c => c.Users);
    }

    private static IEnumerable<SampleGroup> AllGroups()
    {
        return new List<SampleGroup>()
        {
            new SampleGroup()
            {
                GroupID = "GRUPA_A",
                Users = new List<SampleUser>()
                {
                    new SampleUser()
                    {
                        UserID = 000001, // Symbol Globe
                        Name = "Adam",
                        Surname = "Kowalski"
                    },
                    new SampleUser()
                    {
                        UserID = 000002, // Symbol Globe
                        Name = "Jan",
                        Surname = "Nowak"
                    },
                    new SampleUser()
                    {
                        UserID = 000003, // Symbol Globe
                        Name = "Leon",
                        Surname = "Mazur"
                    },
                    new SampleUser()
                    {
                        UserID = 000004, // Symbol Globe
                        Name = "Ola",
                        Surname = "Dudek"
                    },
                }
            },
            new SampleGroup()
            {
                GroupID = "GRUPA_B",
                Users = new List<SampleUser>()
                {
                    new SampleUser()
                    {
                        UserID = 000002, // Symbol Globe
                        Name = "Jan",
                        Surname = "Nowak"
                    },
                    new SampleUser()
                    {
                        UserID = 000003, // Symbol Globe
                        Name = "Leon",
                        Surname = "Mazur"
                    },
                    new SampleUser()
                    {
                        UserID = 000004, // Symbol Globe
                        Name = "Ola",
                        Surname = "Dudek"
                    },
                }
            },
            new SampleGroup()
            {
                GroupID = "GRUPA_C",
                Users = new List<SampleUser>()
                {
                    new SampleUser()
                    {
                        UserID = 000001, // Symbol Globe
                        Name = "Adam",
                        Surname = "Kowalski"
                    },
                    new SampleUser()
                    {
                        UserID = 000002, // Symbol Globe
                        Name = "Jan",
                        Surname = "Nowak"
                    },
                    new SampleUser()
                    {
                        UserID = 000004, // Symbol Globe
                        Name = "Ola",
                        Surname = "Dudek"
                    },
                }
            }
        };
    }

    public async Task<IEnumerable<SampleUser>> GetListDetailsDataAsync()
    {
        if (_allUsers == null)
        {
            _allUsers = new List<SampleUser>(AllUsers());
        }

        await Task.CompletedTask;
        return _allUsers;
    }
}
