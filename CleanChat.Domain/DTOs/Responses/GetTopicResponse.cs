using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Responses
{
    public class GetTopicResponse
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
    }
}
