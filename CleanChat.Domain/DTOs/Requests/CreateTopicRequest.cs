﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Requests
{
    public class CreateTopicRequest
    {
        public string TopicName { get; set; } = string.Empty;
    }
}
