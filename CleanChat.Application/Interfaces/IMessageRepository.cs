using System.Collections.Generic;
using CleanChat.Domain.Entities;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;

namespace CleanChat.Application.Repositories
{
    public interface IMessageRepository
    {
        List<MessageReceiveDto> GetAllMessages();
        List<MessageReceiveDto> GetMessagesByTopic(int topicId);
        MessageReceiveDto GetMessageById(int id);
        MessageSendDto AddMessage(MessageSendDto message);
    }
}