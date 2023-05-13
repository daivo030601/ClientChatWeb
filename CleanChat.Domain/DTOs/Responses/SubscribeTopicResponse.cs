using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Responses
{
    public class SubscribeTopicResponse
    {
        public int TopicId { get; set; }
        public int ClientId { get; set; }
    }
}
