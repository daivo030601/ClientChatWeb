using CleanChat.Application.Repositories;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public List<Message> GetAllMessages()
        {
            return _context.Messages.ToList();
        }

        public List<Message> GetMessagesByTopic(int topicId)
        {
            return _context.Messages.Where(m => m.TopicId == topicId).ToList();
        }

        public Message GetMessageById(int id)
        {
            return _context.Messages.Find(id);
        }

        public Message AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }
    }
}
