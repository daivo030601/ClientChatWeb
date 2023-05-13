using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public ClientRepository(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }
        public Client CreateClient(Client client)
        {
            try
            {
                _chatDbContext.Clients.Add(client);
                _chatDbContext.SaveChanges();
                return client;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Client? Login(Client client)
        {
            var result = _chatDbContext.Clients.FirstOrDefault(c => c.Name == client.Name && c.Password == client.Password);
            if (result != null)
            {
                return result;
            }
            return null;

        }

        public ClientTopic SubscribeTopic(ClientTopic clientTopic)
        {
            try
            {
                _chatDbContext.ClientTopics.Add(clientTopic);
                _chatDbContext.SaveChanges();
                return clientTopic;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
