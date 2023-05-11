using CleanChat.Application;
using CleanChat.Domain;
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
            var topicList = _service.GetAllTopics();
            return Ok(topicList);
        }

        [HttpPost]
        public ActionResult<Topic> PostTopic (Topic topic)
        {
            var resulttopic = _service.CreateTopic(topic);
            return Ok(resulttopic);
        }

       
    }
}
