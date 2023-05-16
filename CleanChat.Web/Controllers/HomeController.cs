using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Index()
        {
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync("api/Topic");
            //    var temp = await response.Content.ReadFromJsonAsync<ApiResponse>();
            //    var data = (temp?.ResponseData)?.ToString();
            //    var topics = JsonSerializer.Deserialize<List<Topic>>(data!) ?? new List<Topic>();
            //    foreach (var topic in topics)
            //    {
            //        Console.WriteLine($"{topic.topicId} : {topic.topicName}");
            //    }
            //    return View("Index", topics);

            //} catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception: {ex.Message}");
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Message", message);
                    response.EnsureSuccessStatusCode();
                    return View("Index", message);

                    //var temp = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    //var data = (temp?.ResponseData)?.ToString();
                    //var topics = JsonSerializer.Deserialize<List<Topic>>(data!) ?? new List<Topic>();
                    //foreach (var topic in topics)
                    //{
                    //    Console.WriteLine($"{topic.topicId} : {topic.topicName}");
                    //}
                    //return View("Index", topics);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}