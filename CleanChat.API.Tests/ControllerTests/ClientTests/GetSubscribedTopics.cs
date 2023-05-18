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

namespace CleanChat.API.Tests.ControllerTests.ClientTests
{
    public class GetSubscribedTopics
    {
        [Fact]
        public void GetSubscribedTopicsSuccess()
        {
            var clientService = new Mock<IClientServices>();

            
            var expected = new List<TopicClientResponse>()
            {
                new TopicClientResponse()
                {
                    TopicId = 1,
                    TopicName = "test1",

                },
                new TopicClientResponse()
                {
                    TopicId = 2,
                    TopicName = "test2"
                }
            };

            clientService.Setup(c => c.GetTopicsFromClient(It.IsAny<TopicsClientRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.Get(1);


            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void GetSubscribedTopicsEmpty()
        {
            var clientService = new Mock<IClientServices>();

            var request = new TopicsClientRequest()
            {
                ClientId = 1
            };
            List<TopicClientResponse>? expected = null;

            clientService.Setup(c => c.GetTopicsFromClient(It.IsAny<TopicsClientRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.Get(1);


            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var badRequestResult = (NotFoundObjectResult)result.Result;
            var apiResponse = (ApiResponse)badRequestResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("2", apiResponse.Code);
            Assert.Null(apiResponse.ResponseData);
        }
    }
}
