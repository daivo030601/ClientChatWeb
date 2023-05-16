/*using CleanChat.Application.Interfaces;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.Infrastructure.Tests.ClientTests
{

    public class LoginTests
    {
        [Fact]
        public void Should_Login_Success_Test()
        {
            // Arrange
            var mockDbContext = new Mock<IChatDbContext>();
            var mockClientDbSet = new Mock<DbSet<Client>>();
            var client = new Client { Name = "testClient", Password = "testPass" };
            var expectedResult = new Client { Name = "testClient", Password = "testPass", *//* other properties *//* };

            mockDbContext.Setup(c => c.Clients).Returns(mockClientDbSet.Object);

            var clientRepo = new ClientRepository(mockDbContext.Object);

            // Act
            var result = clientRepo.Login(client);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
*/