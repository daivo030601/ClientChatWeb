using CleanChat.Application.Interfaces;
using CleanChat.Domain;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
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
        public List<GetTopicResponse> GetAllTopics()
        {
            var topics = _topicRepository.GetAllTopics();
            var result = new List<GetTopicResponse>();
            foreach (var topic in topics)
            {
                var response = new GetTopicResponse();
                response.TopicId = topic.TopicId;
                response.TopicName = topic.TopicName;
                result.Add(response);
            }
            return result;
        }

        public CreateTopicResponse CreateTopic(CreateTopicRequest request)
        {
            Topic topic = new Topic() { TopicName = request.TopicName };
            var result = _topicRepository.CreateTopic(topic);
            CreateTopicResponse response = new CreateTopicResponse();
            response.TopicId = result.TopicId;
            return response;
        }
    }
}
