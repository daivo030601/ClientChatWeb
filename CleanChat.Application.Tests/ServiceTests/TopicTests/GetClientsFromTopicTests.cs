using CleanChat.Application.Interfaces;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Application.Tests.ServiceTests.TopicTests
{
    public class GetClientsFromTopicTests
    {
        [Fact]
        public void GetClientsFromTopicSuccess()
        {
            var mockTopicRepo = new Mock<ITopicRepository>();
            var entities = new List<ClientTopic>()
            {
                new ClientTopic()
                {
                    ClientId = 1,
                    TopicId = 1,
                    Client = new Client()
                    {
                        ClientId = 1,
                        Name = "Tuan Anh"
                    }
                },
                new ClientTopic()
                {
                    ClientId = 2,
                    TopicId = 1,
                    Client = new Client()
                    {
                        ClientId = 2,
                        Name = "Dai"
                    }
                },
                new ClientTopic()
                {
                     ClientId = 3,
                     TopicId = 1,
                     Client = new Client()
                     {
                         ClientId = 3,
                         Name = "Quan"
                     }
                }
            };

            var expected = new List<ClientTopicResponse>()
            {
                new ClientTopicResponse()
                {
                    ClientId = 1,
                    Name = "Tuan Anh"
                },
                new ClientTopicResponse()
                {
                    ClientId = 2,
                    Name = "Dai"
                },
                new ClientTopicResponse()
                {
                    ClientId = 3,
                    Name = "Quan"
                }
            };

            var request = new ClientsTopicRequest() { TopicId = 1 };
            mockTopicRepo.Setup(c => c.GetClientsFromTopic(It.IsAny<int>())).Returns(entities);
            var topicService = new TopicService(mockTopicRepo.Object);

            // Act
            var result = topicService.GetClientsFromTopic(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result, Comparer.Get<ClientTopicResponse>((t1, t2) => t1?.ClientId == t2?.ClientId && t1?.Name == t2?.Name));
        }

        [Fact]
        public void GetClientsFromTopicWithNoClients()
        {
            var mockTopicRepo = new Mock<ITopicRepository>();
            var request = new ClientsTopicRequest { TopicId = 1 };
            mockTopicRepo.Setup(c => c.GetClientsFromTopic(It.IsAny<int>())).Returns(new List<ClientTopic>());
            var topicService = new TopicService(mockTopicRepo.Object);

            // Act
            var result = topicService.GetClientsFromTopic(request);

            // Assert
            Assert.IsType<List<ClientTopicResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetClientsFromUnavailableTopic()
        {
            var mockTopicRepo = new Mock<ITopicRepository>();
            var request = new ClientsTopicRequest { TopicId = 1 };
            List<ClientTopic>? response = null;
            mockTopicRepo.Setup(c => c.GetClientsFromTopic(It.IsAny<int>())).Returns(response);

            var topicService = new TopicService(mockTopicRepo.Object);

            // Act
            var result = topicService.GetClientsFromTopic(request);

            // Assert
            Assert.Null(result);
        }
    }
}
