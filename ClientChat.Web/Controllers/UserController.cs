using CleanChat.Domain.DTOs.Requests;
using CleanChat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CleanChat.Web.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        /*  private readonly ClientController _clientController;*/

        public UserController( HttpClient httpClient )
        {
            _httpClient = httpClient;
        }
        // GET: UserController
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
        {
            try
            {
                var request = new LoginRequest
                {
                    ClientName = user.Username,
                    Password = user.Password
                };



                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");



                var response = await _httpClient.PostAsync("https://localhost:7221/api/Client/Login", content);



                response.EnsureSuccessStatusCode();



                var responseContent = await response.Content.ReadAsStringAsync();



                var loginResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);



                if ( loginResponse != null && loginResponse.Code == "0" )
                {
                    // TODO: Implement user authentication and redirect to main page if successful
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong username or password.");
                    return View(user);
                }
            }
            catch ( Exception ex )
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while logging in: {ex.Message}");
                return View(user);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register( User user )
        {
            // TODO: Validate user input, create user account, and redirect to main page if successful
            try
            {
                if ( user.Password != user.ConfirmPassword )
                {
                    ModelState.AddModelError(string.Empty, "Your Password is not match, please try again");
                    return View(user);
                }
                var createClientObj = new CreateClientRequest
                {
                    ClientName = user.Username,
                    Password = user.Password
                };
                var request = new StringContent(JsonConvert.SerializeObject(createClientObj), Encoding.UTF8, "application/json");



                var response = await _httpClient.PostAsync("https://localhost:7221/api/Client/Create", request);
                



                var responseContent = await response.Content.ReadAsStringAsync();



                var createClientResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                if ( createClientResponse != null && createClientResponse.Code == "0" )
                {
                    return RedirectToAction("Index", "Home");
                } else if ( createClientResponse != null && createClientResponse.Code == "3" )
                {
                    ModelState.AddModelError(string.Empty, createClientResponse.ResponseData.ToString());
                    return View(user);
                }
            }
            catch ( Exception ex )
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while logging in: {ex.Message}");
                return View(user);
            }
            return View();
        }

        public IActionResult Logout()
        {
            // TODO: Clear session and redirect to login page
            return RedirectToAction("Login");
        }
    }
}
