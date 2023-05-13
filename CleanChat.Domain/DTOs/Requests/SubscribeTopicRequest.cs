using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Requests
{
    public class SubscribeTopicRequest
    {
        public int TopicId { get; set; }
        public int ClientId { get; set; }
    }
}
