﻿using Azure;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using CleanChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanChat.API.Controllers
{
    [Route("api/v1/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;
        public TopicController(ITopicService service)
        {
            _service = service;
        }
        [HttpGet()]
        public ActionResult<List<GetTopicResponse>> Get()
        {
            try
            {
                var topics = _service.GetAllTopics();
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, topics));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost()]
        public ActionResult<CreateTopicResponse> CreateTopic (CreateTopicRequest topic)
        {
            try
            {
                var resulttopic = _service.CreateTopic(topic);
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, resulttopic));
            } catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpGet("{topicId}/clients")]
        public ActionResult<List<ClientTopicResponse>> Get(int topicId)
        {
            try
            {
                var request = new ClientsTopicRequest() { TopicId = topicId };
                var result = _service.GetClientsFromTopic(request);
                if (result == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}
