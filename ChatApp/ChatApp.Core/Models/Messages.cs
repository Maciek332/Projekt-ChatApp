using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Models;
public class Messages
{
    [Key]
    public int MessageID
    {
        get; set;
    }

    public DateTime SentDate
    {
        get; set;
    }

    public int MessageAuthor
    {
        get; set;
    }

    public int MessageDestination
    {
        get; set;
    }

    public string MessageContent
    {
        get; set;
    }
}
