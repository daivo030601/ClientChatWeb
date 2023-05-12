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
        [HttpGet]
        public ActionResult<LoginReponse> Login( [FromQuery]LoginRequest request )
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

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<CreateClientResponse> CreateClient(CreateClientRequest request )
        {
            if ( request.ClientName.GetType() != typeof(string) || request.Password.GetType() != typeof(string) ) 
            {
                return BadRequest(ResponseHandler.GetApiResponse(ResponseType.Failure, request));
            }

            var response = _services.CreateClient(request);
            return Ok(ResponseHandler.GetApiResponse(ResponseType.Success,response));
        }

    }
}
