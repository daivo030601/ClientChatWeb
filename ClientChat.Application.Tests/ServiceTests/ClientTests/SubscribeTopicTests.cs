using CleanChat.Application.Interfaces;
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

namespace ClientChat.Application.Tests.ServiceTests.ClientTests
{
    public class SubscribeTopicTests
    {
        [Fact]
        public void SubscribeTopicReturnTrue()
        {
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeToppicRequest = new SubscribeTopicRequest()
            {
                TopicId = 1,
                ClientId = 1,
            };
            bool expected = true;
            mockClientRepo.Setup(c => c.SubscribeTopic(It.IsAny<ClientTopic>())).Returns(expected);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.SubscribeTopic(subscribeToppicRequest);

            // Assert
            Assert.True(result.Status == true);
            Assert.IsType<SubscribeTopicResponse>(result);
        }

        [Fact]
        public void SubscribeTopicReturnFalse()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeToppicRequest = new SubscribeTopicRequest()
            {
                TopicId = 1,
                ClientId = 1,
            };
            bool expected = false;
            mockClientRepo.Setup(c => c.SubscribeTopic(It.IsAny<ClientTopic>())).Returns(expected);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.SubscribeTopic(subscribeToppicRequest);

            // Assert
            Assert.True(result.Status == false);
            Assert.IsType<SubscribeTopicResponse>(result);
        }

        [Fact]
        public void SubscribeTopicReturnNull()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeToppicRequest = new SubscribeTopicRequest()
            {
                TopicId = 1,
                ClientId = 1,
            };
            bool? response = null;
            mockClientRepo.Setup(c => c.SubscribeTopic(It.IsAny<ClientTopic>())).Returns(response);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.SubscribeTopic(subscribeToppicRequest);

            // Assert
            Assert.Null(result);
        }
    }
}
