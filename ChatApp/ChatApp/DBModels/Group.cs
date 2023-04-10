using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.DBModels;

public partial class Group
{
    [Key]
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public DateTime CreationDate { get; set; }

    public List<User> Users { get; set; }
}
