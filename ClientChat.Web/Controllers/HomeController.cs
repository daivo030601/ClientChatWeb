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
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7221/api/Topic"))
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

        [HttpPost]
        public IActionResult PostMessage(int topicId, string messageText)
        {
            // TODO: implement message posting logic
            return RedirectToAction("Topic", new { topicId });
        }
    }
}