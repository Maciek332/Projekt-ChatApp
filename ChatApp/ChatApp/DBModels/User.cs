using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.DBModels;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string EMail { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool IsLogedIn { get; set; }

    public DateTime RegisterDate { get; set; }

    public List<Group> Groups { get; set; }
}
