using CleanChat.Application.Interfaces;
using CleanChat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanChat.API.Response;

namespace CleanChat.Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

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

        [HttpPost]
        public async Task<IActionResult> CreateTopic(Topic topic)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(topic), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7221/api/Topic", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);

                    if (apiResponseObj.Code == "0") // assuming success response has code "200"
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // handle error response
                        return View("Error");
                    }
                }
            }
        }

        public async Task<IActionResult> Subscribe(int topicId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7221/api/Topic/{topicId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseObj = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);

                    if (apiResponseObj.Code == "0") // assuming success response has code "200"
                    {
                        var clients = JsonConvert.DeserializeObject<List<ClientTopic>>(apiResponseObj.ResponseData.ToString());
                        return View(clients);
                    }
                    else
                    {
                        // handle error response
                        return View("Error");
                    }
                }
            }
        }

        public IActionResult PostMessage(int topicId, string messageText)
        {
            // TODO: implement message posting logic
            return RedirectToAction("Topic", new { topicId });
        }
    }
}
