using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LibrariesList()
        {
            return View();
        }
    }
}
