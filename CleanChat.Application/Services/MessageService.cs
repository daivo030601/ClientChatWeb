using CleanChat.Application.Repositories;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CleanChat.Application.Services
{
    public class MessageService : IMessageService
    {
        public IMessageRepository _messageRepository { get; set; }

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<Message> GetAllMessages()
        {
            var messages = _messageRepository.GetAllMessages();
            return messages;
        }

        public List<Message> GetMessagesByTopic(int topicId)
        {
            var messages = _messageRepository.GetMessagesByTopic(topicId);
            return messages;
        }

        public Message GetMessageById(int id)
        {
            var message = _messageRepository.GetMessageById(id);
            return message;
        }

        public Message AddMessage(Message message)
        {
            _messageRepository.AddMessage(message);
            return message;
        }
    }
}
