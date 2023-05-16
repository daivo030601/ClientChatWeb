using CleanChat.Application.Interfaces;
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

namespace ClientChat.Application.Tests.ServiceTests.TopicTests
{
    public class GetAllTopicsTest
    {
        [Fact]
        public  void GetAllTopicsSuccess()
        {
            // Arrange
            var mockTopicRepo = new Mock<ITopicRepository>();
            var entities = new List<Topic>()
            {
                new Topic()
                {
                    TopicId = 1,
                    TopicName = "Test 1",
                },
                new Topic()
                {
                    TopicId = 2,
                    TopicName = "Test 2"
                },
                new Topic()
                {
                    TopicId = 3,
                    TopicName = "Test 3"
                }
            };
            var expected = new List<GetTopicResponse>()
            {
                new GetTopicResponse()
                {
                    TopicId = 1,
                    TopicName = "Test 1",
                },
                new GetTopicResponse()
                {
                    TopicId = 2,
                    TopicName = "Test 2",
                },
                new GetTopicResponse()
                {
                    TopicId = 3,
                    TopicName = "Test 3"
                }
            };
            mockTopicRepo.Setup(c => c.GetAllTopics()).Returns(entities);
            var topicService = new TopicService(mockTopicRepo.Object);

            // Act
            var result = topicService.GetAllTopics();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<GetTopicResponse>>(result);
            Assert.Equal(expected, result, 
                Comparer.Get<GetTopicResponse>((t1, t2) => t1?.TopicId == t2?.TopicId 
                    && t1?.TopicName == t2?.TopicName));
        }

        [Fact]
        public void GetAllTopicsEmpty()
        {
            // Arrange
            var mockTopicRepo = new Mock<ITopicRepository>();
            mockTopicRepo.Setup(c => c.GetAllTopics()).Returns(new List<Topic>());

            var topicService = new TopicService(mockTopicRepo.Object);


            // Act
            var result = topicService.GetAllTopics();

            // Assert
            Assert.Empty(result);
            Assert.IsType<List<GetTopicResponse>>(result);
        }
    }
}
