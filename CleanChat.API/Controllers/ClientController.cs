using Azure;
using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;


namespace CleanChat.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _services;

        public ClientController( IClientServices clientServices )
        {
            _services = clientServices;
        }

        // GET api/<ClientController>/
        [HttpPost("Login")]
        public ActionResult<LoginReponse> Login(LoginRequest request )
        {
            try
                {

                    LoginReponse? response = _services.Login(request);
                    if ( response != null )
                    {
                        return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
                    }

                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
            }
            catch ( Exception ex )
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, ex.Message));
            }
        }

        [HttpGet("Topics/{ClientId}")]
        public ActionResult<List<TopicClientResponse>> Get(int ClientId)
        {
            try
            {
                var request = new TopicsClientRequest() { ClientId = ClientId };
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

        // POST api/<ClientController>
        [HttpPost("Create")]
        public ActionResult<CreateClientResponse> CreateClient(CreateClientRequest request )
        {
            try
            {
                if (request.ClientName.GetType() != typeof(string) || request.Password.GetType() != typeof(string))
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, request));
                }

                var response = _services.CreateClient(request);
                if (response == null)
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.AlreadyExist, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
            } catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }

        [HttpPost("Client/Topic")]
        public ActionResult<SubscribeTopicResponse> SubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _services.SubscribeTopic(request);
                if (result.Status == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                } else if (result.Status == false)
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

        [HttpDelete("Client/Topic")]
        public ActionResult<SubscribeTopicResponse> UnsubscribeTopic(SubscribeTopicRequest request)
        {
            try
            {
                var result = _services.UnsubscribeTopic(request);
                if (result.Status == null)
                {
                    return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
                } else if (result.Status == false)
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, null));
                }
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, result));
            } catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, e));
            }
        }
    }
}
