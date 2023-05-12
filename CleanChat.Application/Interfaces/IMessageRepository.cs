using System.Collections.Generic;
using CleanChat.Domain.Entities;

namespace CleanChat.Application.Repositories
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages();
        List<Message> GetMessagesByTopic(int topicId);
        Message GetMessageById(int id);
        Message AddMessage(Message message);
    }
}