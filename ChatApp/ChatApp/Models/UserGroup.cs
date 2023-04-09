using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models;

public partial class UserGroup
{
    [Key]
    public int UserId { get; set; }
    [Key]
    public int GroupId { get; set; }

    public virtual Group Group { get; set; }

    public virtual User User { get; set; }
}
