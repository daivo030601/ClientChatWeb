using CleanChat.Application.Repositories;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Application.Services
{
    public class TopicService : ITopicService
    {
        public ITopicRepository _topicRepository { get; set; }
        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public List<Topic> GetAllTopics()
        {
            var topics = _topicRepository.GetAllTopics();

            return topics;
        }

        public Topic CreateTopic(Topic topic)
        {
            _topicRepository.CreateTopic(topic);

            return topic;
        }
    }
}
