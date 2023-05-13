using System.Collections.Generic;
using CleanChat.Domain.Entities;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;

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