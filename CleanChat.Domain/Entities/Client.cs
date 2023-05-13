using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? Name { get; set; } 
        public string? Password { get; set; }

        public ICollection<Message>? Messages { get; set; }
        public ICollection<ClientTopic>? ClientTopics { get; set; }
    }
}
