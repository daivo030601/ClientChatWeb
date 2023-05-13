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
    public interface IClientTopicRepository
    {
        public ClientTopic? SubscribeTopic(ClientTopic clientTopic);
    }
}
