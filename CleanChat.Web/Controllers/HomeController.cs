using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CleanChat.Web.Controllers
{
    public class HomeController : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            var clientId = HttpContext.Session.GetString("ClientId");
            var clientName = HttpContext.Session.GetString("ClientName");

            // Pass userId to the Home view
            ViewBag.clientId = clientId;
            ViewBag.clientName = clientName;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7221/api/v1/topics"))
                {
                    var SubedResponse = await httpClient.GetAsync($"https://localhost:7221/api/v1/clients/{clientId}/topics");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    string apiSubedResponse = await SubedResponse.Content.ReadAsStringAsync();


                    var apiResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    var apiSubedResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiSubedResponse);
                    if (apiResponseObj.Code == "0" && apiSubedResponseObj.Code == "0") // assuming success response has code "200"
                    {
                        var topics = JsonConvert.DeserializeObject<List<Topic>>(apiResponseObj.ResponseData.ToString());
                        var subedTopics = JsonConvert.DeserializeObject<List<Topic>>(apiSubedResponseObj.ResponseData.ToString());
                        foreach (var item in subedTopics)
                        {
                            var topic = topics.FirstOrDefault(t => t.TopicId == item.TopicId);
                            topic.Subscribed = true;
                        }

                        return View(topics);
                    }
                    else
                    {
                        // handle error response
                        return View("Error");
                    }
                }
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
                using (var response = await httpClient.GetAsync($"https://localhost:7221/api/v1/topics/{topicId}/messages"))
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