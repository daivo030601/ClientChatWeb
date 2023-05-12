using AutoMapper;
using CleanChat.API.Response;
using CleanChat.Application.Services.Interface;
using CleanChat.Domain.DTOs;
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
        private readonly IMapper _mapper;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAllMessages()
        {
            try
            {
                var messages = _service.GetAllMessages();
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

        [HttpGet("topic/{topicId}")]
        public ActionResult<List<Message>> GetMessagesByTopic(int topicId)
        {
            try
            {
                var messages = _service.GetMessagesByTopic(topicId);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost("{topicId}")]
        public ActionResult AddMessage(int topicId, Message message)
        {
            try
            {
                message.TopicId = topicId; // set the topicId of the message
                var messageDto = _mapper.Map<MessageSendDto>(message); // map Message entity to MessageDto
                _service.AddMessage(message);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, "Message added successfully"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}