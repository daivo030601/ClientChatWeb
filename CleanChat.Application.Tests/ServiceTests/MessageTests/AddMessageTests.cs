using CleanChat.Application.Repositories;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Requests;
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
    public class AddMessageTests
    {
        [Fact]
        public void AddMessageSuccess()
        {
            // Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            var date = DateTime.Now;
            var expected = new Message()
            {
                MessageId = 1,
                SentDate = date,
                Content = "test 1",
                ClientName = "test",
                ClientId = 1,
                TopicId = 1,
            };
            var request = new MessageSendDto()
            {
                Content = "test 1",
                ClientId = 1,
                TopicId = 1,
            };
            mockMessageRepo.Setup(m => m.AddMessage(It.IsAny<Message>())).Returns(expected);
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.AddMessage(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AddedMessageResponse>(result);
            Assert.Equal(expected.MessageId, result.MessageId);
        }

        [Fact]
        public void AddMessageFailed()
        {
            // Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            var date = DateTime.Now;
            var expected = new Message()
            {
                ClientName = "test",
                ClientId = 1,
                TopicId = 1,
            };
            var request = new MessageSendDto()
            {
                Content = "test 1",
                ClientId = 1,
                TopicId = 1,
            };
            mockMessageRepo.Setup(m => m.AddMessage(It.IsAny<Message>())).Returns(expected);
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.AddMessage(request);

            // Assert
            Assert.Null(result.MessageResponse);
            Assert.True(result.MessageId == 0);
        }
    }
}
