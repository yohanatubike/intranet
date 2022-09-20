using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Units()
        {
            return View();
        }

        public IActionResult Sections()
        {
            return View();
        }
    }
}