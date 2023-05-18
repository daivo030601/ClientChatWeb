using CleanChat.API.Controllers;
using CleanChat.API.Response;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CleanChat.API.Tests.ControllerTests.MessageTests
{
    public class GetAllMessageTests
    {
        [Fact]
        public void GetAllMessagesSuccess()
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
            messageService.Setup(m => m.GetAllMessages()).Returns(expected);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.GetAllMessages();

            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);
            Assert.Equal(expected, apiResponse.ResponseData);
        }

        [Fact]
        public void GetAllMessagesNotFound()
        {
            var messageService = new Mock<IMessageService>();
            DateTime date = DateTime.Now;
            var expected = new List<MessageReceiveDto>();
            
            messageService.Setup(m => m.GetAllMessages()).Returns(expected);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.GetAllMessages();

            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = (NotFoundObjectResult)result.Result;
            var apiResponse = (ApiResponse)notFoundResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("2", apiResponse.Code);
            Assert.Equal("Not Found",apiResponse.Message);
            Assert.Null(apiResponse.ResponseData);
        }
        
    }

}
