using AutoMapper;
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
        private readonly IMapper _mapper;

        public MessageController(IMessageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAllMessages()
        {
            try
            {
                var messages = _service.GetAllMessages();
                var response = _mapper.Map<List<MessageReceiveDto>>(messages);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
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
                var response = _mapper.Map<MessageReceiveDto>(message);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("topic/{topicId}")]
        public ActionResult<List<MessageReceiveDto>> GetMessagesByTopic(int topicId)
        {
            try
            {
                var messages = _service.GetMessagesByTopic(topicId);
                var response = _mapper.Map<List<MessageReceiveDto>>(messages);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost("{topicId}")]
        public ActionResult AddMessage(int topicId, [FromBody] MessageSendDto message)
        {
            try
            {
                message.TopicId = topicId;
                var messageEntity = _mapper.Map<MessageSendDto>(message);
                _service.AddMessage(messageEntity);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, "Message added successfully"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}
