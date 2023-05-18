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

namespace CleanChat.Application.Tests.ServiceTests.ClientTests
{
    public class UnsubscribeTopicTests
    {
        [Fact]
        public void UnsubscribeTopicReturnTrue()
        {
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeTopicRequest = new SubscribeTopicRequest()
            {
                ClientId = 100,
                TopicId = 100
            };
            bool expected = true;
            mockClientRepo.Setup(c => c.UnsubscribeTopic(It.IsAny<ClientTopic>())).Returns(expected);

            var clientServices = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientServices.UnsubscribeTopic(subscribeTopicRequest);

            // Assert
            Assert.True(result.Status == true);
            Assert.IsType<SubscribeTopicResponse>(result);
        }

        [Fact]
        public void UnsubscribeTopicReturnFalse()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeToppicRequest = new SubscribeTopicRequest()
            {
                TopicId = 1,
                ClientId = 1,
            };
            bool expected = false;
            mockClientRepo.Setup(c => c.UnsubscribeTopic(It.IsAny<ClientTopic>())).Returns(expected);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.UnsubscribeTopic(subscribeToppicRequest);

            // Assert
            Assert.True(result.Status == false);
            Assert.IsType<SubscribeTopicResponse>(result);
        }

        [Fact]
        public void UnsubscribeTopicReturnNull()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var subscribeToppicRequest = new SubscribeTopicRequest()
            {
                TopicId = 1,
                ClientId = 1,
            };
            bool? response = null;
            mockClientRepo.Setup(c => c.UnsubscribeTopic(It.IsAny<ClientTopic>())).Returns(response);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.UnsubscribeTopic(subscribeToppicRequest);

            // Assert
            Assert.Null(result);
        }
    }
}
