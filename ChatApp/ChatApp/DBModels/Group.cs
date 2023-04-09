using System;
using System.Collections.Generic;

namespace ChatApp.DBModels;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public DateTime CreationDate { get; set; }
}
