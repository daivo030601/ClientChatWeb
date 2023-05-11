using CleanChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Application
{
    public class TopicService : ITopicService
    {
        public ITopicRepository _topicRepository { get; set; }
        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public List<Topic> GetAllAvaiableTopics(int clientId)
        {
            var topics = _topicRepository.GetAllAvaiableTopics(clientId);

            return topics;
        }

        public Topic CreateTopic(Topic topic)
        {
            _topicRepository.CreateTopic(topic);

            return topic;
        }
    }
}
