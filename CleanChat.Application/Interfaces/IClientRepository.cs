using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Application.Interfaces
{
    public interface IClientRepository
    {
        public Client? CreateClient( Client client );
        public Client? Login(Client request );
        public bool? SubscribeTopic(ClientTopic clientTopic);
        public bool? UnsubscribeTopic(ClientTopic clientTopic);
        List<ClientTopic>? GetTopicsFromClient(int clientId);
    }
}
