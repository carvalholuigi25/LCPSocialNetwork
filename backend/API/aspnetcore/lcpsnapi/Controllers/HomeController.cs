using Microsoft.AspNetCore.Mvc;

namespace lcpsnapi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
