using Microsoft.AspNetCore.Mvc;

namespace EFCoreProductApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
