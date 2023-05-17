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
    public class CreateTests
    {
        [Fact]
        public void CreateClientSuccess()
        {
            var clientService = new Mock<IClientServices>();

            var expected = new CreateClientResponse()
            {
                ClientId = 1,
            };
            var request = new CreateClientRequest()
            {
                ClientName = "test",
                Password = "test",
            };

            clientService.Setup(c => c.CreateClient(It.IsAny<CreateClientRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.CreateClient(request);


            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);

        }

        [Fact]
        public void CreateClientFailed()
        {
            var clientService = new Mock<IClientServices>();

            CreateClientResponse? expected = null;
            var request = new CreateClientRequest()
            {
                ClientName = "test",
                Password = "test",
            };

            clientService.Setup(c => c.CreateClient(It.IsAny<CreateClientRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.CreateClient(request);


            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);

            var notFoundResult = (BadRequestObjectResult)result.Result;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("3", apiResponse.Code);
            Assert.Equal("Client has already existed", apiResponse.ResponseData);
        }
    }
}
