using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Responses
{
    public class AddedMessageResponse
    {
        public int MessageId { get; set; }
        public string MessageResponse { get; set; }
    }
}
