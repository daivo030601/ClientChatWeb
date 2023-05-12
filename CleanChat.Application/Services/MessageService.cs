using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CleanChat.Domain.DTOs;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain.Entities;

namespace CleanChat.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly List<Message> _messages = new List<Message>();
        private readonly IMapper _mapper;

        public MessageService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MessageSendDto> GetAllMessages()
        {
            var messages = _messages.ToList();
            return _mapper.Map<List<Message>, List<MessageSendDto>>(messages);
        }

        public IEnumerable<MessageSendDto> GetMessagesByTopic(int topicId)
        {
            var messages = _messages.Where(m => m.TopicId == topicId).ToList();
            return _mapper.Map<List<Message>, List<MessageSendDto>>(messages);
        }

        public MessageSendDto GetMessageById(int id)
        {
            var message = _messages.Find(m => m.MessageId == id);
            return _mapper.Map<Message, MessageSendDto>(message);
        }

        public void AddMessage(MessageSendDto messageDto)
        {
            var message = _mapper.Map<MessageSendDto, Message>(messageDto);
            _messages.Add(message);
        }
    }
}
