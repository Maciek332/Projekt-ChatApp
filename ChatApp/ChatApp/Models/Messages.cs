﻿using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class Messages
{
    public int MessageId { get; set; }

    public DateTime SentDate { get; set; }

    public int MessageAuthor { get; set; }

    public int MessageDestination { get; set; }

    public string MessageContent { get; set; }
}
