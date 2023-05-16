using System.Collections.Generic;
using CleanChat.Domain.DTOs;
using CleanChat.Domain.Entities;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;

namespace CleanChat.Application.Services.Interface
{
    public interface IMessageService
    {
        List<MessageReceiveDto> GetAllMessages();
        List<MessageReceiveDto> GetMessagesByTopic(int topicId);
        MessageReceiveDto GetMessageById(int id);
        AddedMessageResponse AddMessage(MessageSendDto message);
    }
}
