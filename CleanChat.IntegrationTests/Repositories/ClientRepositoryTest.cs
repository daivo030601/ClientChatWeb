using AutoFixture;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.Entities;
using CleanChat.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.IntegrationTests.Repositories
{
    public class ClientRepositoryTest : IClassFixture<ShareDatabase>
    {
        private ShareDatabase _fixture
        {
            get;
        }

        public ClientRepositoryTest(ShareDatabase fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetTopicsById_ReturnAllTopicsSubcribed()
        {

            using (var context = _fixture.CreateContext())
            {
                var repository = new ClientRepository(context);
                var service = new ClientServices(repository);

                TopicsClientRequest request = new TopicsClientRequest { ClientId = 1 };
                var topics = service.GetTopicsFromClient(request);
                Assert.Equal(1, topics.Count);
            }
        }

        [Fact]
        public async void Login_ClientLogin()
        {

            using (var context = _fixture.CreateContext())
            {
                var repository = new ClientRepository(context);
                var service = new ClientServices(repository);

                LoginRequest request = new LoginRequest
                {
                    ClientName = "Chau",
                    Password = "123"
                };
                var response = service.Login(request);
                Assert.Equal(1, response.ClientId);
            }
        }

        [Fact]
        public async void Register_ClientRegister_SavesCorrectData()
        {
            using (var transaction = _fixture.Connection.BeginTransaction())
            {
                var clientId = 0;
                var request = new CreateClientRequest
                {
                    ClientName = "Quan",
                    Password = "123"
                };
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new ClientRepository(context);
                    var service = new ClientServices(repository);

                    var client = service.CreateClient(request);
                    clientId = client.ClientId;
                }
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new ClientRepository(context);
                    var service = new ClientServices(repository);

                    var clientTestId = service.Login(new LoginRequest 
                    { 
                        ClientName = "Quan", 
                        Password = "123"
                    });
                    Assert.NotNull(clientTestId);
                    Assert.Equal(clientId, clientTestId.ClientId);
                }
            }
        }

        [Fact]
        public void SubcribeTopic_avaiableTopic()
        {
            using (var transaction = _fixture.Connection.BeginTransaction())
            {
                bool? result = false;
                var request = new SubscribeTopicRequest
                {
                    TopicId = 2,
                    ClientId = 1
                };
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new ClientRepository(context);
                    var service = new ClientServices(repository);

                    var response = service.SubscribeTopic(request);
                    Assert.Equal(true,response.Status);
                }
                
            }
        }


        [Fact]
        public void SubcribeTopic_AllreadySubedTopic()
        {
            using (var transaction = _fixture.Connection.BeginTransaction())
            {
                bool? result = false;
                var request = new SubscribeTopicRequest
                {
                    TopicId = 1,
                    ClientId = 1
                };
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new ClientRepository(context);
                    var service = new ClientServices(repository);

                    var response = service.SubscribeTopic(request);
                    Assert.Equal(false, response.Status);
                }

            }
        }

    }
}
