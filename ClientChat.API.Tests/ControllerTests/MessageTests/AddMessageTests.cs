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
using System.Threading.Tasks;

namespace CleanChat.API.Tests.ControllerTests.MessageTests
{
    public class AddMessageTests
    {
        [Fact]
        public void AddMessageSuccess()
        {
            var messageService = new Mock<IMessageService>();
            var request = new MessageSendDto()
            {
                ClientId = 1,
                TopicId = 1,
                Content = "test"
            };
            var response = new AddedMessageResponse()
            {
                MessageId = 1,
                MessageResponse = "Message added successfully"
            };
            messageService.Setup(m => m.AddMessage(It.IsAny<MessageSendDto>())).Returns(response);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.AddMessage(request);

            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result;
            var apiResponse = (ApiResponse)okResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("0", apiResponse.Code);

            Assert.Equal(response, apiResponse.ResponseData);
        }

        [Fact]
        public void AddMessageFailed()
        {
            var messageService = new Mock<IMessageService>();
            var request = new MessageSendDto()
            {
                ClientId = 1,
                TopicId = 1,
                Content = "test"
            };
            var response = new AddedMessageResponse()
            {
                MessageId = 0,

            };
            messageService.Setup(m => m.AddMessage(It.IsAny<MessageSendDto>())).Returns(response);
            var messageController = new MessageController(messageService.Object);

            var result = messageController.AddMessage(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            var apiResponse = (ApiResponse)badRequestResult.Value;

            Assert.NotNull(apiResponse);
            Assert.Equal("4", apiResponse.Code);
            response.MessageResponse = "Unable to add message";
            Assert.Equal(response, apiResponse.ResponseData);
        }
    }
}
