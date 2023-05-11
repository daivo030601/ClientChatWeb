using CleanChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Application
{
    public interface ITopicService
    {
        List<Topic> GetAllTopics();
        Topic CreateTopic(Topic topic); 
    }
}
