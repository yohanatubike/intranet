using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class StaffsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
