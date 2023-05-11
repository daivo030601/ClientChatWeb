using CleanChat.Application;
using CleanChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure
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
            _chatDbContext.Topics.Add(topic);
            _chatDbContext.SaveChanges();
            return topic;
        }

        public List<Topic> GetAllAvaiableTopics()
        {
            return _chatDbContext.Topics.ToList();
        }
    }
}
