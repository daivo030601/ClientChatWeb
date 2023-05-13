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
    public class ClientTopicRepository : IClientTopicRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public ClientTopicRepository(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public ClientTopic? SubscribeTopic(ClientTopic clientTopic)
        {
            try
            {
                _chatDbContext.ClientTopics.Add(clientTopic);
                _chatDbContext.SaveChanges();
                return clientTopic;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                return null;
            }
        }
    }
}
