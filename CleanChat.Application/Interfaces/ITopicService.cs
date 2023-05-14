using CleanChat.Domain;
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
    public interface ITopicService
    {
        List<GetTopicResponse> GetAllTopics();
        CreateTopicResponse CreateTopic(CreateTopicRequest request);
        List<ClientTopicResponse>? GetClientsFromTopic(ClientsTopicRequest request);
    }
}
