using CleanChat.Application.Interfaces;
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
    public class ClientTopicService : IClientTopicService
    {
        private readonly IClientTopicRepository _repository;
        public ClientTopicService(IClientTopicRepository clientTopicRepository)
        {
            _repository = clientTopicRepository;
        }
        public SubscribeTopicResponse? SubscribeTopic(SubscribeTopicRequest request)
        {
            SubscribeTopicResponse response = new SubscribeTopicResponse();
            ClientTopic entity = new() { TopicId = request.TopicId, ClientId = request.ClientId };
            var result = _repository.SubscribeTopic(entity);
            if (result != null)
            {
                response.TopicId = result.TopicId;
                response.ClientId = result.ClientId;
                return response;
            }
            return null;
        }
    }
}
