using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime SentDate { get; set; }
        public string Content { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}
