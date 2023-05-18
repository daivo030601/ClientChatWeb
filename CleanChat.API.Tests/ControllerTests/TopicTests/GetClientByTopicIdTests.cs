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
    public class GetClientByTopicIdTests
    {
        [Fact]
        public void GetClientsByTopicIdSuccess()
        {
            var topicService = new Mock<ITopicService>();
            var request = new ClientsTopicRequest() { TopicId = 1 };
            var expected = new List<ClientTopicResponse>()
            {
                new ClientTopicResponse()
                {
                    ClientId = 1,
                    Name = "Test",
                },
                new ClientTopicResponse()
                {
                   ClientId = 2,
                    Name = "Test",
                },
                new ClientTopicResponse()
                {
                   ClientId = 3,
                    Name = "Test",
                }
            };
            topicService.Setup(x => x.GetClientsFromTopic(It.IsAny<ClientsTopicRequest>())).Returns(expected);
            var topicController = new TopicController(topicService.Object);

            var result = topicController.Get(2);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.IsType<List<ClientTopicResponse>>(apiResponse.ResponseData);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void GetClientsByTopicIdFailed()
        {
            var topicService = new Mock<ITopicService>();
            var expected = new ClientsTopicRequest() { TopicId = 2 };
            List<ClientTopicResponse> response;
            response = null;

            topicService.Setup(x => x.GetClientsFromTopic(It.IsAny<ClientsTopicRequest>())).Returns(response);
            var topicController = new TopicController(topicService.Object);

            var result = topicController.Get(2);

            Assert.IsType<NotFoundObjectResult>(result.Result);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Null(apiResponse.ResponseData);
            Assert.Equal("2", apiResponse.Code);
        }
    }
}
