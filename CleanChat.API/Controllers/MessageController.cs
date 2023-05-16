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
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<MessageReceiveDto>> GetAllMessages()
        {
            try
            {
                var messages = _service.GetAllMessages();
                if (messages == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, "Messages not found"));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetMessageById(int id)
        {
            try
            {
                var message = _service.GetMessageById(id);
                if (message == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, "Message not found"));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, message));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("topicId")]
        public ActionResult<List<MessageReceiveDto>> GetMessagesByTopic(int topicId)
        {
            try
            {
                var messages = _service.GetMessagesByTopic(topicId);
                if (messages == null )
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, "Messages not found"));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost]
        public ActionResult AddMessage(MessageSendDto message)
        {
            try
            {
                var response = _service.AddMessage(message);
                response.MessageResponse = response.MessageId != 0 ? "Message added successfully" : "Unable to add message";
                if (response == null )
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, response));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}
