using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanChat.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly HttpClient _httpClient;

        public MessageController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7221/api/message");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(json);
                return View(messages);
            }
            return View(new List<Message>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7221/api/message/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<Message>(json);
                return View(message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(message);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7221/api/message", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(message);
        }

        public async Task<IActionResult> Topic(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7221/api/message/topic/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(json);
                ViewBag.TopicId = id;
                return View(messages);
            }
            return RedirectToAction("Index");
        }
    }
}