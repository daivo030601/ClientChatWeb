using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Responses
{
    public class ClientTopicResponse
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
    }
}
