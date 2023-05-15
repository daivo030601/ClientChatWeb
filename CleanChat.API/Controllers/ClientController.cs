using Azure;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;


namespace CleanChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _services;

        public ClientController( IClientServices clientServices )
        {
            _services = clientServices;
        }

        // GET api/<ClientController>/
        [HttpPost]
        public ActionResult<LoginReponse> Login( LoginRequest request )
        {
            try
                {

                    LoginReponse? response = _services.Login(request);
                    if ( response != null )
                    {
                        return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
                    }

                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.NotFound, request));
            }
            catch ( Exception ex )
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public ActionResult<List<TopicClientResponse>> Get(int id)
        {
            try
            {
                var request = new TopicsClientRequest() { ClientId = id };
                var result = _services.GetTopicsFromClient(request);
                if (result == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, id));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        // POST api/<ClientController>
        [HttpPost("Create")]
        public ActionResult<CreateClientResponse> CreateClient(CreateClientRequest request )
        {
            if ( request.ClientName.GetType() != typeof(string) || request.Password.GetType() != typeof(string) ) 
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, request));
            }

            var response = _services.CreateClient(request);
            return Ok(ResponseHandler.GetApiResponse(ResponseType.Success,response));
        }

        [HttpPost("topic")]
        public ActionResult<SubscribeTopicResponse> SubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _services.SubscribeTopic(request);
                if (result.Status == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, request));
                } else if (result.Status == false)
                {
                    return Ok(ResponseHandler.GetApiResponse(ResponseType.AlreadyExist, request));
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
