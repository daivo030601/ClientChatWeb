using System.Collections.Generic;
using CleanChat.Domain.DTOs;

namespace CleanChat.Application.Services.Interface
{
    public interface IMessageService
    {
        IEnumerable<MessageSendDto> GetAllMessages();
        IEnumerable<MessageSendDto> GetMessagesByTopic(int topicId);
        MessageSendDto GetMessageById(int id);
        void AddMessage(MessageSendDto messageDto);
    }
}
