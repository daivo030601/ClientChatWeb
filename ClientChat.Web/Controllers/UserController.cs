using CleanChat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(User user)
        {
            // TODO: Authenticate user and redirect to main page if successful
            if (user.Username == "admin" && user.Password == "password")
            {
                return RedirectToAction("Index", "Home");
            } 
            else
            {
                ModelState.AddModelError("", "Wrong username or password.");
                return View(user);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            // TODO: Validate user input, create user account, and redirect to main page if successful
            return View();
        }

        public IActionResult Logout()
        {
            // TODO: Clear session and redirect to login page
            return RedirectToAction("Login");
        }
    }
}
