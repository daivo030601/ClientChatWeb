using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Responses
{
    public class MessageReceiveDto
    {
        public int MessageId { get; set; }
        public DateTime SentDate { get; set; }
        public string Content { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int TopicId { get; set; }
    }
}
