using CleanChat.API.Controllers;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.API.Tests.ControllerTests.TopicTests
{
    public class CreateTopicTests
    {
        [Fact]
        public void CreateTopicSuccess()
        {
            var topicService = new Mock<ITopicService>();
            var request = new CreateTopicRequest()
            {
                TopicName = "Test",
            };
            var expected = new CreateTopicResponse()
            {
                TopicId = 1,
            };
            topicService.Setup(x => x.CreateTopic(It.IsAny<CreateTopicRequest>())).Returns(expected);
            var topicController = new TopicController(topicService.Object);

            var result = topicController.CreateTopic(request);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.IsType<CreateTopicResponse>(apiResponse.ResponseData);
            Assert.Equal(expected, apiResponse.ResponseData);
        }
    }
}
