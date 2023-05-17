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
    public class SubscribeTopic
    {
        [Fact]
        public void SubscribeTopicSuccess()
        {
            var clientService = new Mock<IClientServices>();

            var request = new SubscribeTopicRequest()
            {
                ClientId = 1,
                TopicId = 1
            };
            var expected = new SubscribeTopicResponse()
            {
                Status = true
            };

            clientService.Setup(c => c.SubscribeTopic(It.IsAny<SubscribeTopicRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.SubscribeTopic(request);


            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void SubscribeTopicAlreadyExist()
        {
            var clientService = new Mock<IClientServices>();

            var request = new SubscribeTopicRequest()
            {
                ClientId = 1,
                TopicId = 1
            };
            var response = new SubscribeTopicResponse()
            {
                Status = false
            };

            clientService.Setup(c => c.SubscribeTopic(It.IsAny<SubscribeTopicRequest>())).Returns(response);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.SubscribeTopic(request);


            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("3", apiResponse.Code);
            Assert.Equal(request, apiResponse.ResponseData);
        }

        [Fact]
        public void SubscribeTopicFailed()
        {
            var clientService = new Mock<IClientServices>();

            var request = new SubscribeTopicRequest()
            {
                ClientId = 1,
                TopicId = 1
            };

            var response = new SubscribeTopicResponse()
            {
                Status = null
            };

            clientService.Setup(c => c.SubscribeTopic(It.IsAny<SubscribeTopicRequest>())).Returns(response);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.SubscribeTopic(request);


            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = result.Result as NotFoundObjectResult;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("2", apiResponse.Code);
            Assert.Equal(request, apiResponse.ResponseData);
        }
    }
}
