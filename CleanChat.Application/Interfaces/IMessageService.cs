using System.Collections.Generic;
using CleanChat.Domain.DTOs;
using CleanChat.Domain.Entities;

namespace CleanChat.Application.Services.Interface
{
    public interface IMessageService
    {
        List<Message> GetAllMessages();
        List<Message> GetMessagesByTopic(int topicId);
        Message GetMessageById(int id);
        Message AddMessage(Message message);
    }
}
