using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Requests
{
    public class MessageSendDto
    {
        public string Content { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int TopicId { get; set; }
    }
}
