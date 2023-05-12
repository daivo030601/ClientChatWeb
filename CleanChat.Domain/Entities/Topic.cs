using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public ICollection<ClientTopic>? ClientTopics { get; set; }
        public ICollection<Message>? Messages { get; set; }

    }
}
