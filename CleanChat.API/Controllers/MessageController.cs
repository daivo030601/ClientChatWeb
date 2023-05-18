using AutoMapper;
using Azure;
using CleanChat.API.Response;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CleanChat.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("messages")]
        public ActionResult<List<MessageReceiveDto>> GetAllMessages()
        {
            try
            {
                var messages = _service.GetAllMessages();
                if (messages?.Count == 0)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("messages/{messageId}")]
        public ActionResult<MessageReceiveDto> GetMessageById(int messageId)
        {
            try
            {
                var message = _service.GetMessageById(messageId);
                if (message == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, message));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("topics/{topicId}/messages")]
        public ActionResult<List<MessageReceiveDto>> GetMessagesByTopic(int topicId)
        {
            try
            {
                var messages = _service.GetMessagesByTopic(topicId);
                if (messages == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost("messages")]
        public ActionResult<AddedMessageResponse> AddMessage(MessageSendDto message)
        {
            try
            {
                var response = _service.AddMessage(message);
                response.MessageResponse = response.MessageId != 0 ? "Message added successfully" : "Unable to add message";
                if (response.MessageId != 0)
                {
                    return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
                }
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, null));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}