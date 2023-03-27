using System;
using System.Collections.Generic;

namespace ChatApp.Core.Models;

public partial class GroupMessages
{
    public int GroupMessageId { get; set; }

    public DateTime SentDate { get; set; }

    public int MessageAuthor { get; set; }

    public int MessageGroup { get; set; }

    public string MessageContent { get; set; }
}
