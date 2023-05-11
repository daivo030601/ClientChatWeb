using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanChat.Domain;

namespace CleanChat.Application
{
    public interface ITopicRepository
    {
        List<Topic> GetAllTopics();
        Topic CreateTopic(Topic topic);
    }
}
