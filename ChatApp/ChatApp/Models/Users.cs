using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class Users
{
    public int UserId { get; set; }

    public string EMail { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool IsLogedIn { get; set; }

    public DateTime RegisterDate { get; set; }
}
