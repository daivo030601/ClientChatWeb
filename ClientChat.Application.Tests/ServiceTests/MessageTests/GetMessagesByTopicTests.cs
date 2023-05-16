using CleanChat.Application.Repositories;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Application.Tests.ServiceTests.MessageTests
{
    public class GetMessagesByTopicTests
    {
        [Fact]
        public void GetMessagesByTopicSuccess()
        {
            // Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            var date = DateTime.Now;
            var expected = new List<Message>()
            {
                    new Message()
                    {
                        MessageId = 1,
                        SentDate = date,
                        Content = "test 1",
                        ClientName = "test",
                        ClientId = 1,
                        TopicId = 1,
                    },
                    new Message()
                    {
                        MessageId = 2,
                        SentDate = date,
                        Content = "test 2",
                        ClientName = "test 2",
                        ClientId = 2,
                        TopicId = 1,
                    },
                    new Message()
                    {
                        MessageId = 3,
                        SentDate = date,
                        Content = "test 3",
                        ClientName = "test",
                        ClientId = 1,
                        TopicId = 1,
                    },
                     new Message()
                    {
                        MessageId = 4,
                        SentDate = date,
                        Content = "test 4",
                        ClientName = "test 3",
                        ClientId = 3,
                        TopicId = 1,
                    },

            };
            mockMessageRepo.Setup(m => m.GetMessagesByTopic(It.IsAny<int>())).Returns(expected);
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.GetMessagesByTopic(2);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<MessageReceiveDto>>(result);
            Assert.Equal(expected[1].MessageId, result[1].MessageId);
        }

        [Fact]
        public void GetMessagesByTopicEmpty()
        {
            // Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            var date = DateTime.Now;

            mockMessageRepo.Setup(m => m.GetMessagesByTopic(It.IsAny<int>())).Returns(new List<Message>());
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.GetMessagesByTopic(1);

            // Assert
            Assert.Null(result);
        }
    }
}
