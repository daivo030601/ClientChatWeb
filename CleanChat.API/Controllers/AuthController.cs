using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain.DTOs.Requests;
using CleanChat.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CleanChat.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClientServices _services;

        public AuthController(IClientServices services)
        {
            _services = services;
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        public ActionResult<LoginReponse> Login(LoginRequest request)
        {
            try
            {

                LoginReponse? response = _services.Login(request);
                if (response != null)
                {
                    return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, response));
                }

                return NotFound(ResponseHandler.GetApiResponse(ResponseType.NotFound, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, ex.Message));
            }
        }

        [HttpPost("signup")]
        public ActionResult<CreateClientResponse> CreateClient(CreateClientRequest request)
        {
            try
            {
                var response = _services.CreateClient(request);
                if (response == null)
                {
                    return BadRequest(ResponseHandler.GetApiResponse(ResponseType.AlreadyExist, null));
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
