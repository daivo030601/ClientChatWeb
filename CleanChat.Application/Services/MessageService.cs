using CleanChat.Application.Repositories;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanChat.Application.Services
{
    public class MessageService : IMessageService
    {
        public IMessageRepository _messageRepository { get; set; }

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<MessageReceiveDto> GetAllMessages()
        {
            var messages = _messageRepository.GetAllMessages();
            return messages;
        }

        public List<MessageReceiveDto> GetMessagesByTopic(int topicId)
        {
            var messages = _messageRepository.GetMessagesByTopic(topicId);
            return messages;
        }

        public MessageReceiveDto GetMessageById(int id)
        {
            var message = _messageRepository.GetMessageById(id);
            return message;
        }

        public MessageSendDto AddMessage(MessageSendDto message)
        {
            _messageRepository.AddMessage(message);
            return message;
        }
    }
}
