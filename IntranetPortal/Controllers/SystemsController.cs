using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class SystemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Core()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }
    }
}