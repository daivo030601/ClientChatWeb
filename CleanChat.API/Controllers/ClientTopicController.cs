using Azure;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ClientTopicController : ControllerBase
    {
        private readonly IClientTopicService _service;
        public ClientTopicController(IClientTopicService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<SubscribeTopicResponse> SubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _service.SubscribeTopic(request);
                if (result == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, request));
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
