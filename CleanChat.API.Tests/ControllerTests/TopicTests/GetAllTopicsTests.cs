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
    public class GetAllTopicsTests
    {
        [Fact]
        public void GetAllTopicsSuccess()
        {
            var topicService = new Mock<ITopicService>();
            var expected = new List<GetTopicResponse>()
            {
                new GetTopicResponse() 
                {
                    TopicId = 1,
                    TopicName = "Test",
                },
                new GetTopicResponse()
                {
                    TopicId= 2,
                    TopicName = "Test"
                },
                new GetTopicResponse()
                {
                    TopicId= 3,
                    TopicName= "Test",
                }
            };
            topicService.Setup(x => x.GetAllTopics()).Returns(expected);
            var topicController = new TopicController(topicService.Object);

            var result = topicController.Get();

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.IsType<List<GetTopicResponse>>(apiResponse.ResponseData);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void GetAllTopicsEmpty()
        {
            var topicService = new Mock<ITopicService>();
           List<GetTopicResponse>? expected = new List<GetTopicResponse>();
            
            topicService.Setup(x => x.GetAllTopics()).Returns(expected);
            var topicController = new TopicController(topicService.Object);

            var result = topicController.Get();

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal(expected, apiResponse.ResponseData);
            Assert.Equal("Success", apiResponse.Message);

        }




    }
}
