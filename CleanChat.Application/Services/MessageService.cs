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
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<MessageReceiveDto> GetAllMessages()
        {
            List<MessageReceiveDto> response = new List<MessageReceiveDto>();
            var entities = _messageRepository.GetAllMessages();
            if (entities != null) 
            {

                foreach ( var entity in entities )
                {
                    response.Add(new MessageReceiveDto
                    {
                        MessageId = entity.MessageId,
                        SentDate = entity.SentDate,
                        Content = entity.Content,
                        ClientName = entity.ClientName,
                        ClientId = entity.ClientId,
                        TopicId = entity.TopicId,
                    });
                }
            }
            return response;
        }

        public List<MessageReceiveDto> GetMessagesByTopic(int topicId)
        {
            List<MessageReceiveDto> response = new List<MessageReceiveDto>();
            var entities = _messageRepository.GetMessagesByTopic(topicId);
            if ( entities != null )
            {
                foreach ( var entity in entities )
                {
                    response.Add(new MessageReceiveDto
                    {
                        MessageId = entity.MessageId,
                        SentDate = entity.SentDate,
                        Content = entity.Content,
                        ClientName = entity.ClientName,
                        ClientId = entity.ClientId,
                        TopicId = entity.TopicId,
                    });
                }
            }
            return response;
        }

        public MessageReceiveDto GetMessageById(int id)
        {
            var entity = _messageRepository.GetMessageById(id);
            if ( entity != null )
            {
                MessageReceiveDto response = new MessageReceiveDto
                {
                    MessageId = entity.MessageId,
                    SentDate = entity.SentDate,
                    Content = entity.Content,
                    ClientName = entity.ClientName,
                    ClientId = entity.ClientId,
                    TopicId = entity.TopicId,
                };
                return response;
            }
            return null;            
        }

        public AddedMessageResponse AddMessage(MessageSendDto message)
        {
            Message mes = new Message
            {
                Content = message.Content,
                ClientId = message.ClientId,
                TopicId = message.TopicId,
            };
            var entity = _messageRepository.AddMessage(mes);
            AddedMessageResponse response = new AddedMessageResponse
            {
                MessageId = entity.MessageId,
            };
            return response;
        }
    }
}
