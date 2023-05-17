using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var clientId = HttpContext.Session.GetString("ClientId");
            var clientName = HttpContext.Session.GetString("ClientName");

            // Pass userId to the Home view
            ViewBag.clientId = clientId;
            ViewBag.clientName = clientName;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/Topics");
                var api = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (api?.Code != "0")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);

                    if (apiResponseObj.Code == "0") // assuming success response has code "200"
                    {
                        var topics = JsonConvert.DeserializeObject<List<Topic>>(apiResponseObj.ResponseData.ToString());
                        return View(topics);
                    }
                    else
                    {
                        // handle error response
                        return View("Error");
                    }
                }
                return View();
            }
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

        public async Task<IActionResult> Topic(int topicId)
        {
            // Use the 'topicId' parameter as needed, e.g., retrieve data from a database
            // Pass any necessary data to the view if required
            var clientId = HttpContext.Session.GetString("ClientId");
            var clientName = HttpContext.Session.GetString("ClientName");

            // Pass userId to the Home view
            ViewBag.clientId = clientId;
            ViewBag.clientName = clientName;
            ViewData["topicId"] = topicId;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7221/api/Messages/{topicId}"))
                {
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();


                    var apiResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    if (apiResponseObj.Code == "0") // assuming success response has code "200"
                    {
                        var messages = JsonConvert.DeserializeObject<List<Message>>(apiResponseObj.ResponseData.ToString());
                        return View(messages);
                    }
                    else
                    {
                        // handle error response
                        return View("Error");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult PostMessage(int topicId, string messageText)
        {
            // TODO: implement message posting logic
            return RedirectToAction("Topic", new { topicId });
        }
    }
}