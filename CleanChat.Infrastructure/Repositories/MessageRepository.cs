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


        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public List<Message> GetAllMessages()
        {
            var entities = _context.Messages.Join(
                _context.Clients, m => m.ClientId, c => c.ClientId, (m, c) => new Message
            {
                    MessageId = m.MessageId,
                    SentDate = m.SentDate,
                    Content = m.Content,
                    ClientName = c.Name,
                    ClientId = m.ClientId,
                    TopicId = m.TopicId,
            }).ToList();

            return entities;
        }

        public List<Message> GetMessagesByTopic(int topicId)
        {
            var entities = _context.Messages.Join(
                _context.Clients, m => m.ClientId, c => c.ClientId, ( m, c ) => new Message
                {
                    MessageId = m.MessageId,
                    SentDate = m.SentDate,
                    Content = m.Content,
                    ClientName = c.Name,
                    ClientId = m.ClientId,
                    TopicId = m.TopicId,
                }).Where(m => m.TopicId == topicId).ToList();
        
            return entities;
        }

        public Message GetMessageById(int id)
        {
            var entity = _context.Messages.Join(
                _context.Clients, m => m.ClientId, c => c.ClientId, ( m, c ) => new Message
                {
                    MessageId = m.MessageId,
                    SentDate = m.SentDate,
                    Content = m.Content,
                    ClientName = c.Name,
                    ClientId = m.ClientId,
                    TopicId = m.TopicId,
                }).FirstOrDefault(m => m.MessageId == id);


            return entity;
        }

        public Message AddMessage(Message entity)
        {
            try
            {
                _context.Messages.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        

            return entity;
        }
    }
}
