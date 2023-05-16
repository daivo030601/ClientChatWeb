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
    public class GetMessageByIdTests
    {
        [Fact]
        public void GetMessageByIdSuccess()
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

            mockMessageRepo.Setup(m => m.GetMessageById(It.IsAny<int>())).Returns(expected);
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.GetMessageById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MessageReceiveDto>(result);
            Assert.Equal(expected.MessageId, result.MessageId);
        }

        [Fact]
        public void GetMessageByIdFailed()
        {
            // Arrange
            var mockMessageRepo = new Mock<IMessageRepository>();
            var date = DateTime.Now;
            Message? message = null;

            mockMessageRepo.Setup(m => m.GetMessageById(It.IsAny<int>())).Returns(message);
            var messageService = new MessageService(mockMessageRepo.Object);

            // Act
            var result = messageService.GetMessageById(1);

            // Assert
            Assert.Null(result);

        }
    }
}
