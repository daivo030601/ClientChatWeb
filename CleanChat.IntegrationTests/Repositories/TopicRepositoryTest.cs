using AutoFixture;
using AutoMapper;
using CleanChat.Application.Interfaces;
using CleanChat.Application.Services;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.IntegrationTests.Repositories
{
    public class TopicRepositoryTest : IClassFixture<ShareDatabase>
    {
        private ShareDatabase _fixture
        {
            get;
        }

        public TopicRepositoryTest(ShareDatabase fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetTopics_ReturnAllTopics()
        {
            
            using (var context = _fixture.CreateContext())
            {
                var repository = new TopicRepository(context);
                var service = new TopicService(repository);
                var topics = service.GetAllTopics();
                Assert.Equal(8, topics.Count);
            }
        }

        [Fact]
        public void GetClientById_TopicDoesntExist_ResponseCode()
        {
            using (var context = _fixture.CreateContext())
            {
                var repository = new TopicRepository(context);
                var service = new TopicService(repository);
                var request = new ClientsTopicRequest{
                    TopicId = 56
                };
                Assert.Equal(null, service.GetClientsFromTopic(request));
            }
        }

        [Fact]
        public void GetClientById_AvaiableTopic_Response()
        {
            using (var context = _fixture.CreateContext())
            {
                var repository = new TopicRepository(context);
                var service = new TopicService(repository);
                var request = new ClientsTopicRequest
                {
                    TopicId = 1
                };
                Assert.Equal(4, service.GetClientsFromTopic(request).Count);
            }
        }


        [Fact]
        public void CreateTopic_SavesCorrectData()
        {
            using (var transaction = _fixture.Connection.BeginTransaction())
            {
                var topicId = 0;
                var request = new CreateTopicRequest
                {
                    TopicName = "Testing Topic"
                };
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new TopicRepository(context);
                    var service = new TopicService(repository);
                    var topic = service.CreateTopic(request);
                    topicId = topic.TopicId;
                }
                using (var context = _fixture.CreateContext(transaction))
                {
                    var repository = new TopicRepository(context);
                    var service = new TopicService(repository);
                    var topic = context.Topics.FirstOrDefault(t => t.TopicId == topicId);
                    Assert.NotNull(topic);
                    Assert.Equal(request.TopicName, topic.TopicName);
                }
            }
        }
    }
}
