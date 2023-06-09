﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.Entities
{
    public class ClientTopic
    {
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
