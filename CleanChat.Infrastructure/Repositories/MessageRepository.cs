using AutoMapper;
using CleanChat.Application.Interfaces;
using CleanChat.Application.Repositories;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.context;
using System.Collections.Generic;
using System.Linq;

namespace CleanChat.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MessageReceiveDto> GetAllMessages()
        {
            var messages = _context.Messages.ToList();
            var mappedMessages = _mapper.Map<List<MessageReceiveDto>>(messages);
            return mappedMessages;
        }

        public List<MessageReceiveDto> GetMessagesByTopic(int topicId)
        {
            var messages = _context.Messages.Where(m => m.TopicId == topicId).ToList();
            var mappedMessages = _mapper.Map<List<MessageReceiveDto>>(messages);
            return mappedMessages;
        }

        public MessageReceiveDto GetMessageById(int id)
        {
            var message = _context.Messages.Find(id);
            var mappedMessage = _mapper.Map<MessageReceiveDto>(message);
            return mappedMessage;
        }

        public MessageSendDto AddMessage(MessageSendDto message)
        {
            var mappedMessage = _mapper.Map<Message>(message);
            _context.Messages.Add(mappedMessage);
            _context.SaveChanges();
            var mappedResult = _mapper.Map<MessageSendDto>(mappedMessage);
            return mappedResult;
        }
    }
}
