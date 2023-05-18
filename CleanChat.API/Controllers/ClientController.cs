using Azure;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;


namespace CleanChat.API.Controllers
{
    [Route("api/v1/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _services;

        public ClientController(IClientServices clientServices)
        {
            _services = clientServices;
        }

        // POST api/<ClientController>/
        [HttpGet("{clientId}/topics")]
        public ActionResult<List<TopicClientResponse>> Get(int clientId)
        {
            try
            {
                var request = new TopicsClientRequest() { ClientId = clientId };
                var result = _services.GetTopicsFromClient(request);
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

        [HttpPost("topics")]
        public ActionResult<SubscribeTopicResponse> SubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _services.SubscribeTopic(request);
                if (result.Status == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                else if (result.Status == false)
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.AlreadyExist, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpDelete("topics")]
        public ActionResult<SubscribeTopicResponse> UnsubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _services.UnsubscribeTopic(request);
                if (result.Status == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                }
                else if (result.Status == false)
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, null));
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