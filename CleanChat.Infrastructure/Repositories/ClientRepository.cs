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

        public bool? SubscribeTopic( ClientTopic clientTopic)
        {
            try
            {
                if (_chatDbContext.Clients.Any(c => c.ClientId == clientTopic.ClientId)
                    && _chatDbContext.Topics.Any(m => m.TopicId == clientTopic.TopicId))
                {
                    if (_chatDbContext.ClientTopics.Any(c => c.ClientId == clientTopic.ClientId
                        && c.TopicId == clientTopic.TopicId))
                    {
                        return false;
                    }
                    _chatDbContext.ClientTopics.Add(clientTopic);
                    _chatDbContext.SaveChanges();
                    return true;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ClientTopic>? GetTopicsFromClient(int clientId)
        {
            try
            {
                if (_chatDbContext.Clients.Any(c => c.ClientId == clientId))
                {
                    var query = _chatDbContext.ClientTopics.Join(_chatDbContext.Topics,
                        c => c.TopicId, m => m.TopicId, (c, m) => new ClientTopic()
                        {
                            ClientId = c.ClientId,
                            TopicId = c.TopicId,
                            Topic = m
                        });
                    return query.Where(c => c.ClientId == clientId).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
