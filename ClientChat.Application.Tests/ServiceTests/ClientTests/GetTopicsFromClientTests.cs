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
    public class GetTopicsFromClientTests
    {
        [Fact]
        public void GetTopicsFromClientSuccess()
        {
            var mockClientRepo = new Mock<IClientRepository>();
            var entities = new List<ClientTopic>()
            {
                new ClientTopic()
                {
                    ClientId = 1,
                    TopicId = 1,
                    Topic = new Topic()
                    {
                        TopicId = 1,
                        TopicName = "A"
                    }
                },
                new ClientTopic()
                {
                    ClientId = 1,
                    TopicId = 2,
                    Topic = new Topic()
                    {
                        TopicId = 2,
                        TopicName = "B"
                    }
                },
                new ClientTopic()
                {
                    ClientId = 1,
                    TopicId = 3,
                     Topic = new Topic()
                    {
                        TopicId = 3,
                        TopicName = "C"
                    }
                }
            };
            var expected = new List<TopicClientResponse>()
            {
                new TopicClientResponse()
                {
                    TopicId = 1,
                    TopicName = "A",
                },
                new TopicClientResponse()
                {
                    TopicId = 2,
                    TopicName = "B",
                },
                new TopicClientResponse()
                {
                    TopicId= 3,
                    TopicName = "C",
                }
            };
            var clientId = 1;
            var request = new TopicsClientRequest { ClientId = clientId };
            mockClientRepo.Setup(c => c.GetTopicsFromClient(It.IsAny<int>())).Returns(entities);
            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.GetTopicsFromClient(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].TopicName, result[0].TopicName);
        }

        [Fact]
        public void GetTopicsFromClientWithNoTopics()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();   
            var clientId = 1;
            var request = new TopicsClientRequest { ClientId = clientId };
            mockClientRepo.Setup(c => c.GetTopicsFromClient(It.IsAny<int>())).Returns(new List<ClientTopic>());
            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.GetTopicsFromClient(request);

            // Assert
            Assert.IsType<List<TopicClientResponse>>(result);
            Assert.Empty(result);
            
        }

        [Fact]
        public void GetTopicsFromClientReturnNullValue()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var clientId = 1;
            List<ClientTopic> response = null;
            var request = new TopicsClientRequest { ClientId = clientId };
            mockClientRepo.Setup(c => c.GetTopicsFromClient(It.IsAny<int>())).Returns(response);
            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.GetTopicsFromClient(request);

            // Assert
            Assert.Null(result);

        }
    }
}
