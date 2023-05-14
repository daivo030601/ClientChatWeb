using Microsoft.AspNetCore.Mvc;

namespace CleanChat.Web.Controllers
{
    public class WebSocketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
