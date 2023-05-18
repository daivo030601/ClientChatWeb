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
    [Route("api")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("Messages")]
        public ActionResult<List<MessageReceiveDto>> GetAllMessages()
        {
            try
            {
                var messages = _service.GetAllMessages();
                if (messages.Count == 0)
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

        [HttpGet("Message/{MessageId}")]
        public ActionResult<MessageReceiveDto> GetMessageById(int MessageId)
        {
            try
            {
                var message = _service.GetMessageById(MessageId);
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

        [HttpGet("Messages/{TopicId}")]
        public ActionResult<List<MessageReceiveDto>> GetMessagesByTopic(int TopicId)
        {
            try
            {
                var messages = _service.GetMessagesByTopic(TopicId);
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

        [HttpPost("Message")]
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