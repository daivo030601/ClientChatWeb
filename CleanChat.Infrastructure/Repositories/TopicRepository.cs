using CleanChat.Application.Interfaces;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ChatDbContext _chatDbContext;
        public TopicRepository(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public Topic CreateTopic(Topic topic)
        {
            try
            {
                _chatDbContext.Topics.Add(topic);
                _chatDbContext.SaveChanges();
                return topic;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Topic> GetAllTopics()
        {
            try
            {
                return _chatDbContext.Topics.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ClientTopic>? GetClientsFromTopic(int topic)
        {
            try
            {
                if (_chatDbContext.Topics.Any(c => c.TopicId == topic))
                {
                    var query = _chatDbContext.ClientTopics.Join(_chatDbContext.Clients,
                        c => c.ClientId, m => m.ClientId, (c, m) => new ClientTopic()
                        {
                            ClientId = c.ClientId,
                            TopicId = c.TopicId,
                            Client = m
                        });
                    return query.Where(c => c.TopicId == topic).ToList();
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
