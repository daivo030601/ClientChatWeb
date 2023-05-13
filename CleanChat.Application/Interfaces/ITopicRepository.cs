using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanChat.Domain;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.Entities;

namespace CleanChat.Application.Interfaces
{
    public interface ITopicRepository
    {
        List<Topic> GetAllTopics();
        Topic CreateTopic(Topic topic);
    }
}
