using CleanChat.Domain;
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
        List<Topic> GetAllTopics();
        Topic CreateTopic(Topic topic);
    }
}
