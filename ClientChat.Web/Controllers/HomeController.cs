using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CleanChat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7221/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/Topics");
                var api = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (api?.Code != "0")
                {
                    return View("Error");
                } 
                var data = (api?.ResponseData)?.ToString();
                var topicRevs = JsonSerializer.Deserialize<List<TopicRev>>(data!) ?? new List<TopicRev>();
                foreach (var topicRev in topicRevs)
                {
                    Console.WriteLine($"{topicRev.topicId} : {topicRev.topicName}");
                }
                List<Topic> topics = topicRevs.Select(c => new Topic
                {
                    TopicId = c.topicId,
                    TopicName = c.topicName,
                }).ToList();
                return View("Index", topics);
            } catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return View();
        }

        [Route("Home/Topic/{id:int}")]
        public IActionResult Topic(int id)
        {
            return View(id);
        }

        public IActionResult CreateTopic()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult CreateTopic(Topic topic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _topicRepository.AddTopic(topic);
        //        return RedirectToAction("Index");
        //    }

        //    return View(topic);
        //}

        public IActionResult Subscribe(int topicId)
        {
            // TODO: implement subscription logic
            return RedirectToAction("Index");
        }

        //public IActionResult Topic(int topicId)
        //{
        //    var topic = _topicRepository.GetTopic(topicId);
        //    return View(topic);
        //}

        [HttpPost]
        public IActionResult PostMessage(int topicId, string messageText)
        {
            // TODO: implement message posting logic
            return RedirectToAction("Topic", new { topicId });
        }
    }
}