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
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _repository;

        public ClientServices(IClientRepository clientRepository)
        {
            _repository = clientRepository;
        }

        public CreateClientResponse? CreateClient( CreateClientRequest client )
        {
            CreateClientResponse response = new CreateClientResponse();
            Client entity = new() { Name = client.ClientName, Password = client.Password };
            var result = _repository.CreateClient( entity );
            if ( result != null ) 
            {
                response.ClientId = result.ClientId;
                return response;
            }
            return null;
        }

        public LoginReponse? Login( LoginRequest request )
        {
            Client entity = new Client() { Name = request.ClientName, Password = request.Password };
            Client? result = _repository.Login( entity );
            if ( result != null ) 
            {
                LoginReponse response = new() { 
                    ClientId = result.ClientId,
                    ClientName = result.Name,
                };
                return response;
            }
            return null;
        }

        public SubscribeTopicResponse? SubscribeTopic(SubscribeTopicRequest request)
        {
            SubscribeTopicResponse response = new SubscribeTopicResponse();
            ClientTopic entity = new() { TopicId = request.TopicId, ClientId = request.ClientId };
            var result = _repository.SubscribeTopic(entity);
            if ( result != null )
            {
                response.Status = result;
                return response;
            }
            return null;
        }

        public SubscribeTopicResponse UnsubscribeTopic(SubscribeTopicRequest request)
        {
            SubscribeTopicResponse response = new SubscribeTopicResponse();
            ClientTopic entity = new() { TopicId= request.TopicId, ClientId= request.ClientId };
            var result = _repository.UnsubscribeTopic(entity);
            response.Status = result;
            return response;
        }

        public List<TopicClientResponse>? GetTopicsFromClient(TopicsClientRequest request)
        {
            var topics = _repository.GetTopicsFromClient( request.ClientId );
            var result = new List<TopicClientResponse>();
            if ( topics != null )
            {
                foreach ( var topic in topics )
                {
                    var response = new TopicClientResponse
                    {
                        TopicId = topic.TopicId,
                        TopicName = topic!.Topic!.TopicName
                    };
                    result.Add(response);
                }
                return result;
            }
            return null;
        }
    }
}
