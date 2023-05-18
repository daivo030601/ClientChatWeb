using CleanChat.API.Controllers;
using CleanChat.API.Response;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.API.Tests.ControllerTests.MessageTests
{
    public class GetMessageByTopicTests
    {
        [Fact]
        public void GetMessagesByTopicSuccess()
        {
            var messageService = new Mock<IMessageService>();
            DateTime date = DateTime.Now;
            var expected = new List<MessageReceiveDto>
            {
                new MessageReceiveDto
                {
                        MessageId = 1,
                        SentDate = date,
                        Content = "test message",
                        ClientName = "test",
                        ClientId = 1,
                        TopicId = 1,
                },
                new MessageReceiveDto
                {
                        MessageId = 2,
                        SentDate = date,
                        Content = "test message",
                        ClientName = "test",
                        ClientId = 1,
                        TopicId = 1,
                },
                new MessageReceiveDto
                {
                        MessageId = 3,
                        SentDate = date,
                        Content = "test message",
                        ClientName = "test",
                        ClientId = 2,
                        TopicId = 1,
                }
            };
            messageService.Setup(m => m.GetMessagesByTopic(It.IsAny<int>())).Returns(expected);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.GetMessagesByTopic(1);

            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void GetMessagesByTopicNotFound()
        {
            var messageService = new Mock<IMessageService>();
            DateTime date = DateTime.Now;
            List<MessageReceiveDto>? expected = null;

            messageService.Setup(m => m.GetMessagesByTopic(It.IsAny<int>())).Returns(expected);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.GetMessagesByTopic(10);

            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = (NotFoundObjectResult)result.Result;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("2", apiResponse.Code);
            Assert.Equal("Not Found", apiResponse.Message);
        }

        /*[Fact]
        public void GetMessagesByTopicFailed()
        {
            // Arrange
            var messageService = new Mock<IMessageService>();
            List<MessageReceiveDto>? expected;
            expected = null;
            messageService.Setup(m => m.GetMessagesByTopic(It.IsAny<int>())).Returns(expected);
            var messageController = new MessageController(messageService.Object);

            // Act
            var result = messageController.GetMessagesByTopic(10);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(badRequestResult.Value);

            Assert.NotNull(apiResponse);
            Assert.Equal("4", apiResponse.Code);
            Assert.Equal("Failed", apiResponse.ResponseData);*/
        }
    }
