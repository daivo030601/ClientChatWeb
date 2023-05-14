using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanChat.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CleanChat.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;

        public UserController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            // Get list of users from API
            var users = await GetUsersAsync();

            // Pass users to view
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Get user from API
            var user = await GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Pass user to view
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Create user via API
                var response = await CreateAsync(user);

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to list of users if successful
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Show error message if unsuccessful
                    ModelState.AddModelError("", "Error creating user. Please try again.");
                }
            }

            // Return to create view with validation errors
            return View(user);
        }

        private async Task<List<User>> GetUsersAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"{_config["ApiUrl"]}/users";
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var users = JsonSerializer.Deserialize<List<User>>(content);
                        return users;
                    }
                    else
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }
                }
            }
        }

        private async Task<User> GetUserAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"{_config["ApiUrl"]}/users/{id}";
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var user = JsonSerializer.Deserialize<User>(content);
                        return user;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                    else
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }
                }
            }
        }

        private async Task<HttpResponseMessage> CreateAsync(User user)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"{_config["ApiUrl"]}/users";
                var json = JsonSerializer.Serialize(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync(url, data);
            }
        }
    }
}
