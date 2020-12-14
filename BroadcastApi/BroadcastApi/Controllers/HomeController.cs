using Microsoft.AspNetCore.Mvc;

namespace BroadcastApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
