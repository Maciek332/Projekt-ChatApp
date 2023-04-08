using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class GroupMessage
{
    public int GroupMessageId { get; set; }

    public string GroupName { get; set; }

    public DateTime SentDate { get; set; }

    public int MessageAuthor { get; set; }

    public int MessageGroup { get; set; }

    public string MessageContent { get; set; }
}
