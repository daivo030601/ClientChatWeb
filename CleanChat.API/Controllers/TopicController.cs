using CleanChat.API.Response;
using CleanChat.Application.Interfaces;
using CleanChat.Domain;
using CleanChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;
        public TopicController(ITopicService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Topic>> Get()
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

        [HttpPost]
        public ActionResult<Topic> PostTopic (Topic topic)
        {
            var resulttopic = _service.CreateTopic(topic);
            return Ok(resulttopic);
        }

       
    }
}
