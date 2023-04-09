using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.DBModels;

public class Usergroup
{
    public int UserId { get; set; }

    public int GroupId { get; set; }

    public virtual User User { get; set; }

    public virtual Group Group { get; set; }
}
