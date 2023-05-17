using CleanChat.Application.Interfaces;
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

namespace ClientChat.Application.Tests.ServiceTests.TopicTests
{
    public class CreateTopicTests
    {
        [Fact]
        public void CreateTopicSuccess()
        {
            var mockTopicRepo = new Mock<ITopicRepository>();
            var entity = new Topic()
            {
                TopicId = 1,
                TopicName = "Test"
            };
            var request = new CreateTopicRequest
            {
                TopicName = "Test"
            };

            mockTopicRepo.Setup(c => c.CreateTopic(It.IsAny<Topic>())).Returns(entity);
            var topicService = new TopicService(mockTopicRepo.Object);
            var result = topicService.CreateTopic(request);

            Assert.NotNull(result);
            Assert.IsType<CreateTopicResponse>(result);
            Assert.Equal(entity.TopicId, result.TopicId);
        }
    }
}
