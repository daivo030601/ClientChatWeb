using CleanChat.API.Controllers;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.API.Tests.ControllerTests.ClientTests
{
    public class LoginTests
    {
        [Fact]
        public void LoginSuccess()
        {
            var clientService = new Mock<IClientServices>();

            var expected = new LoginReponse()
            {
                ClientId = 1,
            };
            var request = new LoginRequest()
            {
                ClientName = "test",
                Password = "test",
            };

            clientService.Setup(c => c.Login(It.IsAny<LoginRequest>())).Returns(expected);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.Login(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);


        }

        [Fact]
        public void LoginFailed()
        {
            var clientService = new Mock<IClientServices>();

            LoginReponse? response = null;
            var request = new LoginRequest()
            {
                ClientName = "test",
                Password = "test",
            };

            clientService.Setup(c => c.Login(It.IsAny<LoginRequest>())).Returns(response);

            var clientController = new ClientController(clientService.Object);

            // Act
            var result = clientController.Login(request);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = (NotFoundObjectResult)result.Result;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("2", apiResponse.Code);
            Assert.Null(apiResponse.ResponseData);


        }      
    }
}
