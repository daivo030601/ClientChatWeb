using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Application.Interfaces
{
    public interface IClientServices
    {
        public LoginReponse? Login( LoginRequest request );
        public CreateClientResponse? CreateClient( CreateClientRequest client );
        public SubscribeTopicResponse? SubscribeTopic(SubscribeTopicRequest request);
        public SubscribeTopicResponse UnsubscribeTopic(SubscribeTopicRequest request);

        public List<TopicClientResponse>? GetTopicsFromClient(TopicsClientRequest request);
    }
}
