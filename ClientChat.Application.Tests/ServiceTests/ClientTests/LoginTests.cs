using CleanChat.Application.Interfaces;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Application.Tests.ServiceTests.ClientTests
{
    public class LoginTests
    {
        [Fact]
        public void LoginSuccessTest()
        {
            // Arrange
            var mockClientRepo = new Mock<IClientRepository>();
            var expectedClient = new Client
            {
                ClientId = 1,
                Name = "test",
                Password = "123"
            };
            var loginRequest = new LoginRequest
            {
                ClientName = "test",
                Password = "123"
            };

            mockClientRepo.Setup(c => c.Login(It.IsAny<Client>())).Returns(expectedClient);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act
            var result = clientService.Login(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedClient.ClientId, result.ClientId);
        }
        [Fact]
        public void LoginFailureTest()
        {
            var mockClientRepo = new Mock<IClientRepository>();
            
            var loginRequest = new LoginRequest
            {
                ClientName = "test",
                Password = "123"
            };
            Client? expected = null;
            mockClientRepo.Setup(c => c.Login(It.IsAny<Client>())).Returns(expected);

            var clientService = new ClientServices(mockClientRepo.Object);

            // Act 
            var result = clientService.Login(loginRequest);

            Assert.Null(result);
        }
    }
}
