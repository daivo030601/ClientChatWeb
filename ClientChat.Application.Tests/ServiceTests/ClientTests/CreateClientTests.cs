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

namespace ClientChat.Application.Tests.ServiceTests.ClientTests
{

    public class CreateClientTests
    {
        [Fact]
        public void CreateClientSuccess()
        {
            var mockClientRepo = new Mock<IClientRepository>();
            var entity = new Client()
            {
                ClientId = 1,
                Name = "test",
                Password = "123",
            };
            var request = new CreateClientRequest
            {
                ClientName = "test",
                Password = "123"
            };
            mockClientRepo.Setup(c => c.CreateClient(It.IsAny<Client>())).Returns(entity);

            var clientService = new ClientServices(mockClientRepo.Object);

            var result = clientService.CreateClient(request);

            Assert.NotNull(result);
            Assert.IsType<CreateClientResponse>(result);
            Assert.Equal(entity.ClientId, result.ClientId);

        }

        [Fact]
        public void CreateClientFailure()
        {
            var mockClientRepo = new Mock<IClientRepository>();

            var request = new CreateClientRequest
            {
                ClientName = "test",
                Password = "123"
            };
            mockClientRepo.Setup(c => c.CreateClient(It.IsAny<Client>())).Returns(new Client());

            var clientService = new ClientServices(mockClientRepo.Object);

            var result = clientService.CreateClient(request);

            Assert.Null(result);
        }

    }
}
