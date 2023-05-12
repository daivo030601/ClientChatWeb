using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Domain.DTOs.Requests
{
    public class CreateClientRequest
    {
        public string ClientName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
